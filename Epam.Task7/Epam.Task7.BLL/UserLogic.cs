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

        public bool Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.FirstName == null || user.LastName == null || user.BirthDate == null || user.FirstName.Length < 1 || user.FirstName.Length > 30 ||
                user.LastName.Length < 1 || user.LastName.Length > 30 || user.BirthDate > DateTime.Now || DateTime.Now.Year - user.BirthDate.Year >= 150)
            {
                throw new ArgumentException("Wrong user data");
            }

            try
            {
                this.cacheLogic.Remove(UsersCacheKey);
                this.userDao.Add(user);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<User> GetAll()
        {
            bool cacheResult = this.cacheLogic.Get(UsersCacheKey, out IEnumerable<User> cacheData);

            if (!cacheResult)
            {
                var data = this.userDao.GetAll().ToList();

                this.cacheLogic.Add(UsersCacheKey, data);

                return data;
            }

            return cacheData;
        }

        public User GetById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be above 0.");
            }

            bool cacheResult = this.cacheLogic.Get(LastUserCacheKey, out User cacheData);

            if (cacheResult && cacheData.Id == id)
            {
                return cacheData;
            }
            else
            {
                var data = this.userDao.GetById(id);

                this.cacheLogic.Remove(LastUserCacheKey);
                this.cacheLogic.Add(LastUserCacheKey, data);
                return data;
            }
        }

        public bool Remove(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be above 0.");
            }

            this.cacheLogic.Remove(UsersCacheKey);

            bool cacheResult = this.cacheLogic.Get(LastUserCacheKey, out User cacheData);

            if (cacheResult && cacheData.Id == id)
            {
                this.cacheLogic.Remove(LastUserCacheKey);
            }

            return this.userDao.Remove(id);
        }

        public bool Update(int id, User user)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be above 0.");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.FirstName == null || user.LastName == null || user.BirthDate == null || user.FirstName.Length < 1 || user.FirstName.Length > 30 ||
                user.LastName.Length < 1 || user.LastName.Length > 30 || user.BirthDate > DateTime.Now || DateTime.Now.Year - user.BirthDate.Year >= 150)
            {
                throw new ArgumentException("Wrong user data");
            }

            this.cacheLogic.Remove(UsersCacheKey);

            bool cacheResult = this.cacheLogic.Get(LastUserCacheKey, out User cacheData);

            if (cacheResult && cacheData.Id == id)
            {
                this.cacheLogic.Remove(LastUserCacheKey);
            }

            return this.userDao.Update(id, user);
        }
    }
}
