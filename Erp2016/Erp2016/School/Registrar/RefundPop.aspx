<%@ Page Title="Refund" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="RefundPop.aspx.cs" Inherits="School.Registrar.RefundPop" %>


<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="InvoiceItemGrid" Src="~/App_Data/InvoiceItemGrid.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="CreditMemoPayout" Src="~/App_Data/CreditMemoPayout.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadpaneBasic" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="mainToolBar" runat="server" OnButtonClick="RadToolBar_ButtonClick" OnClientButtonClicking=" ToolbarButtonClick ">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="TempSave" ToolTip="TempSave" ValidationGroup="Info" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_mark.png" Text="Request" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton Text="Close" ToolTip="Close" Value="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="Radsplitter2" runat="server" Orientation="Vertical">

                    <telerik:RadPane ID="Radpane2" runat="server" Width="50%" Scrolling="None">
                        <UserControl:InvoiceItemGrid ID="InvoiceItemGrid1" runat="server" />
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

                    <telerik:RadPane ID="Radpane3" runat="server">
                        <telerik:RadSplitter ID="Radsplitter3" runat="server" Orientation="Horizontal">
                            <telerik:RadPane ID="Radpane5" runat="server">
                                <UserControl:CreditMemoPayout ID="CreditMemoPayout1" runat="server" />
                            </telerik:RadPane>

                            <telerik:RadSplitBar ID="Radsplitbar3" runat="server" CollapseMode="None" EnableResize="true" />

                            <telerik:RadPane ID="Radpane11" runat="server" Height="150px">
                                <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />
                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPane>

                </telerik:RadSplitter>

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
                if (button.get_text() === "TempSave") {
                    if (!confirm('Do you want to save it?'))
                        args.set_cancel(true);
                } else if (button.get_text() === "Request") {
                    if (!confirm('Do you want to request it?'))
                        args.set_cancel(true);
                }
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
