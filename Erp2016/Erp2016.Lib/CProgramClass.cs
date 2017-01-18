using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgramClass
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CProgramClass()
        {
        }

        public ProgramClass Get(int id)
        {
            return _db.ProgramClasses.FirstOrDefault(q => q.ProgramClassId == id);
        }

        public int Add(ProgramClass obj)
        {
            try
            {
                _db.ProgramClasses.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramClasses.Max(x => x.ProgramClassId);
        }

        public bool Update(ProgramClass obj)
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

        public bool Delete(ProgramClass obj)
        {
            try
            {
                _db.ProgramClasses.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public int GetInstructorId(int id)
        {
            var qry = _db.ProgramClasses.FirstOrDefault(q => q.ProgramClassId == id);

            if (qry != null)
            {
                return (int)qry.InstructorId;
            }
            return -1;
        }

        // for grade Schema
        public List<CListModel> GetProgramClassList(int programId, int programCourseId, int programCourseLevelId)
        {
            var result = new List<CListModel>();
            var qry = _db.ProgramClasses.Where(q => q.ProgramId == programId);

            if (programCourseId != 0)
                qry = qry.Where(x => x.ProgramCourseId == programCourseId);

            if (programCourseLevelId != 0)
                qry = qry.Where(x => x.ProgramCourseLevelId == programCourseLevelId);

            qry = qry.OrderBy(q => q.Name);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.Name, Value = q.ProgramClassId.ToString() });
            }

            return result;
        }

        // for class movement
        public List<CListModel> GetProgramClassList(int programCourseLevelId, int currentProgramClassId)
        {
            var result = new List<CListModel>();
            var qry = _db.ProgramClasses.Where(q => q.ProgramCourseLevelId == programCourseLevelId && q.ProgramClassId != currentProgramClassId).OrderBy(q => q.Name);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.Name, Value = q.ProgramClassId.ToString() });
            }

            return result;
        }

        public vwProgramClass GetVwProgramClass(int programClassId)
        {
            return _dbView.vwProgramClasses.FirstOrDefault(x => x.ProgramClassId == programClassId);
        }

    }
}