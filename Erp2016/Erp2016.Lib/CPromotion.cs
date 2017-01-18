using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CPromotion
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CPromotion()
        {

        }

        public Promotion Get(int id)
        {
            return _db.Promotions.FirstOrDefault(q => q.PromotionId == id);
        }

        public Promotion Get(string promotionMasterNo)
        {
            return _db.Promotions.FirstOrDefault(q => q.PromotionMasterNo == promotionMasterNo);
        }

        public int PromotionPrimaryKeyCheck(string promotionMasterNo)
        {
            var qry = _db.Promotions.FirstOrDefault(q => q.PromotionMasterNo == promotionMasterNo);

            if (qry != null)
            {
                return qry.PromotionId;
            }
            return -1;
        }

        public int Add(Promotion obj)
        {
            try
            {
                var nowYear = DateTime.Now.ToString("yy");

                var last = (from q in _db.Promotions
                            where q.PromotionMasterNo.Substring(0, 2) == nowYear
                            orderby q.PromotionIndex descending
                            select q).FirstOrDefault();

                if (last == null)
                    obj.PromotionIndex = 1;
                else
                    obj.PromotionIndex = last.PromotionIndex + 1;

                obj.PromotionMasterNo = nowYear + '-' + new Random().Next(9999).ToString("D4") + '-' + obj.PromotionIndex.ToString("D6");
                obj.CreatedDate = DateTime.Now;

                _db.Promotions.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Promotions.Max(x => x.PromotionId);
        }

        public bool Update(Promotion obj)
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

        public bool Delete(Promotion obj)
        {
            try
            {
                _db.Promotions.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public CPromotionModel GetPromotion(string promotionMasterNo, int siteLocationId)
        {
            var result = _db.Promotions.Join(_db.PromotionSiteLocations, x => x.PromotionId, y => y.PromotionId, (a, b) => new { a, b }).FirstOrDefault(q => q.a.PromotionMasterNo == promotionMasterNo && q.a.ApprovalStatus == 99 && q.a.IsActive && q.b.SiteLocationId == siteLocationId);
            if (result != null)
            {
                return new CPromotionModel()
                {
                    Amount = result.a.Amount,
                    PromotionId = result.a.PromotionId
                };
            }
            return null;
        }

        public string GetTableNameForVwPromotion()
        {
            return CGlobal.GetTableName(_dbView.vwPromotions.ToString());
        }

        public string GetTableNameForVwPromotionApprovalList()
        {
            return CGlobal.GetTableName(_dbView.vwPromotionApprovalLists.ToString());
        }
    }
}