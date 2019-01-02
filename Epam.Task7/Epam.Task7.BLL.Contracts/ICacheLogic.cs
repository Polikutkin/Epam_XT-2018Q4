using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task7.BLL.Contracts
{
    public interface ICacheLogic
    {
        bool Add<T>(string key, T value);

        bool Get<T>(string key, out T result);

        bool Remove(string key);
    }
}
