<%@ Page Title="Program Groups" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="ProgramGroup.aspx.cs" Inherits="School.AcademicRegistrar.ProgramGroup" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">
            <telerik:RadPane ID="GridPane" runat="server" Height="27px" Scrolling="None">
                <h4>Program Group List</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="RadPane2" runat="server" Height="60%" Scrolling="None">
                <telerik:RadGrid ID="Grid" DataSourceID="LinqDataSourceProgramGroup" runat="server" PageSize="30" AutoGenerateColumns="false" AllowPaging="true"
                    AllowSorting="true" AllowFilteringByColumn="true" Height="100%"
                    OnSelectedIndexChanged="SelectProgram" OnFilterCheckListItemsRequested="Grid_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataSourceID="LinqDataSourceProgramGroup" TableLayout="Fixed" DataKeyNames="ProgramGroupId">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="No" DataField="ProgramGroupId" SortExpression="ProgramGroupId" UniqueName="ProgramGroupId"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
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
                                HeaderText="Description" DataField="Description" SortExpression="Description" UniqueName="Description"
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

                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="True" />
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                </telerik:RadGrid>

                <asp:LinqDataSource ID="LinqDataSourceProgramGroup" runat="server" ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" TableName="vwProgramGroups" OrderBy="CreatedDate DESC" />

            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radslitbar1" runat="server" CollapseMode="Both" EnableResize="True"></telerik:RadSplitBar>

            <telerik:RadPane ID="MainPane" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="RadSplitter2" runat="server" Orientation="Horizontal">

                    <telerik:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                        <telerik:RadToolBar ID="RadToolBarProgramGroup" runat="server" OnButtonClick="ToolbarClick" OnClientButtonClicking=" ToolbarClick ">
                            <Items>
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Save" ValidationGroup="Info" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_new.png" Text="New" Enabled="false" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>

                    <telerik:RadPane ID="RadPane3" runat="server">
                        <div class="formStyle3">
                            <fieldset>
                                <legend>Information</legend>
                                <div>
                                    <label>Faculty</label>
                                    <telerik:RadComboBox ID="RadComboBoxFaculty" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" OnSelectedIndexChanged="ddlFaculty_OnSelectedIndexChanged" />
                                </div>
                                <div>
                                    <label><b style="color: red">*</b> Program Group</label>
                                    <telerik:RadTextBox ID="tbProgram" runat="server" CssClass="RadSizeLarge" />
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbProgram" Display="Dynamic" ErrorMessage="Program Group Required" ForeColor="Red" ValidationGroup="Info" />
                                </div>
                                <div>
                                    <label>Description</label>
                                    <telerik:RadTextBox ID="tbDescript" CssClass="RadSizeMultiLine" runat="server" TextMode="MultiLine" />
                                </div>
                                <div>
                                    <label><b style="color: red">*</b> Active</label>
                                    <telerik:RadButton ID="RadButtonActive" runat="server" AutoPostBack="False" ToggleType="CheckBox" ButtonType="ToggleButton" />
                                </div>
                            </fieldset>
                        </div>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>
        </telerik:RadSplitter>

    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ToolbarClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() === "Update") {
                    if (!confirm('Do you want to update it?'))
                        args.set_cancel(true);
                } else if (button.get_text() === "Save") {
                    if (!confirm('Do you want to save it?'))
                        args.set_cancel(true);
                }
            }
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
