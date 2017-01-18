using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace School.Shared
{
    public partial class InvoiceItemGridPop : PageBase
    {
        private LinqDataSource _sqlDataSourceInvoiceItems;
        private RadGrid _radGridInvoiceItems;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // find user control
            _sqlDataSourceInvoiceItems = InvoiceItemGrid1.GetSqlDataSourceInvoiceItems();
            _radGridInvoiceItems = InvoiceItemGrid1.GetRadGridInvoiceItems();
            // just view
            InvoiceItemGrid1.SetEditMode(false);

            _sqlDataSourceInvoiceItems.WhereParameters.Clear();
            _sqlDataSourceInvoiceItems.WhereParameters.Add("InvoiceId", DbType.Int32, Request["id"]);
            _sqlDataSourceInvoiceItems.Where = "InvoiceId == @InvoiceId";
        }

    }
}