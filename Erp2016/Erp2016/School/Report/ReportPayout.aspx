<%@ Page Title="" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="ReportPayout.aspx.cs" Inherits="School_Report_ReportPayout" %>


<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=10.2.16.914, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter2" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane3" runat="server" Scrolling="None" BackColor="White">
                <telerik:ReportViewer ID="RV_Payout" runat="server" Height="100%" Width="100%" />
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>
</asp:Content>

