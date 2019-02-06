using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.Task7.Entities;

namespace Epam.Task7.BLL.Contracts
{
    public interface IAwardLogic
    {
        IEnumerable<Award> GetAll();

        void Add(Award award);

        bool Remove(int id);

        bool GiveAward(int userId, int awardId);

        bool TakeAward(int userId, int awardId);

        void AddImage(int id, byte[] image);

        bool RemoveImage(int id);
    }
}
