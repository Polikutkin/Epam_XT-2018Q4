using Epam.Task7.BLL.Contracts;
using Epam.Task7.Common;

namespace Epam.Task11.WebPages.CS
{
    public static class BLLProvider
    {
        public static IUserLogic UserLogic { get; } = DependenciesResolver.UserLogic;

        public static IAwardLogic AwardLogic { get; } = DependenciesResolver.AwardLogic;

        public static IAccountLogic AccountLogic { get; } = DependenciesResolver.AccountLogic;
    }
}