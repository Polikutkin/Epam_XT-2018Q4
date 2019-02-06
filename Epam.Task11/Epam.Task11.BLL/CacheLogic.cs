using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.Task7.BLL.Contracts;

namespace Epam.Task7.BLL
{
    public class CacheLogic : ICacheLogic
    {
        private static Dictionary<string, object> data = new Dictionary<string, object>();

        public CacheLogic()
        {
        }

        public bool Add<T>(string key, T value)
        {
            if (data.ContainsKey(key))
            {
                return false;
            }

            data.Add(key, value);

            return true;
        }

        public bool Get<T>(string key, out T result)
        {
            result = default(T);

            if (!data.ContainsKey(key))
            {
                return false;
            }

            try
            {
                result = (T)data[key];
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Remove(string key)
        {
            return data.Remove(key);
        }
    }
}
