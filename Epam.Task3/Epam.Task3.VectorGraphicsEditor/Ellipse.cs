using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.VectorGraphicsEditor
{
    public class Ellipse : Shape
    {
        public Ellipse()
        {
        }

        public Ellipse(double r1, double r2, double x, double y)
        {
            if (r1 <= 0 || r2 <= 0)
            {
                try
                {
                    throw new ArgumentException("Ellipse with a radius equal to or less than 0 cannot exist.");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

            this.XRadius = r1;
            this.YRadius = r2;
            this.X = x;
            this.Y = y;
        }

        public override double X { get; set; }

        public override double Y { get; set; }

        public double XRadius { get; }

        public double YRadius { get; }

        public double Length => Math.PI * (this.XRadius + this.YRadius);

        public double Square => Math.PI * this.XRadius * this.YRadius;

        public override void ShowInfo()
        {
            Console.WriteLine();
            Console.WriteLine($"Shape: {this.GetType().Name}");
            Console.WriteLine($"Coordinate X: {this.X}");
            Console.WriteLine($"Coordinate Y: {this.Y}");
            Console.WriteLine($"XRadius: {this.XRadius}");
            Console.WriteLine($"YRadius: {this.YRadius}");
            Console.WriteLine($"Circumference: {this.Length}");
            Console.WriteLine($"Area: {this.Square}");
        }
    }
}
