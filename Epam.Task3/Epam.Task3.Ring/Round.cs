using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Ring
{
    public class Round
    {
        private double radius;

        public Round()
        {
        }

        public Round(double x, double y, double r)
        {
            if (r <= 0)
            {
                try
                {
                    throw new ArgumentException("Circle with a radius equal to or less than 0 cannot exist.", nameof(r));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

            this.X = x;
            this.Y = y;
            this.Radius = r;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Radius
        {
            get
            {
                return this.radius;
            }

            set
            {
                if (value <= 0)
                {
                    try
                    {
                        throw new ArgumentException("Circle with a radius equal to or less than 0 cannot exist.", nameof(value));
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                this.radius = value;
            }
        }

        public double Length => 2 * Math.PI * this.Radius;

        public double Square => Math.PI * Math.Pow(this.Radius, 2);

        public void ShowInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Round parameters is: ");
            Console.WriteLine($"Coordinate X: {this.X}");
            Console.WriteLine($"Coordinate Y: {this.Y}");
            Console.WriteLine($"Radius: {this.Radius}");
            Console.WriteLine($"Circumference: {this.Length}");
            Console.WriteLine($"Area: {this.Square}");
        }
    }
}
