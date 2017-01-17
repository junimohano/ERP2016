using System;
using System.Web.UI;
using Erp2016.Lib;

namespace App_Data
{
    public partial class CreditMemoPayout : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public Erp2016.Lib.CreditMemoPayout GetData(Erp2016.Lib.CreditMemoPayout creditMemo)
        {
            creditMemo.ChkHolderName = string.Empty;
            creditMemo.ChkAddress = string.Empty;
            creditMemo.ChkPhoneNum = string.Empty;

            creditMemo.BankName = string.Empty;
            creditMemo.BankAccountName = string.Empty;
            creditMemo.BankBranchAddress = string.Empty;
            creditMemo.InstitutionNo = string.Empty;
            creditMemo.BranchNo = string.Empty;
            creditMemo.TransitNo = string.Empty;
            creditMemo.SwiftCode = string.Empty;
            creditMemo.BankRouting = string.Empty;
            creditMemo.AccountNo = string.Empty;

            creditMemo.PayoutMethod = RadTabStrip2.SelectedIndex + 1;
            switch (creditMemo.PayoutMethod)
            {
                // Credit
                case 1:
                    break;

                // Cheque
                case 2:
                    creditMemo.ChkHolderName = tbHolderNameCheque.Text;
                    creditMemo.ChkAddress = tbAddressCheque.Text;
                    creditMemo.ChkPhoneNum = tbPhoneNumberCheque.Text;
                    break;

                // Wiring
                case 3:
                    creditMemo.BankName = tbBankNameWiring.Text;
                    creditMemo.BankAccountName = tbBankAccountWiring.Text;
                    creditMemo.BankBranchAddress = tbBankBranchAddrsWiring.Text;
                    creditMemo.InstitutionNo = tbInstitutionWiring.Text;
                    creditMemo.BranchNo = tbBranchNumWiring.Text;
                    creditMemo.TransitNo = tbTransitNumWiring.Text;
                    creditMemo.SwiftCode = tbSwiftWiring.Text;
                    creditMemo.BankRouting = tbBankRoutingWiring.Text;
                    creditMemo.AccountNo = tbAccountNumWiring.Text;
                    break;
            }

            return creditMemo;
        }

        private void SetCheque(Erp2016.Lib.CreditMemoPayout creditMemo)
        {
            tbHolderNameCheque.Text = creditMemo.ChkHolderName;
            tbAddressCheque.Text = creditMemo.ChkAddress;
            tbPhoneNumberCheque.Text = creditMemo.ChkPhoneNum;
        }

        private void SetWiring(Erp2016.Lib.CreditMemoPayout creditMemoPayout)
        {
            tbBankNameWiring.Text = creditMemoPayout.BankName;
            tbBankAccountWiring.Text = creditMemoPayout.BankAccountName;
            tbBankBranchAddrsWiring.Text = creditMemoPayout.BankBranchAddress;
            tbInstitutionWiring.Text = creditMemoPayout.InstitutionNo;
            tbBranchNumWiring.Text = creditMemoPayout.BranchNo;
            tbTransitNumWiring.Text = creditMemoPayout.TransitNo;
            tbSwiftWiring.Text = creditMemoPayout.SwiftCode;
            tbBankRoutingWiring.Text = creditMemoPayout.BankRouting;
            tbAccountNumWiring.Text = creditMemoPayout.AccountNo;
        }

        public void SetCreditVisible(bool isVisible)
        {
            RadTabStrip2.Tabs[0].Visible = isVisible;
        }


        public void SetReadonly(bool isReadonly)
        {
            tbHolderNameCheque.ReadOnly = isReadonly;
            tbAddressCheque.ReadOnly = isReadonly;
            tbPhoneNumberCheque.ReadOnly = isReadonly;

            tbBankNameWiring.ReadOnly = isReadonly;
            tbBankAccountWiring.ReadOnly = isReadonly;
            tbBankBranchAddrsWiring.ReadOnly = isReadonly;
            tbInstitutionWiring.ReadOnly = isReadonly;
            tbBranchNumWiring.ReadOnly = isReadonly;
            tbTransitNumWiring.ReadOnly = isReadonly;
            tbSwiftWiring.ReadOnly = isReadonly;
            tbBankRoutingWiring.ReadOnly = isReadonly;
            tbAccountNumWiring.ReadOnly = isReadonly;
        }

        public void SetData(Erp2016.Lib.CreditMemoPayout creditMemo)
        {
            if (creditMemo != null)
            {
                if (creditMemo.PayoutMethod != null)
                {
                    RadTabStrip2.SelectedIndex = (int)creditMemo.PayoutMethod - 1;

                    switch (creditMemo.PayoutMethod)
                    {
                        // Credit
                        case 1:
                            RadPageViewCredit.Selected = true;
                            break;

                        // Cheque
                        case 2:
                            RadPageViewCheque.Selected = true;
                            SetCheque(creditMemo);
                            break;

                        // Wiring
                        case 3:
                            RadPageViewWiring.Selected = true;
                            SetWiring(creditMemo);
                            break;
                    }
                }
                else
                {
                    SetCheque(new Erp2016.Lib.CreditMemoPayout());
                    SetWiring(new Erp2016.Lib.CreditMemoPayout());
                }
            }
            else
            {
                SetCheque(new Erp2016.Lib.CreditMemoPayout());
                SetWiring(new Erp2016.Lib.CreditMemoPayout());
            }
        }

    }
}