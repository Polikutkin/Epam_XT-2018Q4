using System.Configuration;
using Epam.Task7.BLL;
using Epam.Task7.BLL.Contracts;
using Epam.Task7.DAL.Contracts;

namespace Epam.Task7.Common
{
    public class DependenciesResolver
    {
        private static string key = ConfigurationManager.AppSettings["UserAwardDaoKey"];
        private static string connectonString = ConfigurationManager.ConnectionStrings["UserAward2"].ConnectionString;

        private static IUserLogic userLogic;
        private static IUserDao userDao;

        private static IAwardLogic awardLogic;
        private static IAwardDao awardDao;

        private static ICacheLogic cacheLogic;

        private static IAccountLogic accountLogic;
        private static IAccountDao accountDao;

        public static IUserLogic UserLogic => userLogic ?? (userLogic = new UserLogic(UserDao, CacheLogic));

        public static IAwardLogic AwardLogic => awardLogic ?? (awardLogic = new AwardLogic(AwardDao, CacheLogic));

        public static ICacheLogic CacheLogic => cacheLogic ?? (cacheLogic = new CacheLogic());

        public static IAccountLogic AccountLogic => accountLogic ?? (accountLogic = new AccountLogic(AccountDao, CacheLogic));

        public static IUserDao UserDao
        {
            get
            {
                if (userDao == null)
                {
                    switch (key.ToLower())
                    {
                        case "sql":
                            userDao = new Epam.Task12.DAL.Sql.UserDao(connectonString);
                            break;
                        case "text":
                            userDao = new Epam.Task7.DAL.TextFiles.UserDao();
                            break;
                        default:
                            break;
                    }
                }

                return userDao;
            }
        }

        public static IAwardDao AwardDao
        {
            get
            {
                if (awardDao == null)
                {
                    switch (key.ToLower())
                    {
                        case "sql":
                            awardDao = new Epam.Task12.DAL.Sql.AwardDao(connectonString);
                            break;
                        case "text":
                            awardDao = new Epam.Task7.DAL.TextFiles.AwardDao();
                            break;
                        default:
                            break;
                    }
                }

                return awardDao;
            }
        }

        public static IAccountDao AccountDao
        {
            get
            {
                if (accountDao == null)
                {
                    switch (key.ToLower())
                    {
                        case "sql":
                            accountDao = new Epam.Task12.DAL.Sql.AccountDao(connectonString);
                            break;
                        case "text":
                            accountDao = new Epam.Task7.DAL.TextFiles.AccountDao();
                            break;
                        default:
                            break;
                    }
                }

                return accountDao;
            }
        }
    }
}
