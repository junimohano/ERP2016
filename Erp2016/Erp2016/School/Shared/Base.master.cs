using System;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Shared
{
    public partial class Base : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageBase p = (PageBase)Page;
                var cUserPermission = new CUserPermission();
                var userPermissionList = cUserPermission.GetUserPermissionModelList(p.CurrentUserId);

                foreach (var userPermission in userPermissionList)
                {
                    foreach (int index in Enum.GetValues(typeof(CConstValue.Menu)))
                    {
                        // permission type == Access (0)
                        if (userPermission.MenuId == index && userPermission.IsAccess)
                            GetMenuItem(RadMenu1.Items, Enum.GetName(typeof(CConstValue.Menu), index).ToLower());
                    }
                }

                var logoPath = new CGlobal().GetLogoImagePath(p.CurrentSiteLocationId, CConstValue.ImageType.Small);
                if (logoPath != string.Empty)
                {
                    try
                    {
                        RadBinaryImageSiteLogo.DataValue = File.ReadAllBytes(logoPath);
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                    }
                }
            }
        }

        private void GetMenuItem(RadMenuItemCollection radMenuItem, string enumMenuName)
        {
            foreach (RadMenuItem menu in radMenuItem)
            {
                if (menu.Text == string.Empty) continue;

                var menuName = menu.Value == string.Empty ? menu.Text.Replace(" ", string.Empty).ToLower() : menu.Value.Replace(" ", string.Empty).ToLower();
                if (enumMenuName == menuName)
                {
                    menu.Enabled = true;

                    // check select
                    if (menu.NavigateUrl.ToLower() == ('~' + HttpContext.Current.Request.Url.AbsolutePath.ToLower()))
                    {
                        //menu.Selected = true;
                        if (menu.Parent.GetType() == typeof(RadMenuItem))
                            ((RadMenuItem)menu.Parent).Selected = true;
                    }
                }
                GetMenuItem(menu.Items, enumMenuName);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //if (Session["SeletedMenuIndex"] != null)
            //    RadMenu1.Items[Convert.ToInt32(Session["SeletedMenuIndex"])].Selected = true;
        }

        protected void RadButtonLogout_OnClick(object sender, EventArgs e)
        {
            if (Session != null && Session["UserId"] != null)
                Session["UserId"] = null;
            Response.Redirect("~");
        }

        protected void RadSkinManager1_OnSkinChanged(object sender, SkinChangedEventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void ButtonNotification_OnClick(object sender, EventArgs e)
        {
            RadNotification1.Show();
        }

        //Custom Themes
        protected void RadSkinManager1_PreRender(object sender, EventArgs e)
        {
            //RadComboBox skinChooser = RadSkinManager1.FindControl("SkinChooser") as RadComboBox;
            //int i = 0;
            //while (i < skinChooser.Items.Count)
            //{
            //    if (skinChooser.Items[i].Text == "Black")
            //    {
            //        skinChooser.Items.Remove(skinChooser.Items[i]);
            //    }
            //    if (skinChooser.Items[i].Text == "BlackMetroTouch")
            //    {
            //        skinChooser.Items.Remove(skinChooser.Items[i]);
            //    }
            //    if (skinChooser.Items[i].Text == "Metro")
            //    {
            //        skinChooser.Items.Remove(skinChooser.Items[i]);
            //    }
            //    if (skinChooser.Items[i].Text == "MetroTouch")
            //    {
            //        skinChooser.Items.Remove(skinChooser.Items[i]);
            //    }
            //    if (skinChooser.Items[i].Text == "Office2010Black")
            //    {
            //        skinChooser.Items.Remove(skinChooser.Items[i]);
            //    }
            //    if (skinChooser.Items[i].Text == "Office2010Blue")
            //    {
            //        skinChooser.Items.Remove(skinChooser.Items[i]);
            //    }
            //    if (skinChooser.Items[i].Text == "Office2010Silver")
            //    {
            //        skinChooser.Items.Remove(skinChooser.Items[i]);
            //    }
            //    if (skinChooser.Items[i].Text == "Windows7")
            //    {
            //        skinChooser.Items.Remove(skinChooser.Items[i]);
            //    }

            //    i++;
            //    //more conditions here  
            // }
        }
    }
}