using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgramFee
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CProgramFee()
        {
        }

        public ProgramFee Get(int id)
        {
            return _db.ProgramFees.FirstOrDefault(q => q.ProgramFeeId == id);
        }

        public int Add(ProgramFee obj)
        {
            try
            {
                _db.ProgramFees.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramFees.Max(x => x.ProgramFeeId);
        }

        public bool Update(ProgramFee obj)
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

        public bool Delete(ProgramFee obj)
        {
            try
            {
                _db.ProgramFees.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool AddFee(ProgramFee obj, decimal fee)
        {
            obj.Amount = fee;
            obj.ProgramTuitionId = 0;
            obj.IsActive = true;
            return Add(obj) > 0;
        }

        public int GetId(int id)
        {
            var qry = _db.ProgramFees.FirstOrDefault(q => q.ProgramId == id);

            if (qry != null)
            {
                return qry.ProgramFeeId;
            }

            return -1;
        }
    }
}