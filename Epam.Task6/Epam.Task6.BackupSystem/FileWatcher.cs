using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Epam.Task6.BackupSystem
{
    public partial class FileWatcher
    {
        private readonly string path;
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

        private string DTFormat => "yyyyMMddHHmm";

        private string ChangesPath => this.changesDirectory.FullName;

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

            using (StreamWriter sw = new StreamWriter(this.FileState))
            {
                foreach (var txt in this.OriginalTxtFiles)
                {
                    sw.WriteLine(txt);

                    File.Copy(txt, $"{this.ChangesPath}{this.TimeToMin}_{GetFullFileName(txt)}", true);
                }
            }

            this.watcher.EnableRaisingEvents = true;
        }

        public bool Backup(DateTime backupDateTime)
        {
            var txtFileStates = Directory.EnumerateFileSystemEntries(this.FileStatePath, "*.txt");

            string closestState = txtFileStates.First() ?? string.Empty;

            bool backupDTParse = long.TryParse(backupDateTime.ToString(this.DTFormat), out var backupTime);
            bool closestDTParse = long.TryParse(GetFileFriendlyName(closestState), out var closestTime);

            if (txtFileStates.Count() == 0 || !closestDTParse || backupTime < closestTime)
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

            foreach (var state in txtFileStates)
            {
                bool stateDTParse = long.TryParse(GetFileFriendlyName(state), out var stateTime);

                if (stateTime > closestTime && stateTime <= backupTime)
                {
                    closestState = state;
                    closestTime = stateTime;
                }
            }

            using (StreamReader sr = new StreamReader(closestState))
            {
                while (!sr.EndOfStream)
                {
                    string originalTxt = sr.ReadLine();
                    string originalTxtFullName = GetFullFileName(originalTxt);

                    string txtToCopy = string.Empty;
                    long closestFileTIme = 0L;

                    var backupTxtFiles = Directory.EnumerateFileSystemEntries(this.ChangesPath, "*.txt");

                    foreach (var backupTxt in backupTxtFiles)
                    {
                        if (backupTxt.Contains(originalTxtFullName))
                        {
                            string timePartOfName = GetFileFriendlyName(backupTxt).TakeWhile(c => c != '_').CharCollectionToString();

                            bool needFileDTParse = long.TryParse(timePartOfName, out var needFileTime);

                            if (needFileTime >= closestFileTIme && needFileTime <= backupTime)
                            {
                                txtToCopy = backupTxt;
                                closestFileTIme = needFileTime;
                            }
                        }
                    }

                    if (!Directory.Exists(GetDirectory(originalTxt)))
                    {
                        Directory.CreateDirectory(GetDirectory(originalTxt));
                    }

                    File.Copy(txtToCopy, originalTxt, true);
                }
            }

            return true;
        }

        public void StopWatching()
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            this.Now = DateTime.Now;

            try
            {
                this.watcher.EnableRaisingEvents = false;
            }
            finally
            {
                this.watcher.EnableRaisingEvents = true;
            }

            using (StreamWriter sw = new StreamWriter(this.FileState))
            {
                foreach (var item in this.OriginalTxtFiles)
                {
                    sw.WriteLine(item);
                }
            }

            File.Copy(e.FullPath, $"{this.ChangesPath}{this.TimeToMin}_{GetFullFileName(e.FullPath)}", true);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            this.Now = DateTime.Now;

            using (StreamWriter sw = new StreamWriter(this.FileState))
            {
                foreach (var item in this.OriginalTxtFiles)
                {
                    sw.WriteLine(item);
                }
            }

            File.Copy(e.FullPath, $"{this.ChangesPath}{this.TimeToMin}_{GetFullFileName(e.FullPath)}", true);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            this.Now = DateTime.Now;

            using (StreamWriter sw = new StreamWriter(this.FileState))
            {
                foreach (var item in this.OriginalTxtFiles)
                {
                    sw.WriteLine(item);
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

            using (StreamWriter sw = new StreamWriter(this.FileState))
            {
                foreach (var item in this.OriginalTxtFiles)
                {
                    sw.WriteLine(item);
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
