using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.VectorGraphicsEditor
{
    public class Ring : Shape
    {
        public Ring()
        {
        }

        public Ring(double outerRadius, double innerRadius, double x, double y)
        {
            if (innerRadius <= 0 || outerRadius <= 0 || outerRadius <= innerRadius)
            {
                try
                {
                    throw new InvalidOperationException("Ring with a radius equal to or less than 0 cannot exist.");
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

            this.OuterRadius = outerRadius;
            this.InnerRadius = innerRadius;
            this.X = x;
            this.Y = y;
        }

        public override double X { get; set; }

        public override double Y { get; set; }

        public double OuterRadius { get; }

        public double InnerRadius { get; }

        public double Length => 2 * Math.PI * (this.OuterRadius + this.InnerRadius);

        public double Square => Math.PI * (Math.Pow(this.OuterRadius, 2) - Math.Pow(this.InnerRadius, 2));

        public override void ShowInfo()
        {
            Console.WriteLine();
            Console.WriteLine($"Shape: {this.GetType().Name}");
            Console.WriteLine($"Coordinate X: {this.X}");
            Console.WriteLine($"Coordinate Y: {this.Y}");
            Console.WriteLine($"OuterRadius: {this.OuterRadius}");
            Console.WriteLine($"InnerRadius: {this.InnerRadius}");
            Console.WriteLine($"Sum of Circumferences: {this.Length}");
            Console.WriteLine($"Area: {this.Square}");
        }
    }
}
