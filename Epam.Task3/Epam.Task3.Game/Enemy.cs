using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Game
{
    public abstract class Enemy
    {
        public abstract void Move();

        public abstract void GetDamage();
    }
}
