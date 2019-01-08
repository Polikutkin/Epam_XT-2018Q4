using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Epam.Task7.DAL.Contracts;
using Epam.Task7.Entities;

namespace Epam.Task7.DAL.TextFiles
{
    public class UserDao : IUserDao
    {
        internal const string DateFormat = "yyyyMMdd";

        internal static readonly string UsersFilePath;
        internal static readonly string CurrentIdFilePath;

        private const string UsersFile = "users.txt";
        private const string CurrentId = "userId.txt";
        private const char InfoSeparator = '|';

        private int maxId;

        static UserDao()
        {
            UsersFilePath = Path.Combine(UserAwardDao.Folder, UsersFile);
            CurrentIdFilePath = Path.Combine(UserAwardDao.Folder, CurrentId);
        }

        public UserDao()
        {
            if (File.Exists(CurrentIdFilePath))
            {
                bool idParse = int.TryParse(File.ReadAllText(CurrentIdFilePath), out var id);

                this.maxId = id;
            }
        }

        public void Add(User user)
        {
            user.Id = ++this.maxId;

            File.WriteAllText(CurrentIdFilePath, this.maxId.ToString());
            File.AppendAllLines(UsersFilePath, new[] { UserAsTxt(user) });
        }

        public IEnumerable<User> GetAll()
        {
            return UserAwardDao.GetUsers();
        }

        public User GetById(int id)
        {
            return UserAwardDao.GetUserById(id);            
        }

        public bool Remove(int id)
        {
            var users = this.GetAll().ToList();
            User user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            users.Remove(user);
            UserAwardDao.RemoveUserAwards(user.Id);

            var usersAsTxt = users.Select(UserAsTxt);

            File.WriteAllLines(UsersFilePath, usersAsTxt);

            return true;
        }

        public void Update(int id, User user)
        {
            var users = this.GetAll().ToList();
            User userToUpdate = users.FirstOrDefault(u => u.Id == id);

            if (userToUpdate != null)
            {
                int index = users.IndexOf(userToUpdate);

                user.Id = userToUpdate.Id;
                users[index] = user;

                var usersAsTxt = users.Select(UserAsTxt);

                File.WriteAllLines(UsersFilePath, usersAsTxt);
            }
        }

        private static string UserAsTxt(User u)
        {
            return $"{u.Id}{InfoSeparator}{u.FirstName}{InfoSeparator}{u.LastName}{InfoSeparator}{u.BirthDate.ToString(DateFormat)}{InfoSeparator}{u.Age}";
        }
    }
}
