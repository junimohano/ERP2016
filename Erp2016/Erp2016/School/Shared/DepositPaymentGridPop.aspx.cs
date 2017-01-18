using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace School.Shared
{
    public partial class DepositPaymentGridPop : PageBase
    {
        private LinqDataSource _linqDataSourceDepositPayment;
        private RadGrid _radGridDepositPayment;

        protected void Page_Load(object sender, EventArgs e)
        {
            // find user control
            _linqDataSourceDepositPayment = DepositPaymentGrid1.GetLinqDataSourceDepositPayment();
            _radGridDepositPayment = DepositPaymentGrid1.GetRadGridDepositPayment();

            _linqDataSourceDepositPayment.WhereParameters.Clear();
            _linqDataSourceDepositPayment.WhereParameters.Add("DepositId", DbType.Int32, Request["id"]);
            _linqDataSourceDepositPayment.Where = "DepositId == @DepositId";
        }

    }
}