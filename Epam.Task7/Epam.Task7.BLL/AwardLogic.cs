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
        internal const string AwardsCacheKey = "GetAllAwards";

        private readonly IAwardDao awardDao;
        private readonly ICacheLogic cacheLogic;

        public AwardLogic()
        {
        }

        public AwardLogic(IAwardDao awardDao, ICacheLogic cacheLogic)
        {
            this.awardDao = awardDao;
            this.cacheLogic = cacheLogic;
        }

        public void Add(Award award)
        {
            if (award == null)
            {
                throw new ArgumentNullException();
            }

            if (award.Title == null 
                || award.Title.Length < 1 
                || award.Title.Length > 30)
            {
                throw new ArgumentException();
            }

            this.cacheLogic.Remove(AwardsCacheKey);
            this.awardDao.Add(award);
        }

        public IEnumerable<Award> GetAll()
        {
            bool cacheResult = this.cacheLogic.Get(AwardsCacheKey, out IEnumerable<Award> cacheData);

            if (!cacheResult)
            {
                var data = this.awardDao.GetAll().ToList();

                this.cacheLogic.Add(AwardsCacheKey, data);

                return data;
            }

            return cacheData;
        }

        public bool GiveAward(int userId, int awardId)
        {
            if (userId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), UserLogic.IdExceptionMessage);
            }

            if (awardId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(awardId), UserLogic.IdExceptionMessage);
            }

            this.cacheLogic.Remove(UserLogic.UsersCacheKey);

            bool result = this.awardDao.GiveAward(userId, awardId) ? true : false;

            if (result)
            {
                bool cacheResult = this.cacheLogic.Get(UserLogic.LastUserCacheKey, out User cacheData);

                if (cacheResult && cacheData.Id == userId)
                {
                    this.cacheLogic.Remove(UserLogic.LastUserCacheKey);
                }
            }

            return result;
        }

        public bool TakeAward(int userId, int awardId)
        {
            if (userId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), UserLogic.IdExceptionMessage);
            }

            if (awardId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(awardId), UserLogic.IdExceptionMessage);
            }

            this.cacheLogic.Remove(UserLogic.UsersCacheKey);

            bool result = this.awardDao.TakeAward(userId, awardId) ? true : false;

            if (result)
            {
                bool cacheResult = this.cacheLogic.Get(UserLogic.LastUserCacheKey, out User cacheData);

                if (cacheResult && cacheData.Id == userId)
                {
                    this.cacheLogic.Remove(UserLogic.LastUserCacheKey);
                }
            }

            return result;
        }

        public bool Remove(int id)
        {
            this.cacheLogic.Remove(AwardsCacheKey);

            return this.awardDao.Remove(id);
        }
    }
}
