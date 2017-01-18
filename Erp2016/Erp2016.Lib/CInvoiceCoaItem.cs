using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CInvoiceCoaItem
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CInvoiceCoaItem()
        {
        }

        public InvoiceCoaItem Get(int id)
        {
            return _db.InvoiceCoaItems.FirstOrDefault(q => q.InvoiceCoaItemId == id);
        }

        public InvoiceCoaItem Get(string invoiceCoaItemName)
        {
            return _db.InvoiceCoaItems.FirstOrDefault(q => q.ItemDetail == invoiceCoaItemName);
        }
        public InvoiceCoaItem GetInvoiceCoItembyItemNameDetail(string ItemName, string ItemDetail)
        {
            return _db.InvoiceCoaItems.FirstOrDefault(q => q.ItemName == ItemName && q.ItemDetail == ItemDetail);

            
        }

        public int Add(InvoiceCoaItem obj)
        {
            try
            {
                _db.InvoiceCoaItems.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.InvoiceCoaItems.Max(x => x.InvoiceCoaItemId);
        }

        public bool Update(InvoiceCoaItem obj)
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

        public bool Delete(InvoiceCoaItem obj)
        {
            try
            {
                _db.InvoiceCoaItems.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public string GetCoaItemName(int id)
        {
            string ItemName = null;
            var qry = _db.InvoiceCoaItems.Where(q => q.InvoiceCoaItemId == id).First();

            if (qry != null)
                ItemName = qry.ItemName;

            return ItemName;
        }

        public int GetCoaItemTypeValue(int id)
        {
            var itemType = 0;
            var qry = _db.InvoiceCoaItems.Where(q => q.InvoiceCoaItemId == id).First();

            if (qry != null)
                itemType = qry.ItemType;

            return itemType;
        }

        public List<CFilterListModel> GetInvoiceCoaItemIdNameList()
        {
            return _db.InvoiceCoaItems.OrderBy(q => q.ItemName).Select(p => new CFilterListModel { InvoiceCoaItemId = p.ItemName }).Distinct().ToList();
        }

    }
}