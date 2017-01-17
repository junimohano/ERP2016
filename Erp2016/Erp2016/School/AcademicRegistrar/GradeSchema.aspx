<%@ Page Title="Grade Schema" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="GradeSchema.aspx.cs" Inherits="School.AcademicRegistrar.GradeSchema" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <div id="test" style="display: none">
            <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane runat="server" Scrolling="None">

                <telerik:RadSplitter runat="server" ID="RadSplitter3" Height="100%" Width="100%" Orientation="Horizontal">
                    <telerik:RadPane ID="RadPane3" runat="server" Height="27px" Scrolling="None">
                        <h4>Grade Schema name</h4>
                    </telerik:RadPane>

                    <telerik:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                        <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_OnButtonClick">
                            <Items>
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_new.png" Text="Add Grade Name" />
                                <telerik:RadToolBarButton IsSeparator="True" />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/icon_s_edit.png" Text="Modify Grade Name" />
                                <telerik:RadToolBarButton IsSeparator="True" />
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>

                    <telerik:RadPane ID="RadPane2" runat="server" Scrolling="None">
                        <telerik:RadGrid ID="RadGridGradeName" DataSourceID="LinqDataSourceGradeName" runat="server" PageSize="20" AutoGenerateColumns="false" AllowPaging="true" Height="100%"
                            AllowCustomPaging="false" AllowSorting="true" AllowFilteringByColumn="True" EnableLinqExpressions="false" OnFilterCheckListItemsRequested="RadGridGradeName_OnFilterCheckListItemsRequested"
                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                            <MasterTableView DataSourceID="LinqDataSourceGradeName" TableLayout="Fixed" DataKeyNames="GradeSchemaId">
                                <Columns>
                                    <telerik:GridBoundColumn
                                        HeaderText="No" DataField="GradeSchemaId" SortExpression="GradeSchemaId" UniqueName="GradeSchemaId"
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
                                        HeaderText="Grade Name" DataField="Name" SortExpression="Name" UniqueName="Name"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridCheckBoxColumn
                                        HeaderText="IsGlobal" DataField="IsGlobal" SortExpression="IsGlobal" UniqueName="IsGlobal"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridCheckBoxColumn>
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
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                    ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                            </ClientSettings>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" />
                        </telerik:RadGrid>

                        <asp:LinqDataSource ID="LinqDataSourceGradeName" runat="server"
                            ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" TableName="vwGradeSchemaNameLists"
                            Where="GradeSchemaId == @GradeSchemaId">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="0" Name="GradeSchemaId" Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>

                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>

            <telerik:RadSplitBar runat="server" EnableResize="True" CollapseMode="Both"></telerik:RadSplitBar>

            <telerik:RadPane runat="server" Scrolling="None">
                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Vertical">

                    <telerik:RadPane runat="server" Scrolling="None">
                        <telerik:RadSplitter runat="server" ID="RadSplitter5" Orientation="Horizontal">
                            <telerik:RadPane runat="server" Height="27px" Scrolling="None">
                                <h4>Grade item</h4>
                            </telerik:RadPane>

                            <telerik:RadPane runat="server" Scrolling="None">
                                <telerik:RadGrid ID="RadGridGrade" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AllowPaging="False" AutoGenerateColumns="false" DataSourceID="LinqDataSourceGrade" MasterTableView-ShowFooter="True"
                                    OnBatchEditCommand="RadGridGrade_OnBatchEditCommand" PageSize="20" ShowFooter="True" Height="100%" OnPreRender="RadGridGrade_OnPreRender" AllowFilteringByColumn="True" OnFilterCheckListItemsRequested="RadGridGrade_OnFilterCheckListItemsRequested"
                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                    <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add Item" CommandItemSettings-SaveChangesText="Save Changes Item" DataKeyNames="GradeSchemaItemId" DataSourceID="LinqDataSourceGrade" EditMode="Batch">
                                        <BatchEditingSettings EditType="Row" />
                                        <CommandItemSettings ShowSaveChangesButton="True" AddNewRecordText="Add Item" SaveChangesText="Save Changes Item" />
                                        <Columns>

                                            <telerik:GridTemplateColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName" FooterText="Total">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("ItemName") %>' Width="100%" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox runat="server" Width="100%" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn DataField="Score" HeaderText="Score" UniqueName="Score">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("Score") %>' Width="100%" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="radNumTextBoxScore" runat="server" Type="Number" Width="100%">
                                                        <ClientEvents OnValueChanged="RadGridGrade_ValueChanged" />
                                                    </telerik:RadNumericTextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Number" Width="100%">
                                                        <ClientEvents OnLoad="ScoreLoad" />
                                                    </telerik:RadNumericTextBox>
                                                </FooterTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px" ConfirmDialogType="RadWindow" ConfirmText="Delete this Item?" ConfirmTitle="Delete" HeaderStyle-Width="30px" HeaderText="Del" Text="Delete" UniqueName="DeleteColumn">
                                                <HeaderStyle Width="30px" />
                                            </telerik:GridButtonColumn>

                                        </Columns>

                                    </MasterTableView>

                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                        <ClientEvents OnCommand="RadGridGrade_Command" OnGridCreated="RadGridGrade_GridCreated" OnRowDeleted="RadGridGrade_RowDeleted" />
                                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                    </ClientSettings>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" />
                                </telerik:RadGrid>

                                <asp:LinqDataSource ID="LinqDataSourceGrade" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="GradeSchemaItems"
                                    Where="GradeSchemaItemId == @GradeSchemaItemId" OrderBy="Score DESC">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="0" Name="GradeSchemaItemId" Type="Int32" />
                                    </WhereParameters>

                                </asp:LinqDataSource>
                            </telerik:RadPane>
                        </telerik:RadSplitter>

                    </telerik:RadPane>

                    <telerik:RadSplitBar runat="server" EnableResize="True" CollapseMode="Both"></telerik:RadSplitBar>

                    <telerik:RadPane runat="server" Scrolling="None">
                        <telerik:RadSplitter runat="server" ID="RadSplitter6" Orientation="Horizontal">
                            <telerik:RadPane runat="server" Height="27px" Scrolling="None">
                                <h4>Grade Letter item</h4>
                            </telerik:RadPane>

                            <telerik:RadPane runat="server" Scrolling="None">
                                <telerik:RadGrid ID="RadGridGradeLetter" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AllowPaging="False" AutoGenerateColumns="false" DataSourceID="LinqDataSourceGradeLetter" MasterTableView-ShowFooter="False"
                                    OnBatchEditCommand="RadGridGradeLetter_OnBatchEditCommand" PageSize="20" ShowFooter="True" Height="100%" OnPreRender="RadGridGradeLetter_OnPreRender" AllowFilteringByColumn="True" OnFilterCheckListItemsRequested="RadGridGradeLetter_OnFilterCheckListItemsRequested"
                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                    <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add Item" CommandItemSettings-SaveChangesText="Save Changes Item" DataKeyNames="GradeSchemaLetterItemId" DataSourceID="LinqDataSourceGradeLetter" EditMode="Batch">
                                        <BatchEditingSettings EditType="Row" />
                                        <CommandItemSettings ShowSaveChangesButton="True" AddNewRecordText="Add Item" SaveChangesText="Save Changes Item" />
                                        <Columns>

                                            <telerik:GridTemplateColumn DataField="LetterGrade" HeaderText="Letter Grade" UniqueName="LetterGrade">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("LetterGrade") %>' Width="100%" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox runat="server" Width="100%" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn DataField="RangeFrom" HeaderText="Range From" UniqueName="RangeFrom">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("RangeFrom") %>' Width="100%" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="radNumTextBoxRangeFrom" runat="server" Type="Number" Width="100%" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn DataField="RangeTo" HeaderText="Range To" UniqueName="RangeTo">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("RangeTo") %>' Width="100%" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="radNumTextBoxRangeTo" runat="server" Type="Number" Width="100%" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px" ConfirmDialogType="RadWindow" ConfirmText="Delete this Item?" ConfirmTitle="Delete" HeaderStyle-Width="30px" HeaderText="Del" Text="Delete" UniqueName="DeleteColumn">
                                                <HeaderStyle Width="30px" />
                                            </telerik:GridButtonColumn>
                                        </Columns>

                                    </MasterTableView>

                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                    </ClientSettings>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" />
                                </telerik:RadGrid>

                                <asp:LinqDataSource ID="LinqDataSourceGradeLetter" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="GradeSchemaLetterItems"
                                    Where="GradeSchemaLetterItemId == @GradeSchemaLetterItemId" OrderBy="RangeFrom DESC">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="0" Name="GradeSchemaLetterItemId" Type="Int32" />
                                    </WhereParameters>

                                </asp:LinqDataSource>
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

            function ShowPop(id, type) {
                var oWnd = window.radopen('GradeSchemaPop?id=' + id + '&type=' + type, 0, 0, 0, 0);
                //var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;
                //if (displayWidth > 1500)
                //    displayWidth = 1500;
                //oWnd.setSize(displayWidth, displayHeight);
                oWnd.setSize(700, 500);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function OnClientClose(sender, args) {
                <%= Page.GetPostBackEventReference(ButtonGridRefresh) %>;

            }
        </script>


    </telerik:RadCodeBlock>
</asp:Content>
