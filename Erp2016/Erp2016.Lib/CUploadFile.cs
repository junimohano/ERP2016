using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace Erp2016.Lib
{
    public class CUploadFile
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CUploadFile()
        {
        }

        public UploadFile Get(int id)
        {
            return _db.UploadFiles.FirstOrDefault(q => q.UploadFileId == id);
        }
        
        public IEnumerable<UploadFile> GetUploadFiles(int uploadType, int seqId)
        {
            var tempList = new List<UploadFile>();
            var queryList = _db.UploadFiles.Where(x => x.UploadType == uploadType && x.SeqId == seqId);
            foreach (var query in queryList)
            {
                tempList.Add(query);
            }
            return tempList;
        }

        public void DelUploadFile(int uploadFileId)
        {
            var query = _db.UploadFiles.FirstOrDefault(x => x.UploadFileId == uploadFileId);

            if (query != null)
            {
                // move file 
                var fullPath = HttpContext.Current.Server.MapPath(System.IO.Path.Combine(query.Path, query.Name));

                if (File.Exists(fullPath))
                {
                    const string movingPath = "~/Upload/Removed";
                    if (Directory.Exists(movingPath) == false)
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(movingPath));

                    var path = HttpContext.Current.Server.MapPath(movingPath);
                    File.Move(fullPath, System.IO.Path.Combine(path, query.Name));
                }

                _db.UploadFiles.DeleteOnSubmit(query);
                _db.SubmitChanges();
            }
        }

        public int Add(UploadFile obj)
        {
            try
            {
                obj.UpdatedDate = DateTime.Now;

                _db.UploadFiles.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.UploadFiles.Max(x => x.UploadFileId);
        }

        public bool Update(UploadFile obj)
        {
            try
            {
                obj.UpdatedDate = DateTime.Now;

                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool Delete(UploadFile obj)
        {
            try
            {
                _db.UploadFiles.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }
        
        public List<CUploadFile> GetFiles(int filefor, int attachid)
        {
            var result = new List<CUploadFile>();

            return result;
        }
    }
}