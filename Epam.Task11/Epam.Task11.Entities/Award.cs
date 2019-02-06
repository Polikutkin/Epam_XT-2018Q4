using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task7.Entities
{
    public class Award
    {
        private int id;
        private string title;

        public Award()
        {
        }

        public Award(string title)
        {
            this.Title = title;
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                if (value < 1)
                {
                    throw new ArgumentException();
                }

                this.id = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (value.Length < 1 || value.Length > 30 || !value.Any(c => char.IsLetterOrDigit(c)))
                {
                    throw new ArgumentException();
                }

                this.title = value;
            }
        }

        public byte[] Image { get; set; }

        public override string ToString()
        {
            return $"ID: {this.Id}, Title: {this.Title}";
        }
    }
}
