using System;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Sales
{
    public partial class DepositAddExtraPaymentPop : PageBase
    {
        private int PaymentId { get; set; }

        public DepositAddExtraPaymentPop() : base((int)CConstValue.Menu.Deposit)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PaymentId = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                var global = new CGlobal();
                RadComboBoxExtraPayment.DataSource = global.GetDictionary(1440);
                RadComboBoxExtraPayment.DataTextField = "Name";
                RadComboBoxExtraPayment.DataValueField = "Value";
                RadComboBoxExtraPayment.DataBind();

                RadDatePickerReceiptDate.SelectedDate = DateTime.Now;

                var cPayment = new CPayment();
                var payment = cPayment.Get(PaymentId);
                if (payment != null)
                {
                    if (payment.ExtraDate != null)
                        RadDatePickerReceiptDate.SelectedDate = payment.ExtraDate;

                    if (payment.ExtraAmount != null)
                        RadNumericTextBoxAmount.Value = (double)payment.ExtraAmount;

                    if (payment.ExtraType != null)
                    {
                        foreach (RadComboBoxItem item in RadComboBoxExtraPayment.Items)
                        {
                            if (item.Value == payment.ExtraType.ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }
                    }
                }

            }
        }
        
        protected void RadToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Save":
                    if (IsValid)
                    {
                        var cPayment = new CPayment();
                        var payment = cPayment.Get(PaymentId);
                        if (payment != null)
                        {
                            payment.ExtraType = Convert.ToInt32(RadComboBoxExtraPayment.SelectedValue);
                            payment.ExtraAmount = (decimal)RadNumericTextBoxAmount.Value;
                            payment.ExtraDate = RadDatePickerReceiptDate.SelectedDate;

                            if (cPayment.Update(payment))
                                RunClientScript("Close();");
                            else
                                ShowMessage("Error updating");

                        }
                        else
                            ShowMessage("Error can't find payment");
                    }
                    break;

                case "Cancel":
                    RunClientScript("Close();");
                    break;
            }
        }
    }
}