using System;
using System.Web.Services;
using Erp2016.Lib;

namespace School.Shared
{
    public partial class Portal : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static string GetMessageCount(int userId)
        {
            var messageCount = new CMessage().GetMessageCount(userId);
            return messageCount > 0 ? "(" + messageCount + ")" : string.Empty;
        }
    }
}