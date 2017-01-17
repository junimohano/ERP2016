<%@ Page Title="Vacation Info" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="VacationInfoPop.aspx.cs" Inherits="School.OfficeAdmin.VacationInfoPop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ApprovalListView">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ApprovalListView" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadCalendar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadCalendar1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%" OnAjaxRequest="RadAjaxPanel1_OnAjaxRequest">--%>
    <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

        <telerik:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
            <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_ButtonClick">
                <Items>
                    <telerik:RadToolBarButton runat="server" Text="Print" />
                    <telerik:RadToolBarButton IsSeparator="true" />
                    <telerik:RadToolBarButton runat="server" Text="Close" />
                    <telerik:RadToolBarButton IsSeparator="true" />
                </Items>
            </telerik:RadToolBar>
        </telerik:RadPane>

        <telerik:RadPane ID="RadPane1" runat="server" Scrolling="Both">

            <div id="divPrint">

                <telerik:RadGrid ID="RadGridInfo" runat="server" GroupPanelPosition="Top" />

                <telerik:RadGrid ID="RadGridDays" runat="server" GroupPanelPosition="Top" OnLoad="RadGridDays_Load" />

                <telerik:RadCalendar ID="RadCalendar1" runat="server"
                    OnSelectionChanged="RadCalendar1_OnSelectionChanged" AutoPostBack="true" ShowOtherMonthsDays="false" MultiViewColumns="4" MultiViewRows="3" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    EnableMultiSelect="false" OnPreRender="RadCalendar1_OnPreRender" OnDayRender="RadCalendar1_OnDayRender" OnLoad="RadCalendar1_OnLoad">
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

            </div>
        </telerik:RadPane>

    </telerik:RadSplitter>
    <%--</telerik:RadAjaxPanel>--%>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

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

            function ShowNewPop(id, createOrListType, requestOrApprovalType, approvalType) {
                var oWnd = window.radopen('VacationPop?id=' + id + '&createOrListType=' + createOrListType + '&requestOrApprovalType=' + requestOrApprovalType + '&approvalType=' + approvalType + '', 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;
                if (displayWidth > 1500)
                    displayWidth = 1500;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                //oWnd.add_close(OnClientCloseHandler);
                return false;
            }

            function ShowPrint() {
                $("#divPrint").printArea();
            }

        </script>

    </telerik:RadCodeBlock>

</asp:Content>
