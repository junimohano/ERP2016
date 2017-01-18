<%@ Page Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Dict.aspx.cs" Inherits="School.Systems.Dict" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="TopPane" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%">
                    <Items>
                        <telerik:RadToolBarButton>
                            <ItemTemplate>
                                <div class="pagetitle">System Dict</div>
                            </ItemTemplate>
                        </telerik:RadToolBarButton>
                        <telerik:RadToolBarButton>
                            <ItemTemplate>
                                <telerik:RadComboBox ID="ddlSearchType" runat="server" Width="200" DropDownAutoWidth="Enabled" DataSourceID="odsType" DataTextField="Name" DataValueField="Value" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="TypeChange">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Select Dict Type" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </ItemTemplate>
                        </telerik:RadToolBarButton>
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="MainPane" runat="server" Scrolling="None">
                <div>
                    <div style="float: left; padding: 15px; width: 200px;">
                        <telerik:RadListBox ID="lbDicts" runat="server" Width="180px" Height="600px" OnSelectedIndexChanged="SelectDict" AutoPostBack="true">
                            <ItemTemplate>
                                <%# Eval("Name") %>
                            </ItemTemplate>
                        </telerik:RadListBox>
                        <div style="margin: 5px 0px;">
                            <telerik:RadButton ID="btNew" runat="server" Width="170px" Text="Add A New Dict" Icon-PrimaryIconUrl="~/assets/img/icon_s_add.png" OnClick="NewClick" />
                        </div>
                    </div>
                    <div style="float: left; min-width: 500px; padding: 20px;">
                        <asp:HiddenField ID="hfID" runat="server" />
                        <table class="form">
                            <tbody>
                                <tr>
                                    <td>
                                        <label>Name:</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="tbName" runat="server" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Value:</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="tbValue" runat="server" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Type:</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="ddlType" runat="server" Width="200" DropDownAutoWidth="Enabled" DataSourceID="odsType" DataTextField="Name" DataValueField="Value" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Abbreviation:</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="tbAbbreviation" runat="server" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Comment:</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="tbComment" TextMode="MultiLine" Width="200px" Height="80px" runat="server" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <telerik:RadButton ID="btSave" runat="server" Text="Save" Width="60px" Icon-PrimaryIconUrl="~/assets/img/icon_s_save.png" OnClick="SaveClick" />
                                        <telerik:RadButton ID="btDelete" Visible="false" runat="server" Text="Delete" Width="60px" Icon-PrimaryIconUrl="~/assets/img/icon_s_delete.png" OnClick="DeleteClick" OnClientClicking="DeleteConfirm" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <asp:ObjectDataSource ID="odsType" runat="server"
                    SelectMethod="GetDictType" TypeName="Erp2016.Lib.CDict"></asp:ObjectDataSource>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

        
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
