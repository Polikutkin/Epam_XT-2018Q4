using System;
using Epam.Task7.BLL.Contracts;
using Epam.Task7.Common;
using Epam.Task7.Entities;

namespace Epam.Task7.PL.ConsoleApplication
{
    public static class WorkWithAwards
    {
        public static readonly IAwardLogic AwardLogic = DependenciesResolver.AwardLogic;

        internal static void ShowAwards()
        {
            foreach (var award in AwardLogic.GetAll())
            {
                Console.WriteLine(award.ToString());
            }
        }

        internal static void AddAward()
        {
            string awardName = ServantClass.AddAwardTitle("Enter Award title: ");

            if (AwardLogic.Add(new Award(awardName)))
            {
                Console.WriteLine("Award successfully added.");
            }
            else
            {
                Console.WriteLine("Cannot to add award.");
            }
        }

        internal static void RemoveAward()
        {
            int id = ServantClass.CheckId("Enter award ID to remove award: ");

            if (AwardLogic.Remove(id))
            {
                Console.WriteLine("Award successfully removed.");
            }
            else
            {
                Console.WriteLine("Cannot to remove award.");
            }
        }

        internal static void GiveToUser()
        {
            int userId = ServantClass.CheckId("Enter user ID: ");
            int awardId = ServantClass.CheckId("Enter award ID: ");

            if (AwardLogic.GiveAward(userId, awardId))
            {
                Console.WriteLine("Award successfully given.");
            }
            else
            {
                Console.WriteLine("Cannot to give award.");
            }
        }

        internal static void TakeFromUser()
        {
            int userId = ServantClass.CheckId("Enter user ID: ");
            int awardId = ServantClass.CheckId("Enter award ID: ");

            if (AwardLogic.TakeAward(userId, awardId))
            {
                Console.WriteLine("Award successfully taken.");
            }
            else
            {
                Console.WriteLine("Cannot to take award.");
            }
        }
    }
}
