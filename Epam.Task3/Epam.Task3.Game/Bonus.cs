using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Game
{
    public abstract class Bonus : GameSubject
    {
        public abstract TimeSpan BonusTime { get; }
    }
}
