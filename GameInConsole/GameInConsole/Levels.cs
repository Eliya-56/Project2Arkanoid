using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInConsole
{
    //Набор уровней. Все уровни отличаются только набором кирпичей, поэтому здесь набор алгоритмов для их размещения
    static class Levels
    {
        public static void SeventhLevel(List<IBrick> bricks, ConsoleGraphics graphic)
        {
            foreach (var brick in bricks)
                brick.IsAlive = false;
            bricks.Clear();
            for (var i = 0; i < 13; i++)
                for (int j = 0; j < 7; j++)
                {
                    if (i == 7 && j == 5)
                        bricks.Add(new Brick(graphic, 0xFF68452F, 45 + i * 98, 40 + j * 30, 98, 30));
                }
        }

        public static void SixthLevel(List<IBrick> bricks, ConsoleGraphics graphic)
        {
            foreach (var brick in bricks)
                brick.IsAlive = false;
            bricks.Clear();
            for (var i = 0; i < 13; i++)
                for (int j = 0; j < 7; j++)
                {
                    if (i % 2 == 1 || j % 2 != 1)
                        bricks.Add(new Brick(graphic, 0xFF68452F, 45 + i * 98, 40 + j * 30, 98, 30));
                }
        }

        public static void FifthLevel(List<IBrick> bricks, ConsoleGraphics graphic)
        {
            foreach (var brick in bricks)
                brick.IsAlive = false;
            bricks.Clear();
            for (var i = 0; i < 13; i++)
                for (int j = 0; j < 7; j++)
                {
                    if (i % 2 != 1 || j % 2 == 1)
                        bricks.Add(new Brick(graphic, 0xFF68452F, 45 + i * 98, 40 + j * 30, 98, 30));
                }
        }

        public static void FourthLevel(List<IBrick> bricks, ConsoleGraphics graphic)
        {
            foreach (var brick in bricks)
                brick.IsAlive = false;
            bricks.Clear();
            for (var i = 0; i < 13; i++)
                for (int j = 0; j < 7; j++)
                {
                    if(i % 2 == 1 && j % 2 != 1)
                    bricks.Add(new Brick(graphic, 0xFF68452F, 45 + i * 98, 40 + j * 30, 98, 30));
                }
        }

        public static void ThirdLevel(List<IBrick> bricks, ConsoleGraphics graphic)
        {
            foreach(var brick in bricks)
                brick.IsAlive = false;
            bricks.Clear();
            for (var i = 0; i < 13; i++)
                for (int j = 0; j < 7; j++)
                {
                    bricks.Add(new Brick(graphic, 0xFF68452F, 45 + i * 98, 40 + j * 30, 98, 30));
                }
        }

        public static void SecondLevel(List<IBrick> bricks, ConsoleGraphics graphic)
        {
            foreach (var brick in bricks)
                brick.IsAlive = false;
            bricks.Clear();
            for (var i = 0; i < 13; i++)
                for (int j = 0; j < 7; j++)
                {
                    if(j % 2 == 0)
                        bricks.Add(new Brick(graphic, 0xFF68452F, 45 + i * 98, 40 + j * 30, 98, 30));
                }
        }

        public static void FirstLevel(List<IBrick> bricks, ConsoleGraphics graphic)
        {
            foreach (var brick in bricks)
                brick.IsAlive = false;
            bricks.Clear();
            for (var i = 0; i < 13; i++)
                for (int j = 0; j < 7; j++)
                {
                    if(j == 0 || j == 6)
                        bricks.Add(new Brick(graphic, 0xFF68452F, 45 + i * 98, 40 + j * 30, 98, 30));
                    else if (i % 2 == 0)
                        bricks.Add(new Brick(graphic, 0xFF68452F, 45 + i * 98, 40 + j * 30, 98, 30));
                }
        }
    }
}
