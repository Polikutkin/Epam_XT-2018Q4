using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Ring
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("This program allows you to create a ring.");

            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}Enter coondinates:");

                Console.Write("X: ");
                string s1 = Console.ReadLine();

                Console.Write("Y: ");
                string s2 = Console.ReadLine();

                Console.Write("Enter outer radius of a ring: ");
                string s3 = Console.ReadLine();

                Console.Write("Enter inner radius of a ring: ");
                string s4 = Console.ReadLine();

                bool xCoordinate = double.TryParse(s1, out var x);
                bool yCoordinate = double.TryParse(s2, out var y);
                bool outerRadius = double.TryParse(s3, out var outer);
                bool innerRadius = double.TryParse(s4, out var inner);

                if (!xCoordinate || !yCoordinate || !outerRadius || !innerRadius)
                {
                    Console.WriteLine("Ring parameters is not correct. Please enter the valid parameters.");
                    continue;
                }

                if (inner <= 0 || inner >= outer)
                {
                    Console.WriteLine("Radius of the ring must be more than 0. Outer radius must be more than inner radius.");
                    continue;
                }

                Ring ring = new Ring(x, y, outer, inner);
                ring.ShowInfo();
            }
        }
    }
}
