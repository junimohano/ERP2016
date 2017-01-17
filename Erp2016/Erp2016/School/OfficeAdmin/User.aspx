<%@ Page Title="User" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="School.OfficeAdmin.User" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane4" runat="server" Height="27px" Scrolling="None">
                <h4>User Information</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Height="35%" Scrolling="None">
                <telerik:RadGrid ID="RadGridUser" runat="server" AllowFilteringByColumn="True" AutoGenerateColumns="False" PageSize="20" Height="100%"
                    AllowPaging="True" AllowSorting="True" DataSourceID="LinqDataSourceUser" ShowFooter="False" OnSelectedIndexChanged="RadGridUser_OnSelectedIndexChanged" OnPreRender="RadGridUser_PreRender" OnFilterCheckListItemsRequested="RadGridUser_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="UserId" TableLayout="Fixed" DataSourceID="LinqDataSourceUser">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="No" DataField="UserId" SortExpression="UserId" UniqueName="UserId"
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
                                HeaderText="Login Id" DataField="LoginId" SortExpression="LoginId" UniqueName="LoginId"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Employee Number" DataField="EmployeeNumber" SortExpression="EmployeeNumber" UniqueName="EmployeeNumber"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="User Name" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Position" DataField="PositionName" SortExpression="PositionName" UniqueName="PositionName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Phone" DataField="Phone" SortExpression="Phone" UniqueName="Phone"
                                FilterCheckListEnableLoadOnDemand="false" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Email" DataField="Email" SortExpression="Email" UniqueName="Email"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Created User" DataField="CreatedUserName" SortExpression="CreatedUserName" UniqueName="CreatedUserName"
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
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                </telerik:RadGrid>
                <asp:LinqDataSource ID="LinqDataSourceUser" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" OrderBy="CreatedDate DESC"
                    TableName="vwUsers"
                    Where="UserId == @UserId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="UserId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None">

                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Horizontal">

                    <telerik:RadPane ID="Radpane3" runat="server" Scrolling="None" Height="40px">
                        <telerik:RadToolBar ID="RadToolBarUser" runat="server" OnClientButtonClicking=" ToolbarButtonClick " OnButtonClick="StaffButtonClicked">
                            <Items>
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Save" ValidationGroup="Info" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_new.png" Enabled="false" Text="New" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/icon_s_edit.png" Enabled="false" Text="Permission" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/icon_s_edit.png" Enabled="false" Text="User Information" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>

                    <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">

                        <telerik:RadSplitter ID="RadSplitter4" runat="server" Orientation="Horizontal">
                            <telerik:RadPane ID="Radpane6" runat="server">

                                <div class="formStyle3">

                                    <div style="float: left; width: 450px;">
                                        <fieldset>
                                            <legend>Basic Information 1</legend>
                                            <div>
                                                <label><b style="color: red">*</b> Site</label>
                                                <telerik:RadComboBox ID="RadComboBoxSite" EmptyMessage="Choose a Site" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" OnSelectedIndexChanged="RadComboBoxSite_OnSelectedIndexChanged" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator20" ControlToValidate="RadComboBoxSite" Display="Dynamic" ErrorMessage="Site Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Site Location</label>
                                                <telerik:RadComboBox ID="RadComboBoxSiteLocation" EmptyMessage="Choose a Site Location" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="RadComboBoxSiteLocation" Display="Dynamic" ErrorMessage="Site Location Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Login ID</label>
                                                <telerik:RadTextBox ID="tbUserID" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" OnTextChanged="tbUserID_TextChanged" AutoCompleteType="None" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="tbUserID" Display="Dynamic" ErrorMessage="Login ID Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Password</label>
                                                <telerik:RadTextBox ID="tbPassWord" CssClass="RadSizeMiddle" runat="server" TextMode="Password" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> First Name</label>
                                                <telerik:RadTextBox ID="tbFName" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="tbFName" Display="Dynamic" ErrorMessage="First Name Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label>Middle Name</label>
                                                <telerik:RadTextBox ID="tbMName" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Last Name</label>
                                                <telerik:RadTextBox ID="tbLName" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="tbLName" Display="Dynamic" ErrorMessage="Last Name Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> S.I.N Number</label>
                                                <telerik:RadMaskedTextBox ID="tbSIN" CssClass="RadSizeMiddle" runat="server" Mask="###-###-###" AutoPostBack="true" OnTextChanged="tbSIN_TextChanged" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="tbSIN" Display="Dynamic" ErrorMessage="S.I.N Number Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> D.O.B</label>
                                                <telerik:RadDatePicker DateInput-DisplayDateFormat="MMMM-dd-yyyy" ID="tbDOB" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Date of Birthday" MinDate="01/01/1900" MaxDate="01/01/3000" OnSelectedDateChanged="Age" AutoPostBack="true"></telerik:RadDatePicker>
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="tbDOB" Display="Dynamic" ErrorMessage="D.O.B Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Age</label>
                                                <telerik:RadTextBox ID="tbAge" CssClass="RadSizeMiddle" runat="server" ReadOnly="true" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="tbAge" Display="Dynamic" ErrorMessage="Age Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Marital Status</label>
                                                <telerik:RadComboBox ID="ddlMarital" runat="server" CssClass="RadSizeMiddle" AppendDataBoundItems="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Married" Value="False" />
                                                        <telerik:RadComboBoxItem Text="Single" Value="True" Checked="True" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="ddlMarital" Display="Dynamic" ErrorMessage="Marital Status Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Gender</label>
                                                <telerik:RadComboBox ID="ddlGender" runat="server" CssClass="RadSizeMiddle" AppendDataBoundItems="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Male" Value="0" Checked="True" />
                                                        <telerik:RadComboBoxItem Text="Female" Value="1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="ddlGender" Display="Dynamic" ErrorMessage="Gender Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label>Work Email</label>
                                                <telerik:RadTextBox ID="tbWEmail" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" /><br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Work Phone</label>
                                                <telerik:RadMaskedTextBox ID="tbWPhone" CssClass="RadSizeMiddle" runat="server" Mask="###-###-#### Ext (###)" EmptyMessage="(Optional)" /><br style="clear: both;" />
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div style="float: left; width: 450px;">
                                        <fieldset>
                                            <legend>Basic Information 2</legend>
                                            <div>
                                                <label><b style="color: red">*</b> User Group</label>
                                                <telerik:RadComboBox ID="RadComboBoxUserGroup" EmptyMessage="Choose a User Group" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" OnSelectedIndexChanged="RadComboBoxUserGroup_OnSelectedIndexChanged" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="RadComboBoxUserGroup" Display="Dynamic" ErrorMessage="User Group Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> User Position</label>
                                                <telerik:RadComboBox ID="RadComboBoxUserPosition" EmptyMessage="Choose a Position" CssClass="RadSizeMiddle" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="RadComboBoxUserPosition" Display="Dynamic" ErrorMessage="User Position Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label>Approval Manager</label>
                                                <telerik:RadComboBox ID="RadComboBoxSuper" EmptyMessage="Choose a Supervisor" CssClass="RadSizeMiddle" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Employee No</label>
                                                <telerik:RadTextBox ID="tbEmpNo" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="tbEmpNo" Display="Dynamic" ErrorMessage="Employee No Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Active</label>
                                                <telerik:RadButton ID="cbActive" runat="server" AutoPostBack="False" ToggleType="CheckBox" ButtonType="ToggleButton" />
                                                <br style="clear: both;" />
                                            </div>
                                        </fieldset>

                                        <fieldset>
                                            <legend>Contact Information</legend>

                                            <div>
                                                <label><b style="color: red">*</b> Address 1</label>
                                                <telerik:RadTextBox ID="tbPAddess1" Height="100" runat="server" TextMode="MultiLine" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="tbPAddess1" Display="Dynamic" ErrorMessage="Address 1 Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label>Address 2</label>
                                                <telerik:RadTextBox ID="tbPAddess2" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> City</label>
                                                <telerik:RadTextBox ID="tbPCity" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="tbPCity" Display="Dynamic" ErrorMessage="City Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Province</label>
                                                <telerik:RadTextBox ID="tbPProvince" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="tbPProvince" Display="Dynamic" ErrorMessage="Province Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Postal Code</label>
                                                <telerik:RadTextBox ID="tbPPostal" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator17" ControlToValidate="tbPPostal" Display="Dynamic" ErrorMessage="Postal Code Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label>Home Phone</label>
                                                <telerik:RadMaskedTextBox ID="tbPHomePhone" CssClass="RadSizeMiddle" runat="server" Mask="###-###-####" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Cell Phone</label>
                                                <telerik:RadMaskedTextBox ID="tbPCell" CssClass="RadSizeMiddle" runat="server" Mask="###-###-####" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator18" ControlToValidate="tbPCell" Display="Dynamic" ErrorMessage="Cell Phone Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label>Personal Email</label>
                                                <telerik:RadTextBox ID="tbPEmail" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                        </fieldset>
                                        <br />
                                    </div>

                                    <div style="float: left; width: 450px;">
                                        <fieldset>
                                            <legend>Picture</legend>
                                            <telerik:RadBinaryImage ID="RadBinaryImagePicture" runat="server" Width="256" Height="256" ResizeMode="Fit" AutoAdjustImageControlSize="False" />
                                            <br />
                                            <telerik:RadAsyncUpload runat="server" ID="AsyncUploadPicture" Width="100%"
                                                HideFileInput="False" MultipleFileSelection="Disabled" AllowedFileExtensions=".jpeg,.jpg,.png" />
                                        </fieldset>

                                        <fieldset>
                                            <legend>Emergency Contact (Optional)</legend>
                                            <div>
                                                <label>Contact Name</label>
                                                <telerik:RadTextBox ID="tbEName" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Relationship</label>
                                                <telerik:RadTextBox ID="tbERelation" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Phone</label>
                                                <telerik:RadMaskedTextBox ID="tbEPhone" CssClass="RadSizeMiddle" runat="server" Mask="###-###-####" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Address</label>
                                                <telerik:RadTextBox ID="tbEAddress" Height="100" runat="server" TextMode="MultiLine" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>

                            </telerik:RadPane>

                        </telerik:RadSplitter>

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
                    if (!confirm('If you change supervisor, you will be changed on all list of approval line. Do you still want to update it?'))
                        args.set_cancel(true);
                }
            }

            function ShowPermission(id) {
                var oWnd = window.radopen('UserPermissionPop?id=' + id, 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;

                if (displayWidth > 1500)
                    displayWidth = 1500;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Minimize + Telerik.Web.UI.WindowBehaviors.Maximize);
                return false;
            }

            function ShowUserInformation(id) {
                var oWnd = window.radopen('UserInformationPop?id=' + id, 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;

                if (displayWidth > 1500)
                    displayWidth = 1500;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Minimize + Telerik.Web.UI.WindowBehaviors.Maximize);
                return false;
            }
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
