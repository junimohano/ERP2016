<%@ Page Title="New Program" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="ProgramNewPop.aspx.cs" Inherits="School.Registrar.Student.ProgramNewPop" %>

<%@ Register TagPrefix="UserControl" TagName="InvoiceItemGrid" Src="~/App_Data/InvoiceItemGrid.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadpaneBasic" runat="server" Height="40px" Scrolling="None">

                <telerik:RadToolBar ID="mainToolBar" runat="server" OnButtonClick="ToolbarButtonClick" OnClientButtonClicking=" ToolbarButtonClick ">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_save.png" Text="Save" ToolTip="Save" ValidationGroup="Info" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">

                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Vertical">

                    <telerik:RadPane ID="Radpane1" runat="server" Width="50%">

                        <div class="formStyle3">

                            <div style="float: left; width: 100%;">
                                <telerik:RadTextTile ID="ttName1" runat="server" Height="40px" Width="100%" Font-Size="Large"></telerik:RadTextTile>
                                <telerik:RadTextTile ID="ttName2" runat="server" Height="40px" Width="100%" Font-Size="Large"></telerik:RadTextTile>

                                <fieldset>
                                    <legend>Agency Info</legend>
                                    <div>
                                        <label>Agency</label>
                                        <telerik:RadComboBox ID="ddlAgency" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAgency_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label>Commission Type</label>
                                        <telerik:RadButton GroupName="AgencyRate" ID="RadButtonAgencyRateBasic" runat="server" Checked="True" Text="Basic" ToggleType="Radio" ButtonType="ToggleButton" OnCheckedChanged="RadButtonAgencyRateBasic_OnCheckedChanged"></telerik:RadButton>
                                        <telerik:RadButton GroupName="AgencyRate" ID="RadButtonAgencyRateSeasonal" runat="server" Text="Seasonal" ToggleType="Radio" ButtonType="ToggleButton" OnCheckedChanged="RadButtonAgencyRateSeasonal_OnCheckedChanged"></telerik:RadButton>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label>Commission Rate</label>
                                        <telerik:RadNumericTextBox ID="tbCommissionRate" Value="0" CssClass="RadSizeMiddle" runat="server" EmptyMessage="" Type="Percent" Enabled="true" AutoPostBack="True"></telerik:RadNumericTextBox>
                                        <br style="clear: both;" />
                                    </div>
                                </fieldset>

                                <fieldset>
                                    <legend>Program Info</legend>
                                    <div>
                                        <label>Faculty</label><telerik:RadComboBox ID="ddlFaculty" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                        </telerik:RadComboBox>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label>Program Group</label><telerik:RadComboBox ID="ddlProgramGrp" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramGrp_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                        </telerik:RadComboBox>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label><b style="color: red">*</b> Program Name</label><telerik:RadComboBox ID="ddlProgramName" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramName_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlProgramName" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label><b style="color: red">*</b> Start Date</label><telerik:RadDatePicker ID="tbPrgStartDate" AutoPostBack="true" OnSelectedDateChanged="tbPrgStartDate_OnSelectedDateChanged" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPrgStartDate" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label><b style="color: red">*</b> Weeks / Semester</label><telerik:RadComboBox ID="ddlProgramWeeks" Enabled="false" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramWeeks_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlProgramWeeks" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label><b style="color: red">*</b> End Date</label><telerik:RadDatePicker ID="tbPrgEndDate" AutoPostBack="True" Enabled="false" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPrgEndDate" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label><b style="color: red">*</b> Hours</label><telerik:RadComboBox ID="ddlPrgHours" Enabled="false" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPrgHours_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPrgHours" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label>Standard Tuition</label>
                                        <telerik:RadNumericTextBox ID="tbPrgStandardTuition" Value="0" CssClass="RadSizeMiddle" runat="server" EmptyMessage="" Type="Currency" Enabled="false"></telerik:RadNumericTextBox>
                                        <br style="clear: both;" />
                                        <label>Country Tuition</label>
                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxCountryMarketTuition" Value="0" CssClass="RadSizeMiddle" runat="server" EmptyMessage="" Type="Currency" Enabled="false"></telerik:RadNumericTextBox>
                                        <br style="clear: both;" />
                                        <label><b style="color: red">*</b> Tuition</label>
                                        <telerik:RadNumericTextBox ID="tbPrgTuition" Value="0" CssClass="RadSizeMiddle" runat="server" EmptyMessage="" AutoPostBack="true" Type="Currency"></telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPrgTuition" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                        <br style="clear: both;" />
                                    </div>
                                </fieldset>

                                <fieldset>
                                    <legend>Promotion Info</legend>
                                    <label>Promotion</label>
                                    <asp:Image ID="ImagePromotionSuccess" runat="server" ImageUrl="~/assets/img/Ok-20.png" Visible="False" />
                                    <asp:Image ID="ImagePromotionFail" runat="server" ImageUrl="~/assets/img/Cancel-20.png" Visible="False" />
                                    <telerik:RadTextBox ID="RadTextBoxPromotion" CssClass="RadSizeSmall" runat="server" EmptyMessage="put Promotion Number" Type="Number" AutoPostBack="True" />
                                </fieldset>

                                <fieldset>
                                    <legend>Scholarship Info</legend>
                                    <label>Scholarship</label>
                                    <asp:Image ID="ImageScholarshipSuccess" runat="server" ImageUrl="~/assets/img/Ok-20.png" Visible="False" />
                                    <asp:Image ID="ImageScholarshipFail" runat="server" ImageUrl="~/assets/img/Cancel-20.png" Visible="False" />
                                    <telerik:RadTextBox ID="RadTextBoxScholarship" CssClass="RadSizeSmall" runat="server" EmptyMessage="put Scholarship Number" AutoPostBack="True" />
                                    
                                    <br style="clear: both;" />
                                    <br style="clear: both;" />

                                    <label>Available Amount</label>
                                    <telerik:RadButton GroupName="radio" ID="RadButtonAvailableScholarshipAmount" runat="server" Text="" ToggleType="Radio" ButtonType="ToggleButton" Checked="True" Enabled="False" />
                                    <telerik:RadNumericTextBox runat="server" CssClass="RadSizeSmall" Type="Currency" ID="RadNumericTextBoxAvailableScholarshipAmount" Enabled="False" />

                                    <br style="clear: both;" />
                                    <label>Amount</label>
                                    <telerik:RadNumericTextBox runat="server" CssClass="RadSizeMiddle" Type="Currency" ID="RadNumericTextBoxScholarshipAmount" AutoPostBack="True" />

                                    <br style="clear: both;" />
                                    <br style="clear: both;" />

                                    <label>Available Weeks</label>
                                    <telerik:RadButton GroupName="radio" ID="RadButtonAvailableScholarshipWeeks" runat="server" Text="" ToggleType="Radio" ButtonType="ToggleButton" Enabled="False" />
                                    <telerik:RadNumericTextBox ID="RadNumericTextBoxAvailableScholarshipWeeks" CssClass="RadSizeSmall" runat="server" Enabled="False" />

                                    <br style="clear: both;" />
                                    <label>Weeks</label>
                                    <telerik:RadNumericTextBox runat="server" CssClass="RadSizeMiddle" Type="Number" ID="RadNumericTextBoxScholarshipWeeks" AutoPostBack="True" />
                                </fieldset>
                            </div>

                        </div>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

                    <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None">

                        <telerik:RadSplitter runat="server" ID="RadSplitter3" Orientation="Horizontal">
                            <telerik:RadPane ID="Radpane3" runat="server" Height="27px" Scrolling="None">
                                <h4>Invoice</h4>
                            </telerik:RadPane>

                            <telerik:RadPane ID="Radpane4" runat="server" Scrolling="None">
                                <UserControl:InvoiceItemGrid ID="InvoiceItemGrid1" runat="server" />
                            </telerik:RadPane>
                        </telerik:RadSplitter>

                    </telerik:RadPane>
                </telerik:RadSplitter>
                
            </telerik:RadPane>
        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function Close() {
                var oWnd = GetRadWindow();
                oWnd.close();
            }

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() == "Save") {
                    if (!confirm('Do you want to save it?'))
                        args.set_cancel(true);
                }
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
