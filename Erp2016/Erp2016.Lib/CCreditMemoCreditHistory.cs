using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CCreditMemoCreditHistory
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CCreditMemoCreditHistory()
        {
        }

        public CreditMemoCreditHistory Get(int id)
        {
            return _db.CreditMemoCreditHistories.FirstOrDefault(q => q.CreditMemoCreditHistoryId == id);
        }
        
        public int Add(CreditMemoCreditHistory obj)
        {
            try
            {
                obj.CreatedDate = DateTime.Now;

                _db.CreditMemoCreditHistories.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.CreditMemoCreditHistories.Max(x => x.CreditMemoCreditHistoryId);
        }

        public bool Update(CreditMemoCreditHistory obj)
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

        public bool Delete(CreditMemoCreditHistory obj)
        {
            try
            {
                _db.CreditMemoCreditHistories.DeleteOnSubmit(obj);
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