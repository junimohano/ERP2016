using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Home
{
    public partial class Dashboard : PageBase
    {
        public Dashboard() : base((int)CConstValue.Menu.BulletinBoard)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
                //scriptManager.Scripts.Add(new ScriptReference {Path = ResolveUrl("~/assets/js/jquery.flot.min.js")});
                //scriptManager.Scripts.Add(new ScriptReference {Path = ResolveUrl("~/assets/js/jquery.flot.time.min.js")});

                //GetChart();
            }

            // request
            LinqDataSourceRequest.WhereParameters.Clear();
            LinqDataSourceRequest.WhereParameters.Add("CurrentUserId", DbType.Int32, CurrentUserId.ToString());
            LinqDataSourceRequest.Where = "ApprovalUser == @CurrentUserId && CreatedId == @CurrentUserId";

            // approval
            LinqDataSourceApproval.WhereParameters.Clear();
            LinqDataSourceApproval.WhereParameters.Add("CurrentUserId", DbType.Int32, CurrentUserId.ToString());
            LinqDataSourceApproval.WhereParameters.Add("IsApprovalRequest", DbType.Boolean, Boolean.TrueString);
            LinqDataSourceApproval.WhereParameters.Add("ApprovalStatusCanceled", DbType.Int32, ((int)CConstValue.ApprovalStatus.Canceled).ToString());
            LinqDataSourceApproval.WhereParameters.Add("ApprovalStatusRejected", DbType.Int32, ((int)CConstValue.ApprovalStatus.Rejected).ToString());
            LinqDataSourceApproval.Where = "ApprovalStep = null && IsApprovalRequest == @IsApprovalRequest && (ApprovalUser = Supervisor) && Supervisor == @CurrentUserId && (ApprovalStatus != @ApprovalStatusCanceled && ApprovalStatus != @ApprovalStatusRejected)";
        }

        //public Color GetColorNext(Random r)
        //{

        //    // to create lighter colours:
        //    // take a random integer between 0 & 128 (rather than between 0 and 255)
        //    // and then add 127 to make the colour lighter
        //    byte[] colorBytes = new byte[3];
        //    colorBytes[0] = (byte)(r.Next(128) + 127);
        //    colorBytes[1] = (byte)(r.Next(128) + 127);
        //    colorBytes[2] = (byte)(r.Next(128) + 127);

        //    return Color.FromArgb(colorBytes[0], colorBytes[1], colorBytes[2]);
        //}

        //private void GetChart()
        //{
        //var random = new Random();

        //var userList = (new CUser()).GetUserCount();
        //FunnelSeries fsUser = (FunnelSeries)RadHtmlChartUsers.PlotArea.Series[0];
        //for (int i = 0; i < userList.Count; i++)
        //{
        //    fsUser.SeriesItems.Add(new FunnelSeriesItem(userList[i].Count, userList[i].Name, GetColorNext(random)));
        //}

        //var studentList = (new CStudent()).GetStudentCount();
        //FunnelSeries fsStudent = (FunnelSeries)RadHtmlChartStudents.PlotArea.Series[0];
        //for (int i = 0; i < studentList.Count; i++)
        //{
        //    fsStudent.SeriesItems.Add(new FunnelSeriesItem(studentList[i].Count, studentList[i].Name, GetColorNext(random)));
        //}
        //}

        //        protected void RadGridApprovalHistory_OnItemDataBound(object sender, GridItemEventArgs e)
        //        {
        //if (e.Item is GridDataItem)
        //{
        //    var dataItem = (GridDataItem)e.Item;

        //    int menuId = Convert.ToInt32(dataItem.GetDataKeyValue("MenuSeqId").ToString());

        //    string no = string.Empty;
        //    string approvalType = string.Empty;
        //    DateTime? requestedDate = null;
        //    int? createdId = null;
        //    int? approvalStatus = null;
        //    string requestedUser = string.Empty;

        //    switch (Convert.ToInt32(dataItem.GetDataKeyValue("ApproveType").ToString()))
        //    {
        //        case (int)CConstValue.Approval.Basic:
        //            approvalType = "Basic";
        //            break;

        //        case (int)CConstValue.Approval.Agency:
        //            approvalType = "Agency";
        //            var agency = new CAgency().Get(menuId);
        //            if (agency != null)
        //            {
        //                no = agency.AgencyId.ToString();
        //                createdId = agency.CreatedId;
        //                requestedDate = agency.CreatedDate;
        //                approvalStatus = agency.ApprovalStatus;
        //            }
        //            break;

        //        case (int)CConstValue.Approval.Scholarship:
        //            approvalType = "Scholaship";
        //            var scholarship = new CScholarship().Get(menuId);
        //            if (scholarship != null)
        //            {
        //                no = scholarship.ScholarshipMasterNo;
        //                createdId = scholarship.CreatedId;
        //                requestedDate = scholarship.CreatedDate;
        //                approvalStatus = scholarship.ApprovalStatus;
        //            }
        //            break;

        //        case (int)CConstValue.Approval.Refund:
        //            approvalType = "Refund";
        //            var refund = new CRefund().Get(menuId);
        //            if (refund != null)
        //            {
        //                no = refund.RefundId.ToString();
        //                createdId = refund.CreatedId;
        //                requestedDate = refund.CreatedDate;
        //                approvalStatus = refund.ApprovalStatus;
        //            }
        //            break;

        //        case (int)CConstValue.Approval.CorporateCreditCard:
        //            approvalType = "Corporate Credit Card";
        //            var corporateCreditCard = new CCorporateCreditCard().Get(menuId);
        //            if (corporateCreditCard != null)
        //            {
        //                createdId = corporateCreditCard.CreatedId;
        //                requestedDate = corporateCreditCard.CreatedDate;
        //                approvalStatus = corporateCreditCard.ApprovalStatus;
        //            }
        //            break;

        //        case (int)CConstValue.Approval.BusinessTrip:
        //            approvalType = "BusinessTrip";
        //            var businessTrip = new CBusinessTrip().Get(menuId);
        //            if (businessTrip != null)
        //            {
        //                no = businessTrip.BusinessTripId.ToString();
        //                createdId = businessTrip.CreatedId;
        //                requestedDate = businessTrip.CreatedDate;
        //                approvalStatus = businessTrip.ApprovalStatus;
        //            }
        //            break;

        //        case (int)CConstValue.Approval.PackageProgram:
        //            approvalType = "PakcageProgram";
        //            var packageProgram = new CPackageProgram().GetPackageProgram(menuId);
        //            if (packageProgram != null)
        //            {
        //                no = packageProgram.PackageProgramId.ToString();
        //                createdId = packageProgram.CreatedId;
        //                requestedDate = packageProgram.CreatedDate;
        //                approvalStatus = packageProgram.ApprovalStatus;
        //            }
        //            break;

        //        case (int)CConstValue.Approval.PurchaseOrder:
        //            approvalType = "PurchaseOrder";
        //            var purchaseOrder = new CPurchaseOrder().Get(menuId);
        //            if (purchaseOrder != null)
        //            {
        //                no = purchaseOrder.PurchaseOrderId.ToString();
        //                createdId = purchaseOrder.CreatedId;
        //                requestedDate = purchaseOrder.CreatedDate;
        //                approvalStatus = purchaseOrder.ApprovalStatus;
        //            }
        //            break;

        //        case (int)CConstValue.Approval.Expense:
        //            approvalType = "Expense";
        //            var expense = new CExpense().Get(menuId);
        //            if (expense != null)
        //            {
        //                no = expense.ExpenseId.ToString();
        //                createdId = expense.CreatedId;
        //                requestedDate = expense.CreatedDate;
        //                approvalStatus = expense.ApprovalStatus;
        //            }
        //            break;

        //        case (int)CConstValue.Approval.Hire:
        //            approvalType = "Hire";
        //            var hire = new CHire().Get(menuId);
        //            if (hire != null)
        //            {
        //                no = hire.HireId.ToString();
        //                createdId = hire.CreatedId;
        //                requestedDate = hire.CreatedDate;
        //                approvalStatus = hire.ApprovalStatus;
        //            }
        //            break;

        //        case (int)CConstValue.Approval.Promotion:
        //            approvalType = "Promotion";
        //            var promotion = new CPromotion().Get(menuId);
        //            if (promotion != null)
        //            {
        //                no = promotion.PromotionMasterNo;
        //                createdId = promotion.CreatedId;
        //                requestedDate = promotion.CreatedDate;
        //                approvalStatus = promotion.ApprovalStatus;
        //            }
        //            break;

        //        case (int)CConstValue.Approval.Vacation:
        //            approvalType = "Vacation";
        //            var vacation = new CVacation().Get(menuId);
        //            if (vacation != null)
        //            {
        //                no = vacation.VacationId.ToString();
        //                createdId = vacation.CreatedId;
        //                requestedDate = vacation.CreatedDate;
        //                approvalStatus = vacation.ApprovalStatus;
        //            }
        //            break;

        //        case (int)CConstValue.Approval.CreditMemoPayout:
        //            approvalType = "CreditMemo";
        //            var creditMemo = new CCreditMemoPayout().Get(menuId);
        //            if (creditMemo != null)
        //            {
        //                no = creditMemo.CreditMemoId.ToString();
        //                createdId = creditMemo.CreatedId;
        //                requestedDate = creditMemo.CreatedDate;
        //                approvalStatus = creditMemo.ApprovalStatus;
        //            }
        //            break;
        //    }

        //    // init
        //    dataItem["RequestedDate"].Text = string.Empty;
        //    dataItem["UserName"].Text = string.Empty;
        //    dataItem["Status"].Text = string.Empty;

        //    // set
        //    if (createdId != null)
        //    {
        //        var cUser = new CUser();
        //        var c = cUser.Get((int)createdId);
        //        requestedUser = cUser.GetUserName(c);

        //        dataItem["RequestedDate"].Text = CGlobal.GetDateFormat(requestedDate.Value);
        //        dataItem["UserName"].Text = requestedUser;

        //        if (approvalStatus != null)
        //        {
        //            var cDict = new CDict().GetDictByTypeAndValue(217, (int)approvalStatus);
        //            if (cDict != null)
        //            {
        //                dataItem["Status"].Text = cDict.Name;
        //            }
        //        }
        //    }
        //    dataItem["ApprovalType"].Text = approvalType;
        //    dataItem["MenuSeqId"].Text = no;
        //}
        //else if (e.Item is GridFooterItem)
        //{
        //    var footer = (GridFooterItem)e.Item;
        //    //(footer["AgencyPrice"].FindControl("TotalAgency") as RadNumericTextBox).Value = double.Parse(agencySum.ToString());
        //}
        //        }

        protected void RadGridApprovalHistory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(((RadGrid)sender).SelectedValues["TypeId"]))
            {
                case (int)CConstValue.Approval.Basic:
                    break;
                case (int)CConstValue.Approval.Agency:
                    Response.Redirect("~/Agency", true);
                    break;
                case (int)CConstValue.Approval.Scholarship:
                    Response.Redirect("~/Scholarship", true);
                    break;
                case (int)CConstValue.Approval.Refund:
                    Response.Redirect("~/Refund", true);
                    break;
                case (int)CConstValue.Approval.CorporateCreditCard:
                    Response.Redirect("~/CorporateCreditCard", true);
                    break;
                case (int)CConstValue.Approval.BusinessTrip:
                    Response.Redirect("~/BusinessTrip", true);
                    break;
                case (int)CConstValue.Approval.Package:
                    Response.Redirect("~/PackageProgram", true);
                    break;
                case (int)CConstValue.Approval.PurchaseOrder:
                    Response.Redirect("~/PurchaseOrder", true);
                    break;
                case (int)CConstValue.Approval.Expense:
                    Response.Redirect("~/Expense", true);
                    break;
                case (int)CConstValue.Approval.Homestay:
                    Response.Redirect("~/Homestay", true);
                    break;
                case (int)CConstValue.Approval.Hire:
                    Response.Redirect("~/Hire", true);
                    break;
                case (int)CConstValue.Approval.Promotion:
                    Response.Redirect("~/Promotion", true);
                    break;
                case (int)CConstValue.Approval.Vacation:
                    Response.Redirect("~/Vacation", true);
                    break;
                case (int)CConstValue.Approval.CreditMemoPayout:
                    Response.Redirect("~/CreditMemo", true);
                    break;
            }

        }

        protected void GridBulletinBoard_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            RunClientScript("ShowPop('" + GridBulletinBoard.SelectedValue + "', '1');");
        }

        protected void RadToolBarBulletinBoard_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "New post")
            {
                RunClientScript("ShowPop('" + CurrentUserId + "', '0');");
            }
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            GridBulletinBoard.Rebind();
        }

        protected void GridBulletinBoard_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridRequest_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
                RadToolBarBulletinBoard.FindItemByText("New post").Enabled = false;
        }

        protected void RadGridApproval_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}