﻿using System;
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

                if (!idParse)
                {
                    this.maxId = 0;
                }
                else
                {
                    this.maxId = id;
                }
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
            var users = this.GetAll().ToList();
            User user = users.FirstOrDefault(u => u.Id == id);

            return user;
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

            File.WriteAllLines(UsersFilePath, users.Select(UserAsTxt));

            return true;
        }

        public bool Update(int id, User user)
        {
            var users = this.GetAll().ToList();
            User userToUpdate = users.FirstOrDefault(u => u.Id == id);

            if (userToUpdate == null)
            {
                return false;
            }

            int index = users.IndexOf(userToUpdate);

            user.Id = userToUpdate.Id;
            users[index] = user;

            File.WriteAllLines(UsersFilePath, users.Select(UserAsTxt));

            return true;
        }

        private static string UserAsTxt(User user)
        {
            return $"{user.Id}{InfoSeparator}{user.FirstName}{InfoSeparator}{user.LastName}{InfoSeparator}{user.BirthDate.ToString(DateFormat)}{InfoSeparator}{user.Age}";
        }
    }
}
