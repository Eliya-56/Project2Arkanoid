using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GameInConsole
{
    delegate void LevelConstruct(List<IBrick> bricks, ConsoleGraphics graphic);

    enum Scene
    {
        Menu = 0,
        PlayGame,
        EndGame,
        Win,
        Pause,
    }

    class GameEngine
    {
        private IMenu menu;
        private IPlayGame playGame;
        private ConsoleGraphics graphic;
        private Scene scene = Scene.Menu;
        private List<LevelConstruct> levels = new List<LevelConstruct>();
        private BinaryFormatter formatter = new BinaryFormatter();
        private int highscore = 0;

        public GameEngine(ConsoleGraphics graphic)
        {
            this.graphic = graphic;
            levels.Add(Levels.FirstLevel);
            levels.Add(Levels.SecondLevel);
            levels.Add(Levels.ThirdLevel); 
            levels.Add(Levels.FourthLevel);
            levels.Add(Levels.FifthLevel); 
            levels.Add(Levels.SixthLevel);
            levels.Add(Levels.SeventhLevel);
        }

        public void Start()
        {
            menu = new Menu(graphic);
            if(!File.Exists("highscore.txt"))
            {
                File.WriteAllText("highscore.txt", "0");
            }
            else
            {
                if (!int.TryParse(File.ReadAllText("highscore.txt"), out highscore))
                {
                    highscore = 0;
                    File.WriteAllText("highscore.txt", "0");
                }
            }

            while (true)
            {
                graphic.FillRectangle(Program.BackGround, 0, 0, graphic.ClientWidth, graphic.ClientHeight);

                switch (scene)
                {
                    case Scene.Menu:
                        if (menu.StartGame)
                        {
                            playGame = new PlayGame(graphic, levels);
                            menu.StartGame = false;
                            scene = Scene.PlayGame;
                            break;
                        }
                        else if (menu.LoadGame)
                        {
                            if (File.Exists("savegame.dat"))
                            {
                                using (FileStream fs = new FileStream("savegame.dat", FileMode.OpenOrCreate))
                                {
                                    playGame = (IPlayGame)formatter.Deserialize(fs);
                                }
                                scene = Scene.PlayGame;
                                playGame.Render(graphic);
                                graphic.DrawString("Start", "Arial", 0xFFFFFFFF, graphic.ClientWidth / 2, graphic.ClientHeight / 3 + 50, 30);
                                graphic.FlipPages();
                                Thread.Sleep(1200);
                            }
                            else
                            {
                                graphic.FillRectangle(Program.BackGround, 0, 0, graphic.ClientWidth, graphic.ClientHeight);
                                graphic.DrawString("No savegame file found", "Arial", 0xFFFFFFFF, graphic.ClientWidth / 2 - 150, graphic.ClientHeight / 3 + 50, 30);
                                graphic.FlipPages();
                                Thread.Sleep(1200);
                            }
                            menu.LoadGame = false;
                            break;
                        }
                        else if (menu.Quit)
                        {
                            Environment.Exit(0);
                            break;
                        }

                        menu.Update();
                        menu.Render(graphic);
                        break;

                    case Scene.PlayGame:
                        if (Input.IsKeyDown(Keys.KEY_S))
                        {
                            using (FileStream fs = new FileStream("savegame.dat", FileMode.OpenOrCreate))
                            {
                                formatter.Serialize(fs, playGame);
                            }
                            playGame.Render(graphic);
                            graphic.DrawString("Game saved", "Arial", 0xFFFFFFFF, graphic.ClientWidth / 2 - 100, graphic.ClientHeight / 2, 30);
                            graphic.FlipPages();
                            Thread.Sleep(700);
                        }
                        if (Input.IsKeyDown(Keys.KEY_P))
                        {
                            scene = Scene.Pause;
                            Thread.Sleep(200);
                        }
                        if (playGame.IsEndGame)
                        {
                            playGame.IsEndGame = false;
                            scene = Scene.EndGame;
                            break;
                        }
                        playGame.Update();
                        playGame.Render(graphic);
                        break;

                    case Scene.EndGame:
                        if (highscore < playGame.Points)
                        {
                            File.WriteAllText("highscore.txt", playGame.Points.ToString());
                            graphic.DrawString("Your Score is " + playGame.Points, "Arial", 0xFFFFFFFF, graphic.ClientWidth / 2 - 150, graphic.ClientHeight / 3 + 50, 30);
                            graphic.DrawString("BEST SCORE", "Arial", 0xFFFF0000, graphic.ClientWidth / 2 - 125, graphic.ClientHeight / 3 + 100, 30);
                            graphic.DrawString("Press enter to continue", "Arial", 0xFFFFFFFF, graphic.ClientWidth / 2 - 200, graphic.ClientHeight / 3 + 150, 30);
                        }
                        else
                        {
                            graphic.DrawString("Your Score is " + playGame.Points, "Arial", 0xFFFFFFFF, graphic.ClientWidth / 2 - 150, graphic.ClientHeight / 3 + 50, 30);
                            graphic.DrawString("Press enter to continue", "Arial", 0xFFFFFFFF, graphic.ClientWidth / 2 - 200, graphic.ClientHeight / 3 + 100, 30);
                        }
                        if (Input.IsKeyDown(Keys.RETURN))
                        {
                            scene = Scene.Menu;
                            Thread.Sleep(200);
                        }
                        break;


                    case Scene.Pause:
                        if (Input.IsKeyDown(Keys.KEY_P))
                        {
                            scene = Scene.PlayGame;
                            Thread.Sleep(200);
                        }
                        playGame.Render(graphic);
                        graphic.DrawString("PAUSE", "Arial", 0xFFFFFFFF, graphic.ClientWidth / 2 - 100, graphic.ClientHeight / 2, 30);
                        break;
                }

                graphic.FlipPages();
            }
        }
    }
}
