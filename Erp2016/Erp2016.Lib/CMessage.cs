using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CMessage
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CMessage()
        {
        }

        public Message Get(int messageId)
        {
            return _db.Messages.FirstOrDefault(x => x.MessageId == messageId);
        }

        public int Add(Message obj)
        {
            try
            {
                _db.Messages.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Messages.Max(x => x.MessageId);
        }

        public bool Update(Message obj)
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

        public bool Delete(Message obj)
        {
            try
            {
                _db.Messages.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public int GetMessageCount(int userId)
        {
            return _db.Messages.Count(x => x.IsRead == false && x.UserId == userId);
        }
    }
}