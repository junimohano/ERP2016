using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CUserGroup
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CUserGroup()
        {
        }

        public UserGroup Get(int id)
        {
            return _db.UserGroups.FirstOrDefault(q => q.UserGroupId == id);
        }


        public int Add(UserGroup obj)
        {
            try
            {
                _db.UserGroups.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.UserGroups.Max(x => x.UserGroupId);
        }

        public bool Update(UserGroup obj)
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

        public bool Delete(UserGroup obj)
        {
            try
            {
                _db.UserGroups.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CListModel> GetUserGroupList(int siteId)
        {
            return _db.UserGroups.Where(q => q.SiteId == siteId)
                .Select(
                    q =>
                        new CListModel
                        {
                            Name = q.Name,
                            Value = q.UserGroupId.ToString()
                        })
                .ToList();
        }

    }
}