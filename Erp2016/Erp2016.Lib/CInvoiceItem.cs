using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CInvoiceItem
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CInvoiceItem()
        {
        }

        public InvoiceItem Get(int id)
        {
            return _db.InvoiceItems.FirstOrDefault(q => q.InvoiceItemId == id);
        }

        public void RefundItemsUpdate(int originalInvoiceId, int invoiceId, decimal rate, int userId)
        {
            var itemqry = _db.InvoiceItems.Where(q => q.InvoiceId == originalInvoiceId).Join(_db.InvoiceCoaItems, a => a.InvoiceCoaItemId, b => b.InvoiceCoaItemId,
                (a, b) => new
                {
                    a.InvoiceItemId,
                    a.InvoiceCoaItemId,
                    a.StandardPrice,
                    a.StudentPrice,
                    a.AgencyPrice,
                    a.Remark,
                    a.Rank,
                    b.RevenueRecognition
                });

            foreach (var i in itemqry)
            {
                var cInvoiceCoaItem = new CInvoiceCoaItem();
                var invoiceCoaItem = cInvoiceCoaItem.Get(i.InvoiceCoaItemId);

                decimal studentPrice = 0;
                decimal agecyPrice = 0;

                if (invoiceCoaItem.RefundFlag)
                {
                    studentPrice = ((decimal)i.StudentPrice * rate / 100) * -1;
                    agecyPrice = ((decimal)i.AgencyPrice * rate / 100) * -1;
                }

                var newqry = new InvoiceItem();
                newqry.InvoiceId = invoiceId;
                newqry.InvoiceCoaItemId = i.InvoiceCoaItemId;
                newqry.StandardPrice = i.StandardPrice;
                newqry.StudentPrice = studentPrice;
                newqry.AgencyPrice = agecyPrice;
                newqry.Remark = i.Remark;
                newqry.Rank = i.Rank;

                newqry.CreatedId = userId;
                newqry.CreatedDate = DateTime.Now;

                _db.InvoiceItems.InsertOnSubmit(newqry);
                _db.SubmitChanges();
            }
        }

        public int Add(InvoiceItem obj)
        {
            try
            {
                _db.InvoiceItems.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.InvoiceItems.Max(x => x.InvoiceItemId);
        }

        public bool Add(IEnumerable<InvoiceItem> objs)
        {
            try
            {
                _db.InvoiceItems.InsertAllOnSubmit(objs);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(InvoiceItem obj)
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

        public bool Delete(InvoiceItem obj)
        {
            try
            {
                _db.InvoiceItems.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }


        public decimal TotalAmount(int invoiceId)
        {
            decimal totalAmt = 0;

            var qry = _db.InvoiceItems.Where(q => q.InvoiceId == invoiceId).ToList();

            if (qry != null)
            {
                foreach (var a in qry)
                {
                    totalAmt += Convert.ToDecimal(a.AgencyPrice);
                }
            }
            return totalAmt;
        }

        public List<InvoiceItem> GetInvoiceItems(int invoiceId)
        {
            return _db.InvoiceItems.Where(x => x.InvoiceId == invoiceId).OrderBy(x => x.InvoiceCoaItemId).ToList();
        }

        public List<CInvoiceItemModel> GetInvoiceItemModels(int invoiceId)
        {
            return _db.InvoiceItems.Where(x => x.InvoiceId == invoiceId)
                         .Join(_db.InvoiceCoaItems, a => a.InvoiceCoaItemId, b => b.InvoiceCoaItemId,
                         (a, b) => new CInvoiceItemModel()
                         {
                             InvoiceItem = a,
                             InvoiceCoaItem = b
                         }).OrderBy(x => x.InvoiceCoaItem.Rank).ToList();
        }
        
    }
}