using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgramClassStudent
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public ProgramClassStudent Get(int programClassStudentId)
        {
            return _db.ProgramClassStudents.FirstOrDefault(x => x.ProgramClassStudentId == programClassStudentId);
        }

        public int Add(ProgramClassStudent obj)
        {
            try
            {
                _db.ProgramClassStudents.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramClassStudents.Max(x => x.ProgramClassStudentId);
        }

        public bool Update(ProgramClassStudent obj)
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

        public bool Delete(ProgramClassStudent obj)
        {
            try
            {
                _db.ProgramClassStudents.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

    }
}