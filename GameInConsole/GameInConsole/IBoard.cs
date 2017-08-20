using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInConsole
{
    interface IBoard : IGameObj
    {

        int X { get; }
        int Y { get; }
        int W { get; }
        int H { get; }
        void StartPositon();
    }
}
