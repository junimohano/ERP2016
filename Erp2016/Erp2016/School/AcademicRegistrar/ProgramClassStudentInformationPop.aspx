<%@ Page Title="Class Student Information" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="ProgramClassStudentInformationPop.aspx.cs" Inherits="School.AcademicRegistrar.ProgramClassStudentInformationPop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <div style="display: none">
            <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadPane1" runat="server" Height="27px" Scrolling="None">
                <h4>Class Student List</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane4" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_OnButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/icon_s_edit.png" Text="Move students to another class" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <%-- Program Student list--%>
            <telerik:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <telerik:RadGrid ID="RadGridClassStudent" runat="server" AllowFilteringByColumn="True" Height="100%"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="20"
                    DataSourceID="LinqDataSourceClassStudentList" ShowFooter="false" OnFilterCheckListItemsRequested="RadGridClassStudent_OnFilterCheckListItemsRequested" OnRowDrop="RadGridClassStudent_OnRowDrop" AllowMultiRowSelection="True"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="StudentId, ProgramClassStudentId" TableLayout="Fixed" DataSourceID="LinqDataSourceClassStudentList" AllowMultiColumnSorting="True">
                        <Columns>
                            <%--<telerik:GridBoundColumn
                                HeaderText="No" DataField="ProgramClassStudentId" SortExpression="ProgramClassStudentId" UniqueName="ProgramClassStudentId"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>--%>
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
                            <telerik:GridCheckBoxColumn
                                HeaderText="IsActive" DataField="IsActive" SortExpression="IsActive" UniqueName="IsActive"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridCheckBoxColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="True" AllowRowsDragDrop="True">
                        <Scrolling AllowScroll="true" SaveScrollPosition="true" UseStaticHeaders="true" />
                        <Selecting AllowRowSelect="True" />
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                        <ClientEvents OnRowDropping="RadGridClassStudent_OnRowDropping"></ClientEvents>
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

            <telerik:RadSplitBar runat="server" EnableResize="True" CollapseMode="Both" />

            <telerik:RadPane ID="GridPane" runat="server" Height="27px" Scrolling="None">
                <h4>Unassigned Student List</h4>
            </telerik:RadPane>

            <%-- Class Student list --%>
            <telerik:RadPane ID="RadPane2" runat="server" Height="50%" Scrolling="None">

                <telerik:RadGrid ID="RadGridProgramStudent" runat="server" AllowFilteringByColumn="True" Height="100%"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="20"
                    DataSourceID="LinqDataSourceProgramStudentList" ShowFooter="false" OnFilterCheckListItemsRequested="RadGridProgramStudent_OnFilterCheckListItemsRequested" OnRowDrop="RadGridProgramStudent_OnRowDrop" AllowMultiRowSelection="True"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="StudentId, ProgramRegistrationId" TableLayout="Fixed" DataSourceID="LinqDataSourceProgramStudentList" AllowMultiColumnSorting="True">
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
                            <telerik:GridCheckBoxColumn
                                HeaderText="IsActive" DataField="IsActive" SortExpression="IsActive" UniqueName="IsActive"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridCheckBoxColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="True" AllowRowsDragDrop="True">
                        <Scrolling AllowScroll="true" SaveScrollPosition="true" UseStaticHeaders="true" />
                        <Selecting AllowRowSelect="True" />
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                        <ClientEvents OnRowDropping="RadGridProgramStudent_OnRowDropping"></ClientEvents>
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                </telerik:RadGrid>
                <asp:LinqDataSource ID="LinqDataSourceProgramStudentList" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                    TableName="vwProgramStudentLists" OrderBy="StudentId Descending"
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
            function RadGridProgramStudent_OnRowDropping(sender, args) {
                if (!confirm('Do you want to add it?'))
                    args.set_cancel(true);
            }
            function RadGridClassStudent_OnRowDropping(sender, args) {
                if (!confirm('Do you want to remove it?'))
                    args.set_cancel(true);
            }

            function ShowStudentMovement(programClassStudentIdList) {
                var oWnd = window.radopen('ProgramClassStudentInformationMovementPop?programClassStudentIdList=' + programClassStudentIdList, 0, 0, 0, 0);
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

            function OnClientClose(oWnd, args) {
                <%= Page.GetPostBackEventReference(ButtonGridRefresh) %>;
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
