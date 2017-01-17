using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CCorporateCreditCardSchema
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CCorporateCreditCardSchema()
        {
        }

        public CorporateCreditCardSchema Get(int corporateCreditCardSchemaId)
        {
            return _db.CorporateCreditCardSchemas.FirstOrDefault(x => x.CorporateCreditCardSchemaId == corporateCreditCardSchemaId);
        }

        public CorporateCreditCardSchema GetByUserId(int userId)
        {
            return _db.CorporateCreditCardSchemas.FirstOrDefault(x => x.UserId == userId);
        }

        public int Add(CorporateCreditCardSchema obj)
        {
            try
            {
                _db.CorporateCreditCardSchemas.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.CorporateCreditCardSchemas.Max(x => x.CorporateCreditCardSchemaId);
        }

        public bool Update(CorporateCreditCardSchema obj)
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

        public bool Delete(CorporateCreditCardSchema obj)
        {
            try
            {
                _db.CorporateCreditCardSchemas.DeleteOnSubmit(obj);
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