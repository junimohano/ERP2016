using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgramRegistration
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CProgramRegistration()
        {
        }

        public ProgramRegistration Get(int id)
        {
            return _db.ProgramRegistrations.FirstOrDefault(q => q.ProgramRegistrationId == id);
        }

        public int Add(ProgramRegistration obj)
        {
            try
            {
                _db.ProgramRegistrations.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramRegistrations.Max(x => x.ProgramRegistrationId);
        }

        public bool Update(ProgramRegistration obj)
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

        public bool Delete(ProgramRegistration obj)
        {
            try
            {
                _db.ProgramRegistrations.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public static DateTime GetEndDate(DateTime startdate, int weeks)
        {
            var enddate = startdate.AddDays(weeks * 7);

            if (enddate.DayOfWeek == DayOfWeek.Monday) enddate = enddate.AddDays(-3);
            if (enddate.DayOfWeek == DayOfWeek.Tuesday) enddate = enddate.AddDays(-4);
            if (enddate.DayOfWeek == DayOfWeek.Wednesday) enddate = enddate.AddDays(2);
            if (enddate.DayOfWeek == DayOfWeek.Thursday) enddate = enddate.AddDays(1);
            if (enddate.DayOfWeek == DayOfWeek.Saturday) enddate = enddate.AddDays(-1);
            if (enddate.DayOfWeek == DayOfWeek.Sunday) enddate = enddate.AddDays(-2);

            //var holiday = new cLoyHoliday();
            //int wks = 0;
            //var nwks = (int)((from h in holiday.GetTerms(startdate, enddate) select (h.EndDate - h.StartDate).TotalDays).Sum() / 7);

            //while (nwks > wks)
            //{
            //    enddate = enddate.AddDays((nwks - wks) * 7);
            //    wks = nwks;
            //    nwks = (int)((from h in holiday.GetTerms(startdate, enddate) select (h.EndDate - h.StartDate).TotalDays).Sum() / 7);
            //}
            //test
            return enddate;
        }

        public int GetProgramregId(int id)
        {
            var qry = _db.ProgramRegistrations.FirstOrDefault(q => q.StudentId == id);

            if (qry != null)
            {
                return qry.ProgramRegistrationId;
            }

            return -1;
        }

        public List<CFilterListModel> GetProgramStatusList()
        {
            return _db.Dicts.Where(x => x.DictType == 1494).OrderBy(q => q.Value).Select(p => new CFilterListModel { ProgramStatusName = p.Name }).Distinct().ToList();
        }
    }
}