using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInConsole
{
    interface IMenu : IGameObj
    { 
        bool StartGame { get; set; }
        bool LoadGame { get; set; }
        bool Quit { get; set; }
    }
}
