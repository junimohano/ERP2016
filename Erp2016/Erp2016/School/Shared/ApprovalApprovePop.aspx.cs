using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using Erp2016.Lib;
using Telerik.Web.UI;
using Convert = System.Convert;

namespace School.Shared
{
    public partial class ApprovalApprovePop : PageBase
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
            if (e.Item.Text == @"Approve" && !string.IsNullOrEmpty(hfId.Value))
            {
                if (IsValid)
                {
                    var type = Convert.ToInt32(hfType.Value);
                    var id = Convert.ToInt32(hfId.Value);
                    var idNumber = string.Empty;

                    var cApprovalHistory = new CApprovalHistory();
                    var approvalHistory = cApprovalHistory.Get(type, id, CurrentUserId);
                    approvalHistory.ApprovalDate = DateTime.Now;
                    approvalHistory.ApprovalMemo = tbRemark.Text;
                    // cal
                    int approvalStatus = cApprovalHistory.CheckApprovalStep(type, id);
                    if (approvalStatus == (int)CConstValue.ApprovalStatus.Approved)
                        approvalStatus = new CGlobal().GetApprovalValue(type);
                    approvalHistory.ApprovalStep = approvalStatus;
                    // update approvalHistory
                    cApprovalHistory.Update(approvalHistory);

                    //Refund & Commission
                    if (type == (int)CConstValue.Approval.Refund)
                    {
                        var cRefundInfo = new CRefund();
                        var refundInfo = cRefundInfo.Get(Convert.ToInt32(hfId.Value));

                        refundInfo.ApprovalDate = approvalHistory.ApprovalDate;
                        refundInfo.ApprovalId = approvalHistory.ApprovalUser;
                        refundInfo.ApprovalMemo = approvalHistory.ApprovalMemo;
                        refundInfo.ApprovalStatus = approvalHistory.ApprovalStep;

                        if (cRefundInfo.Update(refundInfo))
                        {
                            //if last approve
                            if (refundInfo.ApprovalStatus == (int)CConstValue.ApprovalStatus.Approved)
                            {
                                var vwRefund = cRefundInfo.GetVwRefund(refundInfo.RefundId);
                                var refundAmount = Math.Abs((decimal)vwRefund.RefundAmount);
                                var sumOfMdfAndCp = new CCreditMemo().GetSumOfMdfAndCp(vwRefund.RefundId);

                                var cCreditMemoPayout = new CCreditMemoPayout();
                                var creditMemoPayout = cCreditMemoPayout.Get(refundInfo.CreditMemoPayoutId);

                                var cCreditMemo = new CCreditMemo();
                                var creditMemo = cCreditMemo.Get(creditMemoPayout.CreditMemoId);

                                creditMemo.OriginalCreditMemoAmount = refundAmount - sumOfMdfAndCp;
                                creditMemo.CreditMemoStartDate = DateTime.Now;
                                creditMemo.CreditMemoEndDate = DateTime.Now.AddYears(1);
                                creditMemo.IsActive = true;
                                creditMemo.UpdatedId = CurrentUserId;
                                creditMemo.UpdatedDate = DateTime.Now;

                                // Approved
                                creditMemoPayout.ApprovalStatus = (int)CConstValue.ApprovalStatus.Approved;
                                creditMemoPayout.ApprovalDate = DateTime.Now;
                                creditMemoPayout.ApprovalId = CurrentUserId;
                                creditMemoPayout.Amount = refundAmount;
                                creditMemoPayout.PayoutMethod = vwRefund.PayoutMethod;
                                creditMemoPayout.IsActive = true;

                                creditMemoPayout.UpdatedId = CurrentUserId;
                                creditMemoPayout.UpdatedDate = DateTime.Now;

                                cCreditMemoPayout.Update(creditMemoPayout);

                                if (cCreditMemo.Update(creditMemo))
                                {
                                    var cInvoiceInfo = new CInvoice();
                                    var invoiceInfo = cInvoiceInfo.Get(refundInfo.InvoiceId);

                                    var cOriginalInvoiceInfo = new CInvoice();
                                    var originalInvoiceInfo = cOriginalInvoiceInfo.Get(Convert.ToInt32(invoiceInfo.OriginalInvoiceId));

                                    invoiceInfo.Status = (int)CConstValue.InvoiceStatus.Invoiced; // Invoiced
                                    invoiceInfo.UpdatedId = CurrentUserId;

                                    originalInvoiceInfo.Status = (int)CConstValue.InvoiceStatus.Cancelled_RF; // Canceled_RF
                                    originalInvoiceInfo.UpdatedId = CurrentUserId;

                                    var startDate = DateTime.Now;
                                    var endDate = DateTime.Now;

                                    //invoice status update
                                    if (cInvoiceInfo.Update(invoiceInfo) && cOriginalInvoiceInfo.Update(originalInvoiceInfo))
                                    {
                                        // Program
                                        if (invoiceInfo.ProgramRegistrationId != null)
                                        {
                                            var cProgramRegiInfo = new CProgramRegistration();
                                            var programRegiInfo = cProgramRegiInfo.Get(Convert.ToInt32(invoiceInfo.ProgramRegistrationId));
                                            programRegiInfo.UpdatedId = CurrentUserId;
                                            programRegiInfo.UpdatedDate = DateTime.Now;
                                            programRegiInfo.ProgramRegistrationType = 12; // cancel

                                            cProgramRegiInfo.Update(programRegiInfo);

                                            startDate = programRegiInfo.StartDate.Value;
                                            endDate = programRegiInfo.EndDate.Value;
                                        }
                                        // Homestay
                                        else if (invoiceInfo.HomestayRegistrationId != null)
                                        {
                                            var cHomestayStudentRequest = new CHomestayStudentRequest();
                                            var homestayStudentRequest = cHomestayStudentRequest.GetHomestayStudentRequest(Convert.ToInt32(invoiceInfo.HomestayRegistrationId));
                                            homestayStudentRequest.UpdateUserId = CurrentUserId;
                                            homestayStudentRequest.UpdatedDate = DateTime.Now;
                                            homestayStudentRequest.HomestayStudentStatus = 1; // cancel

                                            cHomestayStudentRequest.Update(homestayStudentRequest);

                                            startDate = homestayStudentRequest.StartDate.Value;
                                            endDate = homestayStudentRequest.EndDate.Value;
                                        }
                                        // Dormitory
                                        else if (invoiceInfo.DormitoryRegistrationId != null)
                                        {
                                            var cDormitoryStudentRequest = new CDormitoryRegistrations();
                                            var dormitoryStudentRequest = cDormitoryStudentRequest.GetDormitoryStudentRequest(Convert.ToInt32(invoiceInfo.ProgramRegistrationId));
                                            dormitoryStudentRequest.DormitoryStudentStatus = 1; // cancel
                                            dormitoryStudentRequest.UpdatedId = CurrentUserId;
                                            dormitoryStudentRequest.UpdatedDate = DateTime.Now;

                                            cDormitoryStudentRequest.Update(dormitoryStudentRequest);

                                            startDate = dormitoryStudentRequest.StartDate.Value;
                                            endDate = dormitoryStudentRequest.EndDate.Value;
                                        }
                                        
                                        // Rev
                                        var cInterimInvoice = new CInvoice();
                                        var interimInvoice = new Invoice();
                                        CGlobal.Copy(invoiceInfo, interimInvoice);
                                        interimInvoice.OriginalInvoiceId = invoiceInfo.InvoiceId;
                                        interimInvoice.InvoiceType = (int)CConstValue.InvoiceType.Refund_RV;
                                        interimInvoice.Status = (int)CConstValue.InvoiceStatus.Invoiced;
                                        interimInvoice.CreatedId = CurrentUserId;
                                        interimInvoice.CreatedDate = DateTime.Now;

                                        if (cInterimInvoice.Add(interimInvoice) > 0)
                                        {
                                            var totalDays = Convert.ToDateTime(endDate) - Convert.ToDateTime(startDate);
                                            var interimDays = Convert.ToDateTime(endDate) - Convert.ToDateTime(refundInfo.RefundDate);
                                            var interimRate = Math.Round((interimDays.TotalDays / totalDays.TotalDays), 2) * 100;

                                            var cInvoiceItem = new CInvoiceItem();
                                            var cOriginalInvoiceItemModels = cInvoiceItem.GetInvoiceItemModels(invoiceInfo.InvoiceId);
                                            List<InvoiceItem> newInvoiceItems = new List<InvoiceItem>();
                                            foreach (CInvoiceItemModel ori in cOriginalInvoiceItemModels)
                                            {
                                                if (ori.InvoiceCoaItem.RevenueRecognition == 1)
                                                {
                                                    var newInvoiceRevItem = new InvoiceItem();
                                                    CGlobal.Copy(ori.InvoiceItem, newInvoiceRevItem);
                                                    newInvoiceRevItem.InvoiceId = interimInvoice.InvoiceId;
                                                    
                                                    newInvoiceRevItem.StandardPrice *= (decimal)interimRate;
                                                    newInvoiceRevItem.StudentPrice *= (decimal)interimRate;
                                                    newInvoiceRevItem.AgencyPrice *= (decimal)interimRate;
                                                    
                                                    newInvoiceRevItem.CreatedId = CurrentUserId;
                                                    newInvoiceRevItem.CreatedDate = DateTime.Now;
                                                    newInvoiceRevItem.Remark = "Refund Rev";
                                                    newInvoiceItems.Add(newInvoiceRevItem);
                                                }
                                            }

                                            if (newInvoiceItems.Count > 0)
                                            {
                                                // copy invoiceItems
                                                if (cInvoiceItem.Add(newInvoiceItems))
                                                    RunClientScript("Close();");
                                                else
                                                    ShowMessage("failed to update inqury (Refund Ref Invoice Item)");
                                            }
                                            else
                                                RunClientScript("Close();");
                                        }
                                        else
                                            ShowMessage("failed to update inqury (Refund Ref Invoice)");
                                    }
                                    else
                                        ShowMessage("failed to update inqury (Original Invoice and Refund Invoice)");
                                }
                                else
                                    ShowMessage("failed to update inqury (CreditMemo Info)");
                            }
                            else
                            {
                                RunClientScript("Close();");
                            }
                        }
                        else
                        {
                            ShowMessage("failed to update inqury (Refund Info)");
                        }
                    }
                    // Agency
                    else if (type == (int)CConstValue.Approval.Agency)
                    {
                        try
                        {
                            var cAgency = new CAgency();
                            var agency = cAgency.Get(id);

                            idNumber = agency.AgencyNumber;
                            agency.ApprovalDate = approvalHistory.ApprovalDate;
                            agency.ApprovalId = approvalHistory.ApprovalUser;
                            agency.ApprovalMemo = approvalHistory.ApprovalMemo;
                            agency.ApprovalStatus = approvalHistory.ApprovalStep;

                            cAgency.Update(agency);

                            RunClientScript("Close();");
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

                            cBusinessTrip.Update(businessTrip);

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
                    // Package Program
                    else if (type == (int)CConstValue.Approval.Package)
                    {
                        try
                        {
                            var cP = new CPackageProgram();
                            var packageProgram = cP.GetPackageProgram(id);

                            packageProgram.ApprovalDate = approvalHistory.ApprovalDate;
                            packageProgram.ApprovalId = approvalHistory.ApprovalUser;
                            packageProgram.ApprovalMemo = approvalHistory.ApprovalMemo;
                            packageProgram.ApprovalStatus = approvalHistory.ApprovalStep;
                            if (packageProgram.ApprovalStatus == (int)CConstValue.ApprovalStatus.Approved)
                                packageProgram.IsActive = true;

                            cP.Update(packageProgram);
                            RunClientScript("Close();");
                        }
                        catch (Exception ex)
                        {
                            ShowMessage(ex.Message);
                        }
                    }
                    //Scholarship//
                    else if (type == (int)CConstValue.Approval.Scholarship)
                    {
                        try
                        {
                            var cScholar = new CScholarship();
                            var scholar = cScholar.Get(Convert.ToInt32(id));

                            idNumber = scholar.ScholarshipMasterNo;
                            scholar.ApprovalDate = approvalHistory.ApprovalDate;
                            scholar.ApprovalId = approvalHistory.ApprovalUser;
                            scholar.ApprovalMemo = approvalHistory.ApprovalMemo;
                            scholar.ApprovalStatus = approvalHistory.ApprovalStep;
                            if (scholar.ApprovalStatus == (int)CConstValue.ApprovalStatus.Approved)
                                scholar.IsActive = true;

                            cScholar.Update(scholar);
                            RunClientScript("Close();");
                        }
                        catch (Exception ex)
                        {
                            ShowMessage(ex.Message);
                        }
                    }
                    //Promotion//
                    else if (type == (int)CConstValue.Approval.Promotion)
                    {
                        try
                        {
                            var cPromo = new CPromotion();
                            var promo = cPromo.Get(Convert.ToInt32(id));

                            idNumber = promo.PromotionMasterNo;
                            promo.ApprovalDate = approvalHistory.ApprovalDate;
                            promo.ApprovalId = approvalHistory.ApprovalUser;
                            promo.ApprovalMemo = approvalHistory.ApprovalMemo;
                            promo.ApprovalStatus = approvalHistory.ApprovalStep;
                            if (promo.ApprovalStatus == (int)CConstValue.ApprovalStatus.Approved)
                                promo.IsActive = true;

                            cPromo.Update(promo);
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
                    // Hire
                    else if (type == (int)CConstValue.Approval.Hire)
                    {
                        try
                        {
                            var cHire = new CHire();
                            var hire = cHire.Get(id);

                            hire.ApprovalDate = approvalHistory.ApprovalDate;
                            hire.ApprovalId = approvalHistory.ApprovalUser;
                            hire.ApprovalMemo = approvalHistory.ApprovalMemo;
                            hire.ApprovalStatus = approvalHistory.ApprovalStep;

                            cHire.Update(hire);
                            RunClientScript("Close();");
                        }
                        catch (Exception ex)
                        {
                            ShowMessage(ex.Message);
                        }
                    }
                    // Vacation
                    else if (type == (int)CConstValue.Approval.Vacation)
                    {
                        try
                        {
                            var cVacationDetail = new CVacation();
                            var vacation = cVacationDetail.Get(id);

                            vacation.ApprovalDate = approvalHistory.ApprovalDate;
                            vacation.ApprovalId = approvalHistory.ApprovalUser;
                            vacation.ApprovalMemo = approvalHistory.ApprovalMemo;
                            vacation.ApprovalStatus = approvalHistory.ApprovalStep;

                            cVacationDetail.Update(vacation);
                            RunClientScript("Close();");
                        }
                        catch (Exception ex)
                        {
                            ShowMessage(ex.Message);
                        }
                    }
                    // CreditMemoPayout
                    else if (type == (int)CConstValue.Approval.CreditMemoPayout)
                    {
                        try
                        {
                            var cCreditMemo = new CCreditMemoPayout();
                            var creditMemo = cCreditMemo.Get(id);

                            creditMemo.ApprovalDate = approvalHistory.ApprovalDate;
                            creditMemo.ApprovalId = approvalHistory.ApprovalUser;
                            creditMemo.ApprovalMemo = approvalHistory.ApprovalMemo;
                            creditMemo.ApprovalStatus = approvalHistory.ApprovalStep;

                            cCreditMemo.Update(creditMemo);
                            RunClientScript("Close();");
                        }
                        catch (Exception ex)
                        {
                            ShowMessage(ex.Message);
                        }
                    }


                    if (approvalHistory.ApprovalStep != (int)CConstValue.ApprovalStatus.Approved) //if has a next approver, request approve
                    {
                        var approvalInfo = new CApproval();
                        var supervisor = approvalInfo.GetSupuervisor(type, CurrentUserId);

                        if (supervisor > 0)
                        {
                            var cNextApprove = new CApprovalHistory();
                            var nextApprove = cNextApprove.Get(type, id, supervisor);

                            nextApprove.IsApprovalRequest = true;
                            cNextApprove.Update(nextApprove);
                        }

                        new CMail().SendMail((CConstValue.Approval)type, CConstValue.MailStatus.ToApproveUserAndRequestUser, id, idNumber, CurrentUserId);
                    }
                    // approved
                    else
                        new CMail().SendMail((CConstValue.Approval)type, CConstValue.MailStatus.ToRequestUser, id, idNumber, CurrentUserId);

                }
            }
        }
    }
}