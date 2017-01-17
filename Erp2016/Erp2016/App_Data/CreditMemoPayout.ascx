<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreditMemoPayout.ascx.cs" Inherits="App_Data.CreditMemoPayout" %>

<telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" AutoPostBack="True">
    <Tabs>
        <telerik:RadTab Text="Credit" />

        <telerik:RadTab Text="Cheque" />

        <telerik:RadTab Text="Wiring" Selected="True" />

    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="RadMultiPage" runat="server" RenderSelectedPageOnly="True">

    <telerik:RadPageView ID="RadPageViewCredit" runat="server">
        <fieldset>
            <legend>Credit Memo Payout Information</legend>
        </fieldset>
    </telerik:RadPageView>

    <telerik:RadPageView ID="RadPageViewCheque" runat="server">
        <div class="formStyle3">
            <fieldset>
                <legend>Cheque Payout Information</legend>
                <div style="float: left; width: 100%;">
                    <div>
                        <label>Account Holder Name</label>
                        <telerik:RadTextBox ID="tbHolderNameCheque" runat="server" CssClass="RadSizeMiddle" ReadOnly="true"></telerik:RadTextBox>
                        <br style="clear: both;" />
                    </div>
                    <div>
                        <label>Full Address</label>
                        <telerik:RadTextBox ID="tbAddressCheque" runat="server" CssClass="RadSizeLarge" ReadOnly="true"></telerik:RadTextBox>
                        <br style="clear: both;" />
                    </div>
                    <div>
                        <label>Phone Number</label>
                        <telerik:RadTextBox ID="tbPhoneNumberCheque" runat="server" CssClass="RadSizeMiddle" ReadOnly="true"></telerik:RadTextBox>
                        <br style="clear: both;" />
                    </div>
                </div>
            </fieldset>
        </div>
    </telerik:RadPageView>

    <telerik:RadPageView ID="RadPageViewWiring" runat="server" Selected="true">
        <div class="formStyle3">
            <fieldset>
                <legend>Wiring Payout Information</legend>
                <div style="float: left; width: 100%;">
                    <div>
                        <label>Bank Name</label>
                        <telerik:RadTextBox ID="tbBankNameWiring" runat="server" CssClass="RadSizeMiddle" ReadOnly="true"></telerik:RadTextBox>
                        <br style="clear: both;" />
                    </div>
                    <div>
                        <label>Bank Account Name</label>
                        <telerik:RadTextBox ID="tbBankAccountWiring" runat="server" CssClass="RadSizeMiddle" ReadOnly="true"></telerik:RadTextBox>
                        <br style="clear: both;" />
                    </div>
                    <div>
                        <label>Bank Branch Address</label>
                        <telerik:RadTextBox ID="tbBankBranchAddrsWiring" runat="server" CssClass="RadSizeLarge" ReadOnly="true"></telerik:RadTextBox>
                        <br style="clear: both;" />
                    </div>
                    <div>
                        <label>Institution</label>
                        <telerik:RadTextBox ID="tbInstitutionWiring" runat="server" CssClass="RadSizeMiddle" ReadOnly="true"></telerik:RadTextBox>
                        <br style="clear: both;" />
                    </div>
                    <div>
                        <label>Branch Number</label>
                        <telerik:RadTextBox ID="tbBranchNumWiring" runat="server" CssClass="RadSizeMiddle" ReadOnly="true"></telerik:RadTextBox>
                        <br style="clear: both;" />
                    </div>
                    <div>
                        <label>Transit Number</label>
                        <telerik:RadTextBox ID="tbTransitNumWiring" runat="server" CssClass="RadSizeMiddle" ReadOnly="true"></telerik:RadTextBox>
                        <br style="clear: both;" />
                    </div>
                    <div>
                        <label>SWIFT Code</label>
                        <telerik:RadTextBox ID="tbSwiftWiring" runat="server" CssClass="RadSizeMiddle" ReadOnly="true"></telerik:RadTextBox>
                        <br style="clear: both;" />
                    </div>
                    <div>
                        <label>Bank Routing / ABA or Swift</label>
                        <telerik:RadTextBox ID="tbBankRoutingWiring" runat="server" CssClass="RadSizeMiddle" ReadOnly="true"></telerik:RadTextBox>
                        <br style="clear: both;" />
                    </div>
                    <div>
                        <label>Account Number</label>
                        <telerik:RadTextBox ID="tbAccountNumWiring" runat="server" CssClass="RadSizeMiddle" ReadOnly="true"></telerik:RadTextBox>
                        <br style="clear: both;" />
                    </div>
                </div>
            </fieldset>
        </div>
    </telerik:RadPageView>

</telerik:RadMultiPage>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

    </script>
</telerik:RadCodeBlock>
