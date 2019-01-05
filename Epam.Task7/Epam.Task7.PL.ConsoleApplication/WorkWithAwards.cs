using System;
using Epam.Task7.BLL.Contracts;
using Epam.Task7.Common;
using Epam.Task7.Entities;

namespace Epam.Task7.PL.ConsoleApplication
{
    public static class WorkWithAwards
    {
        public static readonly IAwardLogic AwardLogic = DependenciesResolver.AwardLogic;

        private static readonly string UserIdMessage = "Enter user ID: ";
        private static readonly string AwardIdMessage = "Enter award ID: ";

        public static void AddAward()
        {
            string awardName = ServantClass.AddAwardTitle("Enter Award title: ");

            try
            {
                AwardLogic.Add(new Award(awardName));

                Console.WriteLine("Award successfully added.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot to add award.");
                Console.WriteLine(e.Message);

                throw;
            }
        }

        public static void GiveToUser()
        {
            int userId = ServantClass.CheckId(UserIdMessage);
            int awardId = ServantClass.CheckId(AwardIdMessage);

            if (AwardLogic.GiveAward(userId, awardId))
            {
                Console.WriteLine("Award successfully given.");
            }
            else
            {
                Console.WriteLine("Cannot to give award.");
            }
        }

        public static void RemoveAward()
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

        public static void ShowAwards()
        {
            foreach (var award in AwardLogic.GetAll())
            {
                Console.WriteLine(award.ToString());
            }
        }

        public static void TakeFromUser()
        {
            int userId = ServantClass.CheckId(UserIdMessage);
            int awardId = ServantClass.CheckId(AwardIdMessage);

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
