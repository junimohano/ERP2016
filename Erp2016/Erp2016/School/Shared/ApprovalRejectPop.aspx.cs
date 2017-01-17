using System;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Shared
{
    public partial class ApprovalRejectPop : PageBase
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
            if (e.Item.Text == @"Reject" && !string.IsNullOrEmpty(hfId.Value))
            {
                if (IsValid)
                {
                    try
                    {
                        var type = Convert.ToInt32(hfType.Value);
                        var id = Convert.ToInt32(hfId.Value);
                        var idNumber = string.Empty;

                        var cApprovalHistory = new CApprovalHistory();
                        var approvalHistory = cApprovalHistory.Get(type, id, CurrentUserId);
                        approvalHistory.ApprovalDate = DateTime.Now;
                        approvalHistory.ApprovalMemo = tbRemark.Text;
                        // Reject
                        approvalHistory.ApprovalStep = (int)CConstValue.ApprovalStatus.Rejected;

                        if (type == (int)CConstValue.Approval.Refund)
                        {
                            var cC = new CRefund();
                            var c = cC.Get(id);
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            if (cC.Update(c))
                            {
                                var cInvoiceInfo = new CInvoice();
                                var invoiceInfo = cInvoiceInfo.Get(c.InvoiceId);

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
                        // Agency
                        else if (type == (int)CConstValue.Approval.Agency)
                        {
                            var cC = new CAgency();
                            var c = cC.Get(id);
                            idNumber = c.AgencyNumber;
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
                        // BusinessTrip
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
                        // PackageProgram
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
                        // Expense
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
                        // Purchase Order
                        else if (type == (int)CConstValue.Approval.PurchaseOrder)
                        {
                            var cP = new CPurchaseOrder();
                            var c = cP.Get(id);
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;

                            cP.Update(c);
                        }
                        // Hire
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
                        // Vacation
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
                        //Scholarship
                        else if (type == (int)CConstValue.Approval.Scholarship)
                        {
                            var cC = new CScholarship();
                            var c = cC.Get(id);
                            idNumber = c.ScholarshipMasterNo;
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;
                            cC.Update(c);
                        }
                        //Promotion
                        else if (type == (int)CConstValue.Approval.Promotion)
                        {
                            var cC = new CPromotion();
                            var c = cC.Get(id);
                            idNumber = c.PromotionMasterNo;
                            c.ApprovalDate = approvalHistory.ApprovalDate;
                            c.ApprovalId = approvalHistory.ApprovalUser;
                            c.ApprovalMemo = approvalHistory.ApprovalMemo;
                            c.ApprovalStatus = approvalHistory.ApprovalStep;
                            cC.Update(c);
                        }
                        // CreditMemo
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

                        new CMail().SendMail((CConstValue.Approval)type, CConstValue.MailStatus.ToRequestUser, id, idNumber, CurrentUserId);

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