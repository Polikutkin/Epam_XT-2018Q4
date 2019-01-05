﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Epam.Task7.Entities;

namespace Epam.Task7.DAL.TextFiles
{
    public static class UserAwardDao
    {
        internal static readonly string Folder;
        internal static readonly string UserAwardsFilePath;

        private const string UserAwards = "userAwards.txt";
        private const char InfoSeparator = '|';
        private const char AwardsSeparator = '-';

        static UserAwardDao()
        {
            Folder = AppDomain.CurrentDomain.BaseDirectory;
            UserAwardsFilePath = Path.Combine(Folder, UserAwards);
        }

        internal static IEnumerable<Award> GetAwards()
        {
            if (File.Exists(AwardDao.AwardsFilePath))
            {
                var awards = File.ReadAllLines(AwardDao.AwardsFilePath)
                    .Select(award =>
                    {
                        var awardData = award.Split(new[] { InfoSeparator }, 2);

                        return new Award()
                        {
                            Id = int.Parse(awardData[0]),
                            Title = awardData[1],
                        };
                    });

                return awards;
            }
            else
            {
                return Enumerable.Empty<Award>();
            }
        }

        internal static IEnumerable<User> GetUsers()
        {
            if (File.Exists(UserDao.UsersFilePath))
            {
                IEnumerable<Award> awards = GetAwards();

                var users = File.ReadAllLines(UserDao.UsersFilePath)
                    .Select(user =>
                    {
                        var userData = user.Split(new[] { InfoSeparator }, 5);

                        var newUser = new User
                        {
                            Id = int.Parse(userData[0]),
                            FirstName = userData[1],
                            LastName = userData[2],
                            BirthDate = DateTime.ParseExact(userData[3], UserDao.DateFormat, null),
                        };

                        if (File.Exists(UserAwardsFilePath))
                        {
                            newUser.Awards = GetUserAwards(newUser.Id, awards).ToList();
                        }

                        return newUser;
                    });

                return users;
            }
            else
            {
                return Enumerable.Empty<User>();
            }
        }

        internal static IEnumerable<Award> GetUserAwards(int userId, IEnumerable<Award> awards)
        {
            string userIdTemplate = $"{userId}{InfoSeparator}";

            bool hasUser = false;
            string line = string.Empty;

            using (var reader = new StreamReader(UserAwardsFilePath))
            {
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();

                    if (line.Contains(userIdTemplate))
                    {
                        line = line.Replace(userIdTemplate, string.Empty);
                        hasUser = true;
                        break;
                    }
                }
            }

            if (hasUser)
            {
                return FillUserAwards(awards, line);
            }
            else
            {
                return Enumerable.Empty<Award>();
            }
        }

        internal static void RemoveUserAwards(int userId)
        {
            if (File.Exists(UserAwardsFilePath))
            {
                var userAwards = File.ReadAllLines(UserAwardsFilePath);

                var tempArray = new string[userAwards.Length - 1];

                for (int i = 0; i < userAwards.Length; i++)
                {
                    if (userAwards[i].Contains($"{userId}{InfoSeparator}"))
                    {
                        Array.Copy(userAwards, tempArray, i);
                        Array.Copy(userAwards, i + 1, tempArray, i, userAwards.Length - i - 1);

                        File.WriteAllLines(UserAwardsFilePath, tempArray);
                        break;
                    }
                }
            }
        }

        private static IEnumerable<Award> FillUserAwards(IEnumerable<Award> awards, string awardIdLine)
        {
            var userAwards = new List<Award>();

            string[] stringAwardsId = awardIdLine.Split(AwardsSeparator);

            foreach (var award in awards)
            {
                foreach (var stringId in stringAwardsId)
                {
                    if (stringId == award.Id.ToString())
                    {
                        userAwards.Add(award);
                        break;
                    }
                }
            }

            return userAwards;
        }
    }
}
