using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgramClassRoom
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CProgramClassRoom()
        {
        }

        public ProgramClassRoom Get(int id)
        {
            return _db.ProgramClassRooms.FirstOrDefault(q => q.ProgramClassRoomId == id);
        }
        public int Add(ProgramClassRoom obj)
        {
            try
            {
                _db.ProgramClassRooms.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramClassRooms.Max(x => x.ProgramClassRoomId);
        }

        public bool Update(ProgramClassRoom obj)
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

        public bool Delete(ProgramClassRoom obj)
        {
            try
            {
                _db.ProgramClassRooms.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CListModel> GetClassRoomListBySiteId(int siteid)
        {
            var qry = _db.ProgramClassRooms.Join(_db.SiteLocations, a => a.SiteLocationId, b => b.SiteLocationId, (a, b) => new { a, b }).Where(q => q.b.SiteId == siteid);

            return qry.Select(q => new CListModel { Name = q.a.Name, Value = q.a.ProgramClassRoomId.ToString() }).ToList();
        }

        public List<CListModel> GetClassRoomListBySiteLocationId(int siteLocationId)
        {
            var qry = _db.ProgramClassRooms.Where(q => q.SiteLocationId == siteLocationId);

            return qry.Select(q => new CListModel { Name = q.Name, Value = q.ProgramClassRoomId.ToString() }).ToList();
        }

        public List<CListModel> GetClassRoomItemList(int programClassRoomId)
        {
            var qry = new List<CListModel>();
            var itemList = _db.ProgramClassRoomItems;
            var result = _dbView.vwProgramClassRoomDetails.Where(x => x.ProgramClassRoomId == programClassRoomId);

            foreach (var i in itemList)
            {
                var resultDetail = result.FirstOrDefault(x => x.ProgramClassRoomItemId == i.ProgramClassRoomItemId);
                qry.Add(new CListModel()
                {
                    Name = i.ItemName,
                    Value = i.ProgramClassRoomItemId.ToString(),
                    Comment = resultDetail?.Remark,
                    Selected = resultDetail != null
                });
            }

            return qry;
        }

        public bool SetClassRoomDetails(List<CListModel> list, int programClassRoomId, int currentUserId)
        {
            try
            {
                var result = _db.ProgramClassRoomDetails.Where(x => x.ProgramClassRoomId == programClassRoomId);
                if (result.Any())
                {
                    _db.ProgramClassRoomDetails.DeleteAllOnSubmit(result);
                    _db.SubmitChanges();
                }

                var tempList = new List<ProgramClassRoomDetail>();

                foreach (var l in list)
                {
                    tempList.Add(new ProgramClassRoomDetail()
                    {
                        ProgramClassRoomId = programClassRoomId,
                        CreatedId = currentUserId,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        ProgramClassRoomItemId = Convert.ToInt32(l.Value),
                        Remark = l.Comment
                    });
                }

                _db.ProgramClassRoomDetails.InsertAllOnSubmit(tempList);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return false;
        }
    }
}