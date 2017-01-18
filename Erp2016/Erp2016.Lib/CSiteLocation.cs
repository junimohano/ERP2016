using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CSiteLocation
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CSiteLocation()
        {
        }

        public SiteLocation Get(int id)
        {
            return _db.SiteLocations.FirstOrDefault(q => q.SiteLocationId == id);
        }
        public List<SiteLocation> GetAll()
        {
            return _db.SiteLocations.ToList();
        }
        public List<SiteLocation> GetAll(int siteId)
        {
            return _db.SiteLocations.Where(x => x.SiteId == siteId).ToList();
        }

        public static SiteLocation GetSiteLocation(int id)
        {
            var result = (new CSiteLocation()).Get(id);

            return result;
        }

        public int Add(SiteLocation obj)
        {
            try
            {
                _db.SiteLocations.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.SiteLocations.Max(x => x.SiteLocationId);
        }

        public bool Update(SiteLocation obj)
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

        public bool Delete(SiteLocation obj)
        {
            try
            {
                _db.SiteLocations.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<SiteLocation> GetSiteLocationBySiteId(int siteid)
        {
            var qry = _db.SiteLocations.Where(q => siteid == 0 || q.SiteId == siteid);

            var resultList = new List<SiteLocation>();

            foreach (SiteLocation d in qry)
            {
                resultList.Add(new SiteLocation()
                {
                    SiteLocationId = d.SiteLocationId,
                    Name = d.Name,
                    Abbreviation = d.Abbreviation,
                    BizUnit = d.BizUnit,
                    Phone1 = d.Phone1,
                    Email = d.Email
                });
            }

            return resultList;
        }

        public List<CFilterListModel> GetSiteLocationNameList()
        {
            return _db.SiteLocations.OrderBy(q => q.Name).Select(p => new CFilterListModel { SiteLocationName = p.Name }).Distinct().ToList();
        }

        public vwSiteLocationList GetSiteLocationList(int siteLocationId)
        {
            return _dbView.vwSiteLocationLists.FirstOrDefault(x=>x.SiteLocationId == siteLocationId);
        }

    }
}