using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CApproval
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CApproval()
        {
        }

        public Approval Get(int id)
        {
            return (from q in _db.Approvals where q.UserId == id && q.ApproveType == 1 select q).FirstOrDefault();
        }

        public List<Approval> GetList(int userId)
        {
            return _db.Approvals.Where(x => x.UserId == userId).ToList();
        }

        public Approval Get(int id, int approveType)
        {
            return _db.Approvals.Join(_db.Users, x => x.UserId, y => y.UserId, (x, y) => new { x, y }).Where(w => w.x.UserId == id && w.x.ApproveType == approveType).FirstOrDefault().x;
        }

        public int Add(Approval obj)
        {
            try
            {
                obj.CreatedDate = DateTime.Now;
                _db.Approvals.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Approvals.Max(x => x.ApproveId);
        }

        public bool Update(Approval obj)
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

        public bool Delete(Approval obj)
        {
            try
            {
                _db.Approvals.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CApprovalSupervisorModel> GetSupervisorChart(int menuId, int siteId)
        {
            var result = new List<CApprovalSupervisorModel>();

            var qry = _db.Approvals.Where(q => q.IsActive && q.ApproveType == menuId);

            foreach (var q in qry)
            {
                var n = new CApprovalSupervisorModel();

                var cT = new CUser();
                var t = cT.Get(Convert.ToInt32(q.UserId));

                if (t == null) continue;
              
                n.UserId = q.UserId;
                n.UserName = cT.GetUserName(t);
                n.Supervisor = q.Supervisor;
                n.UserPositionId = t.UserPositionId;

                //switch (MenuId)
                //{
                //    case ConstValue.APPROVAL_BASIC:
                //        if (SiteId == 1)
                //        {
                //            if (n.Supervisor != null && n.Supervisor == n.UserId)
                //                n.Supervisor = null;
                //        }
                //        else
                //        {
                //            if (n.UserId == 577)
                //            {
                //                n.Supervisor = null;
                //                if (n.Supervisor != null && n.Supervisor == n.UserId)
                //                    n.Supervisor = null;
                //            }
                //            else if (n.UserId == 578)
                //            {
                //                n.Supervisor = 0;
                //            }

                //        }
                //        break;
                //    case ConstValue.APPROVAL_AGENCY:
                //        if (n.UserId == 577)
                //        {
                //            n.Supervisor = null;
                //            if (n.Supervisor != null && n.Supervisor == n.UserId)
                //                n.Supervisor = null;
                //        }
                //        else if (n.UserId == 578)
                //        {
                //            n.Supervisor = 0;
                //        }
                //        break;
                //    case ConstValue.APPROVAL_SCHOLASHIP:
                //        break;
                //    case ConstValue.APPROVAL_REFUND:
                //        break;
                //    case ConstValue.APPROVAL_BCT:
                //        break;
                //    case ConstValue.APPROVAL_BUSINESS_TRIP:
                //        break;
                //}

                result.Add(n);
            }
            return result;
        }

        public bool UpdateStaff(int MenuId, int EmpId, int? supervisor)
        {
            try
            {
                var qry = (from q in _db.Approvals where q.UserId == EmpId && q.ApproveType == MenuId select q).FirstOrDefault();
                if (qry != null)
                {
                    qry.Supervisor = supervisor;

                    _db.SubmitChanges();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetSupuervisor(int approveType, int myId)
        {
            var supervisor = 0;

            var qry = _db.Approvals.FirstOrDefault(q => q.ApproveType == approveType && q.UserId == myId);

            if (qry != null)
            {
                if (qry.Supervisor != null)
                    supervisor = Convert.ToInt32(qry.Supervisor);
            }

            return supervisor;
        }

        public int ApproveRequstCreate(int approveType, int myId, int menuSeqId)
        {
            try
            {
                var qryMy = _db.Approvals.Where(q => q.IsActive && q.ApproveType == approveType && q.UserId == myId).FirstOrDefault();

                if (qryMy != null)
                {
                    //기안자 결재(approvalhistory add)
                    var cInqry = new CApprovalHistory();
                    var inqry = new ApprovalHistory();

                    inqry.ApproveType = approveType;
                    inqry.MenuSeqId = menuSeqId;
                    inqry.ApprovalUser = qryMy.UserId;
                    inqry.ApprovalDate = DateTime.Now;
                    inqry.IsApprovalRequest = true;

                    inqry.ApprovalStep = qryMy.Supervisor == null ? new CGlobal().GetApprovalValue(approveType) : 1; // auto approved or requested

                    inqry.CreatedId = myId;
                    inqry.CreatedDate = DateTime.Now;

                    cInqry.Add(inqry);

                    if (qryMy.Supervisor != null)
                    {
                        int? supervisorId = Convert.ToInt32(qryMy.Supervisor);
                        var flg = true;
                        while (supervisorId != null)
                        {
                            //결재자들 결재 생성
                            var super = _db.Approvals.Where(q => q.IsActive && q.ApproveType == approveType && q.UserId == supervisorId).FirstOrDefault();
                            var cSubInqry = new CApprovalHistory();
                            var subInqry = new ApprovalHistory();

                            subInqry.ApproveType = approveType;
                            subInqry.MenuSeqId = menuSeqId;
                            subInqry.ApprovalUser = super.UserId;

                            subInqry.CreatedId = myId;
                            subInqry.CreatedDate = DateTime.Now;

                            if (flg)
                            {
                                subInqry.IsApprovalRequest = true;
                                flg = false;
                            }

                            cSubInqry.Add(subInqry);

                            supervisorId = super.Supervisor;
                        }
                    }
                    return (int)inqry.ApprovalStep;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return -1;
        }
     
        public List<Approval> GetAppType(int sup)
        {

            var qry = (from q in _db.Approvals where q.UserId == sup select q);

            var approvalList = new List<Approval>();

            foreach (Approval a in qry)
            {
                approvalList.Add(new Approval()
                {
                    UserId = a.UserId,
                    ApproveType = a.ApproveType

                });
            }

            return approvalList;
        }

        public List<CFilterListModel> GetStatusNameList()
        {
            return _db.Dicts.Where(x => x.DictType == 217).OrderBy(q => q.Value).Select(p => new CFilterListModel { Status = p.Name }).Distinct().ToList();
        }

        public List<CFilterListModel> GetApprovalTypeNameList()
        {
            var list = new List<CFilterListModel>();
            foreach (int index in Enum.GetValues(typeof(CConstValue.Approval)))
            {
                list.Add(new CFilterListModel()
                {
                    Type = Enum.GetName(typeof(CConstValue.Approval), index)
                });
            }
            return list;
        }
    }
}