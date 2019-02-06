using System.Collections.Generic;
using Epam.Task7.Entities;

namespace Epam.Task7.BLL.Contracts
{
    public interface IAccountLogic
    {
        Account GetAccount(string login);

        IEnumerable<Account> GetAll();

        string[] GetRoles(string login);

        bool GiveAdminRights(string login);

        bool Login(string login, string password);

        bool Register(string email, string login, string password);

        bool TakeAdminRights(string login);
    }
}
