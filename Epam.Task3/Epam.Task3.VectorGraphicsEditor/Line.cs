using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.VectorGraphicsEditor
{
    public class Line : Shape
    {
        public Line()
        {
        }

        public Line(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public override void ShowInfo()
        {
            Console.WriteLine();
            Console.WriteLine($"Shape: {this.GetType().Name}");
            Console.WriteLine($"Coordinate X: {this.X}");
            Console.WriteLine($"Coordinate Y: {this.Y}");
        }
    }
}
