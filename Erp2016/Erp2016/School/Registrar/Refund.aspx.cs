using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar
{
    public partial class Refund : PageBase
    {
        private LinqDataSource _sqlDataSourceInvoiceItemsNew;
        private RadGrid _radGridInvoiceItemsNew;

        public Refund() : base((int)CConstValue.Menu.Refund)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // find user control
            _sqlDataSourceInvoiceItemsNew = InvoiceItemGridNew.GetSqlDataSourceInvoiceItems();
            _radGridInvoiceItemsNew = InvoiceItemGridNew.GetRadGridInvoiceItems();
            _radGridInvoiceItemsNew.PreRender += _radGridInvoiceItemsNew_PreRender;
            InvoiceItemGridNew.SetEditMode(false);

            // init
            FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Refund);
            FileDownloadList1.SetVisibieUploadControls(false);

            if (!IsPostBack)
            {
            }

            SearchRefund();
        }

        private void _radGridInvoiceItemsNew_PreRender(object sender, EventArgs e)
        {
            _sqlDataSourceInvoiceItemsNew.WhereParameters.Clear();
            if (RadGridRefund.SelectedValue == null)
            {
                _sqlDataSourceInvoiceItemsNew.WhereParameters.Add("InvoiceId", DbType.Int32, 0.ToString());
            }
            else
            {
                var cRefund = new CRefund();
                var refund = cRefund.Get(Convert.ToInt32(RadGridRefund.SelectedValue));
                _sqlDataSourceInvoiceItemsNew.WhereParameters.Add("InvoiceId", DbType.Int32, refund.InvoiceId.ToString());
            }
            _sqlDataSourceInvoiceItemsNew.Where = "InvoiceId == @InvoiceId";

        }

        private void RefreshApprovalList()
        {
            LinqDataSource LinqDataSourceApprovalList = ApprovalLine1.GetSqlDataSourceApprovalList();
            if (LinqDataSourceApprovalList != null)
            {
                LinqDataSourceApprovalList.WhereParameters.Clear();
                if (RadGridRefund.SelectedValue == null)
                    LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, "0");
                else
                    LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, RadGridRefund.SelectedValue.ToString());
                LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.Refund).ToString());
                LinqDataSourceApprovalList.Where = "MenuSeqId == @MenuSeqId AND ApproveType == @ApproveType";
            }
        }

        protected void Refresh(object sender, EventArgs e)
        {
            RadGridRefund.Rebind();
        }

        protected void RadGridRefund_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                //if (dataItem["DepositAmount"].Text.Contains("-"))
                //    (dataItem["DepositAmount"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
            }
        }

        protected void RadToolBarApproval_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (RadGridRefund.SelectedValue != null)
            {
                switch (e.Item.Text)
                {
                    case "Request":
                        RunClientScript("ShowPop('" + RadGridRefund.SelectedValue + "', '1');");
                        break;
                    case "Approve":
                        RunClientScript("ShowApprovalWindow('" + RadGridRefund.SelectedValue + "');");
                        break;
                    case "Reject":
                        RunClientScript("ShowApprovalRejectWindow('" + RadGridRefund.SelectedValue + "');");
                        break;
                    case "Revise":
                        RunClientScript("ShowApprovalReviseWindow('" + RadGridRefund.SelectedValue + "');");
                        break;
                    case "Cancel":
                        RunClientScript("ShowApprovalCancelWindow('" + RadGridRefund.SelectedValue + "');");
                        break;
                    case "View Original Invoice":
                        var cRefund = new CRefund();
                        var refund = cRefund.Get(Convert.ToInt32(RadGridRefund.SelectedValue));
                        var cInvoice = new CInvoice();
                        var invoice = cInvoice.Get(refund.InvoiceId);
                        if (invoice.OriginalInvoiceId != null)
                            RunClientScript("ShowInvoiceWindow(" + invoice.OriginalInvoiceId + ");");
                        break;
                    case "Student Page":
                        Response.Redirect("~/Student?id=" + RadGridRefund.SelectedValues["StudentId"]);
                        break;
                    case "Invoice Page":
                        Response.Redirect("~/Invoice?id=" + RadGridRefund.SelectedValues["StudentId"]);
                        break;
                    case "Payment Page":
                        Response.Redirect("~/Payment?id=" + RadGridRefund.SelectedValues["StudentId"]);
                        break;
                    case "Deposit Page":
                        Response.Redirect("~/Deposit?id=" + RadGridRefund.SelectedValues["StudentId"]);
                        break;
                    case "CreditMemo Page":
                        Response.Redirect("~/CreditMemo?id=" + RadGridRefund.SelectedValues["StudentId"]);
                        break;
                }
            }
        }

        protected void RadGridRefund_OnPreRender(object sender, EventArgs e)
        {
            if (ViewState["RefundId"] != null)
            {
                foreach (GridDataItem item in RadGridRefund.Items)
                {
                    if (item.GetDataKeyValue("RefundId").ToString() == ViewState["RefundId"].ToString())
                    {
                        if (item.Selected == false)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }

            // toolbar
            foreach (RadToolBarItem toolbarItem in RadToolBarApproval.Items)
            {
                if (RadGridRefund.SelectedValue != null)
                {
                    var cRefund = new CRefund();
                    var refund = cRefund.Get(Convert.ToInt32(RadGridRefund.SelectedValue.ToString()));

                    // request active check
                    if (toolbarItem.Text == "Request")
                    {
                        if ((refund.ApprovalStatus == null || refund.ApprovalStatus == (int)CConstValue.ApprovalStatus.Revise) && refund.CreatedId == CurrentUserId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                    else if (toolbarItem.Text == "Cancel")
                    {
                        if ((refund.ApprovalStatus == null ||
                            refund.ApprovalStatus == (int)CConstValue.ApprovalStatus.Revise ||
                            refund.ApprovalStatus == (int)CConstValue.ApprovalStatus.Requested) && refund.CreatedId == CurrentUserId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                    else if (toolbarItem.Text == "Approve" || toolbarItem.Text == "Reject")
                    {
                        // approve active check
                        var refundApproveInfo = new CGlobal();
                        var supervisor = refundApproveInfo.CheckApprovalEnable((int)CConstValue.Approval.Refund, Convert.ToInt32(refund.RefundId));

                        // supervisor or loy employees
                        if ((CurrentUserId == supervisor)
                            && refund.ApprovalStatus != (int)CConstValue.ApprovalStatus.Approved
                            && refund.ApprovalStatus != (int)CConstValue.ApprovalStatus.Rejected
                            && refund.ApprovalStatus != (int)CConstValue.ApprovalStatus.Canceled
                            && refund.ApprovalStatus != null
                            && CurrentUserId != refund.ApprovalId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                }
                else
                {
                    if (toolbarItem.Text == "View" ||
                        toolbarItem.Text == "View Original Invoice" ||
                        toolbarItem.Text == "Student Page" ||
                        toolbarItem.Text == "Invoice Page" ||
                        toolbarItem.Text == "Payment Page" ||
                        toolbarItem.Text == "Deposit Page" ||
                        toolbarItem.Text == "CreditMemo Page")
                        continue;
                    toolbarItem.Enabled = false;
                }
            }

            RefreshApprovalList();

        }

        protected void FileDownloadList1_OnPreRender(object sender, EventArgs e)
        {
            if (RadGridRefund.SelectedValue != null)
            {
                FileDownloadList1.GetFileDownload(Convert.ToInt32(RadGridRefund.SelectedValue));
            }
        }

        protected void CreditMemoPayout1_OnPreRender(object sender, EventArgs e)
        {
            if (RadGridRefund.SelectedValue != null)
            {
                var cCreditMemoPayout = new CCreditMemoPayout();
                var creditMemoPayout = cCreditMemoPayout.Get(Convert.ToInt32(RadGridRefund.SelectedValues["CreditMemoPayoutId"]));
                if (creditMemoPayout != null)
                    CreditMemoPayout1.SetData(creditMemoPayout);
            }
        }

        protected void RadGridRefund_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["RefundId"] = RadGridRefund.SelectedValue;
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBarApproval.Items)
                {
                    if (toolbarItem.Text == "View Original Invoice") continue;

                    toolbarItem.Enabled = false;
                }
            }
        }

        protected void RadDropDownListView_OnSelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            ViewState["SelectedTextView"] = e.Text;
            SearchRefund();
        }

        private void SearchRefund()
        {
            LinqDataSourceRefund.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteLocationList)
                LinqDataSourceRefund.WhereParameters.Add(model.SiteLocationIdName, DbType.Int32, model.SiteLocationId.ToString());
            LinqDataSourceRefund.Where = UserPermissionModel.SearchWhereSiteLocationSb.ToString();
            SetViewType(LinqDataSourceRefund, RadGridRefund);

            var studentId = Request["id"];
            if (!IsPostBack && !string.IsNullOrEmpty(studentId))
            {
                LinqDataSourceRefund.WhereParameters.Add("StudentId", DbType.Int32, studentId);
                if (LinqDataSourceRefund.Where.Length > 0)
                    LinqDataSourceRefund.Where += " && StudentId == @StudentId";
                else
                    LinqDataSourceRefund.Where = "StudentId == @StudentId";

                RadGridRefund.MasterTableView.Rebind();

                foreach (GridDataItem item in RadGridRefund.Items)
                {
                    if (item.GetDataKeyValue("StudentId").ToString() == studentId)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }

        protected void RadGridRefund_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}