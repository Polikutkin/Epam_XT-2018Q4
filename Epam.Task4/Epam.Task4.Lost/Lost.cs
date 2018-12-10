using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task4.Lost
{
    public class Lost
    {
        public static int LostMethod(int n)
        {
            List<int> list = new List<int>();

            for (int i = 1; i <= n; i++)
            {
                list.Add(i);
            }

            int counter = 1;

            while (list.Count > 1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (counter % 2 == 0)
                    {
                        list.RemoveAt(i--);
                    }

                    counter++;
                }
            }

            return list.First();
        }
    }
}
