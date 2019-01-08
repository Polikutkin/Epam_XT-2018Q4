using System;
using System.Linq;

namespace Epam.Task7.PL.ConsoleApplication
{
    public static class ServantClass
    {
        public const string ShowUsers = "1";
        public const string GetUser = "2";
        public const string AddUser = "3";
        public const string UpdateUser = "4";
        public const string RemoveUser = "5";
        public const string ShowAwards = "6";
        public const string AddAward = "7";
        public const string RemoveAward = "8";
        public const string GiveAward = "9";
        public const string TakeAward = "0";
        public const string Quit = "q";

        public static string AddAwardTitle(string message)
        {
            while (true)
            {
                Console.Write(message);
                string title = Console.ReadLine();

                title = title.Trim();

                if (title.Length < 1 || title.Length > 30)
                {
                    Console.WriteLine("Please enter a valid title name. Must contain 1 - 30 symbols.");
                    continue;
                }

                if (!title.Any(c => char.IsLetterOrDigit(c) || char.IsSeparator(c)))
                {
                    Console.WriteLine("Please enter a valid title name. Must contain only letters, digits and separator symbols.");
                    continue;
                }

                return title;
            }
        }

        public static DateTime AddUserBirthDate(string message)
        {
            while (true)
            {
                Console.Write(message);
                string date = Console.ReadLine();

                bool birthDateParse = DateTime.TryParseExact(date, "yyyy MM dd", null, System.Globalization.DateTimeStyles.None, out var birthDate);
                DateTime now = DateTime.Now;

                if (!birthDateParse)
                {
                    Console.WriteLine("Please enter a valid date.");
                    continue;
                }

                if (now.Year - birthDate.Year > 150
                    || now.Year - birthDate.Year < 5
                    || birthDate > now)
                {
                    Console.WriteLine("Please enter a valid date. User must be 5 - 150 years old.");
                    continue;
                }

                return birthDate;
            }
        }

        public static string AddUserName(string message)
        {
            while (true)
            {
                Console.Write(message);
                string name = Console.ReadLine();

                name = name.Trim();

                if (name.Length < 1 || name.Length > 30)
                {
                    Console.WriteLine("Please enter a valid name. Must contain 1 - 30 symbols.");
                    continue;
                }

                if (!CheckSymbol(name))
                {
                    Console.WriteLine("Please enter a valid name. Must contain only letters.");
                    continue;
                }

                return name;
            }
        }

        public static int CheckId(string message)
        {
            while (true)
            {
                Console.Write(message);
                string inputId = Console.ReadLine();

                bool idParse = int.TryParse(inputId, out var id);

                if (!idParse || id < 1)
                {
                    Console.WriteLine("Please enter a valid number. Id must be above 0.");
                    continue;
                }

                return id;
            }
        }

        public static void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public static string ReadInput()
        {
            return Console.ReadLine();
        }

        public static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine($"{ShowUsers} - Show all users");
            Console.WriteLine($"{GetUser} - Get user by ID");
            Console.WriteLine($"{AddUser} - Add a new user");
            Console.WriteLine($"{UpdateUser} - Update user by ID");
            Console.WriteLine($"{RemoveUser} - Remove user by ID");
            Console.WriteLine($"{ShowAwards} - Show all awards");
            Console.WriteLine($"{AddAward} - Add a new award");
            Console.WriteLine($"{RemoveAward} - Remove award");
            Console.WriteLine($"{GiveAward} - Give an award to a user");
            Console.WriteLine($"{TakeAward} - Take an award from a user");
            Console.WriteLine($"{Quit} - Quit the program");
            Console.Write("Enter option: ");
        }

        private static bool CheckSymbol(string stringToCheck)
        {
            var allowedSeparatorSymbols = new char[] { '-', '\'', ' ' };

            if (!char.IsLetter(stringToCheck.First())
                || !char.IsLetter(stringToCheck.Last()))
            {
                return false;
            }

            for (int i = 1; i < stringToCheck.Length - 1; i++)
            {
                if (!char.IsLetter(stringToCheck[i]) && !allowedSeparatorSymbols.Contains(stringToCheck[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
