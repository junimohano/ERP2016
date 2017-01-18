<%@ Page Title="BulletinBoard" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="BulletinBoardPop.aspx.cs" Inherits="School.Home.BulletinBoardPop" %>


<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

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
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_save.png" Text="Save" ToolTip="Save" Enabled="true" ValidationGroup="Info" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton Text="Close" ToolTip="Close" Enabled="true" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>

                    <telerik:RadPane ID="RadPane2" runat="server">
                        <table>
                            <col width="150px" />
                            <col width="150px" />

                            <tr style="height: 10px" />

                            <tr>
                                <th>
                                    <label><b style="color: red">*</b> UserName</label>
                                </th>
                                <td>
                                    <telerik:RadTextBox runat="server" ID="RadTextBoxUserName" Width="100%" ReadOnly="True"></telerik:RadTextBox>
                                </td>
                                <th style="width: 150px;">
                                    <label><b style="color: red">*</b> Requested Date</label>
                                </th>
                                <td>
                                    <telerik:RadDatePicker runat="server" ID="RadDatePickerDate" Width="100%" Enabled="False" ReadOnly="True"></telerik:RadDatePicker>
                                </td>
                            </tr>

                            <tr style="height: 10px" />

                            <tr>
                                <th>
                                    <label><b style="color: red">*</b> Subject</label>
                                </th>
                                <td colspan="3">
                                    <telerik:RadTextBox runat="server" ID="RadTextBoxSubject" Width="100%"></telerik:RadTextBox>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorSubject" ControlToValidate="RadTextBoxSubject" Display="Dynamic" ErrorMessage="Subject Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                            </tr>

                            <tr style="height: 5px" />

                            <tr>
                                <th>
                                    <label><b style="color: red">*</b> Body</label>
                                </th>
                                <td colspan="3">
                                    <telerik:RadEditor runat="server" ID="RadEditorBody" ToolbarMode="ShowOnFocus" Width="100%" />
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorBody" ControlToValidate="RadEditorBody" Display="Dynamic" ErrorMessage="Body Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                            </tr>

                            <tr style="height: 10px" />

                            <tr>
                                <th>
                                    <label>Option</label>
                                </th>
                                <td colspan="3">
                                    <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />
                                </td>
                            </tr>
                        </table>

                    </telerik:RadPane>

                </telerik:RadSplitter>

            </telerik:RadPane>
        </telerik:RadSplitter>

    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">


            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() == "Save") {
                    if (!confirm('Do you want to Save?'))
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

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
