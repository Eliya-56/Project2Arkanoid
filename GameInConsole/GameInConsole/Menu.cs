using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;
using System.IO;
using System.Threading;

namespace GameInConsole
{
    class Menu : IGameObj, IMenu
    {
        private const uint NOTCHOSENCOLOR = 0xFFFFFFFF, CHOSENCOLOR = 0xFF00FFFF;
        private int x, y, ydelta;
        private uint startColor, loadColor, quitColor, hscolor;

        public bool MainMenu { get; private set; }
        public bool HighScores { get; private set; }
        public bool Help { get; private set; }

        public bool StartGame { get; set; }
        public bool LoadGame { get; set; }
        public bool Quit { get; set; }

        public Menu(ConsoleGraphics graphic)
        {
            x = graphic.ClientWidth / 2 - 100;
            y = graphic.ClientHeight / 3;
            ydelta = 50;
            startColor = NOTCHOSENCOLOR;
            loadColor = NOTCHOSENCOLOR;
            quitColor = NOTCHOSENCOLOR;
            hscolor = NOTCHOSENCOLOR;
            MainMenu = true;
            StartGame = false;
            LoadGame = false;
            Quit = false;
        }
        public void Render(ConsoleGraphics graphic)
        {
            if(MainMenu)
                RenderMainMenu(graphic);
            if (HighScores)
                RenderHighScores(graphic);
            if (Help)
                RenderHelp(graphic);
        }

        private void RenderHelp(ConsoleGraphics graphic)
        {
            graphic.DrawString("While playing ", "Arial", startColor, x - 40, y, 20);
            graphic.DrawString("press P to pause", "Arial", startColor, x - 40, y + 50, 20);
            graphic.DrawString("press S to save game", "Arial", startColor, x - 40, y + 80, 20);
            graphic.DrawString("Enter to continue", "Arial", startColor, x - 40, y + 130, 20);

        }

        private void RenderHighScores(ConsoleGraphics graphic)
        {
            graphic.DrawString("BEST SCORE", "Arial", startColor, x, y, 40);
            graphic.DrawString(File.ReadAllText("highscore.txt"), "Arial", startColor, x, y + 50, 30);
            graphic.DrawString("Enter to continue", "Arial", startColor, x, y + 100, 30);
        }
        
        public void Update()
        {
            if (MainMenu)
            {
                UpdateMainMenu();
            }
            else if(HighScores)
            {
                UpdateHighScores();
            }
            else if(Help)
            {
                UpdateHelp();
            }

        }

        private void UpdateHelp()
        {
            if(Input.IsKeyDown(Keys.RETURN))
            {
                Help = false;
                MainMenu = true;
                Thread.Sleep(200);
            }
        }

        private void UpdateHighScores()
        {
            if(Input.IsKeyDown(Keys.RETURN))
            {
                MainMenu = true;
                HighScores = false;
                Thread.Sleep(200);
            }
        }

        private void UpdateMainMenu()
        {
            if(Input.IsKeyDown(Keys.F1))
            {
                MainMenu = false;
                Help = true;
            }
            if (Input.MouseX >= x && Input.MouseX <= x + 200)
            {
                if (Input.MouseY >= y && Input.MouseY <= y + 30)
                {
                    startColor = CHOSENCOLOR;
                    loadColor = NOTCHOSENCOLOR;
                    hscolor = NOTCHOSENCOLOR;
                    quitColor = NOTCHOSENCOLOR;
                    if (Input.IsMouseLeftButtonDown || Input.IsKeyDown(Keys.RETURN))
                    {
                        StartGame = true;
                        Thread.Sleep(200);
                    }
                }
                else if (Input.MouseY >= y + ydelta && Input.MouseY <= y + ydelta + 25)
                {
                    startColor = NOTCHOSENCOLOR;
                    loadColor = CHOSENCOLOR;
                    hscolor = NOTCHOSENCOLOR;
                    quitColor = NOTCHOSENCOLOR;
                    if (Input.IsMouseLeftButtonDown || Input.IsKeyDown(Keys.RETURN))
                    {
                        LoadGame = true;
                        Thread.Sleep(200);
                    }
                }
                else if (Input.MouseY >= y + 2 * ydelta && Input.MouseY <= y + 2 * ydelta + 25)
                {
                    startColor = NOTCHOSENCOLOR;
                    loadColor = NOTCHOSENCOLOR;
                    hscolor = CHOSENCOLOR;
                    quitColor = NOTCHOSENCOLOR;
                    if (Input.IsMouseLeftButtonDown || Input.IsKeyDown(Keys.RETURN))
                    {
                        HighScores = true;
                        MainMenu = false;
                        Thread.Sleep(200);
                    }
                }
                else if (Input.MouseY >= y + 2 * ydelta && Input.MouseY <= y + 3 * ydelta + 25)
                {
                    startColor = NOTCHOSENCOLOR;
                    loadColor = NOTCHOSENCOLOR;
                    hscolor = NOTCHOSENCOLOR;
                    quitColor = CHOSENCOLOR;
                    if (Input.IsMouseLeftButtonDown || Input.IsKeyDown(Keys.RETURN))
                    {
                        Quit = true;
                        Thread.Sleep(200);
                    }
                }
            }
            else
            {
                startColor = NOTCHOSENCOLOR;
                loadColor = NOTCHOSENCOLOR;
                hscolor = NOTCHOSENCOLOR;
                quitColor = NOTCHOSENCOLOR;
            }
        }

        private void RenderMainMenu(ConsoleGraphics graphic)
        {
            graphic.DrawString("F1  - Help", "Arial", 0xFF000000, 3, 3, 15);
            graphic.DrawString("Start game", "Arial", startColor, x, y, 30);
            graphic.DrawString("Load game", "Arial", loadColor, x, y + ydelta, 25);
            graphic.DrawString("High score", "Arial", hscolor, x, y + 2 * ydelta, 25);
            graphic.DrawString("Quit", "Arial", quitColor, x, y + 3 * ydelta, 25);
        }


    }
}
