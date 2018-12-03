using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Employee
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

                    continue;
                }
            }
            while (!check);

            return field;
        }

        public static DateTime CheckBirthDate(string info)
        {
            bool check = true;
            string field;
            DateTime now = DateTime.Now;
            DateTime birthDate;

            do
            {
                check = true;

                Console.Write($"{info}: ");

                field = Console.ReadLine();
                bool dateParse = DateTime.TryParse(field, out birthDate);

                if (!dateParse)
                {
                    check = false;
                    Console.WriteLine("Please enter the valid birth date.");
                    continue;
                }

                if (now.Year - birthDate.Year > 150 || now.Year - birthDate.Year < 18 || (now.Year - birthDate.Year == 18 && now.DayOfYear < birthDate.DayOfYear))
                {
                    check = false;
                    Console.WriteLine("User can not be older than 150 years and under 18 years old for hire.");
                    continue;
                }
            }
            while (!check);

            return birthDate;
        }

        public static DateTime CheckHireDate(string info, DateTime birthDate)
        {
            bool check = true;
            string field;
            DateTime now = DateTime.Now;
            DateTime hireDate;

            do
            {
                check = true;

                Console.Write($"{info}: ");

                field = Console.ReadLine();
                bool dateParse = DateTime.TryParse(field, out hireDate);

                if (!dateParse)
                {
                    check = false;
                    Console.WriteLine("Please enter the valid hire date.");
                    continue;
                }

                if (now.CompareTo(hireDate) < 0 || hireDate.Year - birthDate.Year < 18)
                {
                    check = false;
                    Console.WriteLine("The employee must be over 18 to hire. Employee hire date cannot be later until today.");
                    continue;
                }
            }
            while (!check);

            return hireDate;
        }
    }
}
