using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Epam.Task6.BackupSystem
{
    public partial class FileWatcher
    {
        private static readonly char Separator = Path.DirectorySeparatorChar;
        private static readonly char NameSeparator = '.';
        private static readonly char TimePartSeparator = '_';

        private readonly string path;
        private FileSystemWatcher txtWatcher;
        private FileSystemWatcher directoryWatcher;
        private DirectoryInfo changesDirectory;
        private DirectoryInfo fileStateDirectory;

        private DateTime now;

        public FileWatcher(string path)
        {
            this.path = path;
            this.changesDirectory = Directory.CreateDirectory($@"{Path.GetTempPath()}{GetHashCodeName(path)}{Separator}");
            this.fileStateDirectory = Directory.CreateDirectory($@"{this.ChangesPath}{Separator}FileState{Separator}");
        }

        private FileWatcher()
        {
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

        private string TimeToMin => this.Now.ToString(this.DTFormat);

        private string ChangesPath => this.changesDirectory.FullName;

        private string FileStatePath => this.fileStateDirectory.FullName;

        private string FileState => $@"{this.FileStatePath}{Separator}{this.TimeToMin}.txt";

        private string ChangesPathWithTimePart => $"{this.ChangesPath}{this.TimeToMin}{TimePartSeparator}";

        private IEnumerable<string> OriginalTxtFiles => Directory.EnumerateFileSystemEntries(this.path, "*.txt", SearchOption.AllDirectories);

        public void WatchDirectory()
        {
            this.txtWatcher = new FileSystemWatcher(this.path, "*.txt");
            this.directoryWatcher = new FileSystemWatcher(this.path);

            this.txtWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
            this.txtWatcher.IncludeSubdirectories = true;
            this.txtWatcher.InternalBufferSize *= 8;

            this.directoryWatcher.NotifyFilter = NotifyFilters.DirectoryName;
            this.directoryWatcher.IncludeSubdirectories = true;
            this.directoryWatcher.InternalBufferSize *= 8;

            this.txtWatcher.Changed += this.OnChanged;
            this.txtWatcher.Created += this.OnCreated;
            this.txtWatcher.Deleted += this.OnDeleted;
            this.txtWatcher.Renamed += this.OnRenamed;

            this.directoryWatcher.Renamed += this.OnDirecroryRenamed;

            this.Now = DateTime.Now;

            using (StreamWriter sw = new StreamWriter(this.FileState))
            {
                foreach (var txt in this.OriginalTxtFiles)
                {
                    sw.WriteLine(txt);

                    File.Copy(txt, $"{ChangesPathWithTimePart}{GetHashCodeName(txt)}", true);
                }
            }

            this.txtWatcher.EnableRaisingEvents = true;
            this.directoryWatcher.EnableRaisingEvents = true;
        }

        public bool Backup(DateTime backupTime)
        {
            var txtFileStates = Directory.EnumerateFileSystemEntries(this.FileStatePath, "*.txt");

            string closestState = txtFileStates.First() ?? string.Empty;

            bool closestDTParse = DateTime.TryParseExact(GetFriendlyName(closestState), this.DTFormat, null, DateTimeStyles.None, out var closestTime);

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
                bool stateDTParse = DateTime.TryParseExact(GetFriendlyName(state), this.DTFormat, null, DateTimeStyles.None, out var stateTime);

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
                    string originalTxtFullName = GetHashCodeName(originalTxt);

                    string txtToCopy = string.Empty;
                    DateTime closestTxtTime = default(DateTime);

                    var backupTxtFiles = Directory.EnumerateFileSystemEntries(this.ChangesPath, "*.txt");

                    foreach (var backupTxt in backupTxtFiles)
                    {
                        if (backupTxt.Contains(originalTxtFullName))
                        {
                            string timePartOfName = TimePartOfName(backupTxt);

                            bool needTxtDTParse = DateTime.TryParseExact(timePartOfName, this.DTFormat, null, DateTimeStyles.None, out var needTxtTime);

                            if (needTxtTime >= closestTxtTime && needTxtTime <= backupTime)
                            {
                                txtToCopy = backupTxt;
                                closestTxtTime = needTxtTime;
                            }
                        }
                    }

                    if (!Directory.Exists(GetDirectory(originalTxt)))
                    {
                        Directory.CreateDirectory(GetDirectory(originalTxt));
                    }

                    if (txtToCopy != string.Empty)
                    {
                        File.Copy(txtToCopy, originalTxt, true);
                    }
                }
            }

            return true;
        }

        public void StopWatching()
        {
            if (this.txtWatcher != null)
            {
                this.txtWatcher.EnableRaisingEvents = false;
                this.txtWatcher = null;
            }

            if (this.directoryWatcher != null)
            {
                this.directoryWatcher.EnableRaisingEvents = false;
                this.directoryWatcher = null;
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            this.Now = DateTime.Now;

            try
            {
                this.txtWatcher.EnableRaisingEvents = false;
            }
            finally
            {
                this.txtWatcher.EnableRaisingEvents = true;
            }

            using (StreamWriter sw = new StreamWriter(this.FileState))
            {
                foreach (var item in this.OriginalTxtFiles)
                {
                    sw.WriteLine(item);
                }
            }

            File.Copy(e.FullPath, $"{ChangesPathWithTimePart}{GetHashCodeName(e.FullPath)}", true);
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

            File.Copy(e.FullPath, $"{ChangesPathWithTimePart}{GetHashCodeName(e.FullPath)}", true);
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

            if (File.Exists($"{ChangesPathWithTimePart}{GetHashCodeName(e.FullPath)}"))
            {
                File.Delete($"{ChangesPathWithTimePart}{GetHashCodeName(e.FullPath)}");
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

            if (File.Exists($"{ChangesPathWithTimePart}{GetHashCodeName(e.OldFullPath)}"))
            {
                File.Move($"{ChangesPathWithTimePart}{GetHashCodeName(e.OldFullPath)}", $"{ChangesPathWithTimePart}{GetHashCodeName(e.FullPath)}");
            }
            else
            {
                File.Copy(e.FullPath, $"{ChangesPathWithTimePart}{GetHashCodeName(e.FullPath)}", true);
            }
        }

        private void OnDirecroryRenamed(object sender, RenamedEventArgs e)
        {
            this.Now = DateTime.Now;
            var renamedDirTxt = Directory.EnumerateFileSystemEntries(e.FullPath, "*.txt");
            bool directoryChanged = false;

            if (File.Exists(this.FileState))
            {
                using (StreamReader sr = new StreamReader(this.FileState))
                {
                    while (!sr.EndOfStream)
                    {
                        string state = sr.ReadLine();

                        if (state.Contains(e.OldFullPath))
                        {
                            directoryChanged = true;
                            break;
                        }
                    }
                } 
            }
            else if (renamedDirTxt.Any())
            {
                directoryChanged = true;
            }

            if (directoryChanged)
            {
                using (StreamWriter sw = new StreamWriter(this.FileState))
                {
                    foreach (var item in this.OriginalTxtFiles)
                    {
                        sw.WriteLine(item);
                    }
                }

                foreach (var txt in renamedDirTxt)
                {
                    string oldPathTxt = $"{ChangesPathWithTimePart}{GetHashCodeName($"{e.OldFullPath}{Separator}{GetNameWithFormat(txt)}")}";
                    string renamedTxt = $"{ChangesPathWithTimePart}{GetHashCodeName($"{e.FullPath}{Separator}{GetNameWithFormat(txt)}")}";
                    string originalTxt = $@"{e.FullPath}{Separator}{GetNameWithFormat(txt)}";

                    if (File.Exists(oldPathTxt))
                    {
                        File.Move(oldPathTxt, renamedTxt);
                    }
                    else if (File.Exists(originalTxt))
                    {
                        File.Copy(originalTxt, renamedTxt, true);
                    }
                }
            }
        }
    }
}
