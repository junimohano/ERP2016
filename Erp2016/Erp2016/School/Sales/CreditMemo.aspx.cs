using System;
using System.Data;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Sales
{
    public partial class CreditMemo : PageBase
    {
        public CreditMemo() : base((int)CConstValue.Menu.CreditMemo)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // todo: here
                var excel = RadToolBarCreditMemo.FindItemByText("Excel");
                if (CurrentGroupId == (int)CConstValue.UserGroupForAccountExcelExport.Accounting ||
                    CurrentGroupId == (int)CConstValue.UserGroupForAccountExcelExport.IT)
                {
                    excel.Visible = true;
                }

                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.CreditMemo);
                FileDownloadList1.SetVisibieUploadControls(false);

                CreditMemoPayout1.SetCreditVisible(false);
            }

            SearchCreditmemo();
            SearchCreditmemoPayout();
            SearchCreditmemoPayoutHistory();
        }

        protected void RadGridCreditMemo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                if (dataItem["OriginalCreditMemoAmount"].Text.Contains("-"))
                    (dataItem["OriginalCreditMemoAmount"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
                if (dataItem["AvailableCreditAmount"].Text.Contains("-"))
                    (dataItem["AvailableCreditAmount"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
            }
        }

        protected void RadGridCreditMemoHistory_PreRender(object sender, EventArgs e)
        {
            LinqDataSource2.WhereParameters.Clear();
            if (RadGridCreditMemo.SelectedValue == null)
                LinqDataSource2.WhereParameters.Add("CreditMemoId", DbType.Int32, "0");
            else
                LinqDataSource2.WhereParameters.Add("CreditMemoId", DbType.Int32, RadGridCreditMemo.SelectedValue.ToString());
            LinqDataSource2.Where = "CreditMemoId == @CreditMemoId";
        }

        protected void RadGridCreditMemoHistory_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                if ((dataItem["CreditAmount"].FindControl("lblCreditAmount") as Label).Text.Contains("-"))
                    (dataItem["CreditAmount"].FindControl("lblCreditAmount") as Label).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
            }
        }

        private void RefreshApprovalList()
        {
            LinqDataSource LinqDataSourceApprovalList = ApprovalLine1.GetSqlDataSourceApprovalList();
            if (LinqDataSourceApprovalList != null)
            {
                LinqDataSourceApprovalList.WhereParameters.Clear();
                if (RadGridCreditMemoPayout.SelectedValue == null)
                    LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, "0");
                else
                    LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, RadGridCreditMemoPayout.SelectedValue.ToString());
                LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.CreditMemoPayout).ToString());
                LinqDataSourceApprovalList.Where = "MenuSeqId == @MenuSeqId AND ApproveType == @ApproveType";
            }
        }

        protected void Refresh(object sender, EventArgs e)
        {
            RadGridCreditMemo.Rebind();
            RadGridCreditMemoPayout.Rebind();
            RefreshApprovalList();
        }

        protected void RadToolBarCreditMemo_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (RadGridCreditMemo.SelectedValue != null)
            {
                switch (e.Item.Text)
                {
                    case "Disbursement":
                        var cCreditMemoPayout = new CCreditMemoPayout();
                        var creditMemoPayoutList = cCreditMemoPayout.GetCreditMemoPayoutList(Convert.ToInt32(RadGridCreditMemo.SelectedValue));
                        var currentDate = DateTime.Now;
                        foreach (var creditMemoPayout in creditMemoPayoutList)
                        {
                            if (creditMemoPayout.Disbursement == false && creditMemoPayout.ApprovalStatus == (int)CConstValue.ApprovalStatus.Approved)
                            {
                                creditMemoPayout.Disbursement = true;
                                creditMemoPayout.DisbursementDate = currentDate;
                                creditMemoPayout.UpdatedId = CurrentUserId;
                                creditMemoPayout.UpdatedDate = currentDate;
                                cCreditMemoPayout.Update(creditMemoPayout);
                            }
                        }
                        RadGridCreditMemo.Rebind();
                        RadGridCreditMemoPayout.Rebind();
                        break;

                    case "View Invoice":
                        RunClientScript("ShowInvoiceWindow(" + RadGridCreditMemo.SelectedValues["InvoiceId"] + ");");
                        break;

                    case "View Payment":
                        RunClientScript("ShowPaymentHistoryWindow(" + RadGridCreditMemo.SelectedValues["InvoiceId"] + ");");
                        break;

                    case "View Deposit":
                        var cPayment = new CPayment();
                        var payment = cPayment.Get(Convert.ToInt32(RadGridCreditMemo.SelectedValues["PaymentId"]));
                        if (payment != null)
                        {
                            var depositPayment = new CDepositPayment().GetByPaymentId(payment.PaymentId);
                            if (depositPayment != null)
                                RunClientScript("ShowDepositPaymentWindow(" + depositPayment.DepositId + ");");
                        }

                        break;

                    case "Student Page":
                        Response.Redirect("~/Student?id=" + RadGridCreditMemo.SelectedValues["StudentId"]);
                        break;
                    case "Invoice Page":
                        Response.Redirect("~/Invoice?id=" + RadGridCreditMemo.SelectedValues["StudentId"]);
                        break;
                    case "Payment Page":
                        Response.Redirect("~/Payment?id=" + RadGridCreditMemo.SelectedValues["StudentId"]);
                        break;
                    case "Deposit Page":
                        Response.Redirect("~/Deposit?id=" + RadGridCreditMemo.SelectedValues["StudentId"]);
                        break;
                    case "Refund Page":
                        Response.Redirect("~/Refund?id=" + RadGridCreditMemo.SelectedValues["StudentId"]);
                        break;
                }
            }
        }

        protected void RadToolBarCreditMemoPayout_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "New Payout":
                    if (RadGridCreditMemo.SelectedValue != null)
                        RunClientScript("ShowPayoutPop('" + RadGridCreditMemo.SelectedValue + "', '0');");
                    break;
                case "Request":
                    if (RadGridCreditMemoPayout.SelectedValue != null)
                        RunClientScript("ShowPayoutPop('" + RadGridCreditMemoPayout.SelectedValue + "', '1');");
                    break;

                case "Approve":
                    if (RadGridCreditMemoPayout.SelectedValue != null)
                        RunClientScript("ShowApprovalWindow('" + RadGridCreditMemoPayout.SelectedValue + "');");
                    break;

                case "Reject":
                    if (RadGridCreditMemoPayout.SelectedValue != null)
                        RunClientScript("ShowApprovalRejectWindow('" + RadGridCreditMemoPayout.SelectedValue + "');");
                    break;
                case "Revise":
                    if (RadGridCreditMemoPayout.SelectedValue != null)
                        RunClientScript("ShowApprovalReviseWindow('" + RadGridCreditMemoPayout.SelectedValue + "');");
                    break;
                case "Cancel":
                    if (RadGridCreditMemoPayout.SelectedValue != null)
                        RunClientScript("ShowApprovalCancelWindow('" + RadGridCreditMemoPayout.SelectedValue + "');");
                    break;
            }
        }

        protected void RadGridCreditMemoPayout_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                if ((dataItem["Amount"].FindControl("lblAmount") as Label).Text.Contains("-"))
                    (dataItem["Amount"].FindControl("lblAmount") as Label).Style["color"] = CConstValue.NagativeColorName;
                if ((dataItem["AvailableAmount"].FindControl("lblAvailableAmount") as Label).Text.Contains("-"))
                    (dataItem["AvailableAmount"].FindControl("lblAvailableAmount") as Label).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
            }
        }

        protected void RadGridCreditMemo_OnPreRender(object sender, EventArgs e)
        {
            if (ViewState["CreditMemoId"] != null)
            {
                foreach (GridDataItem item in RadGridCreditMemo.Items)
                {
                    if (item.GetDataKeyValue("CreditMemoId").ToString() == ViewState["CreditMemoId"].ToString())
                    {
                        if (item.Selected == false)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        protected void RadGridCreditMemo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["CreditMemoId"] = RadGridCreditMemo.SelectedValue;

            SearchCreditmemoPayout();
            SearchCreditmemoPayoutHistory();
        }

        public void GetCreditMemoPayoutInfo()
        {
            // toolbar
            foreach (RadToolBarItem toolbarItem in RadToolBarCreditMemo.Items)
            {
                if (toolbarItem.Text == "Disbursement")
                {
                    if (RadGridCreditMemo.SelectedValue != null)
                    {
                        var creditMemo = new CCreditMemo().GetVwCreditMemo(Convert.ToInt32(RadGridCreditMemo.SelectedValue));
                        if (creditMemo != null)
                        {
                            var cCreditMemoPayout = new CCreditMemoPayout();
                            var creditMemoPayoutList = cCreditMemoPayout.GetCreditMemoPayoutList(creditMemo.CreditMemoId);
                            bool hasNullOfDisbursement = false;
                            foreach (var c in creditMemoPayoutList)
                            {
                                if (c.Disbursement == false && c.ApprovalStatus == (int)CConstValue.ApprovalStatus.Approved)
                                {
                                    toolbarItem.Enabled = true;
                                    hasNullOfDisbursement = true;
                                    break;
                                }
                            }

                            if (hasNullOfDisbursement == false)
                                toolbarItem.Enabled = false;
                        }
                        else
                            toolbarItem.Enabled = false;
                    }
                    else
                        toolbarItem.Enabled = false;
                }
                else
                {
                    if (toolbarItem.Text == "View Invoice" || toolbarItem.Text == "View Payment" || toolbarItem.Text == "View Deposit" ||
                      toolbarItem.Text == "Student Page" || toolbarItem.Text == "Invoice Page" || toolbarItem.Text == "Payment Page" ||
                      toolbarItem.Text == "Deposit Page" || toolbarItem.Text == "Refund Page" || toolbarItem.Text == "Excel")
                        continue;

                    toolbarItem.Enabled = false;
                }
            }

            // toolbar
            foreach (RadToolBarItem toolbarItem in RadToolBarCreditMemoPayout.Items)
            {
                if (toolbarItem.Text == "New Payout")
                {
                    if (RadGridCreditMemo.SelectedValue != null)
                        toolbarItem.Enabled = true;
                    else
                        toolbarItem.Enabled = false;
                }
            }

            if (RadGridCreditMemoPayout.SelectedValue != null)
            {
                var cCreditMemoPayout = new CCreditMemoPayout();
                var creditMemoPayout = cCreditMemoPayout.Get(Convert.ToInt32(RadGridCreditMemoPayout.SelectedValue));
                CreditMemoPayout1.SetData(creditMemoPayout);

                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBarCreditMemoPayout.Items)
                {
                    // request active check
                    if (toolbarItem.Text == "Request")
                    {
                        if ((creditMemoPayout.ApprovalStatus == null || creditMemoPayout.ApprovalStatus == (int)CConstValue.ApprovalStatus.Revise) && creditMemoPayout.CreatedId == CurrentUserId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                    else if (toolbarItem.Text == "Cancel")
                    {
                        if ((creditMemoPayout.ApprovalStatus == null ||
                            creditMemoPayout.ApprovalStatus == (int)CConstValue.ApprovalStatus.Revise ||
                            creditMemoPayout.ApprovalStatus == (int)CConstValue.ApprovalStatus.Requested) && creditMemoPayout.CreatedId == CurrentUserId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                    else if (toolbarItem.Text == "Approve" || toolbarItem.Text == "Reject" || toolbarItem.Text == "Revise")
                    {
                        // approve active check
                        var refundApproveInfo = new CGlobal();
                        var supervisor = refundApproveInfo.CheckApprovalEnable((int)CConstValue.Approval.CreditMemoPayout, Convert.ToInt32(RadGridCreditMemoPayout.SelectedValue));

                        // supervisor or loy employees
                        if ((CurrentUserId == supervisor)
                            && creditMemoPayout.ApprovalStatus != (int)CConstValue.ApprovalStatus.Approved
                            && creditMemoPayout.ApprovalStatus != (int)CConstValue.ApprovalStatus.Rejected
                            && creditMemoPayout.ApprovalStatus != (int)CConstValue.ApprovalStatus.Canceled
                            && creditMemoPayout.ApprovalStatus != null
                            && CurrentUserId != creditMemoPayout.ApprovalId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                }

                foreach (RadToolBarItem toolbarItem in RadToolBarCreditMemoPayoutHistory.Items)
                {
                    if (toolbarItem.Text == "Add Payout")
                    {
                        if (creditMemoPayout.ApprovalStatus == (int)CConstValue.ApprovalStatus.Approved &&
                            creditMemoPayout.Disbursement &&
                            (CurrentGroupId == (int)CConstValue.UserGroupForCreditPayoutHistory.Accounting ||
                            CurrentGroupId == (int)CConstValue.UserGroupForCreditPayoutHistory.IT))
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                    else if (toolbarItem.Text == "Payout Reverse")
                    {
                        if (CurrentGroupId == (int)CConstValue.UserGroupForCreditPayoutHistory.Accounting ||
                            CurrentGroupId == (int)CConstValue.UserGroupForCreditPayoutHistory.IT)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                }

                // get DownloadList
                FileDownloadList1.GetFileDownload(Convert.ToInt32(RadGridCreditMemoPayout.SelectedValue));
            }
            else
            {
                foreach (RadToolBarItem toolbarItem in RadToolBarCreditMemoPayoutHistory.Items)
                {
                    toolbarItem.Enabled = false;
                }

                // get DownloadList
                FileDownloadList1.GetFileDownload(0);
            }

            RefreshApprovalList();
        }

        protected void RadGridCreditMemo_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridCreditMemoCreditHistory_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridCreditMemoPayout_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBarCreditMemo.Items)
                {
                    if (toolbarItem.Text == "View Invoice" || toolbarItem.Text == "View Payment" || toolbarItem.Text == "View Deposit" ||
                        toolbarItem.Text == "Student Page" || toolbarItem.Text == "Invoice Page" || toolbarItem.Text == "Deposit Page" ||
                        toolbarItem.Text == "CreditMemo Page" || toolbarItem.Text == "Refund Page" || toolbarItem.Text == "Excel") continue;

                    toolbarItem.Enabled = false;
                }

                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBarCreditMemoPayout.Items)
                {
                    toolbarItem.Enabled = false;
                }
            }
        }

        protected void RadDropDownListView_OnSelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            ViewState["SelectedTextView"] = e.Text;
            SearchCreditmemoPayout();
        }

        private void SearchCreditmemo()
        {
            LinqDataSourceCreditMemo.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteLocationList)
                LinqDataSourceCreditMemo.WhereParameters.Add(model.SiteLocationIdName, DbType.Int32, model.SiteLocationId.ToString());
            LinqDataSourceCreditMemo.Where = UserPermissionModel.SearchWhereSiteLocationSb.ToString();

            var studentId = Request["id"];
            if (!string.IsNullOrEmpty(studentId))
            {
                LinqDataSourceCreditMemo.WhereParameters.Add("StudentId", DbType.Int32, studentId);
                if (LinqDataSourceCreditMemo.Where.Length > 0)
                    LinqDataSourceCreditMemo.Where += " && StudentId == @StudentId";
                else
                    LinqDataSourceCreditMemo.Where = "StudentId == @StudentId";

                if (!IsPostBack)
                {
                    RadGridCreditMemo.MasterTableView.Rebind();

                    foreach (GridDataItem item in RadGridCreditMemo.Items)
                    {
                        if (item.GetDataKeyValue("StudentId").ToString() == studentId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        private void SearchCreditmemoPayout()
        {
            LinqDataSourceCreditMemoPayout.WhereParameters.Clear();
            if (RadGridCreditMemo.SelectedValue == null)
                LinqDataSourceCreditMemoPayout.WhereParameters.Add("CreditMemoId", DbType.Int32, "0");
            else
                LinqDataSourceCreditMemoPayout.WhereParameters.Add("CreditMemoId", DbType.Int32, RadGridCreditMemo.SelectedValue.ToString());
            LinqDataSourceCreditMemoPayout.Where = "CreditMemoId == @CreditMemoId";
            SetViewType(LinqDataSourceCreditMemoPayout, RadGridCreditMemoPayout);
        }

        private void SearchCreditmemoPayoutHistory()
        {
            LinqDataSourceCreditMemoPayoutHistory.WhereParameters.Clear();
            if (RadGridCreditMemoPayout.SelectedValue == null || RadGridCreditMemo.SelectedValue == null)
            {
                LinqDataSourceCreditMemoPayoutHistory.WhereParameters.Add("CreditMemoPayoutId", DbType.Int32, "0");
                LinqDataSourceCreditMemoPayoutHistory.WhereParameters.Add("CreditMemoId", DbType.Int32, "0");
            }
            else {
                LinqDataSourceCreditMemoPayoutHistory.WhereParameters.Add("CreditMemoPayoutId", DbType.Int32, RadGridCreditMemoPayout.SelectedValue.ToString());
                LinqDataSourceCreditMemoPayoutHistory.WhereParameters.Add("CreditMemoId", DbType.Int32, RadGridCreditMemo.SelectedValue.ToString());
            }
            LinqDataSourceCreditMemoPayoutHistory.Where = "CreditMemoPayoutId == @CreditMemoPayoutId && CreditMemoId == @CreditMemoId";
        }

        protected void RadGridCreditMemoPayout_OnPreRender(object sender, EventArgs e)
        {
            GetCreditMemoPayoutInfo();
            SetVisibleModifyControllers();
        }

        protected void RadToolBarCreditMemoPayoutHistory_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Add Payout":
                    if (RadGridCreditMemoPayout.SelectedValue != null)
                        RunClientScript("ShowPayoutHistoryPop('" + RadGridCreditMemoPayout.SelectedValue + "', '0');");
                    break;

                case "Payout Reverse":
                    if (RadGridCreditMemoPayoutHistory.SelectedValue != null)
                    {
                        var creditMemoPayoutHistory = new CCreditMemoPayoutHistory().Get(Convert.ToInt32(RadGridCreditMemoPayoutHistory.SelectedValue));
                        if (creditMemoPayoutHistory.PayoutAmount > 0)
                        {
                            var cNewCreditMemoPayoutHistory = new CCreditMemoPayoutHistory();
                            var newCreditMemoPayoutHistory = new Erp2016.Lib.CreditMemoPayoutHistory();
                            CGlobal.Copy(creditMemoPayoutHistory, newCreditMemoPayoutHistory);
                            newCreditMemoPayoutHistory.PayoutAmount *= -1;
                            newCreditMemoPayoutHistory.OriginalCreditMemoPayoutHistoryId = creditMemoPayoutHistory.CreditMemoPayoutHistoryId;

                            cNewCreditMemoPayoutHistory.Add(newCreditMemoPayoutHistory);

                            RadGridCreditMemoPayout.Rebind();
                            RadGridCreditMemoPayoutHistory.Rebind();
                        }
                        else
                            ShowMessage("Negative price can't reverse");
                    }
                    break;
            }
        }

        protected void CreditMemoPayoutHistory_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                if ((dataItem["PayoutAmount"].FindControl("lblPayoutAmount") as Label).Text.Contains("-"))
                    (dataItem["PayoutAmount"].FindControl("lblPayoutAmount") as Label).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
            }
        }

        protected void CreditMemoPayoutHistory_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RefreshPayoutHistory(object sender, EventArgs e)
        {
            RadGridCreditMemo.Rebind();
            RadGridCreditMemoPayout.Rebind();
            RadGridCreditMemoPayoutHistory.Rebind();
        }

        protected void RadGridCreditMemoPayoutHistory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CCreditMemoPayoutHistory cCreditMemoPayoutHistory = new CCreditMemoPayoutHistory();
            var creditMemoPayoutHistory = cCreditMemoPayoutHistory.Get(Convert.ToInt32(RadGridCreditMemoPayoutHistory.SelectedValue));
            var reversePayoutHistory = cCreditMemoPayoutHistory.GetReverseCreditMemoPayoutHistory(creditMemoPayoutHistory.CreditMemoPayoutHistoryId);

            var btnReverse = RadToolBarCreditMemoPayoutHistory.FindItemByText("Payout Reverse");

            if (creditMemoPayoutHistory.OriginalCreditMemoPayoutHistoryId == null && reversePayoutHistory == null &&
                (CurrentGroupId == (int)CConstValue.UserGroupForCreditPayoutHistory.Accounting ||
                 CurrentGroupId == (int)CConstValue.UserGroupForCreditPayoutHistory.IT))
                btnReverse.Enabled = true;
            else
                btnReverse.Enabled = false;
        }

        protected void RadTabStript1_OnTabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.Text == "Payout History")
                RadGridCreditMemoPayoutHistory.Rebind();
            else if (e.Tab.Text == "Payout")
                RadGridCreditMemoPayout.Rebind();
        }

        protected void RadButtonExcel_OnClick(object sender, EventArgs e)
        {
            var filterExpression = ConvertFilterExpressionToSqlExpression(RadGridCreditMemo.MasterTableView.Columns);
            var tempDt = new CCreditMemo().GetVwCreditMemoExcel(filterExpression);
            ExportExcel(tempDt, "CreditMemo Detail");
        }
    }
}