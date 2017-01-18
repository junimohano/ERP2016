using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CCreditMemoPayoutHistory
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CCreditMemoPayoutHistory()
        {
        }

        public CreditMemoPayoutHistory Get(int id)
        {
            return _db.CreditMemoPayoutHistories.FirstOrDefault(q => q.CreditMemoPayoutHistoryId == id);
        }
        
        public int Add(CreditMemoPayoutHistory obj)
        {
            try
            {
                _db.CreditMemoPayoutHistories.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.CreditMemoPayoutHistories.Max(x => x.CreditMemoPayoutHistoryId);
        }

        public bool Update(CreditMemoPayoutHistory obj)
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

        public bool Delete(CreditMemoPayoutHistory obj)
        {
            try
            {
                _db.CreditMemoPayoutHistories.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public CreditMemoPayoutHistory GetReverseCreditMemoPayoutHistory(int originalCreditMemoPayoutHistory)
        {
            return _db.CreditMemoPayoutHistories.FirstOrDefault(q => q.OriginalCreditMemoPayoutHistoryId == originalCreditMemoPayoutHistory);
        }

    }
}