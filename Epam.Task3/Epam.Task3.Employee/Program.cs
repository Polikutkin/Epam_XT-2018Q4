using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Employee
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime now = DateTime.Now;

            Console.WriteLine("This program allows you to create an Employee.");

            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}Enter employee data:");

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Patronymic: ");
                string patronymic = Console.ReadLine();

                Console.Write("LastName: ");
                string lastName = Console.ReadLine();

                Console.Write("Birth date (Day/Month/Year. Example: 27 1 2000): ");
                string birthDate = Console.ReadLine();

                Console.Write("Position: ");
                string position = Console.ReadLine();

                Console.Write("Hire date (Day/Month/Year. Example: 27 1 2000):: ");
                string hireDate = Console.ReadLine();

                bool birthParse = DateTime.TryParse(birthDate, out var birth);
                bool hireParse = DateTime.TryParse(hireDate, out var hire);

                if (!birthParse || !hireParse || now.CompareTo(birth) < 0 || now.CompareTo(birth) < 0)
                {
                    Console.WriteLine("Please enter the date in a valid format.");
                    continue;
                }

                if (birth.Year - hire.Year < 18)
                {
                    Console.WriteLine("The employee must be over 18 to hire.");
                    continue;
                }

                Employee emp = new Employee(name, patronymic, lastName, birth, hire, position);

                Console.WriteLine($"{Environment.NewLine}Employee info:");
                emp.ShowInfo();
            }
        }
    }
}
