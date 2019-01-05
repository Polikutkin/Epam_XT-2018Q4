using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.Task7.Entities;

namespace Epam.Task7.DAL.Contracts
{
    public interface IUserDao
    {
        void Add(User user);

        IEnumerable<User> GetAll();

        User GetById(int id);

        bool Remove(int id);

        void Update(int id, User user);
    }
}
