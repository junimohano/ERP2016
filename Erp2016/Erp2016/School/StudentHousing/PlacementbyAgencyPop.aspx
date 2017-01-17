<%@ Page Title="" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="PlacementbyAgencyPop.aspx.cs" Inherits="School_StudentHousing_PlacementbyAgencyPop" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <style type="text/css">
       
    </style>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadpaneBasic" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="UpdateToolBar" runat="server" OnButtonClick="UpdateToolBar_ButtonClick" OnClientButtonClicking="ToolbarButtonClick">
                    <Items>
                        <telerik:RadToolBarButton TabIndex="0" ImageUrl="~/assets/img/bt_save.png" Text="Accepted" ToolTip="Save" ValidationGroup="Info" />
                        <telerik:RadToolBarButton TabIndex ="1" ImageUrl ="~/assets/img/bt_save.png" Text ="Cancelled" ToolTip ="Cancel"></telerik:RadToolBarButton>
                        <telerik:RadToolBarButton TabIndex ="2" ImageUrl="~/assets/img/bt_save.png" Text="Rejected" ToolTip ="Rejected" ></telerik:RadToolBarButton>
                        <telerik:RadToolBarButton TabIndex ="3" ImageUrl="~/assets/img/bt_back.png" Text="Close Window" ToolTip="Close Window"></telerik:RadToolBarButton>
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">
                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Vertical">
                    <telerik:RadPane ID="Radpane1" runat="server" Width="600px">
                        <br />

                      <table width ="100%">
                          <tr>
                              <td style="width:20%"> 
                                    <telerik:RadLabel runat ="server" ID="lblComments" Text="Comment:" ></telerik:RadLabel>
                              </td>
                              <td style ="width:80%">
                                    <telerik:RadTextBox runat ="server" ID="txt_Comment" Height="93px" TextMode="MultiLine" Width="600px" ></telerik:RadTextBox>

                              </td>
                          </tr>
                          <tr>
                              <td style="width:20%"> 
<telerik:RadLabel runat="server" Text="File Upload:" ID="lblFile" ></telerik:RadLabel>
                              </td>
                              <td style ="width:80%">
                                   <UserControl:FileDownloadList ID="file_upload" runat="server" />
                              </td>
                          </tr>
                      </table>
             </telerik:RadPane>A@QwQW
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
                if (button.get_text() == "Save Homestay Placement by Agency") {
                    if (!confirm('Do you want to Save Homestay Placement by Agency it?'))
                        args.set_cancel(true);
                }
                if (button.get_text() == "Close Window") {
                    Close();
                }
                function OnClientClose(oWnd, args) {
                    Close();
                    //var arg = args.get_argument();
                <%--<%=Page.GetPostBackEventReference(btnRefresh)%>--%>
            }
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
