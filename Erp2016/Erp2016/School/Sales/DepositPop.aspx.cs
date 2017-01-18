using System;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Sales
{
    public partial class DepositPop : PageBase
    {
        private int DepositId { get; set; }

        public DepositPop() : base((int)CConstValue.Menu.Deposit)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DepositId = Convert.ToInt32(Request["id"]);
            DepositInfomation1.InitDepositInformation(true);

            FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Deposit);

            if (!IsPostBack)
            {
                // new
                if (Request["type"] == "0")
                {
                    // nothing
                }
                else
                {
                    var cDeposit = new CDeposit();
                    var deposit = cDeposit.Get(DepositId);
                    DepositInfomation1.SetData(deposit);
                    FileDownloadList1.GetFileDownload(deposit.DepositId);
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
                        var cDeposit = new CDeposit();
                        Erp2016.Lib.Deposit deposit;
                        // new
                        if (Request["type"] == "0")
                        {
                            deposit = new Erp2016.Lib.Deposit();

                            deposit.SiteLocationId = CurrentSiteLocationId;
                            deposit.CreatedId = CurrentUserId;
                            deposit.CreatedDate = DateTime.Now;
                        }
                        // modify
                        else
                            deposit = cDeposit.Get(DepositId);

                        deposit.Bank = DepositInfomation1.GetBank();
                        deposit.Comment = DepositInfomation1.GetComment();
                        deposit.DepositDate = DepositInfomation1.GetDepositDate();
                        
                        // new
                        if (Request["type"] == "0")
                        {
                            int newIndex = cDeposit.Add(deposit);
                            if (newIndex != -1)
                            {
                                FileDownloadList1.SaveFile(deposit.DepositId);
                                RunClientScript("Close();");
                            }
                            else
                                ShowMessage("Error updating");
                        }
                        // modify
                        else
                        {
                            deposit.UpdatedId = CurrentUserId;
                            deposit.UpdatedDate = DateTime.Now;
                            if (cDeposit.Update(deposit))
                            {
                                FileDownloadList1.SaveFile(deposit.DepositId);
                                RunClientScript("Close();");
                            }
                            else
                                ShowMessage("Error updating");
                        }

                    }
                    else
                        ShowMessage("Error can't find deposit");
                    break;

                case "Cancel":
                    RunClientScript("Close();");
                    break;
            }
        }
    }
}