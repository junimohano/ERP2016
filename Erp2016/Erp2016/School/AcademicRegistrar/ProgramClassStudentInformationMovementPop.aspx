<%@ Page Title="Movement" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="ProgramClassStudentInformationMovementPop.aspx.cs" Inherits="School.AcademicRegistrar.ProgramClassStudentInformationMovementPop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter2" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane4" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnClientButtonClicking=" ToolbarButtonClick " OnButtonClick="RadToolBar1_OnButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Save" ValidationGroup="Info" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane5" runat="server" Height="290px">
                <div class="formStyle3">

                    <fieldset>
                        <legend>Information</legend>

                        <div>
                            <div>
                                <label>Faculty</label>
                                <telerik:RadComboBox ID="RadComboBoxFaculty" Width="300" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxFaculty_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" Enabled="False" />
                            </div>
                            <div>
                                <label>Program Group</label>
                                <telerik:RadComboBox ID="RadComboBoxProgramGroup" Width="300" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgramGroup_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" Enabled="False" />
                            </div>
                            <div>
                                <label>Program</label>
                                <telerik:RadComboBox ID="RadComboBoxProgram" Width="300" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgram_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" Enabled="False" />
                            </div>
                            <div>
                                <label>Course</label>
                                <telerik:RadComboBox ID="RadComboBoxProgramCourse" Width="300" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgramCourse_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" Enabled="False" />
                            </div>
                            <div>
                                <label>Level</label>
                                <telerik:RadComboBox ID="RadComboBoxProgramCourseLevel" Width="300" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgramCourseLevel_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                            </div>
                            <div>
                                <label><b style="color: red">*</b> Class</label>
                                <telerik:RadComboBox ID="RadComboBoxProgramClass" Width="300" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadComboBoxProgramClass_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxProgramClass" Display="Dynamic" ErrorMessage="Class Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                        </div>

                    </fieldset>
                </div>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="RadSplitbar3" runat="server" CollapseMode="Both" EnableResize="True"></telerik:RadSplitBar>

            <%-- Program Student list--%>
            <telerik:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <telerik:RadGrid ID="RadGridClassStudent" runat="server" AllowFilteringByColumn="True" Height="100%"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="20"
                    DataSourceID="LinqDataSourceClassStudentList" ShowFooter="false" OnFilterCheckListItemsRequested="RadGridClassStudent_OnFilterCheckListItemsRequested" AllowMultiRowSelection="True"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="StudentId, ProgramClassStudentId" TableLayout="Fixed" DataSourceID="LinqDataSourceClassStudentList" AllowMultiColumnSorting="True">
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
                    <ClientSettings EnableRowHoverStyle="True">
                        <Scrolling AllowScroll="true" SaveScrollPosition="true" UseStaticHeaders="true" />
                        <Selecting AllowRowSelect="True" />
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
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

        </telerik:RadSplitter>

    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();

                if (button.get_text() === "Save") {
                    if (!confirm('Do you want to save it?'))
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
