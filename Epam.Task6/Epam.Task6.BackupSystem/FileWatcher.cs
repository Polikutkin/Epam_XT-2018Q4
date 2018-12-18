using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Epam.Task6.BackupSystem
{
    public class FileWatcher
    {
        private readonly string path;
        private string logFileName;
        private FileSystemWatcher watcher;
        private DirectoryInfo changesDirectory;
        private DirectoryInfo fileStateDirectory;

        private DateTime now;

        public FileWatcher() : this(string.Empty)
        {
        }

        public FileWatcher(string path)
        {
            this.path = path;
            this.changesDirectory = Directory.CreateDirectory($@"{Path.GetTempPath()}{GetFullFileName(path)}\");
            this.fileStateDirectory = Directory.CreateDirectory($@"{this.ChangesPath}\FileState\");
        }

        private DateTime Now
        {
            get
            {
                return this.now;
            }

            set
            {
                this.now = value;
            }
        }

        private string ChangesPath => this.changesDirectory.FullName;

        private string DTFormat => "yyyyMMddHHmm";

        private string FileStatePath => this.fileStateDirectory.FullName;

        private string FileState => $@"{this.FileStatePath}\{this.TimeToMin}.txt";

        private string TimeToMin => this.Now.ToString(this.DTFormat);

        private IEnumerable<string> OriginalTxtFiles => Directory.EnumerateFileSystemEntries(this.path, "*.txt", SearchOption.AllDirectories);

        public void WatchDirectory()
        {
            this.watcher = new FileSystemWatcher(this.path, "*.txt");

            this.watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
            this.watcher.IncludeSubdirectories = true;
            this.watcher.InternalBufferSize *= 16;

            this.watcher.Changed += this.OnChanged;
            this.watcher.Created += this.OnCreated;
            this.watcher.Deleted += this.OnDeleted;
            this.watcher.Renamed += this.OnRenamed;

            this.Now = DateTime.Now;
            this.logFileName = $@"{this.ChangesPath}\logfile.txt";

            using (File.Create(this.logFileName))
            {
            }

            using (StreamWriter sw = new StreamWriter(this.FileState))
            {
                foreach (var txt in this.OriginalTxtFiles)
                {
                    sw.WriteLine(txt);

                    File.Copy(txt, $"{this.ChangesPath}{this.TimeToMin}_{GetFileNameWithFormat(txt)}", true);
                }
            }

            this.watcher.EnableRaisingEvents = true;
        }

        public bool Backup(DateTime backupDateTime)
        {
            var states = Directory.EnumerateFileSystemEntries(this.FileStatePath, "*.txt");

            string closestState = states.First() ?? string.Empty;
            bool fileDTParse = DateTime.TryParseExact(GetFileFriendlyName(closestState), this.DTFormat, null, DateTimeStyles.None, out var closestDateTime);

            if (states.Count() == 0 || !fileDTParse || backupDateTime < closestDateTime)
            {
                return false;
            }

            foreach (var txt in this.OriginalTxtFiles)
            {
                if (File.Exists(txt))
                {
                    File.Delete(txt);
                }
            }

            foreach (var state in states)
            {
                bool stateDTParse = DateTime.TryParseExact(GetFileFriendlyName(state), this.DTFormat, null, DateTimeStyles.None, out var stateDateTime);

                if (stateDateTime > closestDateTime && stateDateTime <= backupDateTime)
                {
                    closestState = state;
                    closestDateTime = stateDateTime;
                }
            }

            using (StreamReader sr = new StreamReader($@"{this.FileStatePath}\{GetFileNameWithFormat(closestState)}"))
            {
                while (!sr.EndOfStream)
                {
                    string txtFile = sr.ReadLine();
                    string txtFileName = GetFullFileName(txtFile);

                    string txtToCopy = string.Empty;
                    DateTime closestFileDateTIme = default(DateTime);

                    var backupTxtFiles = Directory.EnumerateFileSystemEntries(this.ChangesPath, "*.txt");

                    foreach (var txt in backupTxtFiles)
                    {
                        if (txt.Contains(txtFileName))
                        {
                            string timePartOfName = GetFileFriendlyName(txt).TakeWhile(c => c != '_').CharCollectionToString();

                            bool needFileDTParse = DateTime.TryParseExact(timePartOfName, this.DTFormat, null, DateTimeStyles.None, out var backupFileDateTime);

                            if (backupFileDateTime >= closestFileDateTIme && backupFileDateTime <= backupDateTime)
                            {
                                txtToCopy = txt;
                                closestFileDateTIme = backupFileDateTime;
                            }
                        }
                    }

                    if (!Directory.Exists(GetDirectory(txtFile)))
                    {
                        Directory.CreateDirectory(GetDirectory(txtFile));
                    }

                    File.Copy(txtToCopy, txtFile, true);
                }
            }

            return true;
        }

        private static string GetFileFriendlyName(string path)
        {
            return path.Reverse().TakeWhile(c => c != '\\').SkipWhile(c => c != '.').Skip(1).Reverse().CharCollectionToString();
        }

        private static string GetFileNameWithFormat(string path)
        {
            return path.Reverse().TakeWhile(c => c != '\\').Reverse().CharCollectionToString();
        }

        private static string GetFullFileName(string path)
        {
            return path.Replace('\\', '.').SkipWhile(c => c != '.').Skip(1).CharCollectionToString();
        }

        private static string GetDirectory(string path)
        {
            return path.Reverse().SkipWhile(c => c != '\\').Reverse().CharCollectionToString();
        }

        private static string GetLogDateTime(string logNote)
        {
            return logNote.TakeWhile(c => c != ' ').CharCollectionToString();
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            this.Now = DateTime.Now;
            string logInfo = $"{this.TimeToMin} {e.FullPath} {e.ChangeType}";
            bool alreadyHappened = false;
            bool firstInMinute = true;

            try
            {
                this.watcher.EnableRaisingEvents = false;
            }
            finally
            {
                this.watcher.EnableRaisingEvents = true;
            }

            using (StreamReader sr = new StreamReader(this.logFileName))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();

                    if (str == logInfo)
                    {
                        alreadyHappened = true;
                    }

                    if (GetLogDateTime(str) == GetLogDateTime(logInfo))
                    {
                        firstInMinute = false;
                    }
                }
            }

            if (firstInMinute)
            {
                using (StreamWriter sw = new StreamWriter(this.FileState))
                {
                    foreach (var item in this.OriginalTxtFiles)
                    {
                        sw.WriteLine(item);
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(this.logFileName, true))
            {
                if (!alreadyHappened)
                {
                    sw.WriteLine(logInfo);
                }
            }

            File.Copy(e.FullPath, $"{this.ChangesPath}{this.TimeToMin}_{GetFullFileName(e.FullPath)}", true);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            this.Now = DateTime.Now;
            string logInfo = $"{this.TimeToMin} {e.FullPath} {e.ChangeType}";
            bool alreadyHappened = false;

            using (StreamWriter sw = new StreamWriter(this.FileState))
            {
                foreach (var item in this.OriginalTxtFiles)
                {
                    sw.WriteLine(item);
                }
            }

            using (StreamReader sr = new StreamReader(this.logFileName))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();

                    if (str == logInfo)
                    {
                        alreadyHappened = true;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(this.logFileName, true))
            {
                if (!alreadyHappened)
                {
                    sw.WriteLine(logInfo);
                }
            }

            File.Copy(e.FullPath, $"{this.ChangesPath}{this.TimeToMin}_{GetFullFileName(e.FullPath)}", true);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            this.Now = DateTime.Now;
            string logInfo = $"{this.TimeToMin} {e.FullPath} {e.ChangeType}";
            bool alreadyHappened = false;

            using (StreamWriter sw = new StreamWriter(this.FileState))
            {
                foreach (var item in this.OriginalTxtFiles)
                {
                    sw.WriteLine(item);
                }
            }

            using (StreamReader sr = new StreamReader(this.logFileName))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();

                    if (str == logInfo)
                    {
                        alreadyHappened = true;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(this.logFileName, true))
            {
                if (!alreadyHappened)
                {
                    sw.WriteLine(logInfo);
                }
            }

            if (File.Exists($"{this.ChangesPath}{this.TimeToMin}_{GetFullFileName(e.FullPath)}"))
            {
                File.Delete($"{this.ChangesPath}{this.TimeToMin}_{GetFullFileName(e.FullPath)}");
            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            this.Now = DateTime.Now;
            string logInfo = $"{this.TimeToMin} {e.OldFullPath} --> {e.FullPath} {e.ChangeType}";
            bool alreadyHappened = false;

            using (StreamWriter sw = new StreamWriter(this.FileState))
            {
                foreach (var item in this.OriginalTxtFiles)
                {
                    sw.WriteLine(item);
                }
            }

            using (StreamReader sr = new StreamReader(this.logFileName))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();

                    if (str == logInfo)
                    {
                        alreadyHappened = true;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(this.logFileName, true))
            {
                if (!alreadyHappened)
                {
                    sw.WriteLine(logInfo);
                }
            }

            if (File.Exists($"{this.ChangesPath}{this.TimeToMin}_{GetFullFileName(e.OldFullPath)}"))
            {
                File.Move($"{this.ChangesPath}{this.TimeToMin}_{GetFullFileName(e.OldFullPath)}", $"{this.ChangesPath}{this.TimeToMin}_{GetFullFileName(e.FullPath)}");
            }
            else
            {
                File.Copy(e.FullPath, $"{this.ChangesPath}{this.TimeToMin}_{GetFullFileName(e.FullPath)}", true);
            }
        }
    }
}
