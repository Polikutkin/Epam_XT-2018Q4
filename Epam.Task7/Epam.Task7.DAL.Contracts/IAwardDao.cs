using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.Task7.Entities;

namespace Epam.Task7.DAL.Contracts
{
    public interface IAwardDao
    {
        void Add(Award award);

        IEnumerable<Award> GetAll();

        bool Remove(int id);

        bool GiveAward(int userId, int awardId);

        bool TakeAward(int userId, int awardId);
    }
}
