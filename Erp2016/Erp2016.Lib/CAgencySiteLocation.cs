using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    /// <summary>
    ///     Saving Agency Information for schools
    /// </summary>
    public class CAgencySiteLocation
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CAgencySiteLocation()
        {
        }

        public AgencySiteLocation Get(int id)
        {
            return _db.AgencySiteLocations.FirstOrDefault(q => q.AgencySiteLocationId == id);
        }

        public int Add(AgencySiteLocation obj)
        {
            try
            {
                _db.AgencySiteLocations.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.AgencySiteLocations.Max(x => x.AgencySiteLocationId);
        }

        public bool Update(AgencySiteLocation obj)
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

        public bool Delete(AgencySiteLocation obj)
        {
            try
            {
                _db.AgencySiteLocations.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool Delete(IQueryable<AgencySiteLocation> objs)
        {
            try
            {
                _db.AgencySiteLocations.DeleteAllOnSubmit(objs);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<AgencySiteLocation> GetAgencySiteLocationList(int agencyId)
        {
            return _db.AgencySiteLocations.Where(x => x.AgencyId == agencyId).ToList();
        }

        public bool DelAgencySiteLocationList(int agencyId)
        {
            return Delete(_db.AgencySiteLocations.Where(x => x.AgencyId == agencyId));
        }

    }
}