using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.VectorGraphicsEditor
{
    public class Rectangle : Shape
    {
        public Rectangle()
        {
        }

        public Rectangle(double a, double b, double x, double y)
        {
            if (a <= 0 || b <= 0)
            {
                try
                {
                    throw new ArgumentException("Rectangle with a side equal to or less than 0 cannot exist.");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

            this.A = a;
            this.B = b;
            this.X = x;
            this.Y = y;
        }

        public double A { get; }

        public double B { get; }

        public double Perimeter => (this.A + this.B) * 2;

        public double Square => this.A * this.B;

        public override void ShowInfo()
        {
            Console.WriteLine();
            Console.WriteLine($"Shape: {this.GetType().Name}");
            Console.WriteLine($"Bottom left coordinate X: {this.X}");
            Console.WriteLine($"Bottom left coordinate Y: {this.Y}");
            Console.WriteLine($"Side A Length: {this.A}");
            Console.WriteLine($"Side B Length: {this.B}");
            Console.WriteLine($"Perimeter: {this.Perimeter}");
            Console.WriteLine($"Area: {this.Square}");
        }
    }
}
