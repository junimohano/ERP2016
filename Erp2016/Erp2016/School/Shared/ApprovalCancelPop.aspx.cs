using System;
using Erp2016.Lib;
using Telerik.Web.UI;
using Convert = System.Convert;

namespace School.Shared
{
    public partial class ApprovalCancelPop : PageBase
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
            if (e.Item.Text == "Cancel" && !string.IsNullOrEmpty(hfId.Value))
            {
                if (IsValid)
                {
                    try
                    {
                        var type = Convert.ToInt32(hfType.Value);
                        var id = Convert.ToInt32(hfId.Value);

                        var cApprovalHistory = new CApprovalHistory();
                        var approvalHistory = cApprovalHistory.Get(type, id, CurrentUserId);

                        bool isExists = true;
                        if (approvalHistory == null)
                        {
                            approvalHistory = new ApprovalHistory();
                            approvalHistory.ApprovalUser = CurrentUserId;
                            isExists = false;
                        }

                        approvalHistory.ApprovalDate = DateTime.Now;
                        approvalHistory.ApprovalMemo = tbRemark.Text;
                        approvalHistory.ApprovalStep = (int)CConstValue.ApprovalStatus.Canceled;

                        // update approvalHistory
                        if (isExists)
                            cApprovalHistory.Update(approvalHistory);

                        if (type == (int)CConstValue.Approval.Refund)
                        {
                            var cRefund = new CRefund();
                            var refund = cRefund.Get(id);
                            refund.ApprovalDate = approvalHistory.ApprovalDate;
                            refund.ApprovalId = approvalHistory.ApprovalUser;
                            refund.ApprovalMemo = approvalHistory.ApprovalMemo;
                            refund.ApprovalStatus = approvalHistory.ApprovalStep;

                            if (cRefund.Update(refund))
                            {
                                var cInvoiceInfo = new CInvoice();
                                var invoiceInfo = cInvoiceInfo.Get(refund.InvoiceId);

                                var cOriginalInvoiceInfo = new CInvoice();
                                var originalInvoiceInfo = cOriginalInvoiceInfo.Get(Convert.ToInt32(invoiceInfo.OriginalInvoiceId));

                                invoiceInfo.Status = (int)CConstValue.InvoiceStatus.Cancelled_RF; // Canceled_R
                                invoiceInfo.UpdatedId = CurrentUserId;

                                originalInvoiceInfo.Status = (int)CConstValue.InvoiceStatus.Invoiced; // Invoiced
                                originalInvoiceInfo.UpdatedId = CurrentUserId;

                                cInvoiceInfo.Update(invoiceInfo);
                                cOriginalInvoiceInfo.Update(originalInvoiceInfo);
                            }
                        }
                        else if (type == (int)CConstValue.Approval.Agency)
                        {
                            var cAgency = new CAgency();
                            var agency = cAgency.Get(id);
                            agency.ApprovalDate = approvalHistory.ApprovalDate;
                            agency.ApprovalId = approvalHistory.ApprovalUser;
                            agency.ApprovalMemo = approvalHistory.ApprovalMemo;
                            agency.ApprovalStatus = approvalHistory.ApprovalStep;

                            cAgency.Update(agency);
                        }
                        else if (type == (int)CConstValue.Approval.CreditMemoPayout)
                        {
                            var cCreditMemoPayout = new CCreditMemoPayout();
                            var creditMemoPayout = cCreditMemoPayout.Get(id);
                            creditMemoPayout.ApprovalDate = approvalHistory.ApprovalDate;
                            creditMemoPayout.ApprovalId = approvalHistory.ApprovalUser;
                            creditMemoPayout.ApprovalMemo = approvalHistory.ApprovalMemo;
                            creditMemoPayout.ApprovalStatus = approvalHistory.ApprovalStep;

                            cCreditMemoPayout.Update(creditMemoPayout);
                        }
                        else if (type == (int)CConstValue.Approval.Package)
                        {
                            var cPackageProgram = new CPackageProgram();
                            var packageProgram = cPackageProgram.GetPackageProgram(id);
                            packageProgram.ApprovalDate = approvalHistory.ApprovalDate;
                            packageProgram.ApprovalId = approvalHistory.ApprovalUser;
                            packageProgram.ApprovalMemo = approvalHistory.ApprovalMemo;
                            packageProgram.ApprovalStatus = approvalHistory.ApprovalStep;

                            cPackageProgram.Update(packageProgram);
                        }
                        else if (type == (int)CConstValue.Approval.Promotion)
                        {
                            var cPromotion = new CPromotion();
                            var promotion = cPromotion.Get(id);
                            promotion.ApprovalDate = approvalHistory.ApprovalDate;
                            promotion.ApprovalId = approvalHistory.ApprovalUser;
                            promotion.ApprovalMemo = approvalHistory.ApprovalMemo;
                            promotion.ApprovalStatus = approvalHistory.ApprovalStep;

                            cPromotion.Update(promotion);
                        }
                        else if (type == (int)CConstValue.Approval.Scholarship)
                        {
                            var cScholarship = new CScholarship();
                            var scholarship = cScholarship.Get(id);
                            scholarship.ApprovalDate = approvalHistory.ApprovalDate;
                            scholarship.ApprovalId = approvalHistory.ApprovalUser;
                            scholarship.ApprovalMemo = approvalHistory.ApprovalMemo;
                            scholarship.ApprovalStatus = approvalHistory.ApprovalStep;

                            cScholarship.Update(scholarship);
                        }
                        else if (type == (int)CConstValue.Approval.CorporateCreditCard)
                        {
                            var cCorporateCreditCard = new CCorporateCreditCard();
                            var corporateCreditCard = cCorporateCreditCard.Get(id);
                            corporateCreditCard.ApprovalDate = approvalHistory.ApprovalDate;
                            corporateCreditCard.ApprovalId = approvalHistory.ApprovalUser;
                            corporateCreditCard.ApprovalMemo = approvalHistory.ApprovalMemo;
                            corporateCreditCard.ApprovalStatus = approvalHistory.ApprovalStep;

                            cCorporateCreditCard.Update(corporateCreditCard);
                        }
                        // BusinessTrip
                        else if (type == (int)CConstValue.Approval.BusinessTrip)
                        {
                            var cBusinessTrip = new CBusinessTrip();
                            var businessTrip = cBusinessTrip.Get(id);
                            businessTrip.ApprovalDate = approvalHistory.ApprovalDate;
                            businessTrip.ApprovalId = approvalHistory.ApprovalUser;
                            businessTrip.ApprovalMemo = approvalHistory.ApprovalMemo;
                            businessTrip.ApprovalStatus = approvalHistory.ApprovalStep;

                            cBusinessTrip.Update(businessTrip);
                        }
                        // Purchase Order
                        else if (type == (int)CConstValue.Approval.PurchaseOrder)
                        {
                            var cPurchaseOrder = new CPurchaseOrder();
                            var purchaseOrder = cPurchaseOrder.Get(id);
                            purchaseOrder.ApprovalDate = approvalHistory.ApprovalDate;
                            purchaseOrder.ApprovalId = approvalHistory.ApprovalUser;
                            purchaseOrder.ApprovalMemo = approvalHistory.ApprovalMemo;
                            purchaseOrder.ApprovalStatus = approvalHistory.ApprovalStep;

                            cPurchaseOrder.Update(purchaseOrder);
                        }
                        // Expense
                        else if (type == (int)CConstValue.Approval.Expense)
                        {
                            var cExpense = new CExpense();
                            var expense = cExpense.Get(id);
                            expense.ApprovalDate = approvalHistory.ApprovalDate;
                            expense.ApprovalId = approvalHistory.ApprovalUser;
                            expense.ApprovalMemo = approvalHistory.ApprovalMemo;
                            expense.ApprovalStatus = approvalHistory.ApprovalStep;

                            cExpense.Update(expense);
                        }
                        // Hire
                        else if (type == (int)CConstValue.Approval.Hire)
                        {
                            var cHire = new CHire();
                            var hire = cHire.Get(id);
                            hire.ApprovalDate = approvalHistory.ApprovalDate;
                            hire.ApprovalId = approvalHistory.ApprovalUser;
                            hire.ApprovalMemo = approvalHistory.ApprovalMemo;
                            hire.ApprovalStatus = approvalHistory.ApprovalStep;

                            cHire.Update(hire);
                        }
                        // Vacation
                        else if (type == (int)CConstValue.Approval.Vacation)
                        {
                            var cVacation = new CVacation();
                            var vacation = cVacation.Get(id);
                            vacation.ApprovalDate = approvalHistory.ApprovalDate;
                            vacation.ApprovalId = approvalHistory.ApprovalUser;
                            vacation.ApprovalMemo = approvalHistory.ApprovalMemo;
                            vacation.ApprovalStatus = approvalHistory.ApprovalStep;

                            cVacation.Update(vacation);
                        }

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