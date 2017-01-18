using System;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Shared
{
    public partial class ApprovalCompletePop : PageBase
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
            if (e.Item.Text == "Complete" && !string.IsNullOrEmpty(hfId.Value))
            {
                if (IsValid)
                {
                    var type = Convert.ToInt32(hfType.Value);
                    var id = Convert.ToInt32(hfId.Value);
                    var checkNo = RadTextBoxCheckNo.Text;

                    var cApprovalHistory = new CApprovalHistory();
                    var approvalHistory = new ApprovalHistory()
                    {
                        CreatedId = CurrentUserId,
                        CreatedDate = DateTime.Now,
                        ApprovalDate = DateTime.Now,
                        ApprovalMemo = tbRemark.Text,
                        ApproveType = type,
                        MenuSeqId = id,
                        ApprovalUser = CurrentUserId,
                        IsApprovalRequest = true,
                        ApprovalStep = (int)CConstValue.ApprovalStatus.Approved
                    };
                    
                    if (type == (int)CConstValue.Approval.CorporateCreditCard)
                    {
                        try
                        {
                            var cCorporateCreditCard = new CCorporateCreditCard();
                            var corporateCreditCard = cCorporateCreditCard.Get(id);

                            corporateCreditCard.ApprovalDate = approvalHistory.ApprovalDate;
                            corporateCreditCard.ApprovalId = approvalHistory.ApprovalUser;
                            corporateCreditCard.ApprovalMemo = approvalHistory.ApprovalMemo;
                            corporateCreditCard.ApprovalStatus = approvalHistory.ApprovalStep;
                            corporateCreditCard.HqCheckNo = checkNo;

                            cCorporateCreditCard.Update(corporateCreditCard);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage(ex.Message);
                        }
                    }
                    // BusinessTrip
                    else if (type == (int)CConstValue.Approval.BusinessTrip)
                    {
                        try
                        {
                            var cBusinessTrip = new CBusinessTrip();
                            var businessTrip = cBusinessTrip.Get(id);

                            businessTrip.ApprovalDate = approvalHistory.ApprovalDate;
                            businessTrip.ApprovalId = approvalHistory.ApprovalUser;
                            businessTrip.ApprovalMemo = approvalHistory.ApprovalMemo;
                            businessTrip.ApprovalStatus = approvalHistory.ApprovalStep;
                            businessTrip.HqCheckNo = checkNo;

                            cBusinessTrip.Update(businessTrip);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage(ex.Message);
                        }
                    }
                    // Expense Program
                    else if (type == (int)CConstValue.Approval.Expense)
                    {
                        try
                        {
                            var cExpense = new CExpense();
                            var expense = cExpense.Get(id);

                            expense.ApprovalDate = approvalHistory.ApprovalDate;
                            expense.ApprovalId = approvalHistory.ApprovalUser;
                            expense.ApprovalMemo = approvalHistory.ApprovalMemo;
                            expense.ApprovalStatus = approvalHistory.ApprovalStep;
                            expense.HqCheckNo = checkNo;

                            cExpense.Update(expense);
                        }
                        catch (Exception ex)
                        {
                            ShowMessage(ex.Message);
                        }
                    }
                  
                    cApprovalHistory.Add(approvalHistory);

                    new CMail().SendMail((CConstValue.Approval)type, CConstValue.MailStatus.ToRequestUser, id, string.Empty, CurrentUserId);


                    RunClientScript("Close();");
                }
            }
        }
    }
}