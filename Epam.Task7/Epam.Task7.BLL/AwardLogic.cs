using System;
using System.Collections.Generic;
using System.Linq;
using Epam.Task7.BLL.Contracts;
using Epam.Task7.DAL.Contracts;
using Epam.Task7.Entities;

namespace Epam.Task7.BLL
{
    public class AwardLogic : IAwardLogic
    {
        private readonly IAwardDao awardDao;

        public AwardLogic(IAwardDao awardDao)
        {
            this.awardDao = awardDao;
        }

        public bool Add(Award award)
        {
            if (award == null)
            {
                throw new ArgumentNullException();
            }

            if (award.Title == null || award.Title.Length < 1 || award.Title.Length > 30)
            {
                throw new ArgumentException();
            }

            try
            {
                this.awardDao.Add(award);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Award> GetAll()
        {
            return this.awardDao.GetAll().ToList();
        }

        public bool GiveAward(int userId, int awardId)
        {
            if (userId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "ID must be above 0.");
            }

            if (awardId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(awardId), "ID must be above 0.");
            }

            try
            {
                return this.awardDao.GiveAward(userId, awardId) ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TakeAward(int userId, int awardId)
        {
            if (userId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "ID must be above 0.");
            }

            if (awardId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(awardId), "ID must be above 0.");
            }

            try
            {
                return this.awardDao.TakeAward(userId, awardId) ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(int id)
        {
            return this.awardDao.Remove(id);
        }
    }
}
