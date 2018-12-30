﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task7.Entities
{
    public class User
    {
        private int id;
        private string firstName;
        private string lastName;
        private DateTime birthDate;
        private List<Award> awards = new List<Award>();

        public User()
        {
        }

        public User(string firstName, string lastName, DateTime birthDate)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                if (value < 1)
                {
                    throw new ArgumentException();
                }

                this.id = value;
            }
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                if (value.Length < 1 || value.Length > 30 || !value.Any(c => char.IsLetter(c)))
                {
                    throw new ArgumentException();
                }

                this.firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                if (value.Length < 1 || value.Length > 30 || !value.Any(c => char.IsLetter(c)))
                {
                    throw new ArgumentException();
                }

                this.lastName = value;
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return this.birthDate;
            }

            set
            {
                if (DateTime.Now.Year - value.Year > 150 || value > DateTime.Now)
                {
                    throw new ArgumentException();
                }

                this.birthDate = value;
            }
        }

        public int Age
        {
            get
            {
                DateTime now = DateTime.Now;

                if (this.BirthDate.DayOfYear <= now.DayOfYear)
                {
                    return now.Year - this.BirthDate.Year;
                }
                else
                {
                    return now.Year - this.BirthDate.Year - 1;
                }
            }
        }

        public List<Award> Awards
        {
            get
            {
                return this.awards;
            }

            set
            {
                this.awards = value;
            }
        }

        public void AddAward(Award award)
        {
            if (award.Id < 1)
            {
                throw new ArgumentException();
            }

            this.awards.Add(award);
        }

        public void RemoveAward(Award award)
        {
            this.awards.Remove(award);
        }

        public override string ToString()
        {
            return $"ID: {this.Id}, Name: {this.FirstName} {this.LastName}, Birth Date: {this.BirthDate.ToShortDateString()}, Age: {this.Age}";
        }

        public string ShowUserInfo()
        {
            if (this.Awards == null || !this.Awards.Any())
            {
                return this.ToString();
            }

            StringBuilder sb = new StringBuilder();

            foreach (var award in this.Awards)
            {
                sb.Append($"{award.Id} {award.Title}{Environment.NewLine}");
            }

            return this.ToString() + $"{Environment.NewLine}User awards:{Environment.NewLine}" + sb.ToString();
        }
    }
}
