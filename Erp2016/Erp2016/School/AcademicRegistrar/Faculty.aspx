<%@ Page Title="Faculties" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Faculty.aspx.cs" Inherits="School.AcademicRegistrar.Faculty" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">
            <telerik:RadPane ID="Radpane4" runat="server" Height="27px" Scrolling="None">
                <h4>Faculty List</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Height="60%" Scrolling="None">
                <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" OnSelectedIndexChanged="RadGrid1_OnSelectedIndexChanged"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Height="100%" PageSize="30"
                    DataSourceID="LinqDataSourceFaculty" ShowFooter="False" OnFilterCheckListItemsRequested="RadGrid1_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="FacultyId" TableLayout="Fixed" DataSourceID="LinqDataSourceFaculty">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="No" DataField="FacultyId" SortExpression="FacultyId" UniqueName="FacultyId"
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
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="True"></Scrolling>
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" />
                </telerik:RadGrid>
                <asp:LinqDataSource ID="LinqDataSourceFaculty" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" OrderBy="CreatedDate Descending" TableName="vwFaculties"
                    Where="FacultyId == @FacultyId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="FacultyId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="Radsplitter4" runat="server" Orientation="Horizontal">
                    <telerik:RadPane ID="Radpane3" runat="server" Height="40px" Scrolling="None">
                        <telerik:RadToolBar ID="RadToolBarFaculty" runat="server" OnClientButtonClicking=" ToolbarButtonClick " OnButtonClick="FacultyButtonClicked">
                            <Items>
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Save" ValidationGroup="Info" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_new.png" Enabled="false" Text="New" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>

                    <telerik:RadPane ID="Radpane5" runat="server">
                        <div class="formStyle3">
                            <fieldset>
                                <legend>Information</legend>
                                <div>
                                    <label><b style="color: red">*</b> Faculty</label>
                                    <telerik:RadTextBox ID="tbFaculty" CssClass="RadSizeLarge" runat="server" />
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbFaculty" Display="Dynamic" ErrorMessage="Faculty Required" ForeColor="Red" ValidationGroup="Info" />
                                </div>
                                <div>
                                    <label>Description</label>
                                    <telerik:RadTextBox ID="tbDescription" CssClass="RadSizeMultiLine" runat="server" TextMode="MultiLine" />
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
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
