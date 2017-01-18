using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using Erp2016.Lib;


public partial class Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.RemoveAll();
            Session.Clear();

            // get cookie
            RadButtonKeepSign.Checked = ReadCookie("IsKeepSign") == "1" ? true : false;
            if (RadButtonKeepSign.Checked)
                tbUsername.Text = ReadCookie("Username");
        }
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            var cUser = new CUser();
            var user = cUser.Get(tbUsername.Text.Trim());
            if (user != null)
            {
                if (CCryptography.DecryptCipherTextToPlainText(user.Password.Trim()) == tbPassword.Text.Trim())
                {
                    if (user.IsActive)
                    {
                        btLogin.Enabled = false;

                        Session["UserId"] = user.UserId;
                        Session["SiteLocationId"] = user.SiteLocationId;
                        var siteLocation = (new CSiteLocation()).Get(user.SiteLocationId);
                        if (siteLocation != null)
                        {
                            Session["SiteId"] = siteLocation.SiteId;
                            Session["SiteName"] = new CSite().Get(siteLocation.SiteId)?.Abbreviation;
                            Session["SiteLocationName"] = siteLocation.Name;
                        }
                        Session["UserName"] = cUser.GetUserName(user);
                        Session["UserPositionId"] = user.UserPositionId;
                        var userPosition = (new CUserPosition()).Get(user.UserPositionId);
                        if (userPosition != null)
                            Session["UserGroupId"] = userPosition.UserGroupId;

                        var userPermissionModelList = (new CUserPermission()).GetUserPermissionModelList(user.UserId);
                        Session["UserPermissionModelList"] = userPermissionModelList;

                        RadAjaxPanel1.Redirect("~/Dashboard");
                    }
                    else
                    {
                        ShowMessage("Your account is disabled<br /><br />Please contact administrator.");
                    }

                    // set cookie
                    WriteCookie("IsKeepSign", RadButtonKeepSign.Checked ? "1" : "0");
                    if (RadButtonKeepSign.Checked)
                        WriteCookie("Username", tbUsername.Text.Trim());
                }
                else
                {
                    ShowMessage("Wrong Password<br /><br />Please try again!");
                }


            }
            else
            {
                ShowMessage("Invalid Login Id<br /><br />Please try again!");
            }
        }
    }

    private void ShowMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "radalert('" + message + "', 300, 150, 'Message');", true);
    }
    
    public void WriteCookie(string strCookieName, string strCookieValue)
    {
        var hcCookie = new HttpCookie(strCookieName, strCookieValue);
        HttpContext.Current.Response.Cookies.Set(hcCookie);
    }


    public string ReadCookie(string strCookieName)
    {
        foreach (string strCookie in HttpContext.Current.Response.Cookies.AllKeys)
        {
            if (strCookie == strCookieName)
            {
                return HttpContext.Current.Response.Cookies[strCookie].Value;
            }
        }

        foreach (string strCookie in HttpContext.Current.Request.Cookies.AllKeys)
        {
            if (strCookie == strCookieName)
            {
                return HttpContext.Current.Request.Cookies[strCookie].Value;
            }
        }

        return null;
    }
}