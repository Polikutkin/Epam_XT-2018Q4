using Epam.Task7.BLL;
using Epam.Task7.BLL.Contracts;
using Epam.Task7.DAL.Contracts;
using Epam.Task7.DAL.TextFiles;

namespace Epam.Task7.Common
{
    public class DependenciesResolver
    {
        private static IUserLogic userLogic;
        private static IUserDao userDao;

        private static IAwardLogic awardLogic;
        private static IAwardDao awardDao;

        private static ICacheLogic cacheLogic;

        private static IAccountLogic accountLogic;
        private static IAccountDao accountDao;

        public static IUserLogic UserLogic => userLogic ?? (userLogic = new UserLogic(UserDao, CacheLogic));

        public static IUserDao UserDao => userDao ?? (userDao = new UserDao());

        public static IAwardLogic AwardLogic => awardLogic ?? (awardLogic = new AwardLogic(AwardDao, CacheLogic));
        
        public static IAwardDao AwardDao => awardDao ?? (awardDao = new AwardDao());

        public static ICacheLogic CacheLogic => cacheLogic ?? (cacheLogic = new CacheLogic());

        public static IAccountLogic AccountLogic => accountLogic ?? (accountLogic = new AccountLogic(AccountDao));

        public static IAccountDao AccountDao => accountDao ?? (accountDao = new AccountDao());
    }
}
