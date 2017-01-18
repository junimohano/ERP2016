using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgram
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CProgram()
        {
        }

        public Program Get(int id)
        {
            return _db.Programs.FirstOrDefault(q => q.ProgramId == id);
        }

        public int Add(Program obj)
        {
            try
            {
                _db.Programs.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Programs.Max(x => x.ProgramId);
        }

        public bool Update(Program obj)
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

        public bool Delete(Program obj)
        {
            try
            {
                _db.Programs.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CListModel> GetProgramList()
        {
            var result = new List<CListModel>();
            var qry = _db.Programs.OrderBy(q => q.ProgramFullName);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.ProgramFullName, Value = q.ProgramId.ToString() });
            }

            return result;
        }

        public List<CListModel> GetProgramList(int siteLocationId)
        {
            var result = new List<CListModel>();
            var qry = _db.Programs.Join(_db.ProgramSiteLocations, x => x.ProgramId, y => y.ProgramId, (a, b) => new { a, b }).Where(q => q.b.SiteLocationId == siteLocationId).OrderBy(q => q.a.ProgramFullName);

            //if (siteLocationId == CConstValue.SiteLocationHq)
            //    qry = db.Programs.OrderBy(q => q.ProgramFullName);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.a.ProgramFullName, Value = q.a.ProgramId.ToString() });
            }

            return result;
        }

        public List<CListModel> GetProgramList(int siteLocationId, int programgrpId)
        {
            var result = new List<CListModel>();
            var qry = _db.Programs.Join(_db.ProgramSiteLocations, x => x.ProgramId, y => y.ProgramId, (a, b) => new { a, b }).Where(q => q.b.SiteLocationId == siteLocationId && q.a.ProgramGroupId == programgrpId).OrderBy(q => q.a.ProgramFullName);

            if (programgrpId == 0)
            {
                qry = _db.Programs.Join(_db.ProgramSiteLocations, x => x.ProgramId, y => y.ProgramId, (a, b) => new { a, b }).Where(q => q.b.SiteLocationId == siteLocationId).OrderBy(q => q.a.ProgramFullName);
            }
            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.a.ProgramFullName, Value = q.a.ProgramId.ToString() });
            }

            return result;
        }

        public List<CListModel> GetProgramListBySiteId(int siteid)
        {
            var result = new List<CListModel>();
            var qry = _db.Programs.Join(_db.ProgramSiteLocations, x => x.ProgramId, y => y.ProgramId, (a, b) => new { a, b }).Join(_db.SiteLocations, a => a.b.SiteLocationId, b => b.SiteLocationId, (a, b) => new { a, b }).Where(q => q.b.SiteId == siteid).OrderBy(q => q.a.a.ProgramFullName);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.a.a.ProgramFullName, Value = q.a.a.ProgramId.ToString() });
            }

            return result;
        }

        //public List<CListModel> GetProgramListBySiteIdAndSiteLocationId(int siteLocationId) 
        //{
        //    var result = new List<CListModel>();
        //    var qry = _db.Programs.Join(_db.ProgramSiteLocations, x => x.ProgramId, y => y.ProgramId, (a, b) => new { a, b }).Where(q => q.b.SiteLocationId == siteLocationId).OrderBy(q => q.a.ProgramFullName);

        //    foreach (var q in qry)
        //    {
        //        result.Add(new CListModel { Name = q.a.ProgramFullName, Value = q.a.ProgramId.ToString() });
        //    }

        //    return result;
        //}

        public List<CListModel> GetProgramWeeksList()
        {
            var result = new List<CListModel>();

            var weeks = 1;
            while (weeks <= 52)
            {
                result.Add(new CListModel { Name = weeks.ToString(), Value = weeks.ToString() });
                weeks++;
            }

            return result;
        }

        public List<CFilterListModel> GetProgramNameList()
        {
            return _db.Programs.OrderBy(q => q.ProgramFullName).Select(p => new CFilterListModel { ProgramName = p.ProgramFullName }).Distinct().ToList();
        }
        public List<CFilterListModel> GetInvoiceNameList()
        {
            return _db.Programs.OrderBy(q => q.ProgramFullName).Select(p => new CFilterListModel { InvoiceName = p.ProgramFullName }).Distinct().ToList();
        }
    }
}