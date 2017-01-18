<%@ Page Title="Vacation" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="VacationPop.aspx.cs" Inherits="School.OfficeAdmin.VacationPop" %>

<%@ Import Namespace="Erp2016.Lib" %>

<%@ Register TagPrefix="usercontrol" TagName="approvalline" Src="~/App_Data/ApprovalLine.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">

                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking="RadToolBar1_OnClientButtonClicking">
                    <Items>
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_save.png" Text="TempSave" ValidationGroup="Info" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_mark.png" Text="Request" ValidationGroup="Info" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_excute.png" Text="Approve" />
                        <telerik:RadToolBarButton runat="server" Text="Accept" />
                        <telerik:RadToolBarButton runat="server" Text="Complete" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_back.png" Text="Revise" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_clear.png" Text="Reject" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" />
                        <telerik:RadToolBarButton runat="server" Text="Print" />
                        <telerik:RadToolBarButton runat="server" Text="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="RadPane1" runat="server" Scrolling="Both">

                <div id="divPrint">

                    <telerik:RadGrid ID="RadGridInfo" runat="server" GroupPanelPosition="Top" OnLoad="RadGridInfo_Load" />

                    <usercontrol:approvalline ID="ApprovalLine1" runat="server" OnLoad="ApprovalLine1_OnLoad" />

                    <telerik:RadGrid ID="RadGridDays" runat="server" GroupPanelPosition="Top" OnLoad="RadGridDays_Load" />

                    <fieldset>
                        <legend>Vacation Info</legend>

                        <table>

                            <col width="150px" />

                            <tr>
                                <th>
                                    <label><b style="color: red">*</b> Vacation Type</label>
                                </th>
                                <td>
                                    <telerik:RadComboBox runat="server" ID="RadComboBoxVacationType" CssClass="RadSizeMiddle" DataTextField="Name" DataValueField="Value" DataSourceID="LinqDataSourceVacationType" OnSelectedIndexChanged="RadComboBoxVacationType_OnSelectedIndexChanged" AutoPostBack="True" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                    <asp:LinqDataSource ID="LinqDataSourceVacationType" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" TableName="Dicts"
                                        Where="DictType == @DictType">
                                        <WhereParameters>
                                            <asp:Parameter DefaultValue="1376" Name="DictType" Type="Int32" />
                                        </WhereParameters>
                                    </asp:LinqDataSource>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label><b style="color: red">*</b> Day Type</label>
                                </th>
                                <td>
                                    <telerik:RadComboBox runat="server" ID="RadComboBoxDayType" CssClass="RadSizeMiddle" OnSelectedIndexChanged="RadComboBoxDayType_OnSelectedIndexChanged" AutoPostBack="True" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Full Day" Value="1" Selected="True" />
                                            <telerik:RadComboBoxItem runat="server" Text="Half Day" Value="0" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label><b style="color: red">*</b> Calendar</label>
                                </th>
                                <td>
                                    <telerik:RadCalendar ID="RadCalendar1" runat="server" Height="300px" OnSelectionChanged="RadCalendar1_OnSelectionChanged" EnableWeekends="False" AutoPostBack="True" OnLoad="RadCalendar1_OnLoad" MultiViewColumns="2" MultiViewRows="1" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" OnPreRender="RadCalendar1_OnPreRender" OnDayRender="RadCalendar1_OnDayRender">
                                        <CalendarDayTemplates>
                                            <telerik:DayTemplate ID="DayTemplatePaidFullDay" runat="server">
                                                <Content>
                                                    <span class="PaidFullDay">{DayOfMonth}</span>
                                                </Content>
                                            </telerik:DayTemplate>
                                            <telerik:DayTemplate ID="DayTemplatePaidHalfDay" runat="server">
                                                <Content>
                                                    <span class="PaidHalfDay">{DayOfMonth}</span>
                                                </Content>
                                            </telerik:DayTemplate>
                                            <telerik:DayTemplate ID="DayTemplateUnPaidFullDay" runat="server">
                                                <Content>
                                                    <span class="UnPaidFullDay ">{DayOfMonth}</span>
                                                </Content>
                                            </telerik:DayTemplate>
                                            <telerik:DayTemplate ID="DayTemplateUnPaidHalfDay" runat="server">
                                                <Content>
                                                    <span class="UnPaidHalfDay ">{DayOfMonth}</span>
                                                </Content>
                                            </telerik:DayTemplate>
                                            <telerik:DayTemplate ID="DayTemplateSickFullDay" runat="server">
                                                <Content>
                                                    <span class="SickFullDay">{DayOfMonth}</span>
                                                </Content>
                                            </telerik:DayTemplate>
                                            <telerik:DayTemplate ID="DayTemplateSickHalfDay" runat="server">
                                                <Content>
                                                    <span class="SickHalfDay">{DayOfMonth}</span>
                                                </Content>
                                            </telerik:DayTemplate>
                                            <telerik:DayTemplate ID="DayTemplateEntitlementFullDay" runat="server">
                                                <Content>
                                                    <span class="EntitlementFullDay">{DayOfMonth}</span>
                                                </Content>
                                            </telerik:DayTemplate>
                                            <telerik:DayTemplate ID="DayTemplateEntitlementHalfDay" runat="server">
                                                <Content>
                                                    <span class="EntitlementHalfDay">{DayOfMonth}</span>
                                                </Content>
                                            </telerik:DayTemplate>
                                        </CalendarDayTemplates>
                                    </telerik:RadCalendar>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label><b style="color: red">*</b> Start Date</label>
                                </th>
                                <td>
                                    <telerik:RadDatePicker runat="server" CssClass="RadSizeMiddle" ID="RadDatePickerStartDate" Culture="English (Canada)" Enabled="False">
                                        <DateInput DateFormat="MM-dd-yyyy" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerStartDate" Display="Dynamic" ErrorMessage="Start Date Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label><b style="color: red">*</b> End Date</label>
                                </th>
                                <td>
                                    <telerik:RadDatePicker runat="server" CssClass="RadSizeMiddle" ID="RadDatePickerEndDate" Culture="English (Canada)" Enabled="False">
                                        <DateInput DateFormat="MM-dd-yyyy" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerEndDate" Display="Dynamic" ErrorMessage="End Date Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label><b style="color: red">*</b> Days</label>
                                </th>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumTextBoxDays" CssClass="RadSizeMiddle" runat="server" Type="Number" ReadOnly="True">
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="radNumTextBoxDays" Display="Dynamic" ErrorMessage="Days Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label>Remark</label>
                                </th>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxRemark" CssClass="RadSizeMultiLine" TextMode="MultiLine" runat="server" />

                                </td>
                            </tr>
                        </table>
                    </fieldset>

                </div>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function RadToolBar1_OnClientButtonClicking(sender, args) {
                var button = args.get_item();
                if (button.get_text() == "Request") {
                    if (!confirm('Do you want to Request?'))
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

                //Getting rad window manager 
                //var oManager = GetRadWindow().BrowserWindow.GetRadWindowManager();
                //oManager.CloseActiveWindow();
                ////Call GetActiveWindow to get the active window 
                //var oActive = oManager.getActiveWindow();
            }

            function ShowApprovalWindow(id) {
                var oWnd = window.radopen('ApprovalApprovePop?type=' + <%= (int)CConstValue.Approval.Vacation %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalRejectWindow(id) {
                var oWnd = window.radopen('ApprovalRejectPop?type=' + <%= (int)CConstValue.Approval.Vacation %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalReviseWindow(id) {
                var oWnd = window.radopen('ApprovalRevisePop?type=' + <%= (int)CConstValue.Approval.Vacation %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCancelWindow(id) {
                var oWnd = window.radopen('ApprovalCancelPop?type=' + <%= (int)CConstValue.Approval.Vacation %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalAcceptWindow(id) {
                var oWnd = window.radopen('ApprovalAcceptPop?type=' + <%= (int)CConstValue.Approval.Vacation %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCompleteWindow(id) {
                var oWnd = window.radopen('ApprovalCompletePop?type=' + <%= (int)CConstValue.Approval.Vacation %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 300);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function OnClientClose(sender, args) {
                Close();
                //var arg = args.get_argument();
            <%--<%=Page.GetPostBackEventReference(btnRefresh)%>--%>
            }

            function ShowPrint() {
                $("#divPrint").printArea();
            }

        </script>

    </telerik:RadCodeBlock>

</asp:Content>
