<%@ Page Title="Approval Reject" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="ApprovalRejectPop.aspx.cs" Inherits="School.Shared.ApprovalRejectPop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:HiddenField ID="hfType" runat="server" Value="" />
    <asp:HiddenField ID="hfId" runat="server" Value="" />

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">
            <telerik:RadPane ID="RadpaneBasic" runat="server" Height="100%" Scrolling="Y" Width="100%">
                <telerik:RadToolBar ID="MainToolBar" runat="server" OnButtonClick="MainToolBar_ButtonClick" OnClientButtonClicking=" ToolbarButtonClick ">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Reject" Value="Reject" ToolTip="Reject" ValidationGroup="Info" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
                <div class="formStyle3">
                    <div style="float: left; width: 100%;">
                        <fieldset>
                            <legend>Remark</legend>
                            <div style="float: left; width: 100%;">
                                <telerik:RadTextBox ID="tbRemark" TextMode="MultiLine" Height="60" Width="100%" Text="" runat="server"></telerik:RadTextBox>
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
                if (button.get_text() === "Reject") {
                    if (!confirm('Do you want to reject?'))
                        args.set_cancel(true);
                }
            }
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
