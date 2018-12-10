using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Game
{
    public class GameField
    {
        private bool gameOver = false;

        private List<GameSubject> gameSubjects;

        public GameField() : this(string.Empty, Difficulty.Normal)
        {
        }

        public GameField(string playerName, Difficulty dif)
        {
            this.Width = 20 * (int)dif;
            this.Height = 20 * (int)dif;

            this.gameSubjects = new List<GameSubject>();

            this.gameSubjects.Add(new Player(playerName));

            for (int i = 0; i < 4 * (int)dif; i++)
            {
                this.gameSubjects.Add(new BlockTree());
                this.gameSubjects.Add(new BlockStone());
            }

            for (int i = 0; i < 2 * (int)dif; i++)
            {
                this.gameSubjects.Add(new BonusApple());
                this.gameSubjects.Add(new BonusCherry());

                this.gameSubjects.Add(new EnemyBear());
                this.gameSubjects.Add(new EnemyWolf());
            }
        }

        public int Width { get; }

        public int Height { get; }

        public void BonusTaken(Bonus bonus)
        {
        }

        public void DamageTaken(Enemy enemy)
        {
        }

        public void CreateGameField()
        {
            foreach (var subject in gameSubjects)
            {
                subject.ViewOnScreen();
            }
        }
    }
}
