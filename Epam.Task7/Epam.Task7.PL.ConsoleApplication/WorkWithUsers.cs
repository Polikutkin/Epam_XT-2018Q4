using System;
using Epam.Task7.BLL.Contracts;
using Epam.Task7.Common;
using Epam.Task7.Entities;

namespace Epam.Task7.PL.ConsoleApplication
{
    public static class WorkWithUsers
    {
        public static readonly IUserLogic UserLogic = DependenciesResolver.UserLogic;

        public static void AddUser()
        {
            string firstName = ServantClass.AddUserName("Enter user FirstName: ");
            string lasttName = ServantClass.AddUserName("Enter user LastName: ");
            DateTime birthDate = ServantClass.AddUserBirthDate($"Enter birth date of a user.{Environment.NewLine}Format: year month day (Example: 2018 12 24): ");

            if (UserLogic.Add(new User(firstName, lasttName, birthDate)))
            {
                Console.WriteLine("User successfully added.");
            }
            else
            {
                Console.WriteLine("Cannot to add user.");
            }
        }

        public static void GetUserById()
        {
            int id = ServantClass.CheckId("Enter ID number to get user by ID: ");
            User user = UserLogic.GetById(id);

            if (user != null)
            {
                Console.WriteLine(user.ShowUserInfo());
            }
            else
            {
                Console.WriteLine("There are no users with this ID.");
            }
        }

        public static void RemoveUser()
        {
            int id = ServantClass.CheckId("Enter user ID to remove user: ");
            User user = UserLogic.GetById(id);

            if (user != null)
            {
                Console.WriteLine($"User: {user.ToString()}");

                if (UserLogic.Remove(id))
                {
                    Console.WriteLine("User successfully removed.");
                }
                else
                {
                    Console.WriteLine("Cannot to remove user.");
                }
            }
            else
            {
                Console.WriteLine("There are no users with this ID.");
            }
        }

        public static void ShowUsers()
        {
            Console.WriteLine();
            Console.WriteLine("Users:");

            foreach (var user in UserLogic.GetAll())
            {
                Console.WriteLine(user.ShowUserInfo());
                Console.WriteLine();
            }
        }

        public static void UpdateUser()
        {
            int id = ServantClass.CheckId("Enter user ID to update user data: ");
            User user = UserLogic.GetById(id);

            if (user != null)
            {
                Console.Write("User: ");
                Console.WriteLine(user.ShowUserInfo());
            }
            else
            {
                Console.WriteLine("There are no users with this ID.");
                return;
            }

            Console.WriteLine("Enter new user details:");
            string firstName = ServantClass.AddUserName("Enter user FirstName: ");
            string lastName = ServantClass.AddUserName("Enter user LastName: ");
            DateTime birthDate = ServantClass.AddUserBirthDate($"Enter birth date of a user.{Environment.NewLine}Format: year month day (Example: 2018 12 24): ");

            if (UserLogic.Update(id, new User(firstName, lastName, birthDate)))
            {
                Console.WriteLine("User successfully updated.");
            }
            else
            {
                Console.WriteLine("Cannot to update user.");
            }
        }
    }
}
