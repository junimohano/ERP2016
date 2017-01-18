using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    /// <summary>
    ///     Saving Agency Information for schools
    /// </summary>
    public class CPackageProgramSiteLocation
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CPackageProgramSiteLocation()
        {
        }

        public PackageProgramSiteLocation Get(int id)
        {
            return _db.PackageProgramSiteLocations.FirstOrDefault(q => q.PackageProgramSiteLocationId == id);
        }

        public int Add(PackageProgramSiteLocation obj)
        {
            try
            {
                _db.PackageProgramSiteLocations.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.PackageProgramSiteLocations.Max(x => x.PackageProgramSiteLocationId);
        }

        public bool Update(PackageProgramSiteLocation obj)
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

        public bool Delete(PackageProgramSiteLocation obj)
        {
            try
            {
                _db.PackageProgramSiteLocations.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool Delete(IQueryable<PackageProgramSiteLocation> objs)
        {
            try
            {
                _db.PackageProgramSiteLocations.DeleteAllOnSubmit(objs);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<PackageProgramSiteLocation> GetPackageProgramSiteLocationList(int packageProgramId)
        {
            return _db.PackageProgramSiteLocations.Where(x => x.PackageProgramId == packageProgramId).ToList();
        }

        public bool DelPackageProgramSiteLocation(int packageProgramId)
        {
            return Delete(_db.PackageProgramSiteLocations.Where(x => x.PackageProgramId == packageProgramId));
        }

    }
}