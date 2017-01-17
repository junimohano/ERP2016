<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>ERP</title>
    <link rel="shortcut icon" href="~/assets/img/logo_loyalist_24x24.png" />
    <link href="~/assets/css/login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
                <telerik:RadScriptReference Path="~/assets/js/base.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadStyleSheetManager runat="server" ID="RadStyleSheetManager1" />
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="form1" />
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" BackgroundTransparency="80" EnableSkinTransparency="True" BackgroundPosition="Center" />
        <telerik:RadWindowManager ID="RadWindowManager1" ShowOnTopWhenMaximized="false" ReloadOnShow="true" runat="server" EnableShadow="true" DestroyOnClose="True" Modal="true" Style="z-index: 7001" IconUrl="~/assets/img/bt_assessment.png" />
        <telerik:RadSkinManager ID="RadSkinManager1" runat="server" PersistenceKey="Skin" PersistenceMode="Cookie" />

        <telerik:RadWindow ID="wndMessage" IconUrl="assets/img/icon_s_info.png" runat="server" Title="Login" Width="350" Height="150" Behaviors="Close" EnableShadow="true" VisibleStatusbar="false" Modal="true">
            <ContentTemplate>
                <div id="msg" style="margin: 10px; padding-left: 55px; padding-top: 15px; text-align: center;">
                    <div>
                        <div id="ltMsg" class="message"></div>
                        <telerik:RadButton ID="btClose" runat="server" Text="Ok" Width="80px" OnClientClicking="CloseMessageWindow" />
                    </div>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>

        <telerik:RadWindow ID="wndLogin" runat="server" Title="Login" Width="550" VisibleStatusbar="false" Modal="false"
            Height="400" VisibleOnPageLoad="true" Behaviors="Move" EnableShadow="true" Style="overflow: hidden; z-index: 2999;">
            <ContentTemplate>
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
                    <asp:Panel ID="pnLogin" runat="server">
                        <table style="padding: 8% 15% 0px; width: 350px;">
                            <tr>
                                <td class="title">Login ID</td>
                                <td class="field">
                                    <telerik:RadTextBox ID="tbUsername" runat="server" Width="250" Label="" />
                                </td>
                                <td class="msg">
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Required" Display="Dynamic" ValidationGroup="Val" ControlToValidate="tbUsername" />
                                </td>
                            </tr>

                            <tr style="height: 20px;">

                                <tr>
                                    <td class="title">Password</td>
                                    <td class="field">
                                        <telerik:RadTextBox ID="tbPassword" TextMode="Password" Width="250" runat="server" Label="" />
                                    </td>
                                    <td class="msg">
                                        <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Required" Display="Dynamic" ValidationGroup="Val" ControlToValidate="tbPassword" />
                                    </td>
                                </tr>

                                <tr style="height: 20px;">

                                    <tr style="height: 30px;">
                                        <td>&nbsp;</td>
                                        <td>
                                            <span style="float: left; margin-top: 2px;">
                                                <telerik:RadButton ID="RadButtonKeepSign" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="false" Text=""></telerik:RadButton>
                                            </span>
                                            <span style="float: left;">Remember ID</span>
                                            <br style="clear: both;" />
                                        </td>
                                        <td></td>
                                    </tr>

                                    <tr style="height: 10px;">

                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <telerik:RadButton ID="btLogin" ValidationGroup="Val" runat="server" Text="Login" Width="220px" OnClick="Login_Click" />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </tr>
                                </tr>
                            </tr>
                        </table>

                    </asp:Panel>

                </telerik:RadAjaxPanel>
            </ContentTemplate>
        </telerik:RadWindow>

        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                // call when page load.
                function pageLoad(sender, args) {
                    if (args._isPartialLoad === false) {
                        var imageIndex = 0;
                        var cookieValue = getCookie("backgroundnumber");

                        if (cookieValue !== "") {
                            imageIndex = parseInt(cookieValue) + 1;
                            if (imageIndex > 7)
                                imageIndex = 0;
                        }

                        /* Location of the image */
                        document.body.style.backgroundImage = "url(./assets/img/school" + imageIndex + ".jpg)";
                        setCookie("backgroundnumber", imageIndex, 365);
                    }
                }

                function setCookie(cname, cvalue, exdays) {
                    var d = new Date();
                    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
                    var expires = "expires=" + d.toUTCString();
                    document.cookie = cname + "=" + cvalue + "; " + expires;
                }

                function getCookie(cname) {
                    var name = cname + "=";
                    var ca = document.cookie.split(';');
                    for (var i = 0; i < ca.length; i++) {
                        var c = ca[i];
                        while (c.charAt(0) == ' ') c = c.substring(1);
                        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
                    }
                    return "";
                }

                //function getRandomInt(min, max) {
                //    return Math.floor(Math.random() * (max - min + 1) + min);
                //}

                function ShowMessage(msg, title, type) {
                    var wndmsg = $find("<%= wndMessage.ClientID %>");
                    $telerik.$('#ltMsg').html(msg);

                    if (type == 'success') {
                        $telerik.$('#msg').addClass('msg_suc');
                        wndmsg.set_iconUrl("assets/img/icon_ok.png");
                    } else {
                        $telerik.$('#msg').addClass('msg_err');
                        wndmsg.set_iconUrl("assets/img/icon_s_error.png");
                    }

                    wndmsg.set_title(title);
                    wndmsg.show();
                    wndmsg.setSize(300, 180);
                    wndmsg.center();
                }

                function CloseMessageWindow(sender, args) {
                    var wndmsg = $find("<%= wndMessage.ClientID %>");
                    wndmsg.close();
                    args.set_cancel(true);
                }
            </script>
        </telerik:RadCodeBlock>

    </form>
</body>
</html>
