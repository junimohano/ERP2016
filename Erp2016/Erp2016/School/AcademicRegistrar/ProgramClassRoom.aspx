<%@ Page Title="Class Rooms" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="ProgramClassRoom.aspx.cs" Inherits="School.AcademicRegistrar.ProgramClassRoom" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadPane1" runat="server" Height="27px" Scrolling="None">
                <h4>Class Room List</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="GridPane" runat="server" Scrolling="None" Height="40%">
                <telerik:RadGrid ID="Grid" DataSourceID="LinqDataSource1" runat="server" PageSize="20" AutoGenerateColumns="false" Height="100%" AllowPaging="true"
                    AllowCustomPaging="false" AllowSorting="true" AllowFilteringByColumn="True" OnFilterCheckListItemsRequested="Grid_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true"
                    OnSelectedIndexChanged="Grid_OnSelectedIndexChanged">
                    <MasterTableView DataSourceID="LinqDataSource1" TableLayout="Fixed" DataKeyNames="ProgramClassRoomId">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="No" DataField="ProgramClassRoomId" SortExpression="ProgramClassRoomId" UniqueName="ProgramClassRoomId"
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
                                HeaderText="Room Name" DataField="Name" SortExpression="Name" UniqueName="Name"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Room Number" DataField="Number" SortExpression="Number" UniqueName="Number"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Max Capacity" DataField="Capacity" SortExpression="Capacity" UniqueName="Capacity"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Created User" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
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

                    <ClientSettings EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="True"></Scrolling>
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" />
                </telerik:RadGrid>

                <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" TableName="vwProgramClassRooms" Where="SiteId == @SiteId" OrderBy="CreatedDate DESC, Name">
                    <WhereParameters>
                        <asp:SessionParameter Name="SiteId" SessionField="SiteId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>

            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radslitbar1" runat="server" CollapseMode="Both" EnableResize="true"></telerik:RadSplitBar>

            <telerik:RadPane ID="MainPane" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="RadSplitter2" runat="server" Orientation="Horizontal">
                    <telerik:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
                        <telerik:RadToolBar ID="RadToolBar2" runat="server" OnButtonClick="ToolbarClick" OnClientButtonClicking="ToolbarClick">
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
                                    <label><b style="color: red">*</b> Room Name</label>
                                    <telerik:RadTextBox ID="tbName" runat="server" CssClass="RadSizeLarge" />
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbName" Display="Dynamic" ErrorMessage="Room Name Required" ForeColor="Red" ValidationGroup="Info" />
                                </div>
                                <div>
                                    <label><b style="color: red">*</b> Room Number</label>
                                    <telerik:RadTextBox ID="tbNumber" runat="server" CssClass="RadSizeMiddle" />
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbNumber" Display="Dynamic" ErrorMessage="Room Number Required" ForeColor="Red" ValidationGroup="Info" />
                                </div>
                                <div>
                                    <label><b style="color: red">*</b> Room Floor</label>
                                    <telerik:RadTextBox ID="tbFloor" runat="server" CssClass="RadSizeMiddle" />
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbFloor" Display="Dynamic" ErrorMessage="Room Floor Required" ForeColor="Red" ValidationGroup="Info" />
                                </div>
                                <div>
                                    <label><b style="color: red">*</b> Max Capacity</label>
                                    <telerik:RadNumericTextBox ID="tbCapacity" runat="server" Type="Number" NumberFormat-DecimalDigits="0" CssClass="RadSizeMiddle" />
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbCapacity" Display="Dynamic" ErrorMessage="Max Capacity Required" ForeColor="Red" ValidationGroup="Info" />
                                </div>
                                <div>
                                    <label>Components</label>

                                    <telerik:RadListBox runat="server" ID="RadListBoxComponents" Height="250px" CssClass="RadSizeLarge">
                                        <%--<HeaderTemplate>
                                            <h4>Component List</h4>
                                        </HeaderTemplate>--%>

                                        <ItemTemplate>
                                            <telerik:RadButton ID="RadButtonSelect" runat="server" AutoPostBack="False" ToggleType="CheckBox" ButtonType="LinkButton" Checked='<%# Eval("Selected") %>' />
                                            <telerik:RadTextBox ID="RadTextBoxName" Text='<%# Eval("Name") %>' runat="server" Width="150" ReadOnly="True" HoveredStyle-HorizontalAlign="Center" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Center" />
                                            <telerik:RadTextBox ID="RadTextBoxComment" Text='<%# Eval("Comment") %>' runat="server" Width="180" />
                                            <telerik:RadTextBox ID="RadTextBoxValue" Text='<%# Eval("Value") %>' runat="server" Visible="False" />
                                        </ItemTemplate>
                                    </telerik:RadListBox>

                                </div>
                                <div>
                                    <label>Description</label>
                                    <telerik:RadTextBox ID="RadTextBoxDescription" runat="server" TextMode="MultiLine" CssClass="RadSizeMultiLine" />
                                    <br style="clear: both;" />
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

        <asp:ObjectDataSource ID="odsCampus" runat="server"
            SelectMethod="GetSiteLocationBySiteId" TypeName="Erp2016.Lib.CSiteLocation">
            <SelectParameters>
                <asp:SessionParameter Name="siteid" SessionField="SiteId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ToolbarClick(sender, args) {
                var button = args.get_item();

                if (button.get_text() === "Save") {
                    if (!confirm('Do you want to save?'))
                        args.set_cancel(true);
                } else if (button.get_text() === "Update") {
                    if (!confirm('Do you want to update?'))
                        args.set_cancel(true);
                }
            }
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
