using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    /// <summary>
    ///     Saving Agency Information for schools
    /// </summary>
    public class CScholarshipSiteLocation
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CScholarshipSiteLocation()
        {
        }

        public ScholarshipSiteLocation Get(int id)
        {
            return _db.ScholarshipSiteLocations.FirstOrDefault(q => q.ScholarshipSiteLocationId == id);
        }

        public int Add(ScholarshipSiteLocation obj)
        {
            try
            {
                _db.ScholarshipSiteLocations.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ScholarshipSiteLocations.Max(x => x.ScholarshipSiteLocationId);
        }

        public bool Update(ScholarshipSiteLocation obj)
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

        public bool Delete(ScholarshipSiteLocation obj)
        {
            try
            {
                _db.ScholarshipSiteLocations.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool Delete(IQueryable<ScholarshipSiteLocation> objs)
        {
            try
            {
                _db.ScholarshipSiteLocations.DeleteAllOnSubmit(objs);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<ScholarshipSiteLocation> GetScholarshipSiteLocationList(int scholarshipId)
        {
            return _db.ScholarshipSiteLocations.Where(x => x.ScholarshipId == scholarshipId).ToList();
        }

        public bool DelScholarshipSiteLocation(int scholarshipId)
        {
            return Delete(_db.ScholarshipSiteLocations.Where(x => x.ScholarshipId == scholarshipId));
        }

    }
}