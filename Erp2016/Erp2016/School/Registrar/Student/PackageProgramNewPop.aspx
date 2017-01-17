<%@ Page Title="New Package Program" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="PackageProgramNewPop.aspx.cs" Inherits="School.Registrar.Student.PackageProgramNewPop" %>


<%@ Register TagPrefix="UserControl" TagName="InvoiceItemGrid" Src="~/App_Data/InvoiceItemGrid.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane1" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="mainToolBar" runat="server"
                    OnButtonClick="ToolbarButtonClick"
                    OnClientButtonClicking=" ToolbarButtonClick ">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_save.png" Text="Save" ToolTip="Save" ValidationGroup="Info" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="RadpaneBasic" runat="server" Scrolling="None">

                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Vertical">

                    <telerik:RadPane ID="Radpane2" runat="server" Width="50%">
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
                                        <telerik:RadButton GroupName="AgencyRate" ID="RadButtonAgencyRateBasic" runat="server" Checked="True" Text="Basic" ToggleType="Radio" ButtonType="ToggleButton" OnCheckedChanged="RadButtonAgencyRateBasic_OnCheckedChanged" />
                                        <telerik:RadButton GroupName="AgencyRate" ID="RadButtonAgencyRateSeasonal" runat="server" Text="Seasonal" ToggleType="Radio" ButtonType="ToggleButton" OnCheckedChanged="RadButtonAgencyRateSeasonal_OnCheckedChanged" />
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label>Commission Rate</label>
                                        <telerik:RadNumericTextBox ID="tbCommissionRate" Value="0" CssClass="RadSizeMiddle" runat="server" EmptyMessage="" Type="Percent" AutoPostBack="True" />
                                        <br style="clear: both;" />
                                    </div>

                                </fieldset>

                                <fieldset>
                                    <legend>Program Info</legend>
                                    <div>
                                        <label><b style="color: red">*</b> Package Program</label>
                                        <telerik:RadComboBox ID="ddlPackageProgram" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPackageProgram_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPackageProgram" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label>Faculty</label>
                                        <telerik:RadTextBox ID="radTextBoxFaculty" CssClass="RadSizeLarge" Text="" runat="server" ReadOnly="True"></telerik:RadTextBox>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label>Program Group</label>
                                        <telerik:RadTextBox ID="radTextBoxProgramGroup" CssClass="RadSizeLarge" Text="" runat="server" ReadOnly="True"></telerik:RadTextBox>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label>Program Name</label>
                                        <telerik:RadTextBox ID="radTextBoxProgramName" CssClass="RadSizeLarge" Text="" runat="server" ReadOnly="True"></telerik:RadTextBox>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label><b style="color: red">*</b> Start Date</label>
                                        <telerik:RadDatePicker ID="tbPrgStartDate" AutoPostBack="False" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000" Enabled="False">
                                            <DateInput DateFormat="MM-dd-yyyy" EmptyMessage="Start Date" />
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPrgStartDate" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label><b style="color: red">*</b> End Date</label>
                                        <telerik:RadDatePicker ID="tbPrgEndDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000" Enabled="False">
                                            <DateInput DateFormat="MM-dd-yyyy" EmptyMessage="End Date" />
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPrgEndDate" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                        <br style="clear: both;" />
                                    </div>
                                    <div>
                                        <label>Standard Tuition</label>
                                        <telerik:RadNumericTextBox ID="tbPrgStandardTuition" Value="0" CssClass="RadSizeMiddle" runat="server" EmptyMessage="" Type="Currency" Enabled="false"></telerik:RadNumericTextBox>
                                        <br style="clear: both;" />
                                        <label><b style="color: red">*</b> Tuition</label>
                                        <telerik:RadNumericTextBox ID="tbPrgTuition" CssClass="RadSizeMiddle" runat="server" EmptyMessage="" Type="Currency" AutoPostBack="True"></telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="tbPrgTuitionVali" ControlToValidate="tbPrgTuition" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                        <br style="clear: both;" />
                                    </div>
                                </fieldset>
                            </div>

                        </div>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

                    <telerik:RadPane ID="Radpane3" runat="server" Scrolling="None">
                        <telerik:RadSplitter runat="server" ID="RadSplitter3" Orientation="Horizontal">
                            <telerik:RadPane ID="Radpane4" runat="server" Height="27px" Scrolling="None">
                                <h4>Invoice</h4>
                            </telerik:RadPane>

                            <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">
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
