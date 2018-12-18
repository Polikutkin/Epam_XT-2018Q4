using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task6.BackupSystem
{
    public class Program
    {
        private const string Monitoring = "1";
        private const string Backup = "2";

        public static void Main(string[] args)
        {
            Console.WriteLine($"This program allows you to monitor *.txt files in the selected directory{Environment.NewLine}and roll it back.");

            string input;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"To monitor the directory changing enter: \"{Monitoring}\"");
                Console.WriteLine($"To backup changes enter: \"{Backup}\"");

                input = Console.ReadLine();

                if (input == Monitoring || input == Backup)
                {
                    break;
                }

                Console.WriteLine("Enter one of two options.");
                continue;
            }

            FileWatcher txtFileWatcher;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter a full path of the directory:");
                Console.WriteLine($"Example: {Path.GetTempPath()}");

                string path = Console.ReadLine();

                if (!Directory.Exists(path) || Path.GetDirectoryName(path) != path.Take(path.Length - 1).CharCollectionToString())
                {
                    Console.WriteLine("Error. Wrong path sample or directory does not exist.");
                    continue;
                }

                txtFileWatcher = new FileWatcher(Path.GetDirectoryName(path));
                break;
            }

            switch (input)
            {
                case Monitoring:
                    txtFileWatcher.WatchDirectory();

                    Console.WriteLine();
                    Console.WriteLine("Directory monitoring is running...");
                    break;

                case Backup:
                    bool dateTimeParse = false;
                    DateTime backupDateTime;
                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter the date and time you want to backup the directory:");
                        Console.WriteLine("Format: \"year-month-day hour:minute\" (Example: 2018-12-24 18:56)");

                        string dateTime = Console.ReadLine();
                        dateTimeParse = DateTime.TryParseExact(dateTime, "yyyy-MM-dd HH:mm", null, DateTimeStyles.None, out backupDateTime);
                    }
                    while (!dateTimeParse || backupDateTime >= DateTime.Now);

                    bool backupIsReady = txtFileWatcher.Backup(backupDateTime);

                    Console.WriteLine();
                    Console.WriteLine(backupIsReady ? "Backup successfully completed." : "There are no backups for the directory at this date and time.");
                    break;

                default:
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Press Esc to quit the program.");
            ConsoleKeyInfo esc;
            do
            {
                esc = Console.ReadKey();
            }
            while (esc.Key != ConsoleKey.Escape);
        }
    }
}
