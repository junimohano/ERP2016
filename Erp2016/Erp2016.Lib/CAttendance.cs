using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CAttendance
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public IEnumerable<Attendance> Get(int programClassId, int studentId)
        {
            return _db.Attendances.Where(x => x.ProgramClassId == programClassId && x.StudentId == studentId);
        }

        public Attendance Get(int programClassId, int studentId, DateTime date)
        {
            return _db.Attendances.FirstOrDefault(x => x.ProgramClassId == programClassId && x.StudentId == studentId && x.AttendanceDate == date);
        }

        public int Add(Attendance obj)
        {
            try
            {
                _db.Attendances.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Attendances.Max(x => x.AttendanceId);
        }

        public bool Update(Attendance obj)
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

        public bool Delete(Attendance obj)
        {
            try
            {
                _db.Attendances.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CListModel> GetAttendanceTypeList()
        {
            var result = new List<CListModel>();
            var qry = _db.Dicts.Where(q => q.DictType == 1515).OrderBy(q => q.Value);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.Name, Value = q.Value.ToString() });
            }

            return result;
        }
    }
}