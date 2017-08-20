using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace GameInConsole
{
    interface IGameObj
    {
        void Update();
        void Render(ConsoleGraphics graphic);
    }
}
