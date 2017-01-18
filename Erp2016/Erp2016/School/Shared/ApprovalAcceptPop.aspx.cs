using System;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Shared
{
    public partial class ApprovalAcceptPop : PageBase
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
            if (e.Item.Text == @"Accept" && !string.IsNullOrEmpty(hfId.Value))
            {
                if (IsValid)
                {
                    var type = Convert.ToInt32(hfType.Value);
                    var id = Convert.ToInt32(hfId.Value);

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
                        ApprovalStep = (int)CConstValue.ApprovalStatus.InProgress
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

                            cCorporateCreditCard.Update(corporateCreditCard);

                            RunClientScript("Close();");
                        }
                        catch (Exception ex)
                        {
                            ShowMessage(ex.Message);
                        }
                    }
                    // BusinessTrip
                    if (type == (int)CConstValue.Approval.BusinessTrip)
                    {
                        try
                        {
                            var cBusinessTrip = new CBusinessTrip();
                            var businessTrip = cBusinessTrip.Get(id);

                            businessTrip.ApprovalDate = approvalHistory.ApprovalDate;
                            businessTrip.ApprovalId = approvalHistory.ApprovalUser;
                            businessTrip.ApprovalMemo = approvalHistory.ApprovalMemo;
                            businessTrip.ApprovalStatus = approvalHistory.ApprovalStep;

                            cBusinessTrip.Update(businessTrip);

                            RunClientScript("Close();");
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

                            cExpense.Update(expense);

                            RunClientScript("Close();");
                        }
                        catch (Exception ex)
                        {
                            ShowMessage(ex.Message);
                        }
                    }
                    // Purchase Order
                    else if (type == (int)CConstValue.Approval.PurchaseOrder)
                    {
                        try
                        {
                            // approved
                            approvalHistory.ApprovalStep = (int)CConstValue.ApprovalStatus.Approved;

                            var cPurchaseOrder = new CPurchaseOrder();
                            var purchaseOrder = cPurchaseOrder.Get(id);

                            purchaseOrder.ApprovalDate = approvalHistory.ApprovalDate;
                            purchaseOrder.ApprovalId = approvalHistory.ApprovalUser;
                            purchaseOrder.ApprovalMemo = approvalHistory.ApprovalMemo;
                            purchaseOrder.ApprovalStatus = approvalHistory.ApprovalStep;

                            cPurchaseOrder.Update(purchaseOrder);

                            RunClientScript("Close();");
                        }
                        catch (Exception ex)
                        {
                            ShowMessage(ex.Message);
                        }
                    }

                    // update approvalHistory
                    cApprovalHistory.Add(approvalHistory);
                    
                    new CMail().SendMail((CConstValue.Approval)type, CConstValue.MailStatus.ToRequestUser, id, string.Empty, CurrentUserId);
                }
            }
        }
    }
}