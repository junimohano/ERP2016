using System;
using System.Web.UI;
using Erp2016.Lib;

namespace App_Data
{
    public partial class DepositInfomation : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var global = new CGlobal();

                ddlBank.DataSource = global.GetDictionary(67);
                ddlBank.DataTextField = "Name";
                ddlBank.DataValueField = "Value";
                ddlBank.DataBind();

                tbDepositDate.SelectedDate = DateTime.Today;
            }
        }

        public void InitDepositInformation(bool isEnableControls)
        {
            tbDepositDate.Enabled = isEnableControls;
            ddlBank.Enabled = isEnableControls;
            tbComment.ReadOnly = !isEnableControls;
        }

        public void SetData(Erp2016.Lib.Deposit deposit)
        {
            if (deposit != null)
            {
                tbDepositDate.SelectedDate = deposit.DepositDate;
                ddlBank.SelectedValue = deposit.Bank.ToString();
                tbComment.Text = deposit.Comment;
            }
        }

        public DateTime GetDepositDate()
        {
            return (DateTime)tbDepositDate.SelectedDate;
        }

        public int GetBank()
        {
            return Convert.ToInt32(ddlBank.SelectedValue);
        }

        public string GetComment()
        {
            return tbComment.Text;
        }
    }
}