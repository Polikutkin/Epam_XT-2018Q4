using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.VectorGraphicsEditor
{
    public class Program
    {
        private static Random r = new Random();

        public static void Main(string[] args)
        {
            Console.WriteLine("This program allows you to create shapes.");

            List<Shape> shapes = new List<Shape>();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Create a line: 1");
                Console.WriteLine("Create a rectangle: 2");
                Console.WriteLine("Create an ellipse: 3");
                Console.WriteLine("Create a round: 4");
                Console.WriteLine("Create a ring: 5");
                Console.WriteLine();
                Console.WriteLine("Show Info: 6");

                string input = Console.ReadLine();
                bool digit = int.TryParse(input, out var d);

                if (!digit)
                {
                    Console.WriteLine("Please enter the valid number.");
                    continue;
                }

                switch (d)
                {
                    case 1:
                        shapes.Add(new Line(r.Next(-50, 50), r.Next(-50, 50)));
                        Console.WriteLine("Line created.");
                        break;
                    case 2:
                        shapes.Add(new Rectangle(r.Next(1, 50), r.Next(1, 50), r.Next(-50, 50), r.Next(-50, 50)));
                        Console.WriteLine("Rectangle created.");
                        break;
                    case 3:
                        shapes.Add(new Ellipse(r.Next(1, 50), r.Next(1, 50), r.Next(-50, 50), r.Next(-50, 50)));
                        Console.WriteLine("Ellipse created.");
                        break;
                    case 4:
                        shapes.Add(new Round(r.Next(1, 50), r.Next(-50, 50), r.Next(-50, 50)));
                        Console.WriteLine("Round created.");
                        break;
                    case 5:
                        shapes.Add(new Ring(r.Next(25, 50), r.Next(1, 24), r.Next(-50, 50), r.Next(-50, 50)));
                        Console.WriteLine("Ring created.");
                        break;
                    case 6:
                        foreach (var shape in shapes)
                        {
                            shape.ShowInfo();
                        }

                        break;
                    default:
                        break;
                }
            }
        }
    }
}
