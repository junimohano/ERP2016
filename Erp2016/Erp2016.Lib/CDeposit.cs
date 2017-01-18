using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Erp2016.Lib
{
    public class CDeposit
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CDeposit()
        {
        }

        public Deposit Get(int id)
        {
            return _db.Deposits.FirstOrDefault(q => q.DepositId == id);
        }

        public int Add(Deposit obj)
        {
            try
            {
                var nowYear = DateTime.Now.ToString("yy");

                var last = (from q in _db.Deposits
                            where q.DepositNumber.Substring(2, 2) == nowYear
                            orderby q.DepositIndex descending
                            select q).FirstOrDefault();
                
                if (last == null)
                    obj.DepositIndex = 1;
                else
                    obj.DepositIndex = last.DepositIndex + 1;

                obj.DepositNumber = "DL" + nowYear + obj.DepositIndex.ToString("D6");
                obj.CreatedDate = DateTime.Now;

                obj.Status = 1; //Deposit Status(66) :Pending(1)/Created(2)/Confirmed(3)/Confirm Cancelled(0)

                _db.Deposits.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Deposits.Max(x => x.DepositId);
        }

        public bool Update(Deposit obj)
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

        public bool Delete(Deposit obj)
        {
            try
            {
                _db.Deposits.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }


        public List<CFilterListModel> GetDepositBankNameList()
        {
            return _db.Dicts.Where(x => x.DictType == 67).OrderBy(q => q.Value).Select(p => new CFilterListModel { DepositBank = p.Name }).Distinct().ToList();
        }

        public List<CFilterListModel> GetDepositStatusNameList()
        {
            return _db.Dicts.Where(x => x.DictType == 66).OrderBy(q => q.Value).Select(p => new CFilterListModel { DepositStatus = p.Name }).Distinct().ToList();
        }

        public List<CFilterListModel> GetPaidMethodNameList()
        {
            return _db.Dicts.Where(x => x.DictType == 28).OrderBy(q => q.Value).Select(p => new CFilterListModel { PaidMethod = p.Name }).Distinct().ToList();
        }

        public List<CFilterListModel> GetExtraTypeNameList()
        {
            return _db.Dicts.Where(x => x.DictType == 1440).OrderBy(q => q.Value).Select(p => new CFilterListModel { ExtraTypeName = p.Name }).Distinct().ToList();
        }

        public DataTable GetVwDepositExcel(StringBuilder filterExpressionSb)
        {
            var tempDt = _dbView.ExecuteQuery<vwDepositExcel>("SELECT * FROM " + nameof(vwDepositExcel) + (filterExpressionSb.Length == 0 ? string.Empty : " WHERE " + filterExpressionSb));
            return CGlobal.ConvertToDataTable(tempDt);
        }
    }
}