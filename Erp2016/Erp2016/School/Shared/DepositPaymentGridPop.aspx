<%@ Page Title="Deposit Payment" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="DepositPaymentGridPop.aspx.cs" Inherits="School.Shared.DepositPaymentGridPop" %>


<%@ Register TagPrefix="UserControl" TagName="DepositPaymentGrid" Src="~/App_Data/DepositPaymentGrid.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">
            <telerik:RadPane ID="Radpane1" runat="server" Scrolling="None">
                
                <UserControl:DepositPaymentGrid ID="DepositPaymentGrid1" runat="server" />

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
