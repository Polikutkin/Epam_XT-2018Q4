using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2.FontAdjustment
{
    [Flags]
    enum Font : byte
    {
        None = 0,
        Bold = 1,
        Italic = 2,
        Underline = 4,
    }

    class Program
    {
        static Font FontAdjustment(ref int f, int n)
        {
            switch (f)
            {
                case 0:
                    f |= n;
                    break;
                case 1:
                    if(n == 1)
                    {
                        f ^= n;
                    }
                    else
                    {
                        f |= n;
                    }
                    break;
                case 2:
                    if (n == 2)
                    {
                        f ^= n;
                    }
                    else
                    {
                        f |= n;
                    }
                    break;
                case 4:
                    if (n == 4)
                    {
                        f ^= n;
                    }
                    else
                    {
                        f |= n;
                    }
                    break;
                case 3:
                    if(n == 4)
                    {
                        f |= n;
                    }
                    else
                    {
                        f ^= n;
                    }
                    break;
                case 5:
                    if (n == 3)
                    {
                        f |= n;
                    }
                    else
                    {
                        f ^= n;
                    }
                    break;
                case 6:
                    if (n == 1)
                    {
                        f |= n;
                    }
                    else
                    {
                        f ^= n;
                    }
                    break;
                case 7:
                    f ^= n;
                    break;
                default:
                    break;
            }

            return (Font)f;
        }

        static void Main(string[] args)
        {
            int number = 0;
            int font = 0;

            while (true)
            {

                Console.Write("Label parameters: ");

                Console.WriteLine(FontAdjustment(ref font, number));

                Console.WriteLine($"Enter: {Environment.NewLine}\t1: bold {Environment.NewLine}\t2: italic {Environment.NewLine}\t3: underline");

                bool result = int.TryParse(Console.ReadLine(), out var num);

                if (!result || num < 1 || num > 3)
                {
                    Console.WriteLine("Please, enter the correct number.");
                    number = 0;
                    continue;
                }

                if (num == 3)
                {
                    num = 4;
                }

                number = num;
            }
        }
    }
}
