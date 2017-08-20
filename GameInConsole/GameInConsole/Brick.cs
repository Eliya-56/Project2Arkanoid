using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace GameInConsole
{
    [Serializable]
    class Brick : IGameObj, IBrick
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Length { get; private set; }
        public int Thick { get; private set; }
        public uint Color { get; private set; }
        public bool IsAlive { get; set; }

        public Brick(ConsoleGraphics graphic, uint Color, int X, int Y, int Length , int Thick)
        {
            this.X = X;
            this.Y = Y;
            this.Length = Length;
            this.Thick = Thick;
            this.Color = Color;
            IsAlive = true;
        }

        public void Render(ConsoleGraphics graphic)
        {

            graphic.DrawRectangle(Color, X,Y,Length, Thick);
            graphic.FillRectangle(Color, X + 1, Y + 1, Length - 2, Thick - 2);
        }

        public void Update()
        {
            if(!IsAlive)
            {
                Color = Program.BackGround;
            }
        }
    }
}
