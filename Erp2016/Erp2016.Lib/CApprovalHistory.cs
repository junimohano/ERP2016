using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Erp2016.Lib
{
    public class CApprovalHistory
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CApprovalHistory()
        {
        }

        public ApprovalHistory Get(int id)
        {
            return _db.ApprovalHistories.FirstOrDefault(q => q.ApprovalHistoryId == id);
        }

        public ApprovalHistory Get(int approvalType, int menuSeqId, int userId)
        {
            return _db.ApprovalHistories.FirstOrDefault(q => q.ApproveType == approvalType && q.MenuSeqId == menuSeqId && q.ApprovalUser == userId);
        }

        public ApprovalHistory GetApprovalByRequestedUser(int approvalType, int menuSeqId)
        {
            return _db.ApprovalHistories.OrderBy(x => x.ApprovalHistoryId).FirstOrDefault(q => q.ApproveType == approvalType && q.MenuSeqId == menuSeqId);
        }
        public ApprovalHistory GetCurrentApproval(int approvalType, int menuSeqId)
        {
            return _db.ApprovalHistories.OrderByDescending(x => x.ApprovalHistoryId).FirstOrDefault(q => q.ApproveType == approvalType && q.MenuSeqId == menuSeqId && q.IsApprovalRequest == true && q.ApprovalStep != null);
        }

        public int Add(ApprovalHistory obj)
        {
            try
            {
                _db.ApprovalHistories.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ApprovalHistories.Max(x => x.ApprovalHistoryId);
        }

        public bool Update(ApprovalHistory obj)
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

        public bool Delete(ApprovalHistory obj)
        {
            try
            {
                _db.ApprovalHistories.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool DeleteArray(IQueryable<ApprovalHistory> objs)
        {
            try
            {
                _db.ApprovalHistories.DeleteAllOnSubmit(objs);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public void DelApprovalHistory(int approveType, int menuSeqId)
        {
            var result = _db.ApprovalHistories.Where(x => x.ApproveType == approveType && x.MenuSeqId == menuSeqId);
            if (result.Any())
                DeleteArray(result);
        }

        public int CheckApprovalStep(int approvalType, int menuSeqId)
        {
            var qryOne = _db.ApprovalHistories.Where(q => q.ApproveType == approvalType && q.MenuSeqId == menuSeqId).Count();
            var qryTwo = _db.ApprovalHistories.Where(q => q.ApproveType == approvalType && q.MenuSeqId == menuSeqId && q.ApprovalStep != null).Count();
            var returnValue = 0;

            if ((qryOne - qryTwo) != 1)
            {
                returnValue = qryTwo + 1; //1~9 : Approval 1st ~ Approval 9th
            }
            else
            {
                returnValue = 99; //Approved
            }

            return returnValue;
        }
        
    }
}