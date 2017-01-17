<%@ Page Title="Refund" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="StudentRefundPop.aspx.cs" Inherits="School.Registrar.Student.StudentRefundPop" %>

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
