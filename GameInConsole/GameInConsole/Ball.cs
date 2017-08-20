using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace GameInConsole
{
    [Serializable]
    class Ball : IGameObj, IBall
    {
        const int STARTSPEED = 8, CENTERSPEED = 4, RIGHT = -1, LEFT = 1, UP = -1, DOWN = 1, CENTERLEFT = 2, HARDLEFT = 1, CENTERRIGHT = 3, HARDRIGHT = 4,
            VERTICAL = 1, HORIZONTAL = 2;
        private int yc, xc, r, xdir, ydir, xspeed, yspeed, widght, height;
        private IBoard brd;
        private List<IBrick> bricks;
        private int startX, startY;
        public event Action killBrick;

        public bool FallDown { get; set; }

        public Ball(ConsoleGraphics graphics, IBoard brd ,List<IBrick> bricks)
        {
            this.brd = brd;
            r = 10;
            xc = startX = brd.X + brd.W / 2;
            yc = startY = brd.Y - r;
            xdir = LEFT;
            ydir = UP;
            xspeed = STARTSPEED;
            yspeed = STARTSPEED;
            widght = graphics.ClientWidth;
            height = graphics.ClientHeight;
            this.bricks = bricks;
            FallDown = false;
        }

        public void Render(ConsoleGraphics graphics)
        {
            for (double i = 0; i < 6.28; i += 0.01)
            {
                int x = (int)(xc + r * Math.Sin(i));
                int y = (int)(yc + r * Math.Cos(i));
                graphics.DrawLine(0xFF0000FF, xc, yc, x, y, 10);
            }
        }

        public void Update()
        {
            Move();
            int dir;
            if (TouchUp())
                YInverse();
            if (TouchWall())
                XInverse();
            if(TouchBoard(out dir))
            {
                YInverse();
                if (dir == HARDLEFT)
                {
                    xdir = RIGHT;
                    xspeed = STARTSPEED;
                    yspeed = STARTSPEED;
                }
                else if(dir == CENTERLEFT)
                {
                    xdir = RIGHT;
                    xspeed = CENTERSPEED;
                    yspeed = STARTSPEED;
                }
                else if (dir == CENTERRIGHT)
                {
                    xdir = LEFT;
                    xspeed = CENTERSPEED;
                    yspeed = STARTSPEED;
                }
                else if (dir == HARDRIGHT)
                {
                    xdir = LEFT;
                    xspeed = STARTSPEED;
                    yspeed = STARTSPEED;
                }

            }
            if (TouchBrick(out dir))
            {
                if (dir == VERTICAL)
                    XInverse();
                if(dir == HORIZONTAL)
                    YInverse();
            }

            if (yc >= height)
                FallDown = true;
        }

        private void Move()
        {
            yc += ydir * yspeed;
            xc += xdir * xspeed;
        }

        private void XInverse() => xdir *= -1;

        private void YInverse() => ydir *= -1;

        private bool TouchBoard(out int dir)
        {
            if ((yc <= brd.Y + 7 && yc >= brd.Y - 8) && (xc >= brd.X && xc <= brd.X + brd.W))
            {
                if (xc < brd.X + brd.W / 4)
                    dir = HARDLEFT;
                else if (xc < brd.X + brd.W / 2)
                    dir = CENTERLEFT;
                else if (xc < brd.X + 3 * (brd.W / 4))
                    dir = CENTERRIGHT;
                else
                    dir = HARDRIGHT;
                return true;
            }
            dir = 0;
            return false;
        }

        private bool TouchWall()
        {
            if (xc >= widght || xc <= 0)
                return true;
            return false;
        }

        private bool TouchUp()
        {
            if (yc <= 0)
                return true;
            return false;
        }

        private bool TouchBrick(out int dir)
        {
            foreach (var brick in bricks)
            {
                if (brick.IsAlive)
                {
                    if(yc >= brick.Y && yc <= brick.Y + brick.Thick)
                    {
                        if (xc >= brick.X && xc <= brick.X + brick.Length)
                        {
                            if (xc < brick.X + r || xc > brick.X + (brick.Length - r))
                                dir = VERTICAL;
                            else
                                dir = HORIZONTAL;
                            brick.IsAlive = false;
                            if (killBrick != null)
                                killBrick();
                            return true;
                        }
                    }
                }
                else
                    continue;
            }
            dir = 0;
            return false;
        }

        public void StartPositon()
        {
            xc = startX;
            yc = startY;
        }
    }
}
