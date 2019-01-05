using System;
using Epam.Task7.BLL.Contracts;
using Epam.Task7.Common;
using Epam.Task7.Entities;

namespace Epam.Task7.PL.ConsoleApplication
{
    public static class WorkWithUsers
    {
        public static readonly IUserLogic UserLogic = DependenciesResolver.UserLogic;

        private static readonly string FirstNameMessage = "Enter user FirstName: ";
        private static readonly string LastNameMessage = "Enter user LastName: ";
        private static readonly string BirthDateMessage = $"Enter birth date of user.{Environment.NewLine}Format: year month day (Example: 2018 12 24): ";

        public static void AddUser()
        {
            try
            {
                string firstName = ServantClass.AddUserName(FirstNameMessage);
                string lasttName = ServantClass.AddUserName(LastNameMessage);
                DateTime birthDate = ServantClass.AddUserBirthDate(BirthDateMessage);

                UserLogic.Add(new User(firstName, lasttName, birthDate));

                Console.WriteLine("User successfully added.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot to add user");
                Console.WriteLine(e.Message);

                throw;
            }
        }

        public static void GetUserById()
        {
            int id = ServantClass.CheckId("Enter ID number to get user by ID: ");

            try
            {
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
            catch (Exception e)
            {
                Console.WriteLine("Cannot to get user by ID");
                Console.WriteLine(e.Message);

                throw;
            }
        }

        public static void RemoveUser()
        {
            int id = ServantClass.CheckId("Enter user ID to remove user: ");

            try
            {
                User user = UserLogic.GetById(id);

                if (user != null)
                {
                    Console.WriteLine($"User: {user.ShowUserInfo()}");

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
            catch (Exception e)
            {
                Console.WriteLine("Cannot to remove user.");
                Console.WriteLine(e.Message);

                throw;
            }
        }

        public static void ShowUsers()
        {
            Console.WriteLine($"{Environment.NewLine}Users:");

            foreach (var user in UserLogic.GetAll())
            {
                Console.WriteLine($"{user.ShowUserInfo()}{Environment.NewLine}");
            }
        }

        public static void UpdateUser()
        {
            int id = ServantClass.CheckId("Enter user ID to update user data: ");

            try
            {
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

                string firstName = ServantClass.AddUserName(FirstNameMessage);
                string lastName = ServantClass.AddUserName(LastNameMessage);
                DateTime birthDate = ServantClass.AddUserBirthDate(BirthDateMessage);

                UserLogic.Update(id, new User(firstName, lastName, birthDate));

                Console.WriteLine("User successfully updated.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot to update user.");
                Console.WriteLine(e.Message);

                throw;
            }
        }
    }
}
