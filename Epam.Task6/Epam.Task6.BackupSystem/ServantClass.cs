using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task6.BackupSystem
{
    public static class ServantClass
    {
        public static string CharCollectionToString(this IEnumerable<char> collection)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var c in collection)
            {
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
