using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Data
{
    public partial class ApprovalLine : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public LinqDataSource GetSqlDataSourceApprovalList()
        {
            return LinqDataSourceApprovalList;
        }
    }
}