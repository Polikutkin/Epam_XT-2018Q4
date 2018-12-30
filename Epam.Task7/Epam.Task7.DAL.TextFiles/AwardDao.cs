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

            string strUserId = $"{userToUpdate.Id}{InfoSeparator}";
            string strAwardId = $"{awardToGive.Id}{AwardsSeparator}";

            Action appendAllText = () => File.AppendAllText(UserAwardsFilePath, $"{strUserId}{strAwardId}{Environment.NewLine}");

            if (File.Exists(UserAwardsFilePath))
            {
                bool hasUser = false;
                bool hasAward = false;
                string line = string.Empty;
                int lineNumber = 0;

                using (var sr = new StreamReader(UserAwardsFilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        lineNumber++;

                        if (line.Contains(strUserId))
                        {
                            hasUser = true;

                            if (line.Replace(strUserId, string.Empty).Contains(strAwardId))
                            {
                                hasAward = true;
                                break;
                            }

                            break;
                        }
                    }
                }

                if (!hasUser)
                {
                    appendAllText();
                }
                else if (hasUser && !hasAward)
                {
                    var userAwards = File.ReadAllLines(UserAwardsFilePath);
                    userAwards[lineNumber - 1] += strAwardId;

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
            Award awardToGive = awards.FirstOrDefault(a => a.Id == awardId);

            if (userToUpdate == null || awardToGive == null)
            {
                return false;
            }

            string strUserId = $"{userToUpdate.Id}{InfoSeparator}";
            string strAwardId = $"{awardToGive.Id}{AwardsSeparator}";

            if (File.Exists(UserAwardsFilePath))
            {
                bool hasUser = false;
                bool hasAward = false;
                string line = string.Empty;
                int lineNumber = 0;

                using (var sr = new StreamReader(UserAwardsFilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        lineNumber++;

                        if (line.Contains(strUserId))
                        {
                            hasUser = true;

                            if (line.Replace(strUserId, string.Empty).Contains(strAwardId))
                            {
                                hasAward = true;
                                break;
                            }

                            break;
                        }
                    }
                }

                if (hasUser && hasAward)
                {
                    var userAwards = File.ReadAllLines(UserAwardsFilePath);
                    userAwards[lineNumber - 1] = userAwards[lineNumber - 1].Replace(strAwardId, string.Empty);

                    File.WriteAllLines(UserAwardsFilePath, userAwards);
                }
            }
            else
            {
                return false;
            }

            return true;
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
    }
}
