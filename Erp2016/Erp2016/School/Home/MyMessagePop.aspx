<%@ Page Title="Message" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="MyMessagePop.aspx.cs" Inherits="School.Home.MyMessagePop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">
            <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="Radsplitter4" runat="server" Orientation="Horizontal">

                    <telerik:RadPane runat="server" Height="40px" Scrolling="None">
                        <telerik:RadToolBar ID="RadToolBar1" runat="server" OnClientButtonClicking="ToolbarButtonClick" OnButtonClick="RadToolBar1_OnButtonClick">
                            <Items>
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_save.png" Text="Send" ToolTip="Send" Enabled="true" ValidationGroup="Info" />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_back.png" Text="Reply" ToolTip="Reply" Enabled="true" ValidationGroup="Info" />
                                <telerik:RadToolBarButton Text="Close" ToolTip="Close" Enabled="true" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>

                    <telerik:RadPane ID="RadPane2" runat="server">
                        <div style="float: left; width: 33%">
                            <fieldset>
                                <legend><b style="color: red">*</b> User Name</legend>
                                <telerik:RadComboBox ID="RadComboBoxUserName" Width="100%" runat="server" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" EmptyMessage="Choose a user" />
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxUserName" Display="Dynamic" ErrorMessage="User Name Required" ForeColor="Red" ValidationGroup="Info" />
                            </fieldset>
                        </div>
                        <div style="float: left; width: 33%">
                            <fieldset>
                                <legend>Recieved Date</legend>
                                <telerik:RadTextBox runat="server" ID="RadTextBoxDate" Width="100%" Enabled="False" ReadOnly="True" />
                            </fieldset>
                        </div>
                        <div style="float: left; width: 33%">
                            <fieldset>
                                <legend>IsRead</legend>
                                <telerik:RadTextBox runat="server" ID="RadTextBoxIsRead" Width="100%" Enabled="False" ReadOnly="True" />
                            </fieldset>
                        </div>

                        <fieldset>
                            <legend><b style="color: red">*</b> Content</legend>
                            <telerik:RadEditor runat="server" ID="RadEditorContent" ToolbarMode="Default" Width="100%" />
                            <br style="clear: both;" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadEditorContent" Display="Dynamic" ErrorMessage="Content Required" ForeColor="Red" ValidationGroup="Info" />
                        </fieldset>
                    </telerik:RadPane>

                </telerik:RadSplitter>

            </telerik:RadPane>
        </telerik:RadSplitter>

    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() == "Send") {
                    if (!confirm('Do you want to send?'))
                        args.set_cancel(true);
                }
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function Close() {
                var wnd = GetRadWindow();
                wnd.close();
            }

            function ShowPop(messageId, createOrListType) {
                var oWnd = window.radopen('MyMessagePop?id=' + messageId + '&createOrListType=' + createOrListType, 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;
                if (displayWidth > 1500)
                    displayWidth = 1500;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientCloseHandler);
                return false;
            }

            function OnClientCloseHandler(sender, args) {
                Close();
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
