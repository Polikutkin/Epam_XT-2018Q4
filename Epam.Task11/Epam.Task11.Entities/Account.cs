using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task7.Entities
{
    public class Account
    {
        public Account(int id, string login, string password, string email, string role)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.Email = email;
            this.Role = role;
        }

        private Account()
        {
        }

        public int Id { get; }

        public string Login { get; }

        public string Password { get; }

        public string Email { get; }

        public string Role { get; }

        public override string ToString()
        {
            return $"{nameof(this.Id)}: {this.Id}, {nameof(this.Login)}: {this.Login}, {nameof(this.Email)}: {this.Email}";
        }
    }
}
