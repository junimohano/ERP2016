using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CPurchaseOrder
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CPurchaseOrder()
        {
        }

        public CPurchaseOrder(int docNo)
        {
            var query = from b100 in _db.PurchaseOrders
                        join u100 in _db.Users on b100.CreatedId equals u100.UserId
                        join d100 in _db.SiteLocations on u100.SiteLocationId equals d100.SiteLocationId
                        join s100 in _db.Sites on d100.SiteId equals s100.SiteId

                        where b100.PurchaseOrderId == docNo
                        select new
                        {
                            b100,
                            u100,
                            s100
                        };

            foreach (var q in query)
            {
                DocNo = q.b100.PurchaseOrderId;
                Site = q.s100.Name;
                Location = q.s100.City;
                Name = new CUser().GetUserName(q.u100);
                Date = q.b100.CreatedDate.ToString();
                break;
            }
        }

        public int DocNo { get; set; }
        public string Name { get; set; }
        public string Site { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }

        public PurchaseOrder Get(int purchaseOrderId)
        {
            return _db.PurchaseOrders.FirstOrDefault(x => x.PurchaseOrderId == purchaseOrderId);
        }

        public CPurchaseOrder GetNewDocument(int currentUserId)
        {
            var result = _db.Users.Join(_db.SiteLocations, a => a.SiteLocationId, b => b.SiteLocationId, (a, b) => new { a, b }).Join(_db.Sites, a => a.b.SiteId, b => b.SiteId, (a, b) => new { a, b }).FirstOrDefault(x => x.a.a.UserId == currentUserId);
            DocNo = 0;
            Site = result.b.Name;
            Location = result.b.City;
            Name = new CUser().GetUserName(result.a.a);
            Date = DateTime.Now.ToString();
            return this;
        }

        //public void SetDelete(int purchaseOrderId)
        //{
        //    var query = _db.PurchaseOrders.FirstOrDefault(x => x.PurchaseOrderId == purchaseOrderId);
        //    if (query != null)
        //    {
        //        _db.PurchaseOrders.DeleteOnSubmit(query);

        //        var query1 = _db.PurchaseOrderDetails.FirstOrDefault(x => x.PurchaseOrderId == purchaseOrderId);

        //        if (query1 != null)
        //            _db.PurchaseOrderDetails.DeleteOnSubmit(query1);

        //        _db.SubmitChanges();
        //    }
        //}

        public int Add(PurchaseOrder obj)
        {
            try
            {
                _db.PurchaseOrders.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.PurchaseOrders.Max(x => x.PurchaseOrderId);
        }

        public bool Update(PurchaseOrder obj)
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

        public bool Delete(PurchaseOrder obj)
        {
            try
            {
                _db.PurchaseOrders.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CFilterListModel> GetPurchaseOrderTypeNameList()
        {
            return _db.Dicts.Where(x => x.DictType == 1466).OrderBy(q => q.Value).Select(p => new CFilterListModel { PurchaseOrderTypeName = p.Name }).Distinct().ToList();
        }

        public List<CFilterListModel> GetPriorityTypeNameList()
        {
            return _db.Dicts.Where(x => x.DictType == 1470).OrderBy(q => q.Value).Select(p => new CFilterListModel { PriorityTypeName = p.Name }).Distinct().ToList();
        }

        public List<CFilterListModel> GetReviewTypeNameList()
        {
            return _db.Dicts.Where(x => x.DictType == 1489).OrderBy(q => q.Value).Select(p => new CFilterListModel { ReviewTypeName = p.Name }).Distinct().ToList();
        }
    }
}