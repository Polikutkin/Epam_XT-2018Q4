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
            DateTime now = DateTime.Now;

            if (now.Year - birthDate.Year > 150 || now.Year - birthDate.Year < 18 || (now.Year - birthDate.Year == 18 && now.DayOfYear < birthDate.DayOfYear))
            {
                try
                {
                    throw new ArgumentException("User can not be older than 150 years and under 18 years old for hire.");
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

                if (now.DayOfYear >= this.HireDate.DayOfYear)
                {
                    return now.Year - this.HireDate.Year;
                }
                else
                {
                    return now.Year - this.HireDate.Year - 1;
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
