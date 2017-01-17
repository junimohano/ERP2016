using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgramOtherFeeInfo
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CProgramOtherFeeInfo()
        {
        }

        public ProgramOtherFeeInfo Get(int id)
        {
            return _db.ProgramOtherFeeInfos.FirstOrDefault(q => q.ProgramId == id);
        }

        public int Add(ProgramOtherFeeInfo obj)
        {
            try
            {
                _db.ProgramOtherFeeInfos.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramOtherFeeInfos.Max(x => x.ProgramOtherFeeId);
        }

        public bool Update(ProgramOtherFeeInfo obj)
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

        public bool Delete(ProgramOtherFeeInfo obj)
        {
            try
            {
                _db.ProgramOtherFeeInfos.DeleteOnSubmit(obj);
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