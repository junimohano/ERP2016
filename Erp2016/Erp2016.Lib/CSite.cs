using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CSite
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CSite()
        {
        }

        public Site Get(int id)
        {
            return _db.Sites.FirstOrDefault(q => q.SiteId == id);
        }

        public int Add(Site obj)
        {
            try
            {
                _db.Sites.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Sites.Max(x => x.SiteId);
        }

        public bool Update(Site obj)
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

        public bool Delete(Site obj)
        {
            try
            {
                _db.Sites.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<Site> GetSiteList()
        {
            return _db.Sites.Where(q => q.SiteId > 0).ToList();
        }


        public static Site GetSite(int id)
        {
            var result = (new CSite()).Get(id);

            return result;
        }

        public Site GetSiteId(string name)
        {
            return _db.Sites.FirstOrDefault(q => q.Abbreviation == name);
        }

        public List<CFilterListModel> GetSiteNameList()
        {
            return _db.Sites.OrderBy(q => q.Name).Select(p => new CFilterListModel { SiteName = p.Abbreviation }).Distinct().ToList();
        }
    }
}