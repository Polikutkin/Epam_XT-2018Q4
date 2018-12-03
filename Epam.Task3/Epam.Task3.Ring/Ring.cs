using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Ring
{
    public class Ring
    {
        public Ring()
        {
        }

        public Ring(double x, double y, double outerRadius, double innerRadius)
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

            this.X = x;
            this.Y = y;
            this.OuterRadius = outerRadius;
            this.InnerRadius = innerRadius;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double OuterRadius { get; }

        public double InnerRadius { get; }

        public double Length => 2 * Math.PI * (this.OuterRadius + this.InnerRadius);

        public double Square => Math.PI * (Math.Pow(this.OuterRadius, 2) - Math.Pow(this.InnerRadius, 2));

        public void ShowInfo()
        {
            Console.WriteLine($"{Environment.NewLine}Ring parameters is: ");
            Console.WriteLine($"Coordinate X: {this.X}");
            Console.WriteLine($"Coordinate Y: {this.Y}");
            Console.WriteLine($"OuterRadius: {this.OuterRadius}");
            Console.WriteLine($"InnerRadius: {this.InnerRadius}");
            Console.WriteLine($"Sum of Circumferences: {this.Length}");
            Console.WriteLine($"Area: {this.Square}");
        }
    }
}
