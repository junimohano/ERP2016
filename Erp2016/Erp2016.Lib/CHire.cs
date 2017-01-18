using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CHire
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CHire()
        {
        }

        public CHire(int docNo)
        {
            var query = from b100 in _db.Hires
                        join u100 in _db.Users on b100.CreatedId equals u100.UserId
                        join d100 in _db.SiteLocations on u100.SiteLocationId equals d100.SiteLocationId
                        join s100 in _db.Sites on d100.SiteId equals s100.SiteId
                        where b100.HireId == docNo
                        select new
                        {
                            b100,
                            u100,
                            s100
                        };

            foreach (var q in query)
            {
                DocNo = q.b100.HireId;
                DateOfIssue = q.b100.CreatedDate;
                DraftingDepartment = q.s100.Name;
                ShelfLife = 5;
                break;
            }
        }

        public int DocNo { get; set; }
        public DateTime? DateOfIssue { get; set; }
        public string DraftingDepartment { get; set; }
        public int ShelfLife { get; set; }

        public Hire Get(int hireId)
        {
            return _db.Hires.FirstOrDefault(x => x.HireId == hireId);
        }

        public CHire GetNewDocument(int currentUserId)
        {
            var result = _db.Users.Join(_db.SiteLocations, a => a.SiteLocationId, b => b.SiteLocationId, (a, b) => new { a, b }).Join(_db.Sites, a => a.b.SiteId, b => b.SiteId, (a, b) => new { a, b }).FirstOrDefault(x => x.a.a.UserId == currentUserId);
            DocNo = 0;
            DateOfIssue = DateTime.Now;
            DraftingDepartment = result.b.Name;
            ShelfLife = 5;
            return this;
        }
        
        //public void SetDelete(int hireId)
        //{
        //    var query = _db.Hires.FirstOrDefault(x => x.HireId == hireId);
        //    if (query != null)
        //    {
        //        _db.Hires.DeleteOnSubmit(query);

        //        _db.SubmitChanges();
        //    }
        //}

        public int Add(Hire obj)
        {
            try
            {
                _db.Hires.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Hires.Max(x => x.HireId);
        }

        public bool Update(Hire obj)
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

        public bool Delete(Hire obj)
        {
            try
            {
                _db.Hires.DeleteOnSubmit(obj);
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