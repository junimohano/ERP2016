using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    /// <summary>
    ///     Saving Agency Information for schools
    /// </summary>
    public class CProgramSiteLocation
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CProgramSiteLocation()
        {
        }

        public ProgramSiteLocation Get(int programId)
        {
            return _db.ProgramSiteLocations.FirstOrDefault(q => q.ProgramId == programId);
        }

        public int Add(ProgramSiteLocation obj)
        {
            try
            {
                _db.ProgramSiteLocations.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramSiteLocations.Max(x => x.ProgramId);
        }

        public bool Update(ProgramSiteLocation obj)
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

        public bool Delete(ProgramSiteLocation obj)
        {
            try
            {
                _db.ProgramSiteLocations.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool Delete(IQueryable<ProgramSiteLocation> objs)
        {
            try
            {
                _db.ProgramSiteLocations.DeleteAllOnSubmit(objs);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<ProgramSiteLocation> GetProgramSiteLocationList(int programId)
        {
            return _db.ProgramSiteLocations.Where(x => x.ProgramId == programId).ToList();
        }

        public bool DelProgramSiteLocationList(int programId)
        {
            return Delete(_db.ProgramSiteLocations.Where(x => x.ProgramId == programId));
        }

    }
}