using System;
using System.Collections.Generic;
using System.Linq;
using Epam.Task7.BLL.Contracts;
using Epam.Task7.DAL.Contracts;
using Epam.Task7.Entities;

namespace Epam.Task7.BLL
{
    public class UserLogic : IUserLogic
    {
        internal const string UsersCacheKey = "GetAllUsers";
        internal const string LastUserCacheKey = "GetUserById";
        internal const string IdExceptionMessage = "ID must be above 0.";

        private readonly IUserDao userDao;
        private readonly ICacheLogic cacheLogic;

        public UserLogic()
        {
        }

        public UserLogic(IUserDao userDao, ICacheLogic cacheLogic)
        {
            this.userDao = userDao;
            this.cacheLogic = cacheLogic;
        }

        public void Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (CheckUserState(user))
            {
                throw new ArgumentException("Wrong user data");
            }

            this.cacheLogic.Remove(UsersCacheKey);
            this.userDao.Add(user);
        }

        public IEnumerable<User> GetAll()
        {
            bool cacheResultParse = this.cacheLogic.Get(UsersCacheKey, out IEnumerable<User> cacheData);

            if (cacheResultParse)
            {
                return cacheData;
            }

            var data = this.userDao.GetAll().ToList();

            this.cacheLogic.Add(UsersCacheKey, data);

            return data;
        }

        public User GetById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), IdExceptionMessage);
            }

            bool cacheResultParse = this.cacheLogic.Get(LastUserCacheKey, out User cacheData);

            if (cacheResultParse 
                && cacheData != null 
                && cacheData.Id == id)
            {
                return cacheData;
            }

            var data = this.userDao.GetById(id);

            this.cacheLogic.Remove(LastUserCacheKey);
            this.cacheLogic.Add(LastUserCacheKey, data);

            return data;
        }

        public bool Remove(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), IdExceptionMessage);
            }

            this.cacheLogic.Remove(UsersCacheKey);

            bool cacheResultParse = this.cacheLogic.Get(LastUserCacheKey, out User cacheData);

            if (cacheResultParse
                && cacheData != null
                && cacheData.Id == id)
            {
                this.cacheLogic.Remove(LastUserCacheKey);
            }

            return this.userDao.Remove(id);
        }

        public void Update(int id, User user)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), IdExceptionMessage);
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (CheckUserState(user))
            {
                throw new ArgumentException("Wrong user data");
            }

            this.cacheLogic.Remove(UsersCacheKey);

            bool cacheResultParse = this.cacheLogic.Get(LastUserCacheKey, out User cacheData);

            if (cacheResultParse
                && cacheData != null
                && cacheData.Id == id)
            {
                this.cacheLogic.Remove(LastUserCacheKey);
            }

            this.userDao.Update(id, user);
        }

        private static bool CheckUserState(User user)
        {
            bool state = user.FirstName == null
                || user.FirstName.Length < 1
                || user.FirstName.Length > 30
                || !CheckSymbol(user.FirstName)
                || user.LastName == null
                || user.LastName.Length < 1
                || user.LastName.Length > 30
                || !CheckSymbol(user.LastName)
                || user.BirthDate == null
                || user.BirthDate > DateTime.Now
                || DateTime.Now.Year - user.BirthDate.Year > 150
                || DateTime.Now.Year - user.BirthDate.Year < 5;

            return state;

            bool CheckSymbol(string stringToCheck)
            {
                var allowedSeparatorSymbols = new char[] { '-', '\'', ' ' };

                if (!char.IsLetter(stringToCheck.First())
                    || !char.IsLetter(stringToCheck.Last()))
                {
                    return false;
                }

                for (int i = 1; i < stringToCheck.Length - 1; i++)
                {
                    if (!char.IsLetter(stringToCheck[i]) && !allowedSeparatorSymbols.Contains(stringToCheck[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
