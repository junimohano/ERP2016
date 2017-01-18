<%@ Page Title="Program Change" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="StudentProgramChangePop.aspx.cs" Inherits="School.Registrar.Student.StudentProgramChangePop" %>

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
                            <legend>Program Change Info</legend>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 290px;">
                                    <div>
                                        <label>Request Date :</label>
                                        <telerik:RadDatePicker ID="tbRequestDate" DateInput-DisplayDateFormat="MMM dd, yyyy" DateInput-ReadOnly="true" runat="server" Width="140px" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000" DatePopupButton-Visible="false"></telerik:RadDatePicker>
                                    </div>
                                </div>
                                <div style="float: left; width: 290px;">
                                    <div>
                                        <label>Actual Program Change Date :</label>
                                        <telerik:RadDatePicker ID="tbProgramChangeDate" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" Width="140px" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000" AutoPostBack="true" OnSelectedDateChanged="tbProgramChangeDate_SelectedDateChanged"></telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 100%;">
                                    <div>
                                        <label>Program Change Reason :</label>
                                        <telerik:RadTextBox ID="tbProgramChangeReason" Width="50%" runat="server"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Cancel Credit Info</legend>
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
                                        <label>Cancel Days :</label>
                                        <telerik:RadNumericTextBox ID="tbCancelDays" Width="150" Type="Number" NumberFormat-DecimalDigits="0" runat="server" Value="0"></telerik:RadNumericTextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>New Program Info</legend>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 290px;">
                                    <div>
                                        <label>Agency</label>
                                        <telerik:RadComboBox ID="ddlAgency" Width="150" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div>
                                        <label>Commission</label>
                                        <telerik:RadNumericTextBox ID="tbCommissionRate" Value="0" Width="60" runat="server" EmptyMessage="" Type="Percent" Enabled="false"></telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                <div style="float: left; width: 290px;">
                                    <div>
                                        <label>Agency Contact</label>
                                        <telerik:RadComboBox ID="ddlAgencyContact" Width="150" runat="server" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                                <div style="float: left; width: 290px;">
                                </div>
                                <br style="clear: both;" />
                            </div>
                            <div style="float: left; width: 100%;">
                                <div>
                                    <label>Faculty</label>
                                    <telerik:RadComboBox ID="ddlFaculty" Width="300" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                    <br style="clear: both;" />
                                </div>
                                <div>
                                    <label>Program Group</label>
                                    <telerik:RadComboBox ID="ddlProgramGrp" Width="300" runat="server" AutoPostBack="true" OnItemsRequested="ddlProgramGrp_ItemsRequested" OnSelectedIndexChanged="ddlProgramGrp_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                    <br style="clear: both;" />
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div>
                                    <label>Program Name</label>
                                    <telerik:RadComboBox ID="ddlProgramName" Width="300" runat="server" AutoPostBack="true" OnItemsRequested="ddlProgramName_ItemsRequested">
                                    </telerik:RadComboBox>
                                    <br style="clear: both;" />
                                </div>
                            </div>
                            <div style="float: left; width: 290px;">
                                <div>
                                    <label>Start Date</label><telerik:RadDatePicker ID="tbPrgStartDate" AutoPostBack="true" OnSelectedDateChanged="tbPrgStartDate_SelectedDateChanged" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" Width="140px" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                    <br style="clear: both;" />
                                </div>
                            </div>
                            <div style="float: left; width: 290px;">
                                <div>
                                    <label>Program Weeks</label><telerik:RadComboBox ID="ddlProgramWeeks" Enabled="false" Width="150" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramWeeks_SelectedIndexChanged"></telerik:RadComboBox>
                                    <br style="clear: both;" />
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div>
                                    <label>End Date</label><telerik:RadDatePicker ID="tbPrgEndDate" Enabled="false" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" Width="140px" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                    <br style="clear: both;" />
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div>
                                    <label>Program HRS</label><telerik:RadComboBox ID="ddlPrgHours" Enabled="false" Width="150" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPrgHours_SelectedIndexChanged"></telerik:RadComboBox>
                                    <br style="clear: both;" />
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div>
                                    <label>Standard Tuition</label>
                                    <telerik:RadNumericTextBox ID="tbPrgStandardTuition" Value="0" Width="150" runat="server" EmptyMessage="" Type="Currency" Enabled="false"></telerik:RadNumericTextBox>
                                    <telerik:RadTextBox ID="tbPrgStandardTuitionId" Text="" runat="server" Display="false"></telerik:RadTextBox>
                                    <br style="clear: both;" />
                                    <label>Tuition</label>
                                    <telerik:RadNumericTextBox ID="tbPrgTuition" Width="150" runat="server" EmptyMessage="" Type="Currency"></telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="tbPrgTuitionVali" ControlToValidate="tbPrgTuition" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Tuition Required</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="tbPrgTuitionId" Text="" runat="server" Display="false"></telerik:RadTextBox>
                                    <br style="clear: both;" />
                                </div>
                            </div>
                            <br style="clear: both;" />
                        </fieldset>
                    </div>
                </div>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane3" runat="server">
                <UserControl:RefundInfo ID="RefundInfo1" runat="server" />
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
