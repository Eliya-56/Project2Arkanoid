using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInConsole
{
    interface IBrick : IGameObj
    {
        int X { get; }
        int Y { get; }
        int Length { get;}
        int Thick { get; }
        uint Color { get;}
        bool IsAlive { get; set; }
    }
}
