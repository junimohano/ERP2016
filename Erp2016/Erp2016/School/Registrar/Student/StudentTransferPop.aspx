<%@ Page Title="Transfer" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="StudentTransferPop.aspx.cs" Inherits="School.Registrar.Student.StudentTransferPop" %>

<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="RefundInfo" Src="~/App_Data/RefundInfo.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadpaneRefund" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="mainToolBar" runat="server" OnButtonClick="ToolbarButtonClick" OnClientButtonClicking=" ToolbarButtonClick ">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_save.png" Text="Request" ToolTip="Request" Value="Request" ValidationGroup="Info" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_delete.png" Text="Close" ToolTip="Close" Value="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server">
                <div class="formStyle3">
                    <div style="float: left; width: 100%;">
                        <fieldset>
                            <legend>Transfer Info</legend>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 290px;">
                                    <div>
                                        <label>Request Date :</label>
                                        <telerik:RadDatePicker ID="tbRequestDate" DateInput-DisplayDateFormat="MMM dd, yyyy" DateInput-ReadOnly="true" runat="server" Width="140px" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000" DatePopupButton-Visible="false"></telerik:RadDatePicker>
                                    </div>
                                </div>
                                <div style="float: left; width: 290px;">
                                    <div>
                                        <label>Actual Transfer Date :</label>
                                        <telerik:RadDatePicker ID="tbTransferDate" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" Width="140px" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000" AutoPostBack="true" OnSelectedDateChanged="tbTransferDate_SelectedDateChanged"></telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 100%">
                                <div style="float: left; width: 290px;">
                                    <div>
                                        <label>Total Days of Program :</label>
                                        <telerik:RadNumericTextBox ID="tbTotalDaysOfProgram" Width="150" Type="Number" NumberFormat-DecimalDigits="0" runat="server" Value="0"></telerik:RadNumericTextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 100%">
                                <div style="float: left; width: 290px;">
                                    <div>
                                        <label>Total Days Taken :</label>
                                        <telerik:RadNumericTextBox ID="tbTotalTakenDays" Width="150" Type="Number" NumberFormat-DecimalDigits="0" runat="server" Value="0"></telerik:RadNumericTextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 100%">
                                <div style="float: left; width: 290px;">
                                    <div>
                                        <label>Transfer Days :</label>
                                        <telerik:RadNumericTextBox ID="tbTransferDays" Width="150" Type="Number" NumberFormat-DecimalDigits="0" runat="server" Value="0"></telerik:RadNumericTextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 100%;">
                                    <div>
                                        <label>Transfer Reason :</label>
                                        <telerik:RadTextBox ID="tbTransferReason" Width="50%" runat="server"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 290px;">
                                    <div>
                                        <label>School :</label>
                                        <telerik:RadComboBox ID="ddlSite" Width="150" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged"></telerik:RadComboBox>
                                        <br style="clear: both;" />
                                    </div>
                                </div>
                                <div style="float: left; width: 290px;">
                                    <div>
                                        <label>Campus :</label>
                                        <telerik:RadComboBox ID="ddlSiteLocation" Width="150" runat="server"></telerik:RadComboBox>
                                        <br style="clear: both;" />
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane2" runat="server">
                <UserControl:RefundInfo ID="RefundInfo1" runat="server" />
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar2" runat="server" CollapseMode="None" EnableResize="true" />

            <telerik:RadPane ID="Radpane11" runat="server" Height="150px">
                <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />
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
                GetRadWindow().close();
            }

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() === "Request") {
                    if (!confirm('Do you want to request?'))
                        args.set_cancel(true);
                }
            }
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
