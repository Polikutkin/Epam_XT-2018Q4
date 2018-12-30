using System.Collections.Generic;
using Epam.Task7.Entities;

namespace Epam.Task7.BLL.Contracts
{
    public interface IUserLogic
    {
        IEnumerable<User> GetAll();

        User GetById(int id);

        bool Add(User user);

        bool Update(int id, User user);

        bool Remove(int id);
    }
}
