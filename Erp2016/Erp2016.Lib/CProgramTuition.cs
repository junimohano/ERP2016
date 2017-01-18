using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgramTuition
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CProgramTuition()
        {
        }

        public ProgramTuition Get(int id)
        {
            return _db.ProgramTuitions.FirstOrDefault(q => q.ProgramTuitionId == id);
        }

        public int Add(ProgramTuition obj)
        {
            try
            {
                _db.ProgramTuitions.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramTuitions.Max(x => x.ProgramTuitionId);
        }

        //public int Add(List<ProgramTuition> objs)
        //{
        //    try
        //    {
        //        db.ProgramTuitions.InsertAllOnSubmit(objs);
        //        db.SubmitChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Print(ex.Message);
        //        return -1;
        //    }
        //    return db.ProgramTuitions.Max(x => x.ProgramTuitionId);
        //}

        public bool Update(ProgramTuition obj)
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

        public bool Delete(ProgramTuition obj)
        {
            try
            {
                _db.ProgramTuitions.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        //public bool Delete(List<ProgramTuition> objs)
        //{
        //    try
        //    {
        //        db.ProgramTuitions.DeleteAllOnSubmit(objs);
        //        db.SubmitChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Print(ex.Message);
        //        return false;
        //    }
        //    return true;
        //}

        //public List<ProgramTuition> GetTuitionsBelongToHoursByWeeks(int siteLocationId, int programId, int countryMarketId, int weeks)
        //{
        //    return db.ProgramTuitions.Where(x => x.SiteLocationId == siteLocationId && x.ProgramId == programId && x.CountryMarketId == countryMarketId).ToList();
        //}

        //public bool DelProgramTuition(int siteLocationId, int programId, int countryMarketId)
        //{
        //    var programTuitionList = db.ProgramTuitions.Where(x => x.SiteLocationId == siteLocationId && x.ProgramId == programId && x.CountryMarketId == countryMarketId).ToList();
        //    return Delete(programTuitionList);
        //}

        //public bool SetProgramTuitionList(List<ProgramTuition> programTuitionList)
        //{
        //    return Add(programTuitionList) > 0;
        //}

        public ProgramTuition GetStandardTuition(int programId, int weeks, int hrs, int countryMarketId)
        {
            return _db.ProgramTuitions.Where(q => q.ProgramId == programId && q.Weeks == weeks && q.HrsStatus == hrs && q.CountryMarketId == countryMarketId).FirstOrDefault();
        }

    }
}