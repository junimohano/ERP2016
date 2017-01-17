using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CBulletinBoard
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CBulletinBoard()
        {
        }
        
        public BulletinBoard Get(int bulletinBoardId)
        {
            return _db.BulletinBoards.FirstOrDefault(x => x.BulletinBoardId == bulletinBoardId);
        }
        
        public int Add(BulletinBoard obj)
        {
            try
            {
                _db.BulletinBoards.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.BulletinBoards.Max(x => x.BulletinBoardId);
        }

        public bool Update(BulletinBoard obj)
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

        public bool Delete(BulletinBoard obj)
        {
            try
            {
                _db.BulletinBoards.DeleteOnSubmit(obj);
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