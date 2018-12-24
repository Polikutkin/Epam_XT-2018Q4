using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task6.BackupSystem
{
    public partial class FileWatcher
    {
        private static string GetFriendlyName(string path)
        {
            return path.Reverse().TakeWhile(c => c != Separator).SkipWhile(c => c != '.').Skip(1).Reverse().CharCollectionToString();
        }

        private static string GetNameWithFormat(string path)
        {
            return path.Reverse().TakeWhile(c => c != Separator).Reverse().CharCollectionToString();
        }

        private static string GetHashCodeName(string path)
        {
            return path.GetHashCode() + path.Replace(Separator, NameSeparator).Replace(Path.VolumeSeparatorChar.ToString(), string.Empty).CharCollectionToString();
        }

        private static string GetFullName(string path)
        {
            return path.Replace(Separator, NameSeparator).Replace(Path.VolumeSeparatorChar.ToString(), string.Empty).CharCollectionToString();
        }

        private static string GetDirectory(string path)
        {
            return path.Reverse().SkipWhile(c => c != Separator).Reverse().CharCollectionToString();
        }

        private static string TimePartOfName(string path)
        {
            return GetFriendlyName(path).TakeWhile(c => c != TimePartSeparator).CharCollectionToString();
        }
    }
}
