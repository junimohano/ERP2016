﻿<%@ Page Title="Cancel" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="StudentCancelPop.aspx.cs" Inherits="School.Registrar.Student.StudentCancelPop" %>

<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="mainToolBar" runat="server" OnButtonClick="ToolbarButtonClick" OnClientButtonClicking=" ToolbarButtonClick ">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_save.png" Text="Request" ToolTip="Request" ValidationGroup="Info" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_delete.png" Text="Close" ToolTip="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane2" runat="server" Height="170">
                <div class="formStyle3">
                    <div style="float: left; width: 100%;">
                        <fieldset>
                            <legend>Cancel</legend>

                            <div>
                                <label><b style="color: red">*</b> Apply Date</label>
                                <telerik:RadDatePicker ID="RadDatePickerApply" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" Width="140px" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000" Enabled="False" ></telerik:RadDatePicker>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerApply" Display="Dynamic" ErrorMessage="StartDate Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                            
                            <div>
                                <label>Reason</label>
                                <telerik:RadTextBox ID="RadTextBoxReason" Width="250" runat="server" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>

                        </fieldset>
                    </div>
                </div>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="None" EnableResize="true" />

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
