<%@ Page Title="New Simple Invoice" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="SimpleInvoiceNewPop.aspx.cs" Inherits="School.Sales.SimpleInvoiceNewPop" %>

<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="InvoiceItemGrid" Src="~/App_Data/InvoiceItemGrid.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="CreditMemoPayout" Src="~/App_Data/CreditMemoPayout.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadpaneBasic" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking=" ToolbarButtonClick ">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Save" ToolTip="Save" ValidationGroup="Info" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" ToolTip="Cancel" Value="Cancel" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="Radsplitter2" runat="server" Orientation="Vertical">

                    <telerik:RadPane ID="RadPane4" runat="server" Width="30%" Scrolling="Y">
                        <fieldset>
                            <legend>Students</legend>
                            <telerik:RadComboBox ID="RadComboBoxMenu" Width="100%" Height="500px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxMenu_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" />
                            <asp:Literal ID="itemsClientSide" runat="server" />
                        </fieldset>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

                    <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None">
                        <UserControl:InvoiceItemGrid ID="InvoiceItemGrid1" runat="server" />
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
                if (button.get_text() === "Save") {
                    if (!confirm('Do you want to save it?'))
                        args.set_cancel(true);
                }
            }
            
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
