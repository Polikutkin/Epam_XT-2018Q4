using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Epam.Task7.BLL.Contracts;
using Epam.Task7.DAL.Contracts;
using Epam.Task7.Entities;

namespace Epam.Task7.BLL
{
    public class AccountLogic : IAccountLogic
    {
        internal const string AccountsCacheKey = "GetAllAccounts";

        private readonly IAccountDao accountDao;
        private readonly ICacheLogic cacheLogic;

        public AccountLogic()
        {
        }

        public AccountLogic(IAccountDao accountDao, ICacheLogic cacheLogic)
        {
            this.accountDao = accountDao;
            this.cacheLogic = cacheLogic;
        }

        public Account GetAccount(string login)
        {
            return this.accountDao.GetAccount(login);
        }

        public IEnumerable<Account> GetAll()
        {
            bool cacheResultParse = this.cacheLogic.Get(AccountsCacheKey, out IEnumerable<Account> cacheData);

            if (cacheResultParse)
            {
                return cacheData;
            }

            var data = this.accountDao.GetAll().ToList();

            this.cacheLogic.Add(AccountsCacheKey, data);

            return data;
        }

        public string[] GetRoles(string login)
        {
            return this.accountDao.GetRoles(login);
        }

        public bool GiveAdminRights(string login)
        {
            this.cacheLogic.Remove(AccountsCacheKey);

            return this.accountDao.GiveAdminRights(login);
        }

        public bool Login(string login, string password)
        {
            return this.accountDao.Login(login, password);
        }

        public bool Register(string email, string login, string password)
        {
            if (this.IsValidRegistrationInfo(email, login, password))
            {
                return this.accountDao.Register(email, login, password);
            }

            return false;
        }

        public bool TakeAdminRights(string login)
        {
            this.cacheLogic.Remove(AccountsCacheKey);

            return this.accountDao.TakeAdminRights(login);
        }

        private bool IsValidRegistrationInfo(string email, string login, string password)
        {
            return this.CheckEmailFormat(email)
                && this.CheckLoginFormat(login)
                && this.CheckPasswordFormat(password);
        }

        private bool CheckEmailFormat(string email)
        {
            string emailTemplate = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            return Regex.IsMatch(email, emailTemplate, RegexOptions.IgnoreCase);
        }

        private bool CheckLoginFormat(string login)
        {
            string loginTemplate = "^[a-zA-Z]{3,20}$";

            if (this.GetAll().All(acc => acc.Login != login))
            {
                return Regex.IsMatch(login, loginTemplate, RegexOptions.IgnoreCase);
            }

            return false;
        }

        private bool CheckPasswordFormat(string password)
        {
            string passwordTemplate = "^[a-zA-Z0-9$./]{32,128}$";

            return Regex.IsMatch(password, passwordTemplate, RegexOptions.IgnoreCase);
        }
    }
}
