using System.Collections.Generic;
using Epam.Task7.Entities;

namespace Epam.Task7.BLL.Contracts
{
    public interface IUserLogic
    {
        IEnumerable<User> GetAll();

        User GetById(int id);

        void Add(User user);

        void Update(int id, User user);

        bool Remove(int id);

        void AddImage(int id, byte[] image);

        bool RemoveImage(int id);
    }
}
