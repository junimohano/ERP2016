<%@ Page Title="Class Student Attendance" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="ProgramClassStudentAttendancePop.aspx.cs" Inherits="School.AcademicRegistrar.ProgramClassStudentAttendancePop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadPane2" runat="server" Height="27px" Scrolling="None">
                <h4>Class Student List</h4>
            </telerik:RadPane>

            <%-- Program Student list--%>
            <telerik:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <telerik:RadGrid ID="RadGridClassStudent" runat="server" AllowFilteringByColumn="True" Height="100%"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="20" OnSelectedIndexChanged="RadGridClassStudent_OnSelectedIndexChanged"
                    DataSourceID="LinqDataSourceClassStudentList" ShowFooter="false" OnFilterCheckListItemsRequested="RadGridClassStudent_OnFilterCheckListItemsRequested" AllowMultiRowSelection="False"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="StudentId, ProgramClassStudentId, StartDate, EndDate" TableLayout="Fixed" DataSourceID="LinqDataSourceClassStudentList" AllowMultiColumnSorting="True">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Site Location" DataField="SiteLocationName" SortExpression="SiteLocationName" UniqueName="SiteLocationName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Student No" DataField="StudentNo" SortExpression="StudentNo" UniqueName="StudentNo"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Student Name" DataField="StudentName" SortExpression="StudentName" UniqueName="StudentName"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Country" DataField="CountryName" SortExpression="CountryName" UniqueName="CountryName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Faculty" DataField="FacultyName" SortExpression="FacultyName" UniqueName="FacultyName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Program Group" DataField="ProgramGroupName" SortExpression="ProgramGroupName" UniqueName="ProgramGroupName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Program" DataField="ProgramName" SortExpression="ProgramName" UniqueName="ProgramName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Course" DataField="CourseName" SortExpression="CourseName" UniqueName="CourseName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Level" DataField="LevelName" SortExpression="LevelName" UniqueName="LevelName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Class" DataField="ClassName" SortExpression="ClassName" UniqueName="ClassName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Start Date" DataField="StartDate" SortExpression="StartDate" UniqueName="StartDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="End Date" DataField="EndDate" SortExpression="EndDate" UniqueName="EndDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Gender" DataField="Gender" SortExpression="Gender" UniqueName="Gender"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Program Status" DataField="ProgramStatusName" SortExpression="ProgramStatusName" UniqueName="ProgramStatusName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="True">
                        <Scrolling AllowScroll="true" SaveScrollPosition="true" UseStaticHeaders="true" />
                        <Selecting AllowRowSelect="True" />
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" />
                </telerik:RadGrid>
                <asp:LinqDataSource ID="LinqDataSourceClassStudentList" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                    TableName="vwProgramClassStudentLists" OrderBy="StudentId Descending"
                    Where="StudentId == @StudentId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="StudentId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </telerik:RadPane>

            <telerik:RadSplitBar runat="server" EnableResize="True" CollapseMode="Both" />

            <!-- Down -->
            <telerik:RadPane ID="Radpane" runat="server" Height="50%" Scrolling="None">

                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Vertical">

                    <telerik:RadPane runat="server">
                        <div class="formStyle3">
                            <fieldset>
                                <legend>Command</legend>

                                <div>
                                    <label><b style="color: red">*</b> Attendance Type</label>
                                    <telerik:RadComboBox ID="RadComboBoxAttendanceType" CssClass="RadSizeMiddle" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                </div>
                            </fieldset>

                            <fieldset>
                                <legend>Information</legend>
                                <div>
                                    <label>Today Date</label>
                                    <telerik:RadDatePicker runat="server" ID="RadDatePickerToday" CssClass="RadSizeMiddle" ReadOnly="True" Enabled="False" ReadOnlyStyle-HorizontalAlign="Center"></telerik:RadDatePicker>
                                </div>
                                <div>
                                    <label>Attendance Count</label>
                                    <telerik:RadTextBox runat="server" ID="RadTextBoxAttendanceCount" CssClass="RadSizeMiddle" ReadOnly="True" ReadOnlyStyle-HorizontalAlign="Center"></telerik:RadTextBox>
                                </div>
                                <div>
                                    <label>Attendance Rate</label>
                                    <telerik:RadTextBox runat="server" ID="RadTextBoxAttendanceRate" CssClass="RadSizeMiddle" ReadOnly="True" ReadOnlyStyle-HorizontalAlign="Center"></telerik:RadTextBox>
                                </div>
                            </fieldset>
                        </div>
                    </telerik:RadPane>

                    <telerik:RadSplitBar runat="server" EnableResize="True" CollapseMode="None" />

                    <telerik:RadPane runat="server" Width="50%" Scrolling="None">
                        <telerik:RadSplitter runat="server" ID="RadSplitter4" Orientation="Horizontal">

                            <telerik:RadPane ID="Radpane1" runat="server" Height="27px" Scrolling="None">
                                <h4>Attendance calendar</h4>
                            </telerik:RadPane>

                            <telerik:RadPane ID="Radpane5" runat="server">

                                <telerik:RadCalendar ID="RadCalendarAttendance" runat="server" OnPreRender="RadCalendarAttendance_OnPreRender" OnDayRender="RadCalendarAttendance_OnDayRender" OnLoad="RadCalendarAttendance_OnLoad" OnSelectionChanged="RadCalendarAttendance_OnSelectionChanged" EnableWeekends="False" AutoPostBack="True">
                                    <CalendarDayTemplates>
                                        <telerik:DayTemplate ID="DayTemplateAbsentAttendance" runat="server">
                                            <Content>
                                                <span class="AbsentAttendance">{DayOfMonth}</span>
                                            </Content>
                                        </telerik:DayTemplate>
                                        <telerik:DayTemplate ID="DayTemplateBreakAttendance" runat="server">
                                            <Content>
                                                <span class="BreakAttendance">{DayOfMonth}</span>
                                            </Content>
                                        </telerik:DayTemplate>
                                        <telerik:DayTemplate ID="DayTemplateDismissalAttendance" runat="server">
                                            <Content>
                                                <span class="DismissalAttendance ">{DayOfMonth}</span>
                                            </Content>
                                        </telerik:DayTemplate>
                                        <telerik:DayTemplate ID="DayTemplateExcuseAttendance" runat="server">
                                            <Content>
                                                <span class="ExcuseAttendance ">{DayOfMonth}</span>
                                            </Content>
                                        </telerik:DayTemplate>
                                        <telerik:DayTemplate ID="DayTemplateLateAttendance" runat="server">
                                            <Content>
                                                <span class="LateAttendance">{DayOfMonth}</span>
                                            </Content>
                                        </telerik:DayTemplate>
                                        <telerik:DayTemplate ID="DayTemplatePresentAttendance" runat="server">
                                            <Content>
                                                <span class="PresentAttendance">{DayOfMonth}</span>
                                            </Content>
                                        </telerik:DayTemplate>
                                        <telerik:DayTemplate ID="DayTemplateProbationAttendance" runat="server">
                                            <Content>
                                                <span class="ProbationAttendance">{DayOfMonth}</span>
                                            </Content>
                                        </telerik:DayTemplate>
                                        <telerik:DayTemplate ID="DayTemplateCoopAttendance" runat="server">
                                            <Content>
                                                <span class="CoopAttendance">{DayOfMonth}</span>
                                            </Content>
                                        </telerik:DayTemplate>
                                        <telerik:DayTemplate ID="DayTemplateWithdrawalAttendance" runat="server">
                                            <Content>
                                                <span class="WithdrawalAttendance">{DayOfMonth}</span>
                                            </Content>
                                        </telerik:DayTemplate>
                                    </CalendarDayTemplates>
                                    <ClientEvents OnDateSelecting="RadCalendarAttendance_OnDateSelecting" />
                                </telerik:RadCalendar>
                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPane>

                </telerik:RadSplitter>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function RadCalendarAttendance_OnDateSelecting(sender, args) {
                if (!confirm('Do you want to update it?'))
                    args.set_cancel(true);
            }

            function ShowReportPop(programClassId, reportType) {
                var oWnd = window.radopen('ReportPop?id=' + programClassId + '&reportType=' + reportType, 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;

                if (displayWidth > 850)
                    displayWidth = 850;

                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                //oWnd.add_close(OnClientCloseHandler);
                return false;
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
