using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace School.Shared
{
    public partial class BasePop : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonNotification_OnClick(object sender, EventArgs e)
        {
            RadNotification1.Show();
        }
    }
}