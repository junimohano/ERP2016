using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgramCourse
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CProgramCourse()
        {
        }

        public ProgramCourse Get(int id)
        {
            return _db.ProgramCourses.FirstOrDefault(q => q.ProgramCourseId == id);
        }

        public int Add(ProgramCourse obj)
        {
            try
            {
                _db.ProgramCourses.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramCourses.Max(x => x.ProgramCourseId);
        }

        public bool Update(ProgramCourse obj)
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

        public bool Delete(ProgramCourse obj)
        {
            try
            {
                _db.ProgramCourses.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CListModel> GetProgramCourseList(int programId)
        {
            var result = new List<CListModel>();
            var qry = _db.ProgramCourses.Where(q => q.ProgramId == programId).OrderBy(q => q.CourseName);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.CourseName, Value = q.ProgramCourseId.ToString() });
            }

            return result;
        }
    }
}