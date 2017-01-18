using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CCorporateCreditCard
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CCorporateCreditCard()
        {
        }

        public CCorporateCreditCard(int docNo)
        {
            var query = from b100 in _db.CorporateCreditCards
                        join u100 in _db.Users on b100.CreatedId equals u100.UserId
                        join d100 in _db.SiteLocations on u100.SiteLocationId equals d100.SiteLocationId
                        join s100 in _db.Sites on d100.SiteId equals s100.SiteId

                        where b100.CorporateCreditCardId == docNo
                        select new
                        {
                            b100,
                            u100,
                            s100
                        };

            foreach (var q in query)
            {
                DocNo = q.b100.CorporateCreditCardId;
                Site = q.s100.Name;
                Location = q.s100.City;
                Name = new CUser().GetUserName(q.u100);
                Date = q.b100.CreatedDate.ToString();
                StartDate = q.b100.PeriodStart;
                EndDate = q.b100.PeriodEnd;
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

        public CorporateCreditCard Get(int corporateCreditCardId)
        {
            return _db.CorporateCreditCards.FirstOrDefault(x => x.CorporateCreditCardId == corporateCreditCardId);
        }
        public CorporateCreditCard GetByUserId(int userId)
        {
            return _db.CorporateCreditCards.Where(x => x.ApprovalStatus != (int)CConstValue.ApprovalStatus.Rejected || x.ApprovalStatus != (int)CConstValue.ApprovalStatus.Canceled).OrderByDescending(x => x.CorporateCreditCardId).FirstOrDefault(x => x.CreatedId == userId);
        }

        public CCorporateCreditCard GetNewDocument(int currentUserId)
        {
            var result = _db.Users.Join(_db.SiteLocations, a => a.SiteLocationId, b => b.SiteLocationId, (a, b) => new { a, b }).Join(_db.Sites, a => a.b.SiteId, b => b.SiteId, (a, b) => new { a, b }).FirstOrDefault(x => x.a.a.UserId == currentUserId);
            DocNo = 0;
            Site = result.b.Name;
            Location = result.b.City;
            Name = new CUser().GetUserName(result.a.a);
            Date = DateTime.Now.ToString();
            return this;
        }

        public int Add(CorporateCreditCard obj)
        {
            try
            {
                _db.CorporateCreditCards.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.CorporateCreditCards.Max(x => x.CorporateCreditCardId);
        }

        public bool Update(CorporateCreditCard obj)
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

        public bool Delete(CorporateCreditCard obj)
        {
            try
            {
                _db.CorporateCreditCards.DeleteOnSubmit(obj);
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