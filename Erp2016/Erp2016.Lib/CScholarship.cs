using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CScholarship
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CScholarship()
        {
        }

        public Scholarship Get(int id)
        {
            return _db.Scholarships.FirstOrDefault(q => q.ScholarshipId == id);
        }

        //public Scholarship Get(string scholarshipMasterNo)
        //{
        //    return db.Scholarships.FirstOrDefault(q => q.ScholarshipMasterNo == scholarshipMasterNo);
        //}

        public int ScholarshipPrimaryKeyCheck(string id)
        {
            var qry = _db.Scholarships.FirstOrDefault(q => q.ScholarshipMasterNo == id);

            if (qry != null)
            {
                return qry.ScholarshipId;
            }
            return -1;
        }

        public int Add(Scholarship obj)
        {
            try
            {
                var nowYear = DateTime.Now.ToString("yy");

                var last = (from q in _db.Scholarships
                            where q.ScholarshipMasterNo.Substring(0, 2) == nowYear
                            orderby q.ScholarshipIndex descending
                            select q).FirstOrDefault();

                if (last == null)
                    obj.ScholarshipIndex = 1;
                else
                    obj.ScholarshipIndex = last.ScholarshipIndex + 1;

                obj.ScholarshipMasterNo = nowYear + '-' + new Random().Next(9999).ToString("D4") + '-' + obj.ScholarshipIndex.ToString("D6");
                obj.CreatedDate = DateTime.Now;

                _db.Scholarships.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Scholarships.Max(x => x.ScholarshipId);
        }

        public bool Update(Scholarship obj)
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

        public bool Delete(Scholarship obj)
        {
            try
            {
                _db.Scholarships.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public double GetAvailableAmountWeek(string id, double tuition, int Numberweek)
        {
            double amt = 0;
            var qry = _dbView.vwScholarships.FirstOrDefault(q => q.ScholarshipMasterNo == id);
            if (qry != null)
            {
                if (qry.Amount != null)
                {
                    amt = Convert.ToDouble(qry.Amount);
                }
                else
                {
                    amt = Convert.ToDouble(qry.Weeks);
                    amt = (tuition / Numberweek) * amt;
                }
            }
            return amt;
        }

        public CScholarshipModel GetScholarship(string scholarshipMasterNo, int siteLocationId, int weeks, int agencyId)
        {
            var result = _db.Scholarships.Join(_db.ScholarshipSiteLocations, x => x.ScholarshipId, y => y.ScholarshipId, (a, b) => new { a, b }).FirstOrDefault(q => q.a.ScholarshipMasterNo.Replace("-", string.Empty) == scholarshipMasterNo && q.a.ApprovalStatus == 99 && q.a.IsActive && q.b.SiteLocationId == siteLocationId && q.a.MininumRegistrationWeeks <= weeks && q.a.AgencyId == agencyId);
            if (result != null)
            {
                return new CScholarshipModel()
                {
                    Amount = result.a.Amount,
                    ScholarshipId = result.a.ScholarshipId
                };
            }
            return null;
        }

        public vwScholarship GetVwScholarship(int scholarshipId)
        {
            return _dbView.vwScholarships.FirstOrDefault(x => x.ScholarshipId == scholarshipId && (x.AvailableAmount > 0 || x.AvailableWeeks > 0));
        }

        public string GetTableNameForVwScholarship()
        {
            return CGlobal.GetTableName(_dbView.vwScholarships.ToString());
        }

        public string GetTableNameForVwScholarshipApprovalList()
        {
            return CGlobal.GetTableName(_dbView.vwScholarshipApprovalLists.ToString());
        }
    }
}