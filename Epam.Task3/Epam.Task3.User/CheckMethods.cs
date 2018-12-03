using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.User
{
    public static class CheckMethods
    {
        public static string CheckString(string info)
        {
            bool check = true;

            string field;

            do
            {
                check = true;

                Console.Write($"{info}: ");

                field = Console.ReadLine();

                if (field.Length == 0)
                {
                    check = false;
                    Console.WriteLine("String cannot be empty. Please enter the valid data.");
                    continue;
                }

                foreach (var c in field)
                {
                    if (!char.IsLetter(c))
                    {
                        check = false;
                        Console.WriteLine("String must consist only of letters. Please enter the valid data.");
                        break;
                    }
                }
            }
            while (!check);

            return field;
        }

        public static DateTime CheckDate(string info)
        {
            bool check = true;
            string field;
            DateTime now = DateTime.Now;
            DateTime date;

            do
            {
                check = true;

                Console.Write($"{info}: ");

                field = Console.ReadLine();
                bool dateParse = DateTime.TryParse(field, out date);

                if (!dateParse)
                {
                    check = false;
                    Console.WriteLine("Please enter the valid birth date.");
                }

                if (now.CompareTo(date) < 0 || now.Year - date.Year > 150)
                {
                    check = false;
                    Console.WriteLine("User birth date cannot be older than 150 years and later until today.");
                }
            }
            while (!check);

            return date;
        }
    }
}
