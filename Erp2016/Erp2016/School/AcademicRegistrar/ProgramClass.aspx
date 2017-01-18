<%@ Page Title="Class Schedules" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="ProgramClass.aspx.cs" Inherits="School.AcademicRegistrar.ProgramClass" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <div style="display: none">
            <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">
            <telerik:RadPane ID="Radpane4" runat="server" Height="27px" Scrolling="None">
                <h4>Class List</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane3" runat="server" Height="35%" Scrolling="None">
                <telerik:RadGrid ID="RadGridProgramClass" DataSourceID="LinqDataSourceProgramClass" runat="server" PageSize="20" AutoGenerateColumns="false" AllowPaging="true"
                    AllowSorting="true" AllowFilteringByColumn="true" Height="100%"
                    OnSelectedIndexChanged="RadGridProgramClass_OnSelectedIndexChanged" OnFilterCheckListItemsRequested="RadGridProgramClass_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataSourceID="LinqDataSourceProgramClass" TableLayout="Fixed" DataKeyNames="ProgramClassId, ProgramCourseId">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="No" DataField="ProgramClassId" SortExpression="ProgramClassId" UniqueName="ProgramClassId"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Site Location" DataField="SiteLocationName" SortExpression="SiteLocationName" UniqueName="SiteLocationName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
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
                            <telerik:GridBoundColumn
                                HeaderText="Instructor" DataField="InstructorName" SortExpression="InstructorName" UniqueName="InstructorName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="ClassRoom" DataField="ClassRoomName" SortExpression="ClassRoomName" UniqueName="ClassRoomName"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
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
                                HeaderText="Class Week" DataField="ClassWeek" SortExpression="ClassWeek" UniqueName="ClassWeek"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Active Students" DataField="ActiveStudents" SortExpression="ActiveStudents" UniqueName="ActiveStudents"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Total Students" DataField="TotalStudents" SortExpression="TotalStudents" UniqueName="TotalStudents"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Room Capacity" DataField="RoomCapacity" SortExpression="RoomCapacity" UniqueName="RoomCapacity"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Created User" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridCheckBoxColumn
                                HeaderText="IsActive" DataField="IsActive" SortExpression="IsActive" UniqueName="IsActive"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridCheckBoxColumn>
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
                <asp:LinqDataSource ID="LinqDataSourceProgramClass" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" TableName="vwProgramClasses" OrderBy="CreatedDate DESC, SiteLocationName, ProgramName, CourseName, LevelName"
                    Where="ProgramClassId == @ProgramClassId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="ProgramClassId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
                <%--</fieldset>--%>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="Radsplitter4" runat="server" Orientation="Horizontal">

                    <telerik:RadPane ID="Radpane1" runat="server" Height="40px" Scrolling="None">
                        <telerik:RadToolBar ID="RadToolBarProgramClass" runat="server" OnClientButtonClicking=" ToolbarButtonClick " OnButtonClick="RadToolBarProgramClass_OnButtonClick">
                            <Items>
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Save" ValidationGroup="Info" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_new.png" Enabled="false" Text="New" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/icon_s_edit.png" Text="Student Information" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/icon_s_edit.png" Text="Student Attendance" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/icon_s_edit.png" Text="Student Grade" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarSplitButton ImageUrl="~/assets/img/Document-20.png" EnableDefaultButton="False" Text="Report" ToolTip="Report">
                                    <Buttons>
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/Document-20.png" Text="Class Summary" ToolTip="Class Summary" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/Document-20.png" Text="Starting Students" ToolTip="Starting Students" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/Document-20.png" Text="Completed Graduates Students" ToolTip="Completed Graduates Students" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/Document-20.png" Text="Attendance Sheet" ToolTip="Attendance Sheet" />
                                    </Buttons>
                                </telerik:RadToolBarSplitButton>
                                <telerik:RadToolBarButton IsSeparator="true" />
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>

                    <telerik:RadPane ID="Radpane5" runat="server">
                        <div class="formStyle3">
                            <div style="float: left; width: 100%;">
                                <fieldset>
                                    <legend>Information</legend>

                                    <div style="float: left; width: 50%;">
                                        <div>
                                            <label>Faculty</label>
                                            <telerik:RadComboBox ID="RadComboBoxFaculty" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxFaculty_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                            <br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label>Program Group</label>
                                            <telerik:RadComboBox ID="RadComboBoxProgramGroup" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgramGroup_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                            <br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> Program</label>
                                            <telerik:RadComboBox ID="RadComboBoxProgram" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgram_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxProgram" Display="Dynamic" ErrorMessage="Program Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label>Course</label>
                                            <telerik:RadComboBox ID="RadComboBoxProgramCourse" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgramCourse_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                            <br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label>Level</label>
                                            <telerik:RadComboBox ID="RadComboBoxProgramCourseLevel" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgramCourseLevel_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                            <br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> Class</label>
                                            <telerik:RadTextBox ID="RadTextBoxProgramClass" CssClass="RadSizeLarge" runat="server" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxProgramClass" Display="Dynamic" ErrorMessage="Class Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label>Description</label>
                                            <telerik:RadTextBox ID="RadTextBoxDescription" CssClass="RadSizeMultiLine" runat="server" TextMode="MultiLine" />
                                            <br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> Active</label>
                                            <telerik:RadButton ID="RadButtonActive" runat="server" AutoPostBack="False" ToggleType="CheckBox" ButtonType="ToggleButton" />
                                            <br style="clear: both;" />
                                        </div>
                                    </div>

                                    <div style="float: left; width: 50%;">
                                        <div>
                                            <label><b style="color: red">*</b> Instructor</label>
                                            <telerik:RadComboBox ID="RadComboBoxInstructor" CssClass="RadSizeMiddle" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxInstructor" Display="Dynamic" ErrorMessage="Instructor Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> Class Room</label>
                                            <telerik:RadComboBox ID="RadComboBoxClassRoom" CssClass="RadSizeLarge" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxClassRoom" Display="Dynamic" ErrorMessage="Class Room Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> Start Date</label>
                                            <telerik:RadDatePicker ID="RadDatePickerStartDate" CssClass="RadSizeMiddle" AutoPostBack="true" OnSelectedDateChanged="RadDatePickerStartDate_OnSelectedDateChanged" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerStartDate" Display="Dynamic" ErrorMessage="Start Date Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> Weeks</label>
                                            <telerik:RadComboBox ID="RadComboBoxWeeks" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxWeeks_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxWeeks" Display="Dynamic" ErrorMessage="Weeks Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> End Date</label>
                                            <telerik:RadDatePicker ID="RadDatePickerEndDate" CssClass="RadSizeMiddle" AutoPostBack="True" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerEndDate" Display="Dynamic" ErrorMessage="End Date Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label>Class Days</label>
                                            <telerik:RadNumericTextBox ID="RadNumericTextBoxClassDays" CssClass="RadSizeMiddle" AutoPostBack="True" Type="Number" ReadOnly="True" runat="server"></telerik:RadNumericTextBox>
                                            <br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label>Class Hours</label>
                                            <telerik:RadNumericTextBox ID="RadNumericTextBoxClassHours" CssClass="RadSizeMiddle" AutoPostBack="True" Type="Number" ReadOnly="True" runat="server"></telerik:RadNumericTextBox>
                                            <br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label>Todal Class Hours</label>
                                            <telerik:RadNumericTextBox ID="RadNumericTextBoxTotalHours" CssClass="RadSizeMiddle" AutoPostBack="True" Type="Number" ReadOnly="True" runat="server"></telerik:RadNumericTextBox>
                                            <br style="clear: both;" />
                                        </div>
                                    </div>
                                </fieldset>

                                <fieldset>
                                    <legend>Class Time</legend>

                                    <table>
                                        <tr>
                                            <th>
                                                <label>Day</label>
                                            </th>
                                            <th>
                                                <label>Mon</label>
                                            </th>
                                            <th>
                                                <label>Tue</label>
                                            </th>
                                            <th>
                                                <label>Wed</label>
                                            </th>
                                            <th>
                                                <label>Thu</label>
                                            </th>
                                            <th>
                                                <label>Fri</label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label><b style="color: red">*</b> Start Time</label>
                                            </td>
                                            <td>
                                                <telerik:RadTimePicker runat="server" ID="RadTimePickerStartMon" CssClass="RadSizeSmall" AutoPostBack="True" OnSelectedDateChanged="RadTimePicker_OnSelectedDateChanged" />
                                            </td>
                                            <td>
                                                <telerik:RadTimePicker runat="server" ID="RadTimePickerStartTue" CssClass="RadSizeSmall" AutoPostBack="True" OnSelectedDateChanged="RadTimePicker_OnSelectedDateChanged" />
                                            </td>
                                            <td>
                                                <telerik:RadTimePicker runat="server" ID="RadTimePickerStartWed" CssClass="RadSizeSmall" AutoPostBack="True" OnSelectedDateChanged="RadTimePicker_OnSelectedDateChanged" />
                                            </td>
                                            <td>
                                                <telerik:RadTimePicker runat="server" ID="RadTimePickerStartThu" CssClass="RadSizeSmall" AutoPostBack="True" OnSelectedDateChanged="RadTimePicker_OnSelectedDateChanged" />
                                            </td>
                                            <td>
                                                <telerik:RadTimePicker runat="server" ID="RadTimePickerStartFri" CssClass="RadSizeSmall" AutoPostBack="True" OnSelectedDateChanged="RadTimePicker_OnSelectedDateChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label><b style="color: red">*</b> End Time</label>
                                            </td>
                                            <td>
                                                <telerik:RadTimePicker runat="server" ID="RadTimePickerEndMon" CssClass="RadSizeSmall" AutoPostBack="True" OnSelectedDateChanged="RadTimePicker_OnSelectedDateChanged" />
                                            </td>
                                            <td>
                                                <telerik:RadTimePicker runat="server" ID="RadTimePickerEndTue" CssClass="RadSizeSmall" AutoPostBack="True" OnSelectedDateChanged="RadTimePicker_OnSelectedDateChanged" />
                                            </td>
                                            <td>
                                                <telerik:RadTimePicker runat="server" ID="RadTimePickerEndWed" CssClass="RadSizeSmall" AutoPostBack="True" OnSelectedDateChanged="RadTimePicker_OnSelectedDateChanged" />
                                            </td>
                                            <td>
                                                <telerik:RadTimePicker runat="server" ID="RadTimePickerEndThu" CssClass="RadSizeSmall" AutoPostBack="True" OnSelectedDateChanged="RadTimePicker_OnSelectedDateChanged" />
                                            </td>
                                            <td>
                                                <telerik:RadTimePicker runat="server" ID="RadTimePickerEndFri" CssClass="RadSizeSmall" AutoPostBack="True" OnSelectedDateChanged="RadTimePicker_OnSelectedDateChanged" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </div>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            // call when page load.
            function pageLoad() {
                var grid = $find("<%= RadGridProgramClass.ClientID %>");
               if (grid != null) {
                   var columns = grid.get_masterTableView().get_columns();
                   for (var i = 0; i < columns.length; i++) {
                       columns[i].resizeToFit(false, true);
                   }
               }
           }

           function ToolbarButtonClick(sender, args) {
               var button = args.get_item();
               if (button.get_text() === "Update") {
                   if (!confirm('Do you want to update it?'))
                       args.set_cancel(true);
               } else if (button.get_text() === "Save") {
                   if (!confirm('Do you want to save it?'))
                       args.set_cancel(true);
               }
           }

           function ShowStudentClassInfo(programClassId, programCourseId) {
               var oWnd = window.radopen('ProgramClassStudentInformationPop?programClassId=' + programClassId + '&programCourseId=' + programCourseId, 0, 0, 0, 0);
               var displayWidth = $(window).width() * 0.95;
               var displayHeight = $(window).height() * 0.95;
               if (displayWidth > 1500)
                   displayWidth = 1500;
               oWnd.setSize(displayWidth, displayHeight);
               oWnd.center();
               oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
               oWnd.add_close(OnClientClose);
               return false;
           }

           function ShowStudentClassAttendance(programClassId) {
               var oWnd = window.radopen('ProgramClassStudentAttendancePop?programClassId=' + programClassId, 0, 0, 0, 0);
               var displayWidth = $(window).width() * 0.95;
               var displayHeight = $(window).height() * 0.95;
               if (displayWidth > 1500)
                   displayWidth = 1500;
               oWnd.setSize(displayWidth, displayHeight);
               oWnd.center();
               oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
               //oWnd.add_close(OnClientClose);
               return false;
           }

           function ShowStudentClassGrade(programClassId) {
               var oWnd = window.radopen('ProgramClassStudentGradePop?programClassId=' + programClassId, 0, 0, 0, 0);
               var displayWidth = $(window).width() * 0.95;
               var displayHeight = $(window).height() * 0.95;
               if (displayWidth > 1500)
                   displayWidth = 1500;
               oWnd.setSize(displayWidth, displayHeight);
               oWnd.center();
               oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
               //oWnd.add_close(OnClientClose);
               return false;
           }

           function ShowReportPop(invoiceId, reportType) {
               var oWnd = window.radopen('ReportPop?id=' + invoiceId + '&reportType=' + reportType, 0, 0, 0, 0);
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

           function OnClientClose(oWnd, args) {
               <%= Page.GetPostBackEventReference(ButtonGridRefresh) %>;
            }

        </script>

    </telerik:RadCodeBlock>

</asp:Content>
