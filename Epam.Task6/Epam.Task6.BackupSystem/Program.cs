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
        private const string ChooseOption = "3";
        private const string Escape = "q";

        public static void Main(string[] args)
        {
            Console.WriteLine($"This program allows you to watch for *.txt files in the selected directory{Environment.NewLine}and roll it back.");

            FileWatcher txtFileWatcher;
            string mode;
            string path;

            while (true)
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter a full path of the directory:");
                    Console.WriteLine($"Example: {Path.GetTempPath()}");

                    path = Console.ReadLine();

                    if (!Directory.Exists(path))
                    {
                        Console.WriteLine("Error. Wrong path sample or directory does not exist.");
                        continue;
                    }

                    if (path.Last() != Path.DirectorySeparatorChar)
                    {
                        path += new string(Path.DirectorySeparatorChar, 1);
                    }

                    break;
                }

                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine($"To watch for the directory changes enter: \"{Monitoring}\"");
                    Console.WriteLine($"To backup changes enter: \"{Backup}\"");

                    mode = Console.ReadLine();

                    if (mode == Monitoring || mode == Backup)
                    {
                        break;
                    }

                    Console.WriteLine("Enter one of two options.");
                }

                txtFileWatcher = new FileWatcher(path);

                switch (mode)
                {
                    case Monitoring:
                        txtFileWatcher.WatchDirectory();

                        Console.WriteLine();
                        Console.WriteLine("Directory watching is running...");
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
                Console.WriteLine($"Press {ChooseOption} to choose another mode.{Environment.NewLine}Press {Escape} to quit the program.");

                string esc;
                bool choose = false;

                while (true)
                {
                    esc = Console.ReadLine();

                    if (esc == Escape)
                    {
                        choose = false;
                        break;
                    }
                    else if (esc == ChooseOption)
                    {
                        txtFileWatcher.StopWatching();
                        choose = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (!choose)
                {
                    break;
                }
            }
        }
    }
}
