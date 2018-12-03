using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.User
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime now = DateTime.Now;

            Console.WriteLine("This program allows you to create a User.");

            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}Enter user data:");

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Patronymic: ");
                string patronymic = Console.ReadLine();

                Console.Write("LastName: ");
                string lastName = Console.ReadLine();

                Console.Write("Birth date (Day/Month/Year. Example: 27 1 2000): ");
                string birthDate = Console.ReadLine();

                bool dateParse = DateTime.TryParse(birthDate, out var date);

                if (!dateParse || now.CompareTo(date) < 0)
                {
                    Console.WriteLine("Please enter the valid birth date.");
                    continue;
                }

                User user = new User(name, patronymic, lastName, date);

                Console.WriteLine($"{Environment.NewLine}User Data:");
                user.ShowInfo();
            }
        }
    }
}