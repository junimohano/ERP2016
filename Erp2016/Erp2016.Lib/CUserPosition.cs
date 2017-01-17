using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CUserPosition
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CUserPosition()
        {
        }

        public UserPosition Get(int id)
        {
            return _db.UserPositions.FirstOrDefault(q => q.UserPositionId == id);
        }


        public int Add(UserPosition obj)
        {
            try
            {
                _db.UserPositions.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.UserPositions.Max(x => x.UserPositionId);
        }

        public bool Update(UserPosition obj)
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

        public bool Delete(UserPosition obj)
        {
            try
            {
                _db.UserPositions.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CListModel> GetUserPositionList(int userGroupId)
        {
            return _db.UserPositions.Where(q => q.UserGroupId == userGroupId)
                .Select(
                    q =>
                        new CListModel
                        {
                            Name = q.Name,
                            Value = q.UserPositionId.ToString()
                        })
                .ToList();
        }

    }
}