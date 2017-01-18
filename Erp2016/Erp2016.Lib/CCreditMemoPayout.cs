using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CCreditMemoPayout
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CCreditMemoPayout()
        {
        }

        public CreditMemoPayout Get(int id)
        {
            return _db.CreditMemoPayouts.FirstOrDefault(q => q.CreditMemoPayoutId == id);
        }

        public int Add(CreditMemoPayout obj)
        {
            try
            {
                _db.CreditMemoPayouts.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.CreditMemoPayouts.Max(x => x.CreditMemoPayoutId);
        }

        public bool Update(CreditMemoPayout obj)
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

        public bool Delete(CreditMemoPayout obj)
        {
            try
            {
                _db.CreditMemoPayouts.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CFilterListModel> GetPayoutMethodNameList()
        {
            return _db.Dicts.Where(x => x.DictType == 1218).OrderBy(q => q.Value).Select(p => new CFilterListModel { PayoutMethodName = p.Name }).Distinct().ToList();
        }

        public string GetTableNameForVwCreditMemoPayout()
        {
            return CGlobal.GetTableName(_dbView.vwCreditMemoPayoutLists.ToString());
        }

        public string GetTableNameForvwCreditMemoPayoutApprovalList()
        {
            return CGlobal.GetTableName(_dbView.vwCreditMemoPayoutApprovalLists.ToString());
        }

        public decimal GetAvailablePayoutAmount(int creditMemoPayoutId)
        {
            var result = _dbView.vwCreditMemoAvailablePayoutAmounts.FirstOrDefault(x => x.CreditMemoPayoutId == creditMemoPayoutId);
            if (result != null)
            {
                return result.TotalAvailablePayoutAmount.Value;
            }
            return 0;
        }

        public List<CreditMemoPayout> GetCreditMemoPayoutList(int creditMemoId)
        {
            return _db.CreditMemoPayouts.Where(x => x.CreditMemoId == creditMemoId).ToList();
        }

    }
}