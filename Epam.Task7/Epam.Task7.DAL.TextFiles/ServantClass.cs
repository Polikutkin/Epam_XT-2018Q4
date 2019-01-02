using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task7.DAL.TextFiles
{
    public static class ServantClass
    {
        public static string CharCollectionAsString(this IEnumerable<char> collection)
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
