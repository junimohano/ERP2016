<%@ Page Title="Payout" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="CreditMemoPayoutHistoryPop.aspx.cs" Inherits="School.Sales.CreditMemoPayoutHistoryPop" %>

<%@ Register TagPrefix="UserControl" TagName="CreditMemoPayout" Src="~/App_Data/CreditMemoPayout.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane1" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar_ButtonClick" OnClientButtonClicking=" ToolbarButtonClick ">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Save" ToolTip="Save" ValidationGroup="Info" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton Text="Close" ToolTip="Close" Value="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="RadpaneBasic" runat="server" Height="350">

                <div class="formStyle3">
                    <div style="float: left; width: 100%;">
                        <fieldset>
                            <legend>Information</legend>
                            <div>
                                <label><b style="color: red">*</b> Payout Amount</label>
                                <telerik:RadNumericTextBox ID="RadNumericTextBoxAmount" Type="Currency" CssClass="RadSizeMiddle" runat="server"></telerik:RadNumericTextBox>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadNumericTextBoxAmount" Display="Dynamic" ErrorMessage="Payout Amount Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                            <div>
                                <label><b style="color: red">*</b> Payout Date</label>
                                <telerik:RadDatePicker ID="RadDatePickerDate" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/2000" MaxDate="01/01/2100"></telerik:RadDatePicker>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerDate" Display="Dynamic" ErrorMessage="Payout Date Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                            <div>
                                <label>Check No</label>
                                <telerik:RadTextBox ID="RadTextBoxCheckNo" CssClass="RadSizeMiddle" runat="server"></telerik:RadTextBox>
                                <br style="clear: both;" />
                            </div>
                            <div>
                                <label>Wire Transfer No</label>
                                <telerik:RadTextBox ID="RadTextBoxWireTransferNo" CssClass="RadSizeMiddle" runat="server"></telerik:RadTextBox>
                                <br style="clear: both;" />
                            </div>
                            <div>
                                <label>Remark</label>
                                <telerik:RadTextBox ID="RadTextBoxRemark" TextMode="MultiLine" CssClass="RadSizeMultiLine" runat="server"></telerik:RadTextBox>
                                <br style="clear: both;" />
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
                if (button.get_text() === "Save") {
                    if (!confirm('Do you want to save it?'))
                        args.set_cancel(true);
                }
            }
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
