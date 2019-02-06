using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.Task7.BLL.Contracts;
using Epam.Task7.Common;
using Epam.Task7.Entities;

namespace Epam.Task11.WebPages.CS
{
    public static class BLLProvider
    {
        public const string UserImageFIlePath = @"~\Content\images\userIcons\";
        public const string AwardImageFIlePath = @"~\Content\images\awardIcons\";

        public static IUserLogic UserLogic { get; } = DependenciesResolver.UserLogic;

        public static IAwardLogic AwardLogic { get; } = DependenciesResolver.AwardLogic;

        public static IAccountLogic AccountLogic { get; } = DependenciesResolver.AccountLogic;
    }
}