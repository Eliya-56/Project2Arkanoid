using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace GameInConsole
{
    [Serializable]
    class Board : IGameObj, IBoard
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; private set; }
        public int H { get; private set; }
        private int height, width, startX, startY;

        public Board(ConsoleGraphics graphic)
        {
            X = startX = graphic.ClientWidth / 2 - 10;
            Y = startY = graphic.ClientHeight - 60;
            H = 15;
            W = 150;
            height = graphic.ClientHeight;
            width = graphic.ClientWidth;
        }

        public void Render(ConsoleGraphics graphic)
        {
            graphic.FillRectangle(0xFFFF0000, X , Y , W, H);
        }

        public void Update()
        {
            if(Input.IsKeyDown(Keys.LEFT))
            {
                if (X >= 0)
                    X -= 13;
            }
            else if(Input.IsKeyDown(Keys.RIGHT))
            {
                if (X <= width - W)
                    X += 13;
            }
        }

        public void StartPositon()
        {
            X = startX;
            Y = startY;
        }
    }
}
