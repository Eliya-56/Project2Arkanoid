using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;
using System.Threading;

namespace GameInConsole
{
    //Игровой процесс, объединяет все игровые объекты
    [Serializable]
    class PlayGame : IGameObj, IPlayGame
    {
        private const int POINTQUANT = 10;
        private IBoard board;
        private List<IBrick> bricks = new List<IBrick>();
        private IBall ball;
        private int levelCounter = 0;
        public int Points { get; set; }
        private List<LevelConstruct> levels = new List<LevelConstruct>();


        public bool IsEndGame { get; set; }
        public bool NextLevel { get; private set; }
        public bool NextLevelRender { get; private set; }
        public bool IsAllbrickDead { get; set; }

        public PlayGame(ConsoleGraphics graphic, List<LevelConstruct> levels)
        {
            NextLevel = false;
            NextLevelRender = true;
            this.levels = levels;
            Points = 0;
            IsEndGame = false;
            board = new Board(graphic);
            ball = new Ball(graphic, board, bricks);
            NewLevel(levels[levelCounter], graphic);
            ball.killBrick += AddPoints;
        }

        public void Render(ConsoleGraphics graphic)
        {
            ShowPoints(graphic);
            foreach (var brick in bricks)
            {
                brick.Render(graphic);
            }
            if (board != null)
                board.Render(graphic);
            if (ball != null)
                ball.Render(graphic);
            if(NextLevelRender)
            {
                NextLevelRender = false;
                graphic.FillRectangle(0xFFFF4500, 0, 0, graphic.ClientWidth, graphic.ClientHeight);
                graphic.DrawString("Level " + (levelCounter + 1), "Arial", 0xFFFFFFFF, graphic.ClientWidth / 2 - 100, graphic.ClientHeight / 3 + 50, 30);
                graphic.FlipPages();
                Thread.Sleep(2000);
                NewLevel(levels[levelCounter], graphic);
            }
        }

        public void Update()
        {
            if (ball.FallDown)
            {
                ball.FallDown = false;
                IsEndGame = true;
            }
            if (board != null)
                board.Update();
            if (ball != null)
                ball.Update();

            IsAllbrickDead = true;
            foreach (var brick in bricks)
            {
                brick.Update();
                if (brick.IsAlive)
                    IsAllbrickDead = false;
            }
            if (IsAllbrickDead)
            {
                IsAllbrickDead = false;
                NextLevel = true;
            }
            if (NextLevel)
            {
                levelCounter++;
                if (levels.Count <= levelCounter)
                {
                    IsEndGame = true;
                }
                else
                {
                    NextLevelRender = true;
                }
                NextLevel = false;
            }
        }

        private void AddPoints()
        {
            Points += POINTQUANT;
        }

        private void ShowPoints(ConsoleGraphics graphic)
        {
            graphic.DrawString("Points: " + Points, "Arial", 0xF33457B0, 0, 0, 18);
        }

        public void NewLevel(LevelConstruct BuildLevel, ConsoleGraphics graphic)
        {
            board.StartPositon();
            ball.StartPositon();
            BuildLevel(bricks, graphic);
        }
    }
}
