using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Round
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("This program allows you to create a round.");

            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}Enter coondinates:");

                Console.Write("X: ");
                string s1 = Console.ReadLine();

                Console.Write("Y: ");
                string s2 = Console.ReadLine();

                Console.Write("Enter radius of the round: ");
                string s3 = Console.ReadLine();

                bool xCoordinate = double.TryParse(s1, out var x);
                bool yCoordinate = double.TryParse(s2, out var y);
                bool radius = double.TryParse(s3, out var r);

                if (!xCoordinate || !yCoordinate || !radius)
                {
                    Console.WriteLine("Round parameters is not correct. Please enter the valid parameters.");
                    continue;
                }

                if (r <= 0)
                {
                    Console.WriteLine("Radius of the round must be more than 0.");
                    continue;
                }

                Round round = new Round(x, y, r);
                round.ShowInfo(); 
            }
        }
    }
}
