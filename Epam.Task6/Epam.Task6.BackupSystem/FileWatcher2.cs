using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task6.BackupSystem
{
    public partial class FileWatcher
    {
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
            return path.Replace('\\', '.').Replace(":", string.Empty).CharCollectionToString();
        }

        private static string GetDirectory(string path)
        {
            return path.Reverse().SkipWhile(c => c != '\\').Reverse().CharCollectionToString();
        }
    }
}
