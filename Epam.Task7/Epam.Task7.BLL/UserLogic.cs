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
        private readonly IUserDao userDao;

        public UserLogic(IUserDao userDao)
        {
            this.userDao = userDao;
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
            return this.userDao.GetAll().ToList();
        }

        public User GetById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be above 0.");
            }

            return this.userDao.GetById(id);
        }

        public bool Remove(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be above 0.");
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

            return this.userDao.Update(id, user);
        }
    }
}
