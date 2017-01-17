using System;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Shared
{
    public partial class ApprovalRevisePop : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfId.Value = Request["id"];
                hfType.Value = Request["Type"];
            }
        }

        protected void MainToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Revise" && !string.IsNullOrEmpty(hfId.Value))
            {
                if (IsValid)
                {
                    try
                    {
                        var type = Convert.ToInt32(hfType.Value);
                        var id = Convert.ToInt32(hfId.Value);

                        var cApprovalHistory = new CApprovalHistory();
                        var approvalHistory = cApprovalHistory.Get(type, id, CurrentUserId);
                        approvalHistory.ApprovalDate = DateTime.Now;
                        approvalHistory.ApprovalMemo = tbRemark.Text;
                        // Revice
                        approvalHistory.ApprovalStep = (int)CConstValue.ApprovalStatus.Revise;

                        if (type == (int)CConstValue.Approval.Refund)
                        {
                            var cC = new CRefund();
                            var c = cC.Get(id);
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cC.Update(c);
                        }
                        else if (type == (int)CConstValue.Approval.Agency)
                        {
                            var cC = new CAgency();
                            var c = cC.Get(id);
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cC.Update(c);
                        }
                        else if (type == (int)CConstValue.Approval.CorporateCreditCard)
                        {
                            var cC = new CCorporateCreditCard();
                            var c = cC.Get(id);
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cC.Update(c);
                        }
                        else if (type == (int)CConstValue.Approval.BusinessTrip)
                        {
                            var cC = new CBusinessTrip();
                            var c = cC.Get(id);
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cC.Update(c);
                        }
                        else if (type == (int)CConstValue.Approval.Package)
                        {
                            var cC = new CPackageProgram();
                            var c = cC.GetPackageProgram(id);
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cC.Update(c);
                        }
                        else if (type == (int)CConstValue.Approval.Expense)
                        {
                            var cC = new CExpense();
                            var c = cC.Get(id);
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cC.Update(c);
                        }
                        else if (type == (int)CConstValue.Approval.PurchaseOrder)
                        {
                            var cC = new CPurchaseOrder();
                            var c = cC.Get(id);
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cC.Update(c);
                        }
                        else if (type == (int)CConstValue.Approval.Hire)
                        {
                            var cC = new CHire();
                            var c = cC.Get(id);
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cC.Update(c);
                        }
                        else if (type == (int)CConstValue.Approval.Vacation)
                        {
                            var cC = new CVacation();
                            var c = cC.Get(id);
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cC.Update(c);
                        }
                        else if (type == (int)CConstValue.Approval.Scholarship)
                        {
                            var cC = new CScholarship();
                            var c = cC.Get(id);
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cC.Update(c);
                        }
                        else if (type == (int)CConstValue.Approval.Promotion)
                        {
                            var cC = new CPromotion();
                            var c = cC.Get(id);

                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cC.Update(c);
                        }
                        else if (type == (int)CConstValue.Approval.CreditMemoPayout)
                        {
                            var cC = new CCreditMemoPayout();
                            var c = cC.Get(id);

                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cC.Update(c);
                        }

                        // update approvalHistory
                        cApprovalHistory.Update(approvalHistory);

                        new CMail().SendMail((CConstValue.Approval)type, CConstValue.MailStatus.ToRequestUser, id, string.Empty, CurrentUserId);

                        RunClientScript("Close();");

                        
                    }
                    catch (Exception ex)
                    {
                        ShowMessage(ex.Message);
                    }
                }
            }
        }
    }
}