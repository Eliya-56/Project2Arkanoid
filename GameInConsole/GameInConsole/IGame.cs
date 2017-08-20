using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInConsole
{
    interface IPlayGame : IGameObj
    {
        bool IsEndGame { get; set; }
        int Points { get; set; }
    }
}
