using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CVacation
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CVacation()
        {
        }

        public CVacation(int docNo)
        {
            var query = from v100 in _db.Vacations
                        join u100 in _db.Users on v100.CreatedId equals u100.UserId
                        join d100 in _db.SiteLocations on u100.SiteLocationId equals d100.SiteLocationId
                        join s100 in _db.Sites on d100.SiteId equals s100.SiteId
                        where v100.VacationId == docNo
                        select new
                        {
                            v100,
                            u100,
                            s100
                        };

            foreach (var q in query)
            {
                DocNo = q.v100.VacationId;
                DateOfIssue = q.v100.CreatedDate;
                break;
            }
        }

        public int DocNo { get; set; }
        public DateTime? DateOfIssue { get; set; }

        public Vacation Get(int vacationId)
        {
            return _db.Vacations.FirstOrDefault(x => x.VacationId == vacationId);
        }

        public CVacation GetNewDocument(int currentUserId)
        {
            var result = _db.Users.Join(_db.SiteLocations, a => a.SiteLocationId, b => b.SiteLocationId, (a, b) => new { a, b }).Join(_db.Sites, a => a.b.SiteId, b => b.SiteId, (a, b) => new { a, b })
                .FirstOrDefault(x => x.a.a.UserId == currentUserId);
            DocNo = 0;
            DateOfIssue = DateTime.Now;
            return this;
        }
        
        //public void SetDelete(int vacationId)
        //{
        //    var query = _db.Vacations.FirstOrDefault(x => x.VacationId == vacationId);
        //    if (query != null)
        //    {
        //        _db.Vacations.DeleteOnSubmit(query);

        //        _db.SubmitChanges();
        //    }
        //}
        
        public int Add(Vacation obj)
        {
            try
            {
                _db.Vacations.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Vacations.Max(x => x.VacationId);
        }

        public bool Update(Vacation obj)
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

        public bool Delete(Vacation obj)
        {
            try
            {
                _db.Vacations.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public IQueryable<Vacation> GetDetail(int userId)
        {
            return _db.Vacations.Where(x => x.CreatedId == userId);
        }

        public List<CFilterListModel> GetVacationTypeNameList()
        {
               return _db.Dicts.Where(x => x.DictType == 1376).OrderBy(q => q.Value).Select(p => new CFilterListModel { VacationType = p.Name }).Distinct().ToList();
        }
    }
}