<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileDownloadList.ascx.cs" Inherits="App_Data.FileDownloadList" %>

<fieldset>
    <legend>File Upload</legend>
    <div style="float: left; width: 100%;">
        <div style="float: left; width: 70%;">
            <telerik:RadAsyncUpload runat="server" ID="AsyncUpload" MultipleFileSelection="Automatic" UploadedFilesRendering="BelowFileInput" Width="300px" />
        </div>

        <div style="float: left; width: 70%;">
            <telerik:RadListBox ID="RadListBoxFileInfo" runat="server" Width="100%" Height="70px" />
        </div>
        <div style="float: left; width: 30%;">
            <telerik:RadButton ID="RadButtonFileDownload" runat="server" OnClick="RadButtonFileDownload_Click" Text="Download"></telerik:RadButton>
            <telerik:RadButton ID="RadButtonFileDelete" runat="server" OnClick="RadButtonFileDelete_Click" Text="Delete"></telerik:RadButton>
        </div>
    </div>
</fieldset>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

    </script>
</telerik:RadCodeBlock>
