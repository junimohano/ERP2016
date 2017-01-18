<%@ Page Title="BusinessTrip" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="BusinessTripPop.aspx.cs" Inherits="School.OfficeAdmin.BusinessTripPop" %>

<%@ Import Namespace="Erp2016.Lib" %>

<%@ Register TagPrefix="UserControl" TagName="ApprovalLine" Src="~/App_Data/ApprovalLine.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridFlight">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridFlight" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridAccommodation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridAccommodation" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridCash">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridCash" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking="RadToolBar1_OnClientButtonClicking">
                    <Items>
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_save.png" Text="TempSave" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_mark.png" Text="Request" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_excute.png" Text="Approve" />
                        <telerik:RadToolBarButton runat="server" Text="Accept" />
                        <telerik:RadToolBarButton runat="server" Text="Complete" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_back.png" Text="Revise" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_clear.png" Text="Reject" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" />
                        <telerik:RadToolBarButton runat="server" Text="Print" />
                        <telerik:RadToolBarButton runat="server" Text="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="RadPanePrint" runat="server" Scrolling="Both">

                <div id="divPrint">

                    <telerik:RadGrid ID="RadGridInfo" runat="server" GroupPanelPosition="Top">
                    </telerik:RadGrid>

                    <UserControl:ApprovalLine ID="ApprovalLine1" runat="server" OnLoad="ApprovalLine1_OnLoad" />

                    <h4>1. Plan For Flight</h4>

                    <fieldset>
                        <legend>Info</legend>
                        <table>
                            <tr>
                                <td>
                                    <label><b style="color: red">*</b> Type of Flight</label>
                                    <telerik:RadButton ID="RadButtonLocal" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                        Text="Local" GroupName="StandardButton" AutoPostBack="false">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="RadButtonOverseas" runat="server" ToggleType="Radio" Checked="true"
                                        Text="Overseas" GroupName="StandardButton" ButtonType="ToggleButton" AutoPostBack="false">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </fieldset>

                    <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />

                    <telerik:RadGrid ID="RadGridFlight" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AllowPaging="False" AutoGenerateColumns="false" DataSourceID="LinqDataSourceFlight" OnBatchEditCommand="RadGridFlight_OnBatchEditCommand" OnLoad="RadGridFlight_Load" PageSize="20" ShowFooter="True">
                        <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add Invoice Item" CommandItemSettings-SaveChangesText="Save Changes Invoice Item" DataKeyNames="BusinessTripFlightId" DataSourceID="LinqDataSourceFlight" EditMode="Batch" HorizontalAlign="NotSet">
                            <BatchEditingSettings EditType="Row" />
                            <CommandItemSettings ShowSaveChangesButton="False" AddNewRecordText="Add Item" SaveChangesText="Save Changes Item" />
                            <Columns>
                                <telerik:GridTemplateColumn DataField="AirDate" HeaderText="Date &amp; Day" UniqueName="AirDate">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:MM-dd-yyyy}", DataBinder.Eval(Container.DataItem, "AirDate")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%--<span>--%>
                                        <telerik:RadDatePicker ID="RadDatePickerAirDate" runat="server" Width="100%" Culture="English (Canada)">
                                            <DateInput DateFormat="MM-dd-yyyy" />
                                        </telerik:RadDatePicker>
                                        <%--   <span style="color: Red">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="RadDatePickerAirDate" ErrorMessage="Date &amp; Day Required" runat="server" Display="Dynamic" ValidationGroup="Info" />
                                                </span>--%>
                                        <%--</span>--%>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="AirPort" HeaderText="Departure Airport (City)" UniqueName="AirPort">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "AirPort") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Destination" HeaderText="Destination" UniqueName="Destination">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "Destination") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Departure" HeaderText="Departure" UniqueName="Departure">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "Departure") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTimePicker runat="server" AutoPostBack="False" Width="100%">
                                            <TimeView Interval="00:15:00">
                                            </TimeView>
                                        </telerik:RadTimePicker>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="AirLine" HeaderText="Air Line" UniqueName="AirLine">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "AirLine") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        Sub Total
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn FooterText="SubTotal" DataField="AirRate" HeaderText="($)Air Rate" UniqueName="AirRate">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "AirRate")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxAirRate" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridFlight_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox ID="LabelFlightRate2" runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" FlightLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="AirNote" HeaderText="Note" UniqueName="AirNote">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "AirNote") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px" ConfirmDialogType="RadWindow" ConfirmText="Delete this Item?" ConfirmTitle="Delete" HeaderStyle-Width="30px" HeaderText="Del" Text="Delete" UniqueName="DeleteColumn">
                                    <HeaderStyle Width="30px" />
                                </telerik:GridButtonColumn>
                            </Columns>

                        </MasterTableView>

                        <ClientSettings EnableRowHoverStyle="True">
                            <ClientEvents OnGridDestroying="RadGridFlight_GridDestroying" OnCommand="RadGridFlight_Command" OnGridCreated="RadGridFlight_GridCreated" OnRowDeleted="RadGridFlight_RowDeleted" />
                            <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                            <%--AllowRowResize="True"--%>
                        </ClientSettings>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                    </telerik:RadGrid>

                    <asp:LinqDataSource ID="LinqDataSourceFlight" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="BusinessTripFlights"
                        Where="BusinessTripFlightId == @BusinessTripFlightId">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="0" Name="BusinessTripFlightId" Type="Int32" />
                        </WhereParameters>
                    </asp:LinqDataSource>

                    <h4>2. Business Trip Plan For Accommodation</h4>

                    <telerik:RadGrid ID="RadGridAccommodation" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AllowPaging="false" AutoGenerateColumns="false" DataSourceID="LinqDataSourceAccommodation" OnLoad="RadGridAccommodation_Load" OnBatchEditCommand="RadGridAccommodation_OnBatchEditCommand" PageSize="20" ShowFooter="True">
                        <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add Invoice Item" CommandItemSettings-SaveChangesText="Save Changes Invoice Item" DataKeyNames="BusinessTripAccomId" DataSourceID="LinqDataSourceAccommodation" EditMode="Batch" HorizontalAlign="NotSet">
                            <BatchEditingSettings EditType="Row" />
                            <CommandItemSettings ShowSaveChangesButton="False" AddNewRecordText="Add Item" SaveChangesText="Save Changes Item" />
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Check In" DataField="AccomIn" UniqueName="AccomIn">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:MM-dd-yyyy}", DataBinder.Eval(Container.DataItem, "AccomIn")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker runat="server" Width="100%" Culture="English (Canada)">
                                            <DateInput DateFormat="MM-dd-yyyy" />
                                        </telerik:RadDatePicker>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Check Out" DataField="AccomOut" UniqueName="AccomOut">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:MM-dd-yyyy}", DataBinder.Eval(Container.DataItem, "AccomOut")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker runat="server" Width="100%" Culture="English (Canada)">
                                            <DateInput DateFormat="MM-dd-yyyy" />
                                        </telerik:RadDatePicker>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="City Or Country" DataField="City" UniqueName="City">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "City") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Hotel Name" DataField="Hotel" UniqueName="Hotel">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "Hotel") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        Sub Total
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="($)Rate" DataField="AccomRate" UniqueName="AccomRate">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "AccomRate")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxAccomRate" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridAccommodation_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox ID="LabelAccomRate2" runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" AccomLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Note" DataField="AccomNote" UniqueName="AccomNote">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "AccomNote") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px" ConfirmDialogType="RadWindow" ConfirmText="Delete this Item?" ConfirmTitle="Delete" HeaderStyle-Width="30px" HeaderText="Del" Text="Delete" UniqueName="DeleteColumn">
                                    <HeaderStyle Width="30px" />
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="True">
                            <ClientEvents OnGridDestroying="RadGridAccommodation_GridDestroying" OnDataBound="RadGridAccommodation_Command" OnGridCreated="RadGridAccommodation_GridCreated" OnRowDeleted="RadGridAccommodation_RowDeleted" />
                            <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                            <%--AllowRowResize="True"--%>
                        </ClientSettings>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                    </telerik:RadGrid>
                    <asp:LinqDataSource ID="LinqDataSourceAccommodation" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="BusinessTripAccoms"
                        Where="BusinessTripAccomId == @BusinessTripAccomId">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="0" Name="BusinessTripAccomId" Type="Int32" />
                        </WhereParameters>
                    </asp:LinqDataSource>

                    <h4>3. Business Trip Plan For Cash Advance</h4>

                    <telerik:RadGrid ID="RadGridCash" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AllowPaging="false" AutoGenerateColumns="false" DataSourceID="LinqDataSourceCash" OnLoad="RadGridCash_Load" OnBatchEditCommand="RadGridCash_OnBatchEditCommand" PageSize="20" ShowFooter="True">
                        <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add Invoice Item" CommandItemSettings-SaveChangesText="Save Changes Invoice Item" DataKeyNames="BusinessTripCashId" DataSourceID="LinqDataSourceCash" EditMode="Batch" HorizontalAlign="NotSet">
                            <BatchEditingSettings EditType="Row" />
                            <CommandItemSettings ShowSaveChangesButton="False" AddNewRecordText="Add Item" SaveChangesText="Save Changes Item" />
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Date &amp; Day" UniqueName="CashDate">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:MM-dd-yyyy}", DataBinder.Eval(Container.DataItem, "CashDate")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker runat="server" Width="100%" Culture="English (Canada)">
                                            <DateInput DateFormat="MM-dd-yyyy" />
                                        </telerik:RadDatePicker>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="City" UniqueName="CashCity">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "CashCity") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Time" UniqueName="CashTime">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "CashTime") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTimePicker Width="100%" runat="server" AutoPostBack="False">
                                            <TimeView Interval="00:15:00" />
                                        </telerik:RadTimePicker>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Meeting Plan Name" UniqueName="AccomAgent">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "AccomAgent") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        Advance Total
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Meeting Plan Agency" UniqueName="AccomAgency">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "AccomAgency") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox ID="LabelCashAdvanceTotal" runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" CashAdvanceTotalLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="GroundTransportation Type" UniqueName="GroundType">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "GroundType") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        Sub Total
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="($)Ground Transportation Rate" UniqueName="GroundRate">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "GroundRate")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxGroundRate" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridCash_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox ID="LabelCashGroundRate2" runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" CashGroundRateLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Meals &amp; Others Type" UniqueName="MealsType">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "MealsType") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        Sub Total
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="($)Meals &amp; Others Rate" UniqueName="MealsRate">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "MealsRate")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxMealsRate" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridCash_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox ID="LabelCashMealsRate2" runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" CashMealsRateLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Note" UniqueName="CashNote">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "CashNote") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px" ConfirmDialogType="RadWindow" ConfirmText="Delete this Item?" ConfirmTitle="Delete" HeaderStyle-Width="30px" HeaderText="Del" Text="Delete" UniqueName="DeleteColumn">
                                    <HeaderStyle Width="50px" />
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="True">
                            <ClientEvents OnGridDestroying="RadGridCash_GridDestroying" OnDataBound="RadGridCash_Command" OnGridCreated="RadGridCash_GridCreated" OnRowDeleted="RadGridCash_RowDeleted" />
                            <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                            <%--AllowRowResize="True"--%>
                        </ClientSettings>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                    </telerik:RadGrid>
                    <asp:LinqDataSource ID="LinqDataSourceCash" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="BusinessTripCashes"
                        Where="BusinessTripCashId == @BusinessTripCashId">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="0" Name="BusinessTripCashId" Type="Int32" />
                        </WhereParameters>
                    </asp:LinqDataSource>

                    <fieldset>
                        <legend></legend>
                        <div style="float: right;">
                            <label>Grand Total</label>
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxGrandTotal" runat="server" HoveredStyle-HorizontalAlign="Right" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Right" Type="Currency" Width="100px">
                                <ClientEvents OnLoad=" GrandTotalLoad " />
                            </telerik:RadNumericTextBox>
                        </div>
                    </fieldset>

                </div>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>


    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            // jQuery
            $(window).bind("load", function () {
                SetFlightValue();
            });

            // =====================
            // total sum
            // =====================
            // flight
            var sumFlightInput = null;

            function FlightLoad(sender, args) {
                sumFlightInput = sender;
            }

            // accomodation
            var sumAccomInput = null;

            function AccomLoad(sender, args) {
                sumAccomInput = sender;
            }

            // Cash
            var sumCashMealsRateInput = null;

            function CashMealsRateLoad(sender, args) {
                sumCashMealsRateInput = sender;
            }

            var sumCashGroundRateInput = null;

            function CashGroundRateLoad(sender, args) {
                sumCashGroundRateInput = sender;
            }

            var sumCashAdvanceTotalInput = null;

            function CashAdvanceTotalLoad(sender, args) {
                sumCashAdvanceTotalInput = sender;
            }

            // total
            var sumGrandTotalInput = null;

            function GrandTotalLoad(sender, args) {
                sumGrandTotalInput = sender;
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
            function SetFlightValue() {
                var grid = $find("<%= RadGridFlight.ClientID %>"); //grid id

                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var rows = masterTable.get_dataItems();

                    var totalMount = 0.0;
                    for (var i = 0; i < rows.length; i++) {
                        totalMount = totalMount + GetCellValue(rows[i], "AirRate", "radNumTextBoxAirRate");
                    }
                    sumFlightInput.set_value(totalMount);
                    SetTotalValue();
                }
            }

            function SetAccommodationValue() {
                var grid = $find("<%= RadGridAccommodation.ClientID %>");

                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var rows = masterTable.get_dataItems();

                    var totalMount = 0.0;
                    for (var i = 0; i < rows.length; i++) {
                        totalMount = totalMount + GetCellValue(rows[i], "AccomRate", "radNumTextBoxAccomRate");
                    }
                    sumAccomInput.set_value(totalMount);
                    SetTotalValue();
                }
            }

            function SetCashValue() {
                var grid = $find("<%= RadGridCash.ClientID %>");

                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var rows = masterTable.get_dataItems();

                    var totalMount1 = 0.0;
                    var totalMount2 = 0.0;
                    for (var i = 0; i < rows.length; i++) {
                        totalMount1 = totalMount1 + GetCellValue(rows[i], "MealsRate", "radNumTextBoxMealsRate");
                        totalMount2 = totalMount2 + GetCellValue(rows[i], "GroundRate", "radNumTextBoxGroundRate");
                    }
                    sumCashMealsRateInput.set_value(totalMount1);
                    sumCashGroundRateInput.set_value(totalMount2);
                    sumCashAdvanceTotalInput.set_value(sumCashGroundRateInput.get_value() + sumCashMealsRateInput.get_value());
                    SetTotalValue();
                }
            }

            function SetTotalValue() {
                if (sumGrandTotalInput)
                    sumGrandTotalInput.set_value(sumFlightInput.get_value() + sumAccomInput.get_value() + sumCashAdvanceTotalInput.get_value());
            }

            // when grid created
            function RadGridFlight_GridCreated(sender, eventArgs) {
                SetFlightValue();
            }

            function RadGridAccommodation_GridCreated(sender, eventArgs) {
                SetAccommodationValue();
            }

            function RadGridCash_GridCreated(sender, eventArgs) {
                SetCashValue();
            }

            // action
            function RadGridFlight_Command(sender, eventArgs) {
                if (eventArgs.get_commandName() == "Rebind")
                    SetFlightValue();
            }

            function RadGridAccommodation_Command(sender, eventArgs) {
                if (eventArgs.get_commandName() == "Rebind")
                    SetAccommodationValue();
            }

            function RadGridCash_Command(sender, eventArgs) {
                if (eventArgs.get_commandName() == "Rebind")
                    SetCashValue();
            }

            // delete
            function RadGridFlight_RowDeleted(sender, eventArgs) {
                SetFlightValue();
            }

            function RadGridAccommodation_RowDeleted(sender, eventArgs) {
                SetAccommodationValue();
            }

            function RadGridCash_RowDeleted(sender, eventArgs) {
                SetCashValue();
            }

            // change value
            function RadGridFlight_ValueChanged(sender, eventArgs) {
                SetFlightValue();
            }

            function RadGridAccommodation_ValueChanged(sender, eventArgs) {
                SetAccommodationValue();
            }

            function RadGridCash_ValueChanged(sender, eventArgs) {
                SetCashValue();
            }

            // == end total sum ==

            function RadToolBar1_OnClientButtonClicking(sender, args) {
                var button = args.get_item();
                if (button.get_text() == "Request") {
                    if (!confirm('Do you want to Request?'))
                        args.set_cancel(true);
                }
            }

            var isSaving = false;

            function SaveChanges() {
                isSaving = true;
                var gridFlight = $find('<%= RadGridFlight.ClientID %>');
                gridFlight.get_batchEditingManager().saveChanges(gridFlight.get_masterTableView());
                // call flight_gridDestroying when grid Updated
            }

            function RadGridFlight_GridDestroying(sender, eventArgs) {
                if (isSaving) {
                    var gridAccom = $find('<%= RadGridAccommodation.ClientID %>');
                    gridAccom.get_batchEditingManager().saveChanges(gridAccom.get_masterTableView());
                }
            }

            function RadGridAccommodation_GridDestroying(sender, eventArgs) {
                if (isSaving) {
                    var gridCash = $find('<%= RadGridCash.ClientID %>');
                    gridCash.get_batchEditingManager().saveChanges(gridCash.get_masterTableView());
                }
            }

            function RadGridCash_GridDestroying(sender, eventArgs) {
                if (isSaving) {
                    isSaving = false;
                    Close();
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

            function ShowApprovalWindow(id) {
                var oWnd = window.radopen('ApprovalApprovePop?type=' + <%= (int)CConstValue.Approval.BusinessTrip %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalRejectWindow(id) {

                var oWnd = window.radopen('ApprovalRejectPop?type=' + <%= (int)CConstValue.Approval.BusinessTrip %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalReviseWindow(id) {
                var oWnd = window.radopen('ApprovalRevisePop?type=' + <%= (int)CConstValue.Approval.BusinessTrip %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalAcceptWindow(id) {
                var oWnd = window.radopen('ApprovalAcceptPop?type=' + <%= (int)CConstValue.Approval.BusinessTrip %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCompleteWindow(id) {
                var oWnd = window.radopen('ApprovalCompletePop?type=' + <%= (int)CConstValue.Approval.BusinessTrip %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCancelWindow(id) {
                var oWnd = window.radopen('ApprovalCancelPop?type=' + <%= (int)CConstValue.Approval.BusinessTrip %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function OnClientClose(oWnd, args) {
                Close();
                //var arg = args.get_argument();
        <%--<%=Page.GetPostBackEventReference(btnRefresh)%>--%>
            }

            function ShowPrint() {
                $("#divPrint").printArea();
            }

        </script>

    </telerik:RadCodeBlock>

</asp:Content>
