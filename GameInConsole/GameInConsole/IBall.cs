using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInConsole
{
    interface IBall : IGameObj
    {
        bool FallDown { get; set; }
        void StartPositon();
        event Action killBrick;
    }
}
