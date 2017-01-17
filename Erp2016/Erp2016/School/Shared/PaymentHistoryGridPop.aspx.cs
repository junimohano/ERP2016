using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace School.Shared
{
    public partial class PaymentHistoryGridPop : PageBase
    {
        private LinqDataSource _linqDataSourcePaymentHistory;
        private RadGrid _radGridPaymentHistory;

        protected void Page_Load(object sender, EventArgs e)
        {
            // find user control
            _linqDataSourcePaymentHistory = PaymentHistoryGrid1.GetLinqDataSourcePaymentHistory();
            _radGridPaymentHistory = PaymentHistoryGrid1.GetRadGridPaymentHistory();

            _linqDataSourcePaymentHistory.WhereParameters.Clear();
            _linqDataSourcePaymentHistory.WhereParameters.Add("InvoiceId", DbType.Int32, Request["id"]);
            _linqDataSourcePaymentHistory.Where = "InvoiceId == @InvoiceId";
        }

    }
}