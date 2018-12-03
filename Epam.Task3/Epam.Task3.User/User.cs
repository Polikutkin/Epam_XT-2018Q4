using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.User
{
    public class User
    {
        public User()
        {
        }

        public User(string name, string patronymic, string lastName, DateTime birthDate)
        {
            if (DateTime.Now.CompareTo(birthDate) < 0)
            {
                try
                {
                    throw new ArgumentException("Birth date is invalid.", nameof(birthDate));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

            this.Name = name;
            this.Patronymic = patronymic;
            this.LastName = lastName;
            this.BirthDate = birthDate;
        }

        public string Name { get; }

        public string Patronymic { get; }

        public string LastName { get; }

        public DateTime BirthDate { get; }

        public int Age
        {
            get
            {
                DateTime now = DateTime.Now;

                if (now.Month > this.BirthDate.Month)
                {
                    return now.Year - this.BirthDate.Year;
                }
                else if (now.Month == this.BirthDate.Month && now.Day >= this.BirthDate.Day)
                {
                    return now.Year - this.BirthDate.Year;
                }
                else
                {
                    return now.Year - this.BirthDate.Year - 1;
                }
            }
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Name: {this.Name}");
            Console.WriteLine($"Patronymic: {this.Patronymic}");
            Console.WriteLine($"LastName: {this.LastName}");
            Console.WriteLine($"Birth Date: {this.BirthDate.ToString("dd.MM.yyyy")}");
            Console.WriteLine($"Age: {this.Age}");
        }
    }
}