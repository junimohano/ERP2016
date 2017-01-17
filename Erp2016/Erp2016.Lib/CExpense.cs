using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CExpense
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CExpense()
        {
        }

        public CExpense(int docNo)
        {
            var query = from b100 in _db.Expenses
                        join u100 in _db.Users on b100.CreatedId equals u100.UserId
                        join d100 in _db.SiteLocations on u100.SiteLocationId equals d100.SiteLocationId
                        join s100 in _db.Sites on d100.SiteId equals s100.SiteId

                        where b100.ExpenseId == docNo
                        select new
                        {
                            b100,
                            u100,
                            s100
                        };

            foreach (var q in query)
            {
                DocNo = q.b100.ExpenseId;
                Site = q.s100.Name;
                Location = q.s100.City;
                Name = new CUser().GetUserName(q.u100);
                Date = q.b100.CreatedDate.ToString();
                StartDate = q.b100.PeriodStart;
                EndDate = q.b100.PeriodEnd;
                CashAdvance = q.b100.CashAdvance;
                break;
            }
        }

        public int DocNo { get; set; }
        public string Name { get; set; }
        public string Site { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal CashAdvance { get; set; }

        public Expense Get(int expenseId)
        {
            return _db.Expenses.FirstOrDefault(x => x.ExpenseId == expenseId);
        }

        public CExpense GetNewDocument(int currentUserId)
        {
            var result = _db.Users.Join(_db.SiteLocations, a => a.SiteLocationId, b => b.SiteLocationId, (a, b) => new { a, b }).Join(_db.Sites, a => a.b.SiteId, b => b.SiteId, (a, b) => new { a, b }).FirstOrDefault(x => x.a.a.UserId == currentUserId);
            DocNo = 0;
            Site = result.b.Name;
            Location = result.b.City;
            Name = new CUser().GetUserName(result.a.a);
            Date = DateTime.Now.ToString();
            return this;
        }

        //public void SetDelete(int expenseId)
        //{
        //    var query = _db.Expenses.FirstOrDefault(x => x.ExpenseId == expenseId);
        //    if (query != null)
        //    {
        //        _db.Expenses.DeleteOnSubmit(query);

        //        var query1 = _db.ExpenseDetails.FirstOrDefault(x => x.ExpenseId == expenseId);

        //        if (query1 != null)
        //            _db.ExpenseDetails.DeleteOnSubmit(query1);

        //        _db.SubmitChanges();
        //    }
        //}

        public int Add(Expense obj)
        {
            try
            {
                _db.Expenses.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Expenses.Max(x => x.ExpenseId);
        }

        public bool Update(Expense obj)
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

        public bool Delete(Expense obj)
        {
            try
            {
                _db.Expenses.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public DataTable GetExpenseDetailList(int approvalStatus)
        {
            var query = _dbView.vwExpenseHqDetailDownloadLists
                .Where(w => w.ApprovalStatus >= Convert.ToInt32(approvalStatus))
                .Select(w => w);

            var tempDt = CGlobal.ConvertToDataTable(query);

            return tempDt;
        }
    }
}