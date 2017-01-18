using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Sales
{
    public partial class Payment : PageBase
    {
        private LinqDataSource _sqlDataSourceInvoiceItems;
        private RadGrid _radGridInvoiceItems;

        private LinqDataSource _linqDataSourcePaymentHistory;
        private RadGrid _radGridPaymentHistory;

        public Payment() : base((int)CConstValue.Menu.Payment)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // InvoiceItemsGrid Control
            _sqlDataSourceInvoiceItems = InvoiceItemGrid1.GetSqlDataSourceInvoiceItems();
            _radGridInvoiceItems = InvoiceItemGrid1.GetRadGridInvoiceItems();
            _radGridInvoiceItems.PreRender += _radGridInvoiceItems_PreRender;
            InvoiceItemGrid1.SetEditMode(false);

            // PaymentHistoryGrid control
            _linqDataSourcePaymentHistory = PaymentHistoryGrid1.GetLinqDataSourcePaymentHistory();
            _radGridPaymentHistory = PaymentHistoryGrid1.GetRadGridPaymentHistory();
            _radGridPaymentHistory.SelectedIndexChanged += Payment_SelectedIndexChanged;

            if (!IsPostBack)
            {
                var excel = RadToolBarPaymentTop.FindItemByText("Excel");
                if (CurrentGroupId == (int)CConstValue.UserGroupForAccountExcelExport.Accounting ||
                    CurrentGroupId == (int)CConstValue.UserGroupForAccountExcelExport.IT)
                {
                    excel.Visible = true;
                }

                var btnDetailStudentReciept = RadToolBarPayment.FindItemByText("Student Detail Reciept");
                var btnDetailAgencyReciept = RadToolBarPayment.FindItemByText("Agency Detail Reciept");
                btnDetailStudentReciept.Enabled = false;
                btnDetailAgencyReciept.Enabled = false;
            }

            PaymentListSearch();
            GetPaymentItems();
        }

        private void _radGridInvoiceItems_PreRender(object sender, EventArgs e)
        {
            GetInvoiceItems();
        }

        private void Payment_SelectedIndexChanged(object sender, EventArgs e)
        {
            CPayment cPayment = new CPayment();
            vwPaymentHistory paymentHistory = cPayment.GetvwPaymentHistory(Convert.ToInt32(_radGridPaymentHistory.SelectedValue));
            Erp2016.Lib.Payment reversePayment = cPayment.GetReversePayment(Convert.ToInt32(paymentHistory.PaymentId));

            var btnDetailStudentReciept = RadToolBarPayment.FindItemByText("Student Detail Reciept");
            var btnDetailAgencyReciept = RadToolBarPayment.FindItemByText("Agency Detail Reciept");
            var btnReverse = RadToolBarPayment.FindItemByText("Payment Reverse");

            if (paymentHistory.Status != 3 && paymentHistory.OriginalPaymentId == null && reversePayment == null)
                btnReverse.Enabled = true;
            else
                btnReverse.Enabled = false;

            btnDetailStudentReciept.Enabled = true;
            btnDetailAgencyReciept.Enabled = true;
        }

        public void GetPaymentItems()
        {
            _linqDataSourcePaymentHistory.WhereParameters.Clear();
            if (RadGridPayment.SelectedValue == null)
                _linqDataSourcePaymentHistory.WhereParameters.Add("InvoiceId", DbType.Int32, 0.ToString());
            else
                _linqDataSourcePaymentHistory.WhereParameters.Add("InvoiceId", DbType.Int32, RadGridPayment.SelectedValue.ToString());
            _linqDataSourcePaymentHistory.Where = "InvoiceId == @InvoiceId";
        }

        public void GetInvoiceItems()
        {
            _sqlDataSourceInvoiceItems.WhereParameters.Clear();

            if (RadGridPayment.SelectedValue == null)
                _sqlDataSourceInvoiceItems.WhereParameters.Add("InvoiceId", DbType.Int32, 0.ToString());
            else
                _sqlDataSourceInvoiceItems.WhereParameters.Add("InvoiceId", DbType.Int32, RadGridPayment.SelectedValue.ToString());

            _sqlDataSourceInvoiceItems.Where = "InvoiceId == @InvoiceId";
        }

        protected void Refresh(object sender, EventArgs e)
        {
            RadGridPayment.Rebind();
        }

        protected void PaymentToolbar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == @"Search")
            {
                PaymentListSearch();
            }
        }

        protected void PaymentListSearch()
        {
            LinqDataSourcePayment.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteLocationList)
                LinqDataSourcePayment.WhereParameters.Add(model.SiteLocationIdName, DbType.Int32, model.SiteLocationId.ToString());
            LinqDataSourcePayment.Where = UserPermissionModel.SearchWhereSiteLocationSb.ToString();


            var studentId = Request["id"];
            if (!string.IsNullOrEmpty(studentId))
            {
                LinqDataSourcePayment.WhereParameters.Add("StudentId", DbType.Int32, studentId);
                if (LinqDataSourcePayment.Where.Length > 0)
                    LinqDataSourcePayment.Where += " && StudentId == @StudentId";
                else
                    LinqDataSourcePayment.Where = "StudentId == @StudentId";

                if (!IsPostBack)
                {
                    RadGridPayment.MasterTableView.Rebind();

                    foreach (GridDataItem item in RadGridPayment.Items)
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

        protected void RadToolBarPayment_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "New Payment":
                    if (RadGridPayment.SelectedValue != null)
                    {
                        RunClientScript("ShowNewPaymentWindow('" + RadGridPayment.SelectedValue + "');");
                    }
                    break;

                case "Agency Detail Reciept":
                    if (_radGridPaymentHistory.SelectedValue != null)
                    {
                        var selectedInvoiceList = new List<int>();
                        foreach (GridDataItem item in _radGridPaymentHistory.SelectedItems)
                            selectedInvoiceList.Add((int)item.GetDataKeyValue("PaymentId"));

                        RunClientScript("ShowReportPop('" + String.Join(", ", selectedInvoiceList.ToArray()) + "', '" + (int)CConstValue.Report.DetailPaymentAgency + "' );");
                    }
                    break;

                case "Student Detail Reciept":
                    if (_radGridPaymentHistory.SelectedValue != null)
                    {
                        var selectedInvoiceList = new List<int>();
                        foreach (GridDataItem item in _radGridPaymentHistory.SelectedItems)
                            selectedInvoiceList.Add((int)item.GetDataKeyValue("PaymentId"));

                        RunClientScript("ShowReportPop('" + String.Join(", ", selectedInvoiceList.ToArray()) + "', '" + (int)CConstValue.Report.DetailPaymentStudent + "' );");
                    }

                    //if (_radGridPaymentHistory.SelectedValue != null)
                    //{
                    //    RunClientScript("ShowReportPop('" + _radGridPaymentHistory.SelectedValue + "', '" + (int)CConstValue.Report.DetailPaymentStudent + "');");
                    //}
                    break;

                case "Payment Reverse":
                    if (_radGridPaymentHistory.SelectedValue != null)
                    {
                        var payment = new CPayment().Get(Convert.ToInt32(_radGridPaymentHistory.SelectedValue));
                        if (payment.Amount > 0)
                        {
                            var cNewPayment = new CPayment();
                            var newPayment = new Erp2016.Lib.Payment();
                            CGlobal.Copy(payment, newPayment);
                            newPayment.Amount *= -1;
                            newPayment.OriginalPaymentId = payment.PaymentId;

                            cNewPayment.Add(newPayment);

                            RadGridPayment.Rebind();
                        }
                        else
                            ShowMessage("Negative price can't reverse");
                    }
                    break;
            }
        }

        protected void RadGridPayment_OnPreRender(object sender, EventArgs e)
        {
            if (ViewState["InvoiceId"] != null)
            {
                foreach (GridDataItem item in RadGridPayment.Items)
                {
                    if (item.GetDataKeyValue("InvoiceId").ToString() == ViewState["InvoiceId"].ToString())
                    {
                        if (item.Selected == false)
                        {
                            item.Selected = true;
                            GetPaymentItems();
                            GetInvoiceItems();
                            break;
                        }
                    }
                }
            }
        }

        protected void RadGridPayment_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["InvoiceId"] = RadGridPayment.SelectedValue;
            _radGridPaymentHistory.Rebind();
            _radGridInvoiceItems.Rebind();

            var btnNewPayment = RadToolBarPayment.FindItemByText("New Payment");

            var payment = new CPayment().GetvwPayment(Convert.ToInt32(RadGridPayment.SelectedValue));
            if (payment != null)
            {
                if (payment.Balance == 0 || payment.Status != (int)CConstValue.InvoiceStatus.Invoiced)
                    btnNewPayment.Enabled = false;
                else
                    btnNewPayment.Enabled = true;
            }
            else
            {
                btnNewPayment.Enabled = false;
            }


            var btnDetailStudentReciept = RadToolBarPayment.FindItemByText("Student Detail Reciept");
            var btnDetailAgencyReciept = RadToolBarPayment.FindItemByText("Agency Detail Reciept");
            btnDetailStudentReciept.Enabled = false;
            btnDetailAgencyReciept.Enabled = false;
        }

        protected void RadGridPayment_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                if (dataItem["StudentPriceSum"].Text.Contains("-"))
                    (dataItem["StudentPriceSum"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
                if (dataItem["Gross"].Text.Contains("-"))
                    (dataItem["Gross"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
                if (dataItem["CP"].Text.Contains("-"))
                    (dataItem["CP"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
                if (dataItem["MDF"].Text.Contains("-"))
                    (dataItem["MDF"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
                if (dataItem["AgencyPriceSum"].Text.Contains("-"))
                    (dataItem["AgencyPriceSum"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
                if (dataItem["PayAmount"].Text.Contains("-"))
                    (dataItem["PayAmount"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
                if (dataItem["OverPaid"].Text.Contains("-"))
                    (dataItem["OverPaid"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
                if (dataItem["Balance"].Text.Contains("-"))
                    (dataItem["Balance"] as GridTableCell).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
            }
        }

        protected void RadToolBarPaymentTop_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (RadGridPayment.SelectedValue != null)
            {
                switch (e.Item.Text)
                {
                    case "Student Reciept":
                        RunClientScript("ShowReportPop('" + RadGridPayment.SelectedValue + "', '" + (int)CConstValue.Report.PaymentStudent + "');");
                        break;
                    case "Agency Reciept":
                        RunClientScript("ShowReportPop('" + RadGridPayment.SelectedValue + "','" + (int)CConstValue.Report.PaymentAgency + "');");
                        break;
                    case "Student Page":
                        Response.Redirect("~/Student?id=" + RadGridPayment.SelectedValues["StudentId"]);
                        break;
                    case "Invoice Page":
                        Response.Redirect("~/Invoice?id=" + RadGridPayment.SelectedValues["StudentId"]);
                        break;
                    case "Deposit Page":
                        Response.Redirect("~/Deposit?id=" + RadGridPayment.SelectedValues["StudentId"]);
                        break;
                    case "CreditMemo Page":
                        Response.Redirect("~/CreditMemo?id=" + RadGridPayment.SelectedValues["StudentId"]);
                        break;
                    case "Refund Page":
                        Response.Redirect("~/Refund?id=" + RadGridPayment.SelectedValues["StudentId"]);
                        break;
                    case "Change status of Gross Based":
                        var cInvoice = new CInvoice();
                        var invoice = cInvoice.Get(Convert.ToInt32(RadGridPayment.SelectedValue));
                        if (invoice != null)
                        {
                            invoice.IsGross = !invoice.IsGross;
                            invoice.UpdatedId = CurrentUserId;
                            if (cInvoice.Update(invoice))
                            {
                                RadGridPayment.Rebind();
                                ShowMessage("Gross Based has been changed.");
                            }
                            else
                                ShowMessage("Error : Gross Based");
                        }
                        break;
                }
            }
        }

        protected void RadGridPayment_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBarPayment.Items)
                {
                    if (toolbarItem.Text == "Stndeut Detail Reciept" || toolbarItem.Text == "Agency Detail Reciept") continue;

                    toolbarItem.Enabled = false;
                }
            }
        }

        protected void RadButtonExcel_OnClick(object sender, EventArgs e)
        {
            var filterExpression = ConvertFilterExpressionToSqlExpression(RadGridPayment.MasterTableView.Columns);
            var tempDt = new CPayment().GetVwPaymentExcel(filterExpression);
            ExportExcel(tempDt, "Payment Detail");
        }
    }
}