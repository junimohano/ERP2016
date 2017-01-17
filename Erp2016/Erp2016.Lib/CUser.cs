using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Erp2016.Lib
{
    public class CUser
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CUser()
        {
        }

        public User Get(int id)
        {
            return _db.Users.FirstOrDefault(q => q.UserId == id);
        }

        public User Get(string loginId)
        {
            return _db.Users.FirstOrDefault(q => q.LoginId == loginId);
        }

        public int Add(User obj)
        {
            try
            {
                _db.Users.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Users.Max(x => x.UserId);
        }

        public bool Update(User obj)
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

        public bool Delete(User obj)
        {
            try
            {
                _db.Users.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CListModel> GetSupervisorList()
        {
            return _db.Users
                .OrderBy(q => q.FirstName)
                .Select(
                    q =>
                        new CListModel
                        {
                            Name = new CUser().GetUserName(q),
                            Value = q.UserId.ToString()
                        })
                .ToList();
        }

        public List<CListModel> GetMarketerList(int siteid)
        {
            return _db.Users.Join(_db.SiteLocations, a => a.SiteLocationId, b => b.SiteLocationId, (a, b) => new { a, b })
                .Join(_db.UserPositions, x => x.a.UserPositionId, y => y.UserPositionId, (x, y) => new { x, y })
                .Where(x => x.x.b.SiteId == siteid && x.x.a.IsActive &&
                            (x.y.UserGroupId == (int)CConstValue.UserGroupForAdvisor.CAC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForAdvisor.KGIC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForAdvisor.PGIC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForAdvisor.SEC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForAdvisor.UCCBT ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForAdvisor.UIS ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForAdvisor.MTI ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForAdvisor.KGIBC))
                .OrderBy(q => q.x.a.FirstName)
                .Select(
                    q =>
                        new CListModel
                        {
                            Name = new CUser().GetUserName(q.x.a),
                            Value = q.x.a.UserId.ToString()
                        })
                .ToList();
        }

        public string CheckId(string loginId)
        {
            string result = "";

            var qry = _db.Users.FirstOrDefault(q => q.LoginId == loginId);

            if (qry != null)
            {
                result = qry.LoginId;
            }

            return result;
        }

        public string CheckSIN(int sin)
        {
            string result = "";

            var qry = _db.Users.FirstOrDefault(q => q.SINNo == sin);

            if (qry != null)
            {
                result = qry.SINNo.ToString();
            }

            return result;
        }


        public List<vwUserCount> GetUserCount()
        {
            return _dbView.vwUserCounts.ToList();
        }

        public List<CListModel> GetAllUserList()
        {
            return _dbView.vwUsers.Select(x => new CListModel()
            {
                Value = x.UserId.ToString(),
                Name = x.UserName
            }).ToList();
        }

        public List<CListModel> GetInstructorList(int siteLocationId)
        {
            var result = new List<CListModel>();

            // modify
            var qry = _db.Users.Join(_db.UserPositions, x => x.UserPositionId, y => y.UserPositionId, (x, y) => new { x, y })
                .Where(x => x.x.SiteLocationId == siteLocationId && x.x.IsActive &&
                            (x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.CAC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.KGIC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.PGIC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.SEC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.UCCBT ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.UIS ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.MTI ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.KGIBC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.VIA))
                .OrderBy(p => p.x.FirstName)
                .Select(p => p.x);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = GetUserName(q), Value = q.UserId.ToString() });
            }

            return result;
        }

        public List<CFilterListModel> GetInstructorNameList()
        {
            // modify
            return _db.Users.Join(_db.UserPositions, x => x.UserPositionId, y => y.UserPositionId, (x, y) => new { x, y })
                .Where(x => (x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.CAC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.KGIC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.PGIC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.SEC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.UCCBT ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.UIS ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.MTI ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.KGIBC ||
                             x.y.UserGroupId == (int)CConstValue.UserGroupForInstructor.VIA))
                .OrderBy(x => x.x.FirstName).Select(p => new CFilterListModel { InstructorName = new CUser().GetUserName(p.x) }).Distinct().ToList();
        }

        public List<CFilterListModel> GetUserNameList()
        {
            return _db.Users.OrderBy(q => q.FirstName).Select(p => new CFilterListModel { UserName = new CUser().GetUserName(p) }).Distinct().ToList();
        }

        public List<CFilterListModel> GetApprovalUserNameList()
        {
            return _db.Users.OrderBy(q => q.FirstName).Select(p => new CFilterListModel { ApprovalUserName = new CUser().GetUserName(p) }).Distinct().ToList();
        }

        public List<CFilterListModel> GetCreatedUserNameList()
        {
            return _db.Users.OrderBy(q => q.FirstName).Select(p => new CFilterListModel { CreatedUserName = new CUser().GetUserName(p) }).Distinct().ToList();
        }
        public List<CFilterListModel> GetAssignedUserNameList()
        {
            return _db.Users.OrderBy(q => q.FirstName).Select(p => new CFilterListModel { AssignedUserName = new CUser().GetUserName(p) }).Distinct().ToList();
        }
        public List<CFilterListModel> GetUpdatedUserNameList()
        {
            return _db.Users.OrderBy(q => q.FirstName).Select(p => new CFilterListModel { UpdatedUserName = new CUser().GetUserName(p) }).Distinct().ToList();
        }

        public List<CFilterListModel> GetPositionNameList()
        {
            return _dbView.vwUsers.OrderBy(q => q.PositionName).Select(p => new CFilterListModel { PositionName = p.PositionName }).Distinct().ToList();
        }

        public List<CFilterListModel> GetEmailNameList()
        {
            return _db.Users.OrderBy(q => q.Email).Select(p => new CFilterListModel { Email = p.Email }).Distinct().ToList();
        }

        public List<CFilterListModel> GetLoginIdNameList()
        {
            return _db.Users.OrderBy(q => q.LoginId).Select(p => new CFilterListModel { LoginId = p.LoginId }).Distinct().ToList();
        }

        public string GetUserName(User user)
        {
            if (user != null)
                return user.FirstName + " " + user.LastName;

            return string.Empty;
        }

        public bool IsUserPermission(int currentGroupId)
        {
            return currentGroupId == (int)CConstValue.UserGroupForUserPermission.HR ||
                   currentGroupId == (int)CConstValue.UserGroupForUserPermission.IT;
        }

        public bool IsUserInformation(int currentGroupId)
        {
            return currentGroupId == (int)CConstValue.UserGroupForUserInformation.HR ||
                   currentGroupId == (int)CConstValue.UserGroupForUserInformation.IT;
        }

        public List<CListModel> GetUserBySiteLocation(int siteLocationId)
        {
            var result = new List<CListModel>();
            var userList = _db.Users.Where(q => q.SiteLocationId == siteLocationId);

            foreach (var d in userList.OrderBy(q => q.FirstName))
                result.Add(new CListModel { Name = GetUserName(d), Value = d.UserId.ToString() });

            return result;
        }

    }
}