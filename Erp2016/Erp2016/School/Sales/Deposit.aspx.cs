using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Sales
{
    public partial class Deposit : PageBase
    {
        private LinqDataSource _linqDataSourceDepositPayment;
        private RadGrid _radGridDepositPayment;

        public Deposit() : base((int)CConstValue.Menu.Deposit)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // DepositPaymentGrid control
            _linqDataSourceDepositPayment = DepositPaymentGrid1.GetLinqDataSourceDepositPayment();
            _radGridDepositPayment = DepositPaymentGrid1.GetRadGridDepositPayment();
            _radGridDepositPayment.RowDrop += _radGridDepositPayment_RowDrop;

            // init
            FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Deposit);
            FileDownloadList1.SetVisibieUploadControls(false);


            if (!IsPostBack)
            {
                // todo: here fix
                var excel = DepositInfoToolbar.FindItemByText("Excel");
                if (CurrentGroupId == (int)CConstValue.UserGroupForAccountExcelExport.Accounting ||
                    CurrentGroupId == (int)CConstValue.UserGroupForAccountExcelExport.IT)
                {
                    excel.Visible = true;
                }
            }

            DepositListSearch();
        }

        private void _radGridDepositPayment_RowDrop(object sender, GridDragDropEventArgs e)
        {
            //if (e.DraggedItems.Count != 0 && RadGridDepositList.SelectedValue != null && e.DestinationGrid == RadGridUnDepositPayment)
            if (e.DraggedItems.Count != 0 && RadGridDepositList.SelectedValue != null)
            {
                foreach (var dataItem in e.DraggedItems)
                {
                    var cPayDeposit = new CDepositPayment();
                    var payDeposit = cPayDeposit.Get(Convert.ToInt32(dataItem.GetDataKeyValue("DepositPaymentId").ToString()));
                    if (cPayDeposit.Delete(payDeposit))
                    {
                    }
                }

                ShowMessage("Completed deleting payment(s).");
            }
            else
            {
                ShowMessage("Failed adding payment(s).");
            }
        }

        protected void RadGridDepositList_PreRender(object sender, EventArgs e)
        {
            if (ViewState["DepositId"] != null)
            {
                foreach (GridDataItem item in RadGridDepositList.Items)
                {
                    if (item.GetDataKeyValue("DepositId").ToString() == ViewState["DepositId"].ToString())
                    {
                        if (item.Selected == false)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
            GetDepositDetail();
            SetVisibleModifyControllers();
        }

        protected void RadGridDepositList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["DepositId"] = RadGridDepositList.SelectedValue;
        }

        protected void RadGridDepositList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                if (dataItem["DepositAmount"].Text.Contains("-"))
                    (dataItem["DepositAmount"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
            }
        }

        protected void DepositInfoToolbar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Confirm" && RadGridDepositList.SelectedValue != null)
            {
                var cDeposit = new CDeposit();
                var deposit = cDeposit.Get(Convert.ToInt32(RadGridDepositList.SelectedValue.ToString()));

                // HQ
                if (CurrentGroupId == (int)CConstValue.UserGroupForDepositConfirm.Accounting)
                {
                    deposit.Comment = DepositInfomation1.GetComment();
                    deposit.Bank = DepositInfomation1.GetBank();
                    deposit.DepositDate = DepositInfomation1.GetDepositDate();

                    deposit.Status = 3; // 1:Pending 2:Created, 3:Confirm, 0:Confirm Canceled 
                    deposit.HQConfirmUserId = CurrentUserId;
                    deposit.HQConfirmDate = DateTime.Now;

                    if (cDeposit.Update(deposit))
                    {
                        var cCreaditMemo = new CCreditMemo();
                        cCreaditMemo.ValidateOverPaid(deposit.DepositId, CurrentUserId);

                        RadGridDepositList.Rebind();
                    }
                }
                //schools
                else
                {
                    deposit.Comment = DepositInfomation1.GetComment();
                    deposit.Bank = DepositInfomation1.GetBank();
                    deposit.DepositDate = DepositInfomation1.GetDepositDate();

                    deposit.Status = 2; // 1:Pending, 2:Created, 3:Confirm, 0:Confirm Canceled 

                    if (cDeposit.Update(deposit))
                    {
                        RadGridDepositList.Rebind();
                    }
                }
            }
            else if (e.Item.Text == "Cancel" && RadGridDepositList.SelectedValue != null)
            {
                var cDeposit = new CDeposit();
                var deposit = cDeposit.Get(Convert.ToInt32(RadGridDepositList.SelectedValue.ToString()));

                deposit.Comment = DepositInfomation1.GetComment();
                deposit.Bank = DepositInfomation1.GetBank();
                deposit.DepositDate = DepositInfomation1.GetDepositDate();

                if (CurrentGroupId == (int)CConstValue.UserGroupForDepositConfirm.Accounting) // HQ
                    deposit.Status = 1; // 1:Pending, 2:Created, 3:Confirm, 0:Confirm Canceled 
                else
                    deposit.Status = 0; // 1:Pending, 2:Created, 3:Confirm, 0:Confirm Canceled 

                if (cDeposit.Update(deposit))
                {
                    RadGridDepositList.Rebind();
                }
            }
            else if (e.Item.Text == "Modify Deposit")
            {
                if (RadGridDepositList.SelectedValue != null)
                {
                    RunClientScript("ShowPop('" + RadGridDepositList.SelectedValue + "', '1');");
                }
            }
            else if (e.Item.Text == "Add Deposit")
            {
                RunClientScript("ShowPop('0', '0');");
            }
        }

        protected void ApprovedPaymentsToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (_radGridDepositPayment.SelectedValue != null)
            {
                switch (e.Item.Text)
                {
                    case "Add Extra Payment":
                        RunClientScript("ShowAddeExtraPaymentWindow(" + _radGridDepositPayment.SelectedValues["PaymentId"] + ");");
                        break;
                    case "View Invoice":
                        RunClientScript("ShowInvoiceWindow(" + _radGridDepositPayment.SelectedValues["InvoiceId"] + ");");
                        break;
                    case "View Payment":
                        RunClientScript("ShowPaymentHistoryWindow(" + _radGridDepositPayment.SelectedValues["InvoiceId"] + ");");
                        break;
                    case "Student Page":
                        Response.Redirect("~/Student?id=" + _radGridDepositPayment.SelectedValues["StudentId"]);
                        break;
                    case "Invoice Page":
                        Response.Redirect("~/Invoice?id=" + _radGridDepositPayment.SelectedValues["StudentId"]);
                        break;
                    case "Payment Page":
                        Response.Redirect("~/Payment?id=" + _radGridDepositPayment.SelectedValues["StudentId"]);
                        break;
                    case "CreditMemo Page":
                        Response.Redirect("~/CreditMemo?id=" + _radGridDepositPayment.SelectedValues["StudentId"]);
                        break;
                    case "Refund Page":
                        Response.Redirect("~/Refund?id=" + _radGridDepositPayment.SelectedValues["StudentId"]);
                        break;
                }
            }
        }

        protected void UnApprovedPaymentsToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "View Invoice":
                    if (RadGridUnDepositPayment.SelectedValue != null)
                        RunClientScript("ShowInvoiceWindow(" + RadGridUnDepositPayment.SelectedValues["InvoiceId"] + ");");
                    break;
                case "View Payment":
                    if (RadGridUnDepositPayment.SelectedValue != null)
                        RunClientScript("ShowPaymentHistoryWindow(" + RadGridUnDepositPayment.SelectedValues["InvoiceId"] + ");");
                    break;
            }
        }

        protected void GetDepositDetail()
        {
            var buttonList = new List<string>();
            buttonList.Add("Add Deposit");
            buttonList.Add("Excel");

            if (RadGridDepositList.SelectedValue != null)
            {
                var cDeposit = new CDeposit();
                var deposit = cDeposit.Get(Convert.ToInt32(RadGridDepositList.SelectedValue.ToString()));
                DepositInfomation1.SetData(deposit);

                FileDownloadList1.GetFileDownload(Convert.ToInt32(RadGridDepositList.SelectedValue));

                bool isChecked = false;
                switch (deposit.Status)
                {
                    // Pending
                    case 1:
                        if (CurrentUserId == deposit.CreatedId)
                        {
                            buttonList.Add("Modify Deposit");
                            buttonList.Add("Confirm");
                            buttonList.Add("Cancel");
                            isChecked = true;
                        }
                        break;

                    // Created
                    case 2:
                        if (CurrentGroupId == (int)CConstValue.UserGroupForDepositConfirm.Accounting) //HQ
                            buttonList.Add("Confirm");
                        else
                            buttonList.Add("Cancel");
                        break;

                    // Confirmed
                    case 3:
                        if (CurrentGroupId == (int)CConstValue.UserGroupForDepositConfirm.Accounting) //HQ
                            buttonList.Add("Cancel");
                        break;

                    // cancel
                    default:
                        if (CurrentGroupId == (int)CConstValue.UserGroupForDepositConfirm.Accounting) //HQ
                        {
                            // nothing
                        }
                        else
                        {
                            buttonList.Add("Confirm");
                            isChecked = true;
                        }
                        break;
                }

                _radGridDepositPayment.ClientSettings.AllowRowsDragDrop = isChecked;
                RadGridUnDepositPayment.ClientSettings.AllowRowsDragDrop = isChecked;
            }

            foreach (RadToolBarItem item in DepositInfoToolbar.Items)
            {
                if (buttonList.Contains(item.Text))
                    item.Enabled = true;
                else
                    item.Enabled = false;
            }

            _linqDataSourceDepositPayment.WhereParameters.Clear();
            LinqDataSourceUnDepositPayment.WhereParameters.Clear();
            if (RadGridDepositList.SelectedValue != null)
            {
                _linqDataSourceDepositPayment.WhereParameters.Add("DepositId", DbType.Int32, RadGridDepositList.SelectedValue.ToString());
                LinqDataSourceUnDepositPayment.WhereParameters.Add("SiteLocationId", DbType.Int32, CurrentSiteLocationId.ToString());
            }
            else
            {
                _linqDataSourceDepositPayment.WhereParameters.Add("DepositId", DbType.Int32, 0.ToString());
                LinqDataSourceUnDepositPayment.WhereParameters.Add("SiteLocationId", DbType.Int32, 0.ToString());
            }
            _linqDataSourceDepositPayment.Where = "DepositId == @DepositId";
            LinqDataSourceUnDepositPayment.Where = "SiteLocationId == @SiteLocationId";
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            RadGridDepositList.Rebind();
        }

        protected void btnDepositPaymentRefresh_Click(object sender, EventArgs e)
        {
            _radGridDepositPayment.Rebind();
        }

        protected void DepositListToolbar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Search":
                    DepositListSearch();
                    break;
            }
        }

        protected void DepositListSearch()
        {
            LinqDataSource1.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteLocationList)
                LinqDataSource1.WhereParameters.Add(model.SiteLocationIdName, DbType.Int32, model.SiteLocationId.ToString());
            LinqDataSource1.Where = UserPermissionModel.SearchWhereSiteLocationSb.ToString();

            var studentId = Request["id"];
            if (!string.IsNullOrEmpty(studentId))
            {
                var depositPayment = new CDepositPayment().GetDepositListByStudentId(Convert.ToInt32(studentId));
                var depositString = new StringBuilder();
                for (int i = 0; i < depositPayment.Count; i++)
                {
                    LinqDataSource1.WhereParameters.Add("DepositId" + i, DbType.Int32, depositPayment[i].DepositId.ToString());

                    depositString.Append("DepositId == @DepositId" + i + " || ");
                }

                // data exist
                if (depositString.ToString().EndsWith("|| "))
                {
                    depositString = depositString.Remove(depositString.Length - 3, 3);
                    depositString = depositString.Insert(0, "(");
                    depositString = depositString.Insert(depositString.Length - 1, ")");
                }
                // no data
                else
                {
                    LinqDataSource1.WhereParameters.Add("DepositId", DbType.Int32, 0.ToString());
                    depositString.Append("(DepositId == @DepositId)");
                }

                if (LinqDataSource1.Where.Length > 0)
                    LinqDataSource1.Where += " && " + depositString;
                else
                    LinqDataSource1.Where = depositString.ToString();

                if (!IsPostBack)
                {
                    RadGridDepositList.MasterTableView.Rebind();

                    foreach (GridDataItem item in RadGridDepositList.Items)
                    {
                        item.Selected = true;
                        break;
                        //if (item.GetDataKeyValue("StudentId").ToString() == studentId)
                        //{
                        //    item.Selected = true;
                        //    break;
                        //}
                    }
                }
            }
        }

        protected void RadGridUnDepositPayment_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                if (dataItem["PaidAmount"].Text.Contains("-"))
                    (dataItem["PaidAmount"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
                if (dataItem["StudentPriceSum"].Text.Contains("-"))
                    (dataItem["StudentPriceSum"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
                if (dataItem["AgencyPriceSum"].Text.Contains("-"))
                    (dataItem["AgencyPriceSum"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
            }
        }

        protected void RadGridDepositList_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                if (dataItem["PaidAmount"].Text.Contains("-"))
                    (dataItem["PaidAmount"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
                if (dataItem["StudentPriceSum"].Text.Contains("-"))
                    (dataItem["StudentPriceSum"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
                if (dataItem["AgencyPriceSum"].Text.Contains("-"))
                    (dataItem["AgencyPriceSum"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
            }
        }

        protected void RadGridUnDepositPayment_OnRowDrop(object sender, GridDragDropEventArgs e)
        {
            //if (e.DraggedItems.Count != 0 && RadGridDepositList.SelectedValue != null && e.DestinationGrid == _radGridDepositPayment)
            if (e.DraggedItems.Count != 0 && RadGridDepositList.SelectedValue != null)
            {
                foreach (var dataItem in e.DraggedItems)
                {
                    var cPayDeposit = new CDepositPayment();
                    var payDeposit = new Erp2016.Lib.DepositPayment();

                    payDeposit.DepositId = Convert.ToInt32(RadGridDepositList.SelectedValue.ToString());
                    payDeposit.PaymentId = Convert.ToInt32(dataItem.GetDataKeyValue("PaymentId").ToString());
                    payDeposit.CreatedId = CurrentUserId;

                    if (cPayDeposit.Add(payDeposit) > 0)
                    {
                        var cPayment = new CPayment();
                        var payment = cPayment.Get(Convert.ToInt32(dataItem.GetDataKeyValue("PaymentId").ToString()));

                        var cHomestayChkInvoice = new CInvoice();
                        var homestayChkInvoice = cHomestayChkInvoice.Get(payment.InvoiceId);

                        //if (!string.IsNullOrEmpty(homestayChkInvoice.HomestayRegistrationId.ToString()))
                        //{
                        //    var homestayId = Convert.ToInt32(homestayChkInvoice.HomestayRegistrationId);

                        //    var payOut = new CHomestayPayoutRequest();
                        //    var payId = payOut.GetHomestayId(homestayId);

                        //    var cPout = new CHomestayPayoutRequest();
                        //    var pout = cPout.Get(payId);

                        //    var cPmethod = new CPaymentMethod();
                        //    var pmethod = cPmethod.Get(payId);

                        //    pout.PayoutStatus = 1;

                        //    if (cPout.Update(pout))
                        //    {
                        //        if (true)
                        //        {
                        //            pmethod.PayoutId = payId;
                        //            pmethod.PayoutMethod1 = 0;

                        //            pmethod.SiteLocationId = CurrentSiteLocationId;
                        //            pmethod.CreatedId = CurrentUserId;
                        //            pmethod.CreatedDate = DateTime.Now;

                        //            cPmethod.Add(pmethod);

                        //        }
                        //    }
                        //}
                    }
                }

                ShowMessage("Completed adding payment(s).");
            }
            else
            {
                ShowMessage("Failed adding payment(s).");
            }
        }

        protected void RadGridDepositList_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridUnDepositPayment_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in DepositInfoToolbar.Items)
                {
                    if (toolbarItem.Text == "Excel") continue;
                    toolbarItem.Enabled = false;
                }

                // toolbar
                foreach (RadToolBarItem toolbarItem in ApprovedPaymentsToolBar.Items)
                {
                    if (toolbarItem.Text == "View Invoice") continue;

                    toolbarItem.Enabled = false;
                }

                // toolbar
                foreach (RadToolBarItem toolbarItem in UnApprovedPaymentsToolBar.Items)
                {
                    if (toolbarItem.Text == "View Invoice") continue;

                    toolbarItem.Enabled = false;
                }
            }
        }

        protected void RadButtonExcel_OnClick(object sender, EventArgs e)
        {
            var filterExpression = ConvertFilterExpressionToSqlExpression(RadGridDepositList.MasterTableView.Columns);
            var tempDt = new CDeposit().GetVwDepositExcel(filterExpression);
            ExportExcel(tempDt, "Deposit Detail");
        }
    }
}