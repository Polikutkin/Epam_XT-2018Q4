using System;
using System.Linq;

namespace Epam.Task7.PL.ConsoleApplication
{
    public static class ServantClass
    {
        public static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("1 - Show all users");
            Console.WriteLine("2 - Get user by ID");
            Console.WriteLine("3 -Add a new user");
            Console.WriteLine("4 - Update user by ID");
            Console.WriteLine("5 - Remove user by ID");
            Console.WriteLine("6 - Show all awards");
            Console.WriteLine("7 - Add a new award");
            Console.WriteLine("8 - Give an award to a user");
            Console.WriteLine("9 - Take an award from a user");
            Console.WriteLine("q - Quit the program");
            Console.Write("Enter option: ");
        }

        public static string ReadInput()
        {
            return Console.ReadLine();
        }

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

        public static void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
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

                if (!name.Any(c => char.IsLetter(c)))
                {
                    Console.WriteLine("Please enter a valid name. Must contain only letters.");
                    continue;
                }

                return name;
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

                if (now.Year - birthDate.Year > 150 || birthDate > now)
                {
                    Console.WriteLine("Please enter a valid date. User must be 0 - 150 years old.");
                    continue;
                }

                return birthDate;
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
                    Console.WriteLine("Please enter a valid number. Id Must be above 0.");
                    continue;
                }

                return id;
            }
        }
    }
}
