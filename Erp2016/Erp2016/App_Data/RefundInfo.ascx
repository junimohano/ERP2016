<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RefundInfo.ascx.cs" Inherits="App_Data.RefundInfo" %>

<div class="formStyle3">
    <div style="float: left; width: 100%;">
        <fieldset>
            <legend>Info</legend>

            <div style="float: left; width: 100%;">
                <label>Studying Progress</label>
                <telerik:RadNumericTextBox ID="RadNumericTextBoxStudyRate" CssClass="RadSizeMiddle" Type="Percent" runat="server" Value="0" ReadOnly="True"></telerik:RadNumericTextBox>
            </div>

            <div style="float: left; width: 100%;">
                <label><b style="color: red">*</b> Actual Date</label>
                <telerik:RadDatePicker ID="RadDatePickerActualDate" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000" AutoPostBack="true" OnSelectedDateChanged="tbRefundDate_OnSelectedDateChanged" Enabled="False"></telerik:RadDatePicker>
                <br style="clear: both;" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerActualDate" Display="Dynamic" ErrorMessage="Actual Date Required" ForeColor="Red" ValidationGroup="Info" />
            </div>

            <div id="test" style="float: left; width: 100%;">
                <label><b style="color: red">*</b> Refund Rate</label>
                <telerik:RadNumericTextBox ID="RadNumericTextBoxRefundRate" CssClass="RadSizeMiddle" Type="Percent" runat="server" Value="0"></telerik:RadNumericTextBox>
                <br style="clear: both;" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadNumericTextBoxRefundRate" Display="Dynamic" ErrorMessage="Refund Rate Required" ForeColor="Red" ValidationGroup="Info" />
            </div>

            <div style="float: left; width: 100%;">
                <label>Reason</label>
                <telerik:RadTextBox ID="RadTextBoxReason" CssClass="RadSizeMultiLine" runat="server" TextMode="MultiLine"></telerik:RadTextBox>
            </div>
        </fieldset>
    </div>
</div>
