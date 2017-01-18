using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgramCourseLevel
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CProgramCourseLevel()
        {
        }

        public ProgramCourseLevel Get(int id)
        {
            return _db.ProgramCourseLevels.FirstOrDefault(q => q.ProgramCourseLevelId == id);
        }

        public int Add(ProgramCourseLevel obj)
        {
            try
            {
                _db.ProgramCourseLevels.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramCourseLevels.Max(x => x.ProgramCourseLevelId);
        }

        public bool Update(ProgramCourseLevel obj)
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

        public bool Delete(ProgramCourseLevel obj)
        {
            try
            {
                _db.ProgramCourseLevels.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CListModel> GetProgramCourseLevelList(int programCourseId)
        {
            var result = new List<CListModel>();
            var qry = _db.ProgramCourseLevels.Where(q => q.ProgramCourseId == programCourseId).OrderBy(q => q.Level);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.Level, Value = q.ProgramCourseLevelId.ToString() });
            }

            return result;
        }
    }
}