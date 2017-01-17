using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgramOtherInfo
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CProgramOtherInfo()
        {
        }

        public ProgramOtherInfo Get(int id)
        {
            return _db.ProgramOtherInfos.FirstOrDefault(q => q.ProgramId == id);
        }

        public int Add(ProgramOtherInfo obj)
        {
            try
            {
                _db.ProgramOtherInfos.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramOtherInfos.Max(x => x.ProgramOtherInfoId);
        }

        public bool Update(ProgramOtherInfo obj)
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

        public bool Delete(ProgramOtherInfo obj)
        {
            try
            {
                _db.ProgramOtherInfos.DeleteOnSubmit(obj);
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