using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Employee
{
    public class Employee : User
    {
        public Employee()
        {
        }

        public Employee(string name, string patronymic, string lastName, DateTime birthDate, DateTime hireDate, string position) : base(name, patronymic, lastName, birthDate)
        {
            if (hireDate.Year - birthDate.Year < 18)
            {
                try
                {
                    throw new ArgumentException("The employee must be over 18 to hire.");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

            this.HireDate = hireDate;
            this.Position = position;
        }

        public DateTime HireDate { get; }

        public string Position { get; set; }

        public int WorkExperience
        {
            get
            {
                DateTime now = DateTime.Now;

                if (now.Month < this.HireDate.Month)
                {
                    return now.Year - this.HireDate.Year - 1;
                }
                else
                {
                    return now.Year - this.HireDate.Year;
                }
            }
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Work experience (years): {this.WorkExperience}");
            Console.WriteLine($"Position: {this.Position}");
        }
    }
}
