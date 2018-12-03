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
            Console.WriteLine("This program allows you to create a User.");

            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}Enter user data:");

                string name = CheckMethods.CheckString("Name");
                string patronymic = CheckMethods.CheckString("Patronymic");
                string lastName = CheckMethods.CheckString("LastName");
                DateTime birthDate = CheckMethods.CheckDate("Birth date (Day/Month/Year. Example: 27 1 2000)");

                User user = new User(name, patronymic, lastName, birthDate);

                Console.WriteLine($"{Environment.NewLine}User Data:");
                user.ShowInfo();
            }
        }
    }
}