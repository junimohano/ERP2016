using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Erp2016.Lib
{
    public class CHomestayHostFamilyMember
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CHomestayHostFamilyMember()
        {

        }

        public HomestayHostFamilyMember  Get(int id)
        {
            return _db.HomestayHostFamilyMembers.FirstOrDefault(q => q.FamilyMemberId == id);
        }

        public List<HomestayHostFamilyMember> GetFamilyMemberList(int hostid)
        {
            List<HomestayHostFamilyMember> FamilyMember = new List<HomestayHostFamilyMember>();
            var  result = _db.HomestayHostFamilyMembers.Where (q => q.HostId == hostid).OrderBy(q=>q.MemberFirstName).ToList();
            FamilyMember = result;
            return FamilyMember;
        }

        public int GetFamilyMemberNumber(int hostid)
        {
            int number = 0;
            var result = _db.HomestayHostFamilyMembers.Where(q => q.HostId == hostid).Count().ToString();
            number = Convert.ToInt32(result);
            return number;
        }
        public int Add(HomestayHostFamilyMember obj)
        {
            try
            {


                _db.HomestayHostFamilyMembers.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.HomestayHostFamilyMembers.Max(x => x.FamilyMemberId);

        }

        public bool Update(HomestayHostFamilyMember obj)
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

        public bool Delete(HomestayHostFamilyMember obj)
        {
            try
            {
                _db.HomestayHostFamilyMembers.DeleteOnSubmit(obj);
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
