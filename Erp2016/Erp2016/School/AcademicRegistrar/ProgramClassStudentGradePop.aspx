<%@ Page Title="Grade" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="ProgramClassStudentGradePop.aspx.cs" Inherits="School.AcademicRegistrar.ProgramClassStudentGradePop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadPane2" runat="server" Height="27px" Scrolling="None">
                <h4>Class Student List</h4>
            </telerik:RadPane>

            <%-- Program Student list--%>
            <telerik:RadPane ID="RadPane8" runat="server" Scrolling="None">
                <telerik:RadGrid ID="RadGridClassStudent" runat="server" AllowFilteringByColumn="True" Height="100%"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="20" OnSelectedIndexChanged="RadGridClassStudent_OnSelectedIndexChanged"
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
                    <ClientSettings EnableRowHoverStyle="True" EnablePostBackOnRowClick="True">
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

            <telerik:RadSplitBar runat="server" EnableResize="True" CollapseMode="Both"></telerik:RadSplitBar>

            <!-- Down -->
            <telerik:RadPane ID="Radpane6" runat="server" Scrolling="None">

                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Vertical">

                    <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">
                        <telerik:RadSplitter runat="server" ID="RadSplitter3" Orientation="Horizontal">
                            <telerik:RadPane ID="Radpane7" runat="server">

                                <div class="formStyle3">
                                    <fieldset>
                                        <legend>Grade Info</legend>
                                        <div>
                                            <label>Grade Name</label><telerik:RadTextBox runat="server" ID="RadTextBoxGrade" CssClass="RadSizeMiddle" ReadOnly="True" EnabledStyle-HorizontalAlign="Center" />
                                            <br style="clear: both;" />
                                        </div>
                                    </fieldset>

                                    <fieldset>
                                        <legend>Grade Letter</legend>
                                        <div>
                                            <label>Letter Name</label><telerik:RadTextBox runat="server" ID="RadTextBoxLetter" CssClass="RadSizeMiddle" ReadOnly="True" EnabledStyle-HorizontalAlign="Center" />
                                            <br style="clear: both;" />
                                        </div>
                                    </fieldset>
                                </div>

                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </telerik:RadPane>

                    <telerik:RadPane ID="Radpane4" runat="server" Scrolling="None" Width="50%">
                        <telerik:RadSplitter runat="server" ID="RadSplitter4" Orientation="Horizontal">
                            <telerik:RadPane ID="Radpane1" runat="server" Height="27px" Scrolling="None">
                                <h4>Student Grade</h4>
                            </telerik:RadPane>

                            <telerik:RadPane ID="Radpane3" runat="server" Scrolling="None">
                                <telerik:RadGrid ID="RadGridGrade" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AllowPaging="False" AutoGenerateColumns="false" DataSourceID="LinqDataSourceGrade" MasterTableView-ShowFooter="true" OnBatchEditCommand="RadGridGrade_OnBatchEditCommand" OnPreRender="RadGridGrade_OnPreRender" PageSize="20" ShowFooter="True" Height="100%">
                                    <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add Invoice Item" CommandItemSettings-SaveChangesText="Save Changes Invoice Item" DataKeyNames="GradeId" DataSourceID="LinqDataSourceGrade" EditMode="Batch" HorizontalAlign="NotSet">
                                        <BatchEditingSettings EditType="Row" />
                                        <CommandItemSettings ShowSaveChangesButton="True" AddNewRecordText="Add Item" SaveChangesText="Save Changes Item" ShowAddNewRecordButton="False" ShowCancelChangesButton="False" />
                                        <Columns>

                                            <telerik:GridTemplateColumn HeaderText="GradeSchemaItem" UniqueName="GradeSchemaItem" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100%">
                                                <ItemTemplate>
                                                    <telerik:RadComboBox runat="server" Enabled="False" DataSourceID="LinqDataSourceGradeSchemaItem" DataValueField="GradeSchemaItemId" DataTextField="ItemName" EnableScreenBoundaryDetection="False" SelectedValue='<%# Eval("GradeSchemaItemId") %>' Width="100%"></telerik:RadComboBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Total Score" UniqueName="TotalScore" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100%">
                                                <ItemTemplate>
                                                    <telerik:RadComboBox runat="server" DataSourceID="LinqDataSourceGradeSchemaItem" DataValueField="GradeSchemaItemId" DataTextField="Score" SelectedValue='<%# Eval("GradeSchemaItemId") %>' Enabled="False" Width="100%"></telerik:RadComboBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Score" UniqueName="Score" DataField="Score" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100%">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("Score") %>' Width="100%" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="radNumTextBoxScore" runat="server" Type="Number" Width="100%">
                                                        <ClientEvents OnValueChanged="RadGridGrade_ValueChanged" />
                                                    </telerik:RadNumericTextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Center" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Center" Type="Number" Width="100%">
                                                        <ClientEvents OnLoad="ScoreLoad" />
                                                    </telerik:RadNumericTextBox>
                                                </FooterTemplate>
                                            </telerik:GridTemplateColumn>

                                        </Columns>

                                    </MasterTableView>

                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                        <ClientEvents OnCommand="RadGridGrade_Command" OnGridCreated="RadGridGrade_GridCreated" OnRowDeleted="RadGridGrade_RowDeleted" />
                                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                    </ClientSettings>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                </telerik:RadGrid>

                                <asp:LinqDataSource ID="LinqDataSourceGradeSchemaItem" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" TableName="GradeSchemaItems" />
                                <asp:LinqDataSource ID="LinqDataSourceGrade" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="Grades" />

                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPane>

                </telerik:RadSplitter>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            // jQuery
            $(window).bind("load", function () {
                SetScoreValue();
            });

            // =====================
            // total sum
            // =====================
            // Score
            var sumScoreInput = null;

            function ScoreLoad(sender, args) {
                sumScoreInput = sender;
            }

            function GetCellValue(row, columnUniqueName, controlId) {
                var value;
                var testControl = row.findControl(controlId);
                if (testControl) {
                    value = testControl.get_value();
                } else {
                    value = row.get_cell(columnUniqueName).innerText.replace(/[^\d.-]/g, '');
                }
                if (value == "")
                    value = 0;
                return parseFloat(value);
            }

            // set value
            function SetScoreValue() {
                var grid = $find("<%= RadGridGrade.ClientID %>"); //grid id

                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var rows = masterTable.get_dataItems();

                    var totalMount = 0.0;
                    for (var i = 0; i < rows.length; i++) {
                        totalMount = totalMount + GetCellValue(rows[i], "Score", "radNumTextBoxScore");
                    }
                    if (sumScoreInput != null)
                        sumScoreInput.set_value(totalMount);
                }
            }

            // when grid created
            function RadGridGrade_GridCreated(sender, eventArgs) {
                SetScoreValue();
            }

            // action
            function RadGridGrade_Command(sender, eventArgs) {
                if (eventArgs.get_commandName() == "Rebind")
                    SetScoreValue();
            }

            // delete
            function RadGridGrade_RowDeleted(sender, eventArgs) {
                SetScoreValue();
            }

            // change value
            function RadGridGrade_ValueChanged(sender, eventArgs) {
                SetScoreValue();
            }
            // == end total sum ==
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
