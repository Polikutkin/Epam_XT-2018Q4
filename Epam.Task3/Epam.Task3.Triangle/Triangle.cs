using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Triangle
{
    public class Triangle
    {
        public Triangle()
        {
        }

        public Triangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0 || a >= b + c || b >= a + c || c >= a + b)
            {
                try
                {
                    throw new ArgumentException("Triangle with these parameters cannot exist.");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

            this.A = a;
            this.B = b;
            this.C = c;
        }

        public double A { get; }

        public double B { get; }

        public double C { get; }

        public double Perimeter => this.A + this.B + this.C;

        public double Square
        {
            get
            {
                double p = (this.A + this.B + this.C) / 2;
                return Math.Sqrt(p * (p - this.A) * (p - this.B) * (p - this.C));
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Environment.NewLine}Triangle parameters is: ");
            Console.WriteLine($"A: {this.A}");
            Console.WriteLine($"B: {this.B}");
            Console.WriteLine($"C: {this.C}");
            Console.WriteLine($"Perimeter: {this.Perimeter}");
            Console.WriteLine($"Area: {this.Square}");
        }
    }
}
