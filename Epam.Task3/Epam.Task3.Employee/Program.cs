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
            Console.WriteLine("This program allows you to create an Employee.");

            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}Enter employee data:");

                string name = CheckMethods.CheckString("Name");
                string patronymic = CheckMethods.CheckString("Patronymic");
                string lastName = CheckMethods.CheckString("LastName");
                DateTime birthDate = CheckMethods.CheckBirthDate("Birth date (Day/Month/Year. Example: 27 1 2000)");
                DateTime hireDate = CheckMethods.CheckHireDate("Hire date (Day/Month/Year. Example: 27 1 2000)", birthDate);
                string position = CheckMethods.CheckString("Position");

                Employee emp = new Employee(name, patronymic, lastName, birthDate, hireDate, position);

                Console.WriteLine($"{Environment.NewLine}Employee info:");
                emp.ShowInfo();
            }
        }
    }
}
