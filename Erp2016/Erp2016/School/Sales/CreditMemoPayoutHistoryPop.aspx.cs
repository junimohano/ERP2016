using System;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Sales
{
    public partial class CreditMemoPayoutHistoryPop : PageBase
    {
        private int Id { get; set; }

        public CreditMemoPayoutHistoryPop() : base((int)CConstValue.Menu.CreditMemo)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // CreditMemoId in new or CreditMemoPayoutId in modify
            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                //// new
                //if (Request["type"] == "0")
                //{
                RadDatePickerDate.SelectedDate = DateTime.Today;
                //}
                //// modify
                //else
                //{
                //var creditMemoPayoutHistory = new CCreditMemoPayoutHistory().Get(Id);

                //RadNumericTextBoxAmount.Value = (double)creditMemoPayoutHistory.PayoutAmount;
                //RadDatePickerDate.SelectedDate = creditMemoPayoutHistory.PayoutDate;
                //RadTextBoxCheckNo.Text = creditMemoPayoutHistory.CheckNo;
                //RadTextBoxWireTransferNo.Text = creditMemoPayoutHistory.WireTransferNo;
                //RadTextBoxRemark.Text = creditMemoPayoutHistory.Remark;
                //}
            }
        }

        protected void RadToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Save":
                    if (IsValid)
                    {
                        var cC = new CCreditMemoPayoutHistory();
                        var c = new Erp2016.Lib.CreditMemoPayoutHistory();

                        //// new
                        //if (Request["type"] == "0")
                        //{
                        //c = new Erp2016.Lib.CreditMemoPayoutHistory();
                        c.CreatedId = CurrentUserId;
                        c.CreatedDate = DateTime.Now;
                        c.CreditMemoPayoutId = Id;
                        //}
                        //// modify
                        //else
                        //{
                        //    c = cC.Get(Id);
                        //}

                        c.PayoutAmount = (decimal)RadNumericTextBoxAmount.Value;
                        c.PayoutDate = (DateTime)RadDatePickerDate.SelectedDate;
                        c.CheckNo = RadTextBoxCheckNo.Text;
                        c.WireTransferNo = RadTextBoxWireTransferNo.Text;
                        c.Remark = RadTextBoxRemark.Text;

                        //// new
                        //if (Request["type"] == "0")
                        //{
                        decimal availableAmount = new CCreditMemoPayout().GetAvailablePayoutAmount(Id);
                        if (availableAmount >= c.PayoutAmount)
                        {
                            cC.Add(c);
                            RunClientScript("Close();");
                        }
                        else
                            ShowMessage("paid amount is bigger than available payout amount");
                        //}
                        //// modify
                        //else
                        //{
                        //    c.UpdatedId = CurrentUserId;
                        //    c.UpdatedDate = DateTime.Now;
                        //    cC.Update(c);

                        //    creditMemoPayoutId = c.CreditMemoPayoutId;
                        //}
                    }
                    break;

                case "Close":
                    RunClientScript("Close();");
                    break;
            }
        }
    }
}