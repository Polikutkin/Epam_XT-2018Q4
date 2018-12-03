using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Game
{
    public class GameField
    {
        private Player player;
        private List<Block> blocks;
        private List<Bonus> bonuses;
        private List<Enemy> enemies;

        public GameField() : this(string.Empty, Difficulty.Normal)
        {
        }

        public GameField(string playerName, Difficulty dif)
        {
            this.player = new Player(playerName);

            this.Width = 20 * (int)dif;
            this.Height = 20 * (int)dif;

            this.blocks = new List<Block>();
            for (int i = 0; i < 4 * (int)dif; i++)
            {
                this.blocks.Add(new BlockTree());
                this.blocks.Add(new BlockStone());
            }

            this.bonuses = new List<Bonus>();
            for (int i = 0; i < 2 * (int)dif; i++)
            {
                this.bonuses.Add(new BonusApple());
                this.bonuses.Add(new BonusCherry());
            }

            this.enemies = new List<Enemy>();
            for (int i = 0; i < 2 * (int)dif; i++)
            {
                this.enemies.Add(new EnemyBear());
                this.enemies.Add(new EnemyWolf());
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
    }
}
