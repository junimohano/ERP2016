using System;
using System.Data;
using System.Text;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class UserPermissionPop : PageBase
    {
        private int Id { get; set; }

        public UserPermissionPop() : base((int)CConstValue.Menu.User)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (new CUser().IsUserPermission(CurrentGroupId) == false)
                Response.Redirect("~/NoPermission");

            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                foreach (int index in Enum.GetValues(typeof(CConstValue.Menu)))
                {
                    RadComboBoxMenu.Items.Add(new RadComboBoxItem(Enum.GetName(typeof(CConstValue.Menu), index), index.ToString()));
                }
            }
        }

        protected void RadComboBoxMenu_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var sb = new StringBuilder();
            var collection = RadComboBoxMenu.CheckedItems;

            if (collection.Count != 0)
            {
                sb.Append("<h4>Checked SiteLocation List</h4>");

                foreach (var item in collection)
                    sb.Append("<li><label>" + item.Text + "</label></li>");

                sb.Append("</ul>");

                itemsClientSide.Text = sb.ToString();
            }
            else
            {
                itemsClientSide.Text = "<label>No items selected</label>";
            }

            RadCheckBoxAccess.Checked = false;
            RadCheckBoxModify.Checked = false;

            if (RadComboBoxMenu.CheckedItems.Count == 1)
            {
                var cUserPermission = new CUserPermission();
                var user = cUserPermission.Get(Id, Convert.ToInt32(RadComboBoxMenu.CheckedItems[0].Value));
                foreach (UserPermission userPermission in user)
                {
                    switch (userPermission.PermissionType)
                    {
                        // Access Permission
                        case 0:
                            RadCheckBoxAccess.Checked = true;
                            break;

                        // Modify Permission
                        case 1:
                            RadCheckBoxModify.Checked = true;
                            break;
                    }
                }
            }
        }

        private void SetSave(RadCheckBox checkBox)
        {
            var cUserPermission = new CUserPermission();

            bool isSuccess = true;
            foreach (var checkItem in RadComboBoxMenu.CheckedItems)
            {
                UserPermission userPermissionAccess = cUserPermission.Get(Id, Convert.ToInt32(checkItem.Value), Convert.ToInt32(RadCheckBoxAccess.Value));
                UserPermission userPermissionModify = cUserPermission.Get(Id, Convert.ToInt32(checkItem.Value), Convert.ToInt32(RadCheckBoxModify.Value));

                // Access
                if (checkBox == RadCheckBoxAccess)
                {
                    if (RadCheckBoxAccess.Checked == true)
                    {
                        if (userPermissionAccess == null)
                        {
                            int result = cUserPermission.Add(new UserPermission()
                            {
                                PermissionType = Convert.ToInt32(RadCheckBoxAccess.Value),
                                CreatedDate = DateTime.Now,
                                CreatedId = CurrentUserId,
                                UserId = Id,
                                MenuId = Convert.ToInt32(checkItem.Value)
                            });
                            if (result < 0) isSuccess = false;
                        }
                    }
                    else
                    {
                        if (userPermissionAccess != null)
                        {
                            bool result = cUserPermission.Delete(userPermissionAccess);
                            if (result == false) isSuccess = false;
                        }
                    }
                }

                // Modify
                if (checkBox == RadCheckBoxModify)
                {
                    if (RadCheckBoxModify.Checked == true)
                    {
                        if (userPermissionModify == null)
                        {
                            int result = cUserPermission.Add(new UserPermission()
                            {
                                PermissionType = Convert.ToInt32(RadCheckBoxModify.Value),
                                CreatedDate = DateTime.Now,
                                CreatedId = CurrentUserId,
                                UserId = Id,
                                MenuId = Convert.ToInt32(checkItem.Value)
                            });
                            if (result < 0) isSuccess = false;
                        }
                    }
                    else
                    {
                        if (userPermissionModify != null)
                        {
                            bool result = cUserPermission.Delete(userPermissionModify);
                            if (result == false) isSuccess = false;
                        }
                    }
                }
            }

            if (isSuccess)
                ShowMessage("It has been updated.");
            else
                ShowMessage("Failed.");
        }

        protected void RadToolBarPermission_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Reset":
                    var user = new CUser().Get(Id);
                    if (user != null)
                    {
                        new CUserPermission().SetBasicPermission(user, CurrentUserId);
                        RunClientScript("Close();");
                    }

                    break;
                case "Close":
                    RunClientScript("Close();");
                    break;
            }
        }

        protected void RadGridSiteLocation_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridSiteLocationUser_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridConfirm_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridConfirm_OnPreRender(object sender, EventArgs e)
        {
            LinqDataSourceConfirm.WhereParameters.Clear();
            LinqDataSourceConfirm.WhereParameters.Add("UserId", DbType.Int32, Id.ToString());
            LinqDataSourceConfirm.Where = "UserId == @UserId";
        }

        protected void RadGridSiteLocationUser_OnPreRender(object sender, EventArgs e)
        {
            LinqDataSourceSiteLocationUser.WhereParameters.Clear();
            if (RadComboBoxMenu.CheckedItems.Count == 1)
                LinqDataSourceSiteLocationUser.WhereParameters.Add("MenuId", DbType.Int32, RadComboBoxMenu.CheckedItems[0].Value);
            else
                LinqDataSourceSiteLocationUser.WhereParameters.Add("MenuId", DbType.Int32, 0.ToString());
            LinqDataSourceSiteLocationUser.WhereParameters.Add("UserId", DbType.Int32, Id.ToString());
            LinqDataSourceSiteLocationUser.Where = "MenuId == @MenuId && UserId == @UserId";
        }

        protected void RadCheckBoxAccess_OnCheckedChanged(object sender, EventArgs e)
        {
            if (IsPostBack && GetPostBackControl(this).GetType() == typeof(RadCheckBox))
                SetSave(RadCheckBoxAccess);
        }

        protected void RadCheckBoxModify_OnCheckedChanged(object sender, EventArgs e)
        {
            if (IsPostBack && GetPostBackControl(this).GetType() == typeof(RadCheckBox))
                SetSave(RadCheckBoxModify);
        }

        protected void RadGridConfirm_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;

                // init
                dataItem["MenuId"].Text = Enum.GetName(typeof(CConstValue.Menu), Convert.ToInt32(dataItem["MenuId"].Text));
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
                //(footer["AgencyPrice"].FindControl("TotalAgency") as RadNumericTextBox).Value = double.Parse(agencySum.ToString());
            }
        }

        protected void RadGridSiteLocation_OnPreRender(object sender, EventArgs e)
        {
            int menuIndex = 0;
            if (RadComboBoxMenu.CheckedItems.Count == 1)
                menuIndex = Convert.ToInt32(RadComboBoxMenu.CheckedItems[0].Value);

            var cUserPermission = new CUserPermission();
            RadGridSiteLocation.DataSource = HelperFunctions.ToDataTable(cUserPermission.GetUserPermissionSiteLocationListProc(Id, menuIndex));
            RadGridSiteLocation.DataBind();
        }

        protected void RadGridSiteLocation_OnRowDrop(object sender, GridDragDropEventArgs e)
        {
            //if (e.DraggedItems.Count != 0 && RadComboBoxMenu.CheckedItems.Count > 0 && e.DestinationGrid == RadGridSiteLocationUser)
            if (e.DraggedItems.Count != 0 && RadComboBoxMenu.CheckedItems.Count > 0)
            {
                bool isSuccess = true;

                foreach (var dataItem in e.DraggedItems)
                {
                    foreach (var checkedItem in RadComboBoxMenu.CheckedItems)
                    {
                        var cUserPermission = new CUserPermission();
                        // Search Type for siteLocation = 2
                        var userPermission = cUserPermission.Get(Id, Convert.ToInt32(checkedItem.Value), 2, Convert.ToInt32(dataItem.GetDataKeyValue("SiteLocationId").ToString()));
                        if (userPermission == null)
                        {
                            userPermission = new UserPermission()
                            {
                                SiteLocationId = Convert.ToInt32(dataItem.GetDataKeyValue("SiteLocationId").ToString()),
                                CreatedDate = DateTime.Now,
                                CreatedId = CurrentUserId,
                                // Search Type for site location = 2
                                PermissionType = 2,
                                MenuId = Convert.ToInt32(checkedItem.Value),
                                UserId = Id
                            };

                            if (cUserPermission.Add(userPermission) < 0)
                                isSuccess = false;
                        }
                    }
                }

                if (isSuccess)
                    ShowMessage("Move Success");
                else
                    ShowMessage("Move Error");
            }
            else
            {
                ShowMessage("Move Failed");
            }
        }

        protected void RadGridSiteLocationUser_OnRowDrop(object sender, GridDragDropEventArgs e)
        {
            //if (e.DraggedItems.Count != 0 && RadComboBoxMenu.CheckedItems.Count > 0 && e.DestinationGrid == RadGridSiteLocation)
            if (e.DraggedItems.Count != 0 && RadComboBoxMenu.CheckedItems.Count > 0)
            {
                bool isSuccess = true;

                foreach (var dataItem in e.DraggedItems)
                {
                    if (RadComboBoxMenu.CheckedItems.Count == 1)
                    {
                        var cUserPermission = new CUserPermission();
                        var userPermission = cUserPermission.Get(Convert.ToInt32(dataItem.GetDataKeyValue("UserPermissionId").ToString()));
                        if (userPermission != null)
                        {
                            if (cUserPermission.Delete(userPermission) == false)
                                isSuccess = false;
                        }
                    }
                    else
                    {
                        foreach (var checkedItem in RadComboBoxMenu.CheckedItems)
                        {
                            var cUserPermission = new CUserPermission();
                            // Search Type for siteLocation = 2
                            var userPermission = cUserPermission.Get(Id, Convert.ToInt32(checkedItem.Value), 2, Convert.ToInt32(dataItem.GetDataKeyValue("SiteLocationId").ToString()));
                            if (userPermission != null)
                            {
                                if (cUserPermission.Delete(userPermission) == false)
                                    isSuccess = false;
                            }
                        }
                    }
                }

                if (isSuccess)
                    ShowMessage("Move Success");
                else
                    ShowMessage("Move Error");
            }
            else
            {
                ShowMessage("Move Failed");
            }
        }
    }
}