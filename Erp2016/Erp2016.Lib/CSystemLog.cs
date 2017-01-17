using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CSystemLog
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CSystemLog()
        {
        }

        public SystemLog Get(int id)
        {
            return _db.SystemLogs.FirstOrDefault(q => q.LogId == id);
        }
        
        public int Add(SystemLog obj)
        {
            try
            {
                obj.LogTime = DateTime.Now;
                _db.SystemLogs.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.SystemLogs.Max(x => x.LogId);
        }

        public bool Update(SystemLog obj)
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

        public bool Delete(SystemLog obj)
        {
            try
            {
                _db.SystemLogs.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }
        
        public bool NewLog(int userid, string subject, string body, int priority, string ip)
        {
            //var qry = new SystemLog();

            //qry.Subject = subject;
            //qry.Body = body;
            //qry.LogTime = DateTime.Now;
            //qry.UserId = userid;
            //qry.Priority = priority;
            //qry.IPAddress = ip;

            //db.SystemLogs.InsertOnSubmit(qry);
            //db.SubmitChanges();

            //if (qry.LogId > 0)
            //{
            //    LogId = qry.LogId;
            //    return true;
            //}

            return false;
        }

        public List<CSystemLog> SearchLog(string key, DateTime? startdate, DateTime? enddate, int? priority, int? userid)
        {
            var result = new List<CSystemLog>();

            return result;
        }
    }
}