using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.VectorGraphicsEditor
{
    public class Round : Circle
    {
        public Round()
        {
        }

        public Round(double r, double x, double y) : base(r, x, y)
        {
        }

        public double Square => Math.PI * Math.Pow(this.Radius, 2);

        public override void ShowInfo()
        {
            Console.WriteLine();
            Console.WriteLine($"Shape: {this.GetType().Name}");
            Console.WriteLine($"Coordinate X: {this.X}");
            Console.WriteLine($"Coordinate Y: {this.Y}");
            Console.WriteLine($"Radius: {this.Radius}");
            Console.WriteLine($"Circumference: {this.Length}");
            Console.WriteLine($"Area: {this.Square}");
        }
    }
}