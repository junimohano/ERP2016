using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    /// <summary>
    ///     Saving Agency Information for schools
    /// </summary>
    public class CPromotionSiteLocation
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CPromotionSiteLocation()
        {
        }

        public PromotionSiteLocation Get(int id)
        {
            return _db.PromotionSiteLocations.FirstOrDefault(q => q.PromotionSiteLocationId == id);
        }

        public int Add(PromotionSiteLocation obj)
        {
            try
            {
                _db.PromotionSiteLocations.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.PromotionSiteLocations.Max(x => x.PromotionSiteLocationId);
        }

        public bool Update(PromotionSiteLocation obj)
        {
            try
            {
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool Delete(PromotionSiteLocation obj)
        {
            try
            {
                _db.PromotionSiteLocations.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool Delete(IQueryable<PromotionSiteLocation> objs)
        {
            try
            {
                _db.PromotionSiteLocations.DeleteAllOnSubmit(objs);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<PromotionSiteLocation> GetPromotionSiteLocationList(int promotionId)
        {
            return _db.PromotionSiteLocations.Where(x => x.PromotionId == promotionId).ToList();
        }

        public bool DelPromotionSiteLocationList(int promotionId)
        {
            return Delete(_db.PromotionSiteLocations.Where(x => x.PromotionId == promotionId));
        }

    }
}