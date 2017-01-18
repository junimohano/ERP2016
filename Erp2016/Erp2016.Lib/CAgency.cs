using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    /// <summary>
    ///     Saving Agency Information for schools
    /// </summary>
    public class CAgency
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CAgency()
        {
        }

        public Agency Get(int id)
        {
            return _db.Agencies.FirstOrDefault(q => q.AgencyId == id);
        }

        public Agency Get(string name)
        {
            return _db.Agencies.FirstOrDefault(q => q.Name == name);
        }

        public int Add(Agency obj)
        {
            try
            {
                var nowYear = DateTime.Now.ToString("yy");

                var last = (from q in _db.Agencies
                            where q.AgencyNumber.Substring(2, 2) == nowYear
                            orderby q.AgencyIndex descending
                            select q).FirstOrDefault();

                if (last == null)
                    obj.AgencyIndex = 1;
                else
                    obj.AgencyIndex = last.AgencyIndex + 1;

                obj.AgencyNumber = "AT" + nowYear + obj.AgencyIndex.ToString("D6");
                
                _db.Agencies.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Agencies.Max(x => x.AgencyId);
        }

        public bool Update(Agency obj)
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

        public bool Delete(Agency obj)
        {
            try
            {
                _db.Agencies.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CListModel> GetAganecyList(int siteid, int? marketerid)
        {
            var result = new List<CListModel>();

            var qry = _db.Agencies.Join(_db.AgencySiteLocations, x => x.AgencyId, y => y.AgencyId, (a, b) => new { a, b }).Where(q => (siteid == 0 || q.b.SiteLocationId == siteid));

            if (marketerid != null)
            {
                qry = qry.Where(q => q.a.CreatedId == Convert.ToInt32(marketerid));
            }

            foreach (var q in qry.OrderBy(q => q.a.Name))
            {
                result.Add(new CListModel { Name = q.a.Name, Value = q.a.AgencyId.ToString() });
            }

            return result;
        }

        public List<CListModel> GetAgencyName()
        {
            var result = new List<CListModel>();

            foreach (var d in _db.Agencies.OrderBy(q => q.ParentAgencyId))
            {
                result.Add(new CListModel { Name = d.Name, Value = d.ParentAgencyId.ToString() });
            }

            return result;
        }

        public List<CListModel> GetAgencyPName()
        {
            var result = new List<CListModel>();

            foreach (var d in _db.Agencies.Where(q => q.ParentAgencyId == null).OrderBy(q => q.ParentAgencyId))
            {
                result.Add(new CListModel { Name = d.Name, Value = d.ParentAgencyId.ToString() });
            }

            return result;
        }

        public List<CFilterListModel> GetAgencyNameList()
        {
            return _db.Agencies.OrderBy(q => q.Name).Select(p => new CFilterListModel { AgencyName = p.Name }).Distinct().ToList();
        }


        public List<CListModel> GetAgencyContactsListByAgencyId(int agencyid)
        {
            var result = new List<CListModel>();
            var qry = _db.Agencies.FirstOrDefault(q => q.AgencyId == agencyid);

            if (qry != null)
                result.Add(new CListModel { Name = GetAgencyName(qry), Value = qry.AgencyId.ToString() });

            return result;
        }

        public List<CListModel> GetAgencyContactsListByAgencyId()
        {
            var result = new List<CListModel>();
            var qry = _db.Agencies.OrderBy(q => q.FirstName);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = GetAgencyName(q), Value = q.AgencyId.ToString() });
            }

            return result;
        }

        public List<Agency> GetAgencyContactsByAgencyId(int agencyid)
        {
            var result = new List<Agency>();

            var qry = _db.Agencies.Where(q => q.AgencyId == agencyid).OrderBy(q => q.FirstName);

            foreach (var q in qry)
            {
                var n = new Agency();
                n.AgencyId = q.AgencyId;
                n.FirstName = q.FirstName;
                n.LastName = q.LastName;
                n.Address = q.Address;
                n.Phone = q.Phone;

                n.Salutation = q.Salutation;
                n.Fax = q.Fax;

                result.Add(n);
            }

            return result;
        }

        public string GetTableNameForVwAgency()
        {
            return CGlobal.GetTableName(_dbView.vwAgencies.ToString());
        }

        public string GetTableNameForVwAgencyApprovalList()
        {
            return CGlobal.GetTableName(_dbView.vwAgencyApprovalLists.ToString());
        }

        public List<CListModel> GetAgency(int siteLocationId)
        {
            var result = new List<CListModel>();

            var agencies = _db.Agencies.Join(_db.AgencySiteLocations, x => x.AgencyId, y => y.AgencyId, (a, b) => new { a, b }).Where(q => q.b.SiteLocationId == siteLocationId && q.a.ApprovalStatus == 99);

            foreach (var d in agencies.OrderBy(q => q.a.Name))
            {
                result.Add(new CListModel { Name = d.a.Name, Value = d.a.AgencyId.ToString() });
            }

            return result;
        }

        public string GetAgencyName(Agency agency)
        {
            if (agency != null)
                return agency.FirstName + " " + agency.LastName;

            return string.Empty;
        }
    }
}