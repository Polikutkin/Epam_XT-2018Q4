using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.Task7.BLL.Contracts;
using Epam.Task7.DAL.Contracts;
using Epam.Task7.Entities;

namespace Epam.Task7.BLL
{
    public class AccountLogic : IAccountLogic
    {
        private readonly IAccountDao accountDao;

        public AccountLogic()
        {
        }

        public AccountLogic(IAccountDao accountDao)
        {
            this.accountDao = accountDao;
        }

        public Account GetAccount(string login)
        {
            return this.accountDao.GetAccount(login);
        }

        public IEnumerable<Account> GetAll()
        {
            return this.accountDao.GetAll();
        }

        public string[] GetRoles(string login)
        {
            return this.accountDao.GetRoles(login);
        }

        public bool GiveAdminRights(string login)
        {
            return this.accountDao.GiveAdminRights(login);
        }

        public bool Login(string login, string password)
        {
            return this.accountDao.Login(login, password);
        }

        public bool Register(string email, string login, string password)
        {
            return this.accountDao.Register(email, login, password);
        }

        public bool TakeAdminRights(string login)
        {
            return this.accountDao.TakeAdminRights(login);
        }
    }
}
