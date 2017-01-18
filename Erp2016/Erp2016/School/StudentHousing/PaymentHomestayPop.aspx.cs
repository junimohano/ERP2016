using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;
using System.Data;

public partial class School_StudentHousing_PaymentHomestayPop : PageBase
{


    private LinqDataSource _linqDataSourcePaymentHistory;
    private RadGrid _radGridPaymentHistory;

    private LinqDataSource _sqlDataSourceInvoiceItems;
    private RadGrid _radGridInvoiceItems;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Id"] != null)
        {
            int InvoiceId = 0;
            var cHomestayStudent = new CHomestayStudentRequest();
            InvoiceId = cHomestayStudent.GetInvoiceIdbyHomestayStudentId(Convert.ToInt32(Request["Id"]));
            

            // Payment 
            _linqDataSourcePaymentHistory = PaymentHistoryGrid1.GetLinqDataSourcePaymentHistory();
            _radGridPaymentHistory = PaymentHistoryGrid1.GetRadGridPaymentHistory();

            _linqDataSourcePaymentHistory.WhereParameters.Clear();
            _linqDataSourcePaymentHistory.WhereParameters.Add("InvoiceId", DbType.Int32, InvoiceId.ToString());
            //_linqDataSourcePaymentHistory.WhereParameters.Add("InvoiceId", DbType.Int32, "26367");
            _linqDataSourcePaymentHistory.Where = "InvoiceId == @InvoiceId";

            //Invoice Item
            _sqlDataSourceInvoiceItems = InvoiceItemGrid1.GetSqlDataSourceInvoiceItems();
            _radGridInvoiceItems = InvoiceItemGrid1.GetRadGridInvoiceItems();
            // connect event of invoice Items.

            InvoiceItemGrid1.SetEditMode(false);
            _sqlDataSourceInvoiceItems.WhereParameters.Clear();

            _sqlDataSourceInvoiceItems.WhereParameters.Add("InvoiceId", DbType.Int32, InvoiceId.ToString());
            //_sqlDataSourceInvoiceItems.WhereParameters.Add("InvoiceId", DbType.Int32, "26367");

            _sqlDataSourceInvoiceItems.Where = "InvoiceId == @InvoiceId";
        }



    }
    protected void UpdateToolBar_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
    {
      

    }


    protected void RadGridInvoice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Request["Id"] != null)
        {
            var cStudentRequest = new CHomestayStudentRequest();
            RadGridInvoice.DataSource = cStudentRequest.GetHomestayInvoiceByRequestId(Convert.ToInt32(Request["Id"].ToString()));
        }

    }
    protected void GenerateInvoice()
    {
        //int InvoiceId = 0;
        //// Generate Invoice
        //var cInvoice = new CInvoice();
        //var invoice = new Invoice();

        //invoice.HomestayRegistrationId = HomestayStudentId;
        //invoice.StudentId = Convert.ToInt32(ddlStudent.SelectedValue);
        //invoice.SiteLocationId = CurrentSiteLocationId;
        //invoice.InvoiceType = 8;//8 Homestay
        //invoice.Status =

        //invoice.Status = (int)CConstValue.InvoiceStatus.Pending; // Pending
        //invoice.CreatedId = CurrentUserId;
        //invoice.CreatedDate = DateTime.Now;

        //InvoiceId = cInvoice.Add(invoice); //DB:Invoice 

    }
}