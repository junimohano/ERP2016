using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Sales
{
    public partial class PaymentNewPop : PageBase
    {
        public PaymentNewPop() : base((int)CConstValue.Menu.Payment)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            hfInvoiceId.Value = Request["id"];

            var cInvoice = new CInvoice();
            var invoice = cInvoice.Get(Convert.ToInt32(hfInvoiceId.Value));

            LinqDataSource1.WhereParameters.Clear();
            LinqDataSource1.WhereParameters.Add("AvailableCreditAmount", DbType.Decimal, "0");
            LinqDataSource1.WhereParameters.Add("StudentId", DbType.Int32, invoice.StudentId.ToString());
            if (invoice.AgencyId == null)
                LinqDataSource1.Where = "AvailableCreditAmount > @AvailableCreditAmount and StudentId == @StudentId and AgencyId == null";
            else {
                LinqDataSource1.WhereParameters.Add("AgencyId", DbType.Int32, invoice.AgencyId.ToString());
                LinqDataSource1.Where = "AvailableCreditAmount > @AvailableCreditAmount and ((StudentId == @StudentId and AgencyId == null) or AgencyId == @AgencyId)";
            }

            if (!IsPostBack)
            {
                var global = new CGlobal();

                ddlPyamentMethod.DataSource = global.GetDictionary(28);
                ddlPyamentMethod.DataTextField = "Name";
                ddlPyamentMethod.DataValueField = "Value";
                ddlPyamentMethod.DataBind();

                ddlCurrency.Items.Insert(0, new RadComboBoxItem("CAD", "0"));
                ddlCurrency.Items.Insert(1, new RadComboBoxItem("USD", "1"));

                ttInvoice.Text = invoice.InvoiceNumber;

                ddlPyamentMethod.Visible = true;
                tbPaymentAmount.Visible = true;
                ddlCurrency.Visible = true;

                ddlCreditMemo.Visible = false;
                tbCreditAmount.Visible = false;

                // default data
                btCheckPayment.Checked = true;
                tbPaymentDate.SelectedDate = DateTime.Now;
            }
        }

        protected void ToolbarButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == @"Payment Confirm" && !string.IsNullOrEmpty(hfInvoiceId.Value) && (btCheckPayment.Checked || btCheckCredit.Checked) && !string.IsNullOrEmpty(Convert.ToString(tbPaymentDate.SelectedDate)))
            {
                if (IsValid)
                {
                    var paymentAddChk = false;
                    var creditAddChk = 0;

                    if (btCheckPayment.Checked && tbPaymentAmount.Value > 0)
                    {
                        var cPayment = new CPayment();
                        var newPayment = new Erp2016.Lib.Payment();

                        newPayment.InvoiceId = Convert.ToInt32(hfInvoiceId.Value);
                        newPayment.PaymentDate = Convert.ToDateTime(tbPaymentDate.SelectedDate);
                        newPayment.Method = Convert.ToInt32(ddlPyamentMethod.SelectedValue);
                        newPayment.Currency = Convert.ToInt32(ddlCurrency.SelectedValue);
                        newPayment.Amount = Convert.ToDecimal(tbPaymentAmount.Value);
                        newPayment.Remark = tbRemark.Text;

                        newPayment.SiteLocationId = CurrentSiteLocationId;
                        newPayment.CreatedId = CurrentUserId;
                        newPayment.CreatedDate = DateTime.Now;

                        paymentAddChk = cPayment.Add(newPayment) > 0;
                    }

                    if (btCheckCredit.Checked && tbCreditAmount.Value > 0)
                    {
                        var cPayment = new CPayment();
                        var creditPayment = new Erp2016.Lib.Payment();

                        creditPayment.InvoiceId = Convert.ToInt32(hfInvoiceId.Value);
                        creditPayment.PaymentDate = Convert.ToDateTime(tbPaymentDate.SelectedDate);
                        creditPayment.Method = 9; //Payment Method : Credit Memo

                        creditPayment.Amount = Convert.ToDecimal(tbCreditAmount.Value);
                        creditPayment.Remark = tbRemark.Text;

                        creditPayment.SiteLocationId = CurrentSiteLocationId;
                        creditPayment.CreatedId = CurrentUserId;
                        creditPayment.CreatedDate = DateTime.Now;

                        creditAddChk = cPayment.Add(creditPayment);

                        if (creditAddChk > 0)
                        {
                            var cCrreditHistory = new CCreditMemoCreditHistory();
                            var creditHistory = new Erp2016.Lib.CreditMemoCreditHistory();

                            creditHistory.CreditMemoId = Convert.ToInt32(ddlCreditMemo.SelectedValue);
                            creditHistory.PayoutMethod = 1; //CreditPayMethod(1218) : Credit(1)
                            creditHistory.CreditAmount = Convert.ToDecimal(tbCreditAmount.Value);
                            creditHistory.CreditDate = Convert.ToDateTime(tbPaymentDate.SelectedDate);
                            creditHistory.PaymentId = creditAddChk;
                            creditHistory.CreatedId = CurrentUserId;
                            creditHistory.CreatedDate = DateTime.Now;

                            cCrreditHistory.Add(creditHistory);
                        }
                        else
                        {
                            ShowMessage("failed to update inqury (Add CreditMemoPayoutHistory)");
                        }
                    }

                    if (btCheckPayment.Checked && btCheckCredit.Checked)
                    {
                        if (paymentAddChk && creditAddChk > 0)
                        {
                            RunClientScript("Close();");
                        }
                        else
                        {
                            ShowMessage("failed to update inqury (Add Payment)");
                        }
                    }
                    else
                    {
                        if (btCheckPayment.Checked)
                        {
                            if (paymentAddChk)
                            {
                                RunClientScript("Close();");
                            }
                            else
                            {
                                ShowMessage("failed to update inqury (General Pay)");
                            }
                        }
                        else if (btCheckCredit.Checked)
                        {
                            if (creditAddChk > 0)
                            {
                                RunClientScript("Close();");
                            }
                            else
                            {
                                ShowMessage("failed to update inqury (Credit Pay)");
                            }
                        }
                        else
                        {
                            ShowMessage("failed to update inqury (System failed)");
                        }
                    }
                }
            }
            else if (e.Item.Text == "Cancel")
            {
                RunClientScript("Close();");
            }
            else
            {
                ShowMessage("Check Pay Info");
            }
        }

        protected void ddlCreditMemo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((vwCreditMemo)(e.Item.DataItem)).CreditMemoNumber;
            e.Item.Value = ((vwCreditMemo)(e.Item.DataItem)).CreditMemoId.ToString();
        }

        protected void ddlCreditMemo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ddlCreditMemo.DataBind();
        }

        protected void btCheckPayment_CheckedChanged(object sender, EventArgs e)
        {
            var checkBtn = (RadButton)sender;
            if (checkBtn.Checked)
            {
                ddlPyamentMethod.Visible = true;
                tbPaymentAmount.Visible = true;
                ddlCurrency.Visible = true;
            }
            else
            {
                ddlPyamentMethod.Visible = false;
                tbPaymentAmount.Visible = false;
                ddlCurrency.Visible = false;
                tbPaymentAmount.Text = "0";

                var payAmount = (tbPaymentAmount.Text == null || tbPaymentAmount.Text == "") ? 0 : Convert.ToDecimal(tbPaymentAmount.Text);
                tbTotalPayAmount.Text = payAmount.ToString();
            }
        }

        protected void btCheckCredit_CheckedChanged(object sender, EventArgs e)
        {
            var checkBtn = (RadButton)sender;
            if (checkBtn.Checked)
            {
                ddlCreditMemo.Visible = true;
                tbCreditAmount.Visible = true;
            }
            else
            {
                ddlCreditMemo.Visible = false;
                tbCreditAmount.Visible = false;
                tbCreditAmount.Text = "0";

                var creditAmount = (tbCreditAmount.Text == null || tbCreditAmount.Text == "") ? 0 : Convert.ToDecimal(tbCreditAmount.Text);
                tbTotalPayAmount.Text = creditAmount.ToString();
            }
        }

        protected void ddlCreditMemo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var Credit = new CCreditMemo();
            if (!string.IsNullOrEmpty(ddlCreditMemo.SelectedValue))
            {
                tbCreditAmount.Text = Convert.ToString(Credit.GetAvailableCreditAmount(Convert.ToInt32(ddlCreditMemo.SelectedValue)));

                var payAmount = (tbPaymentAmount.Text == null || tbPaymentAmount.Text == "") ? 0 : Convert.ToDecimal(tbPaymentAmount.Text);
                var creditAmount = (tbCreditAmount.Text == null || tbCreditAmount.Text == "") ? 0 : Convert.ToDecimal(tbCreditAmount.Text);
                tbTotalPayAmount.Text = (payAmount + creditAmount).ToString();
            }
        }

        protected void tbPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            var payAmount = (tbPaymentAmount.Text == null || tbPaymentAmount.Text == "") ? 0 : Convert.ToDecimal(tbPaymentAmount.Text);
            var creditAmount = (tbCreditAmount.Text == null || tbCreditAmount.Text == "") ? 0 : Convert.ToDecimal(tbCreditAmount.Text);

            if (btCheckCredit.Checked)
            {
                tbTotalPayAmount.Text = (payAmount + creditAmount).ToString();
            }
            else
            {
                tbTotalPayAmount.Text = payAmount.ToString();
            }
        }

        protected void tbCreditAmount_TextChanged(object sender, EventArgs e)
        {
            var Credit = new CCreditMemo();

            var payAmount = (tbPaymentAmount.Text == null || tbPaymentAmount.Text == "") ? 0 : Convert.ToDecimal(tbPaymentAmount.Text);
            var creditAmount = (tbCreditAmount.Text == null || tbCreditAmount.Text == "") ? 0 : Convert.ToDecimal(tbCreditAmount.Text);

            var availableCredit = (ddlCreditMemo.SelectedValue == null || ddlCreditMemo.SelectedValue == "") ? 0 : Credit.GetAvailableCreditAmount(Convert.ToInt32(ddlCreditMemo.SelectedValue));
            if (creditAmount > availableCredit)
            {
                tbCreditAmount.Text = availableCredit.ToString();
                creditAmount = availableCredit;
            }

            tbTotalPayAmount.Text = (payAmount + creditAmount).ToString();
        }
    }
}