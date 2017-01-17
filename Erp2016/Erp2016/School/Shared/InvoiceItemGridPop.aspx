<%@ Page Title="Invoice items" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="InvoiceItemGridPop.aspx.cs" Inherits="School.Shared.InvoiceItemGridPop" %>


<%@ Register TagPrefix="UserControl" TagName="InvoiceItemGrid" Src="~/App_Data/InvoiceItemGrid.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">
            <telerik:RadPane ID="Radpane1" runat="server" Scrolling="None">
                
                <UserControl:InvoiceItemGrid ID="InvoiceItemGrid1" runat="server" />

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

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
