using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInConsole
{
    class Program
    {
        static public uint BackGround = 0xFFFFD700;

        static void Main(string[] args)
        {
            Console.WindowWidth = 170;
            Console.WindowHeight = 50;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.BackgroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
            Console.Clear();
            ConsoleGraphics graphic = new ConsoleGraphics();
            GameEngine eng = new GameEngine(graphic);

            eng.Start();
        }
    }
}
