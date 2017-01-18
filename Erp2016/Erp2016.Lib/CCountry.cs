using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CCountry
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
    
        public CCountry()
        {
        }

        public Country Get(int id)
        {
            return _db.Countries.FirstOrDefault(q => q.CountryId == id);
        }

        public int Add(Country obj)
        {
            try
            {
                _db.Countries.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Countries.Max(x => x.CountryId);
        }

        public bool Update(Country obj)
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

        public bool Delete(Country obj)
        {
            try
            {
                _db.Countries.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CListModel> GetCountryList()
        {
            var result = new List<CListModel>();

            var qry = _db.Countries.OrderBy(q => q.Name).Where(q => q.RegionId != null);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.Name, Value = q.CountryId.ToString() });
            }

            return result;
        }

        public List<CFilterListModel> GetCountryNameList()
        {
            return _db.Countries.OrderBy(q => q.Name).Select(p => new CFilterListModel { CountryName = p.Name }).Distinct().ToList();
        }
    }
}