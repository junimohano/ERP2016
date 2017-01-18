<%@ Page Title="Add Extra Payment" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="DepositAddExtraPaymentPop.aspx.cs" Inherits="School.Sales.DepositAddExtraPaymentPop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">
            <telerik:RadPane ID="RadpaneBasic" runat="server" Height="100%" Scrolling="Y" Width="100%">
                <telerik:RadToolBar ID="mainToolBar" runat="server" OnButtonClick="RadToolBar_ButtonClick" OnClientButtonClicking=" ToolbarButtonClick ">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Save" ToolTip="Save" ValidationGroup="Info" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" ToolTip="Cancel" Value="Cancel" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>

                <div class="formStyle3">
                    <div style="float: left; width: 100%;">
                        <fieldset>
                            <legend>Information</legend>
                            <div>
                                <label><b style="color: red">*</b> Receipt Date</label>
                                <telerik:RadDatePicker ID="RadDatePickerReceiptDate" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/2000" MaxDate="01/01/2100"></telerik:RadDatePicker>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerReceiptDate" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                            </div>
                            <div>
                                <label><b style="color: red">*</b> Extra Payment</label>
                                <telerik:RadComboBox ID="RadComboBoxExtraPayment" CssClass="RadSizeMiddle" runat="server" />
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxExtraPayment" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                            </div>
                            <div>
                                <label><b style="color: red">*</b> Amount</label>
                                <telerik:RadNumericTextBox ID="RadNumericTextBoxAmount" Type="Currency" Value="0" CssClass="RadSizeMiddle" runat="server"></telerik:RadNumericTextBox>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadNumericTextBoxAmount" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function Close() {
                var oWnd = GetRadWindow();
                oWnd.close();
            }

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() == "Save") {
                    if (!confirm('Do you want to save it?'))
                        args.set_cancel(true);
                }
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
