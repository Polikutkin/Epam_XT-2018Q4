using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Epam.Task7.DAL.Contracts;
using Epam.Task7.Entities;

namespace Epam.Task7.DAL.TextFiles
{
    public class AwardDao : IAwardDao
    {
        internal static readonly string AwardsFilePath;
        internal static readonly string CurrentIdFilePath;
        internal static readonly string UserAwardsFilePath;

        private const string CurrentId = "awardId.txt";
        private const string AwardsFile = "awards.txt";
        private const char InfoSeparator = '|';
        private const char AwardsSeparator = '-';

        private int maxId;

        static AwardDao()
        {
            AwardsFilePath = Path.Combine(UserAwardDao.Folder, AwardsFile);
            CurrentIdFilePath = Path.Combine(UserAwardDao.Folder, CurrentId);
            UserAwardsFilePath = UserAwardDao.UserAwardsFilePath;
        }

        public AwardDao()
        {
            if (File.Exists(CurrentIdFilePath))
            {
                bool idParse = int.TryParse(File.ReadAllText(CurrentIdFilePath), out var id);

                this.maxId = id;
            }
        }

        public void Add(Award award)
        {
            award.Id = ++this.maxId;

            File.WriteAllText(CurrentIdFilePath, this.maxId.ToString());
            File.AppendAllLines(AwardsFilePath, new[] { AwardAsTxt(award) });
        }

        public IEnumerable<Award> GetAll()
        {
            return UserAwardDao.GetAwards();
        }

        public bool GiveAward(int userId, int awardId)
        {
            var users = UserAwardDao.GetUsers();
            var awards = this.GetAll();

            User userToUpdate = users.FirstOrDefault(u => u.Id == userId);
            Award awardToGive = awards.FirstOrDefault(a => a.Id == awardId);

            if (userToUpdate == null || awardToGive == null)
            {
                return false;
            }

            string userIdTemplate = $"{userToUpdate.Id}{InfoSeparator}";
            string awardIdTemplate = $"{awardToGive.Id}{AwardsSeparator}";

            Action appendAllText = () => File.AppendAllText(UserAwardsFilePath, $"{userIdTemplate}{awardIdTemplate}{Environment.NewLine}");

            if (File.Exists(UserAwardsFilePath))
            {
                bool hasUser = false;
                bool hasAward = false;
                int lineNumber = 0;

                CheckUserAwards(userIdTemplate, awardIdTemplate, ref hasUser, ref hasAward, ref lineNumber);

                if (!hasUser)
                {
                    appendAllText();
                }
                else if (hasUser && !hasAward)
                {
                    var userAwards = File.ReadAllLines(UserAwardsFilePath);
                    userAwards[lineNumber - 1] += awardIdTemplate;

                    File.WriteAllLines(UserAwardsFilePath, userAwards);
                }
            }
            else
            {
                appendAllText();
            }

            return true;
        }
        
        public bool TakeAward(int userId, int awardId)
        {
            var users = UserAwardDao.GetUsers();
            var awards = this.GetAll();

            User userToUpdate = users.FirstOrDefault(u => u.Id == userId);
            Award awardToTake = awards.FirstOrDefault(a => a.Id == awardId);

            if (userToUpdate == null || awardToTake == null)
            {
                return false;
            }

            string userIdTemplate = $"{userToUpdate.Id}{InfoSeparator}";
            string awardIdTemplate = $"{awardToTake.Id}{AwardsSeparator}";

            if (File.Exists(UserAwardsFilePath))
            {
                bool hasUser = false;
                bool hasAward = false;
                int lineNumber = 0;

                CheckUserAwards(userIdTemplate, awardIdTemplate, ref hasUser, ref hasAward, ref lineNumber);

                if (hasUser && hasAward)
                {
                    var userAwards = File.ReadAllLines(UserAwardsFilePath);

                    userAwards[lineNumber - 1] = userAwards[lineNumber - 1]
                        .Replace(awardIdTemplate, string.Empty);

                    File.WriteAllLines(UserAwardsFilePath, userAwards);

                    return true;
                }
            }

            return false;
        }

        public bool Remove(int id)
        {
            var awards = this.GetAll().ToList();
            Award award = awards.FirstOrDefault(a => a.Id == id);

            if (award == null)
            {
                return false;
            }

            awards.Remove(award);

            File.WriteAllLines(AwardsFilePath, awards.Select(AwardAsTxt));

            return true;
        }

        private static string AwardAsTxt(Award award)
        {
            return $"{award.Id}{InfoSeparator}{award.Title}";
        }

        private static void CheckUserAwards(string userIdTemplate, string awardIdTemplate, ref bool hasUser, ref bool hasAward, ref int lineNumber)
        {
            string line = string.Empty;

            using (var reader = new StreamReader(UserAwardsFilePath))
            {
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    lineNumber++;

                    if (line.Contains(userIdTemplate))
                    {
                        hasUser = true;

                        if (line.Replace(userIdTemplate, string.Empty)
                            .Contains(awardIdTemplate))
                        {
                            hasAward = true;
                        }

                        break;
                    }
                }
            }
        }
    }
}
