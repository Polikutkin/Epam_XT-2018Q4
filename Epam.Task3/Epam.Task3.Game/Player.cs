using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Game
{
    public class Player
    {
        private bool isDead = false;

        public Player()
        {
        }

        public Player(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public bool OnBonus { get; private set; }

        public void Move(MoveDirection direction)
        {
        }

        public void TakeBonus(Bonus bonus)
        {
        }

        public void TakeDamage()
        {
        }
    }
}
