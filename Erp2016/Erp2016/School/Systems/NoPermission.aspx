<%@ Page Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="NoPermission.aspx.cs" Inherits="School.Systems.NoPermission" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <style type="text/css">
        .container {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translateX(-50%) translateY(-50%);
        }
    </style>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Vertical">

            <telerik:RadPane ID="RadPane1" runat="server">
                <div class="container">
                    <telerik:RadBinaryImage ID="RadBinaryImagePicture" runat="server" ImageUrl="~/assets/img/noentry.jpg" ResizeMode="Fit" Height="320" Width="480" AutoAdjustImageControlSize="False" />
                    <br />
                    <br />
                    <label style="font-size: 70px;">Access denied</label>
                </div>
            </telerik:RadPane>

        </telerik:RadSplitter>

    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
