<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepositInfomation.ascx.cs" Inherits="App_Data.DepositInfomation" %>

<div class="formStyle3">
    <div style="float: left; width: 100%;">
        <fieldset>
            <legend>Information</legend>
            <div>
                <label><b style="color: red">*</b> Deposited Date</label>
                <telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbDepositDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Exemption Date" MinDate="01/01/1900" MaxDate="01/01/3000" Enabled="false"></telerik:RadDatePicker>
                <br style="clear: both;" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbDepositDate" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
            </div>

            <div>
                <label><b style="color: red">*</b> Bank</label>
                <telerik:RadComboBox ID="ddlBank" CssClass="RadSizeMiddle" runat="server" Enabled="False"></telerik:RadComboBox>
                <br style="clear: both;" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlBank" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
            </div>
            <div>
                <label>Comment</label>
                <telerik:RadTextBox ID="tbComment" TextMode="MultiLine" Height="100" runat="server" ReadOnly="True" />
                <br style="clear: both;" />
            </div>
        </fieldset>
    </div>
</div>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

    </script>
</telerik:RadCodeBlock>
