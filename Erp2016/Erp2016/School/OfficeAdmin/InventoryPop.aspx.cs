using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;
using Convert = System.Convert;

namespace School.OfficeAdmin
{
    public partial class InventoryPop : PageBase
    {
        private int Id { get; set; }

        public InventoryPop() : base((int)CConstValue.Menu.Inventory)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Inventory);

                var buttonList = new List<string>();

                // new
                if (Request["createOrListType"] == "0")
                {
                    buttonList.Add("Save");
                    buttonList.Add("Close");
                }
                // select
                else
                {
                    buttonList.Add("Save");
                    buttonList.Add("Print");
                    buttonList.Add("Close");
                }

                foreach (RadToolBarItem item in RadToolBar1.Items)
                {
                    if (buttonList.Contains(item.Text))
                        item.Visible = true;
                    else
                        item.Visible = false;
                }

                LoadInventoryCategory();
                if (!string.IsNullOrEmpty(RadComboBoxInventoryCategory.SelectedValue))
                    LoadInventoryCategoryItem(Convert.ToInt32(RadComboBoxInventoryCategory.SelectedValue));

                LoadSite();
                LoadSiteLocation(CurrentSiteId);
                LoadAssignedUser(CurrentSiteLocationId);

                LoadInUse();
                LoadCondition();

                var inventory = new CInventory().Get(Id);
                if (inventory != null)
                {
                    var inventoryCategoryItem = new CInventoryCategoryItem().Get(inventory.InventoryCategoryItemId);
                    RadComboBoxInventoryCategory.SelectedValue = inventoryCategoryItem.InventoryCategoryId.ToString();
                    RadComboBoxInventoryCategoryItem.SelectedValue = inventory.InventoryCategoryItemId.ToString();

                    var siteLocation = new CSiteLocation().Get(inventory.SiteLocationId);
                    RadComboBoxSite.SelectedValue = siteLocation.SiteId.ToString();
                    RadComboBoxSiteLocation.SelectedValue = inventory.SiteLocationId.ToString();

                    RadComboBoxAssignedUser.SelectedValue = inventory.AssignedUserId.ToString();

                    RadComboBoxInUse.SelectedValue = inventory.InUseType.ToString();
                    RadComboBoxCondition.SelectedValue = inventory.ConditionType.ToString();

                    RadTextBoxCompany.Text = inventory.Company;
                    RadNumericTextBoxPrice.Value = (double)inventory.Price;
                    RadDatePickerPurchased.SelectedDate = inventory.PurchasedDate?.Date;
                    RadDatePickerExpire.SelectedDate = inventory.ExpireDate?.Date;
                    RadTextBoxDepartment.Text = inventory.Company;
                    RadTextBoxModelNo.Text = inventory.ModelNo;
                    RadTextBoxSerialNo.Text = inventory.SerialNo;

                    FileDownloadList1.GetFileDownload(Convert.ToInt32(Id));
                }
            }
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBar1.Items)
                {
                    if (toolbarItem.Text == "Close" || toolbarItem.Text == "Print") continue;

                    toolbarItem.Enabled = false;
                }

                RadComboBoxInventoryCategoryItem.Enabled = false;
                RadComboBoxSiteLocation.Enabled = false;
                RadComboBoxAssignedUser.Enabled = false;
                RadComboBoxCondition.Enabled = false;
                RadComboBoxInUse.Enabled = false;
                RadTextBoxCompany.Enabled = false;
                RadNumericTextBoxPrice.Enabled = false;
                RadDatePickerPurchased.Enabled = false;
                RadDatePickerExpire.Enabled = false;
                RadTextBoxDepartment.Enabled = false;
                RadTextBoxCompany.Enabled = false;
                RadTextBoxSerialNo.Enabled = false;
                FileDownloadList1.SetVisibieUploadControls(false);
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            // Save
            if (e.Item.Text == "Save")
            {
                if (IsValid)
                {
                    var cObj = new CInventory();
                    var obj = cObj.Get(Id);

                    var isNew = false;

                    // new one
                    if (obj == null)
                    {
                        obj = new Erp2016.Lib.Inventory();
                        obj.CreatedId = Convert.ToInt32(CurrentUserId);
                        obj.CreatedDate = DateTime.Now;

                        isNew = true;
                    }
                    else
                    {
                        obj.UpdatedId = Convert.ToInt32(CurrentUserId);
                        obj.UpdatedDate = DateTime.Now;
                    }

                    obj.InventoryCategoryItemId = Convert.ToInt32(RadComboBoxInventoryCategoryItem.SelectedValue);
                    obj.SiteLocationId = Convert.ToInt32(RadComboBoxSiteLocation.SelectedValue);
                    obj.AssignedUserId = Convert.ToInt32(RadComboBoxAssignedUser.SelectedValue);
                    obj.ConditionType = Convert.ToInt32(RadComboBoxCondition.SelectedValue);
                    obj.InUseType = Convert.ToInt32(RadComboBoxInUse.SelectedValue);
                    obj.Company = RadTextBoxCompany.Text;
                    obj.Price = (decimal)(RadNumericTextBoxPrice.Value ?? 0);
                    obj.PurchasedDate = RadDatePickerPurchased.SelectedDate?.Date;
                    obj.ExpireDate = RadDatePickerExpire.SelectedDate?.Date;
                    obj.Department = RadTextBoxDepartment.Text;
                    obj.ModelNo = RadTextBoxModelNo.Text;
                    obj.SerialNo = RadTextBoxSerialNo.Text;

                    var index = 0;
                    if (isNew)
                        index = cObj.Add(obj);
                    else {
                        cObj.Update(obj);
                        index = obj.InventoryId;
                    }

                    // save uploading file
                    FileDownloadList1.SaveFile(index);

                    RunClientScript("Close();");
                }
                else
                    ShowMessage("Failed");
            }
            // Print
            else if (e.Item.Text == "Print")
            {
                RunClientScript("ShowPrint();");
            }
            // close
            else if (e.Item.Text == "Close")
            {
                RunClientScript("Close();");
            }
        }

        protected void RadComboBoxCategory_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadInventoryCategoryItem(Convert.ToInt32(RadComboBoxInventoryCategory.SelectedValue));
        }

        protected void RadComboBoxSite_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSiteLocation(Convert.ToInt32(RadComboBoxSite.SelectedValue));
        }

        protected void RadComboBoxSiteLocation_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadAssignedUser(Convert.ToInt32(RadComboBoxSiteLocation.SelectedValue));
        }

        protected void LoadSite()
        {
            var global = new CGlobal();
            RadComboBoxSite.Items.Clear();
            RadComboBoxSite.Text = string.Empty;
            RadComboBoxSite.DataSource = global.GetSiteId();
            RadComboBoxSite.DataTextField = "Name";
            RadComboBoxSite.DataValueField = "Value";
            RadComboBoxSite.DataBind();
            foreach (RadComboBoxItem item in RadComboBoxSite.Items)
            {
                if (item.Value == CurrentSiteId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }

        protected void LoadSiteLocation(int siteId)
        {
            var global = new CGlobal();
            RadComboBoxSiteLocation.Items.Clear();
            RadComboBoxSiteLocation.Text = string.Empty;
            RadComboBoxSiteLocation.DataSource = global.GetSiteLocationBySiteId(siteId);
            RadComboBoxSiteLocation.DataTextField = "Name";
            RadComboBoxSiteLocation.DataValueField = "Value";
            RadComboBoxSiteLocation.DataBind();

            foreach (RadComboBoxItem item in RadComboBoxSiteLocation.Items)
            {
                if (item.Value == CurrentSiteLocationId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }

        protected void LoadAssignedUser(int siteLocationId)
        {
            RadComboBoxAssignedUser.Items.Clear();
            RadComboBoxAssignedUser.Text = string.Empty;
            RadComboBoxAssignedUser.DataSource = new CUser().GetUserBySiteLocation(siteLocationId);
            RadComboBoxAssignedUser.DataTextField = "Name";
            RadComboBoxAssignedUser.DataValueField = "Value";
            RadComboBoxAssignedUser.DataBind();

            foreach (RadComboBoxItem item in RadComboBoxAssignedUser.Items)
            {
                item.Selected = true;
                break;
            }
        }

        protected void LoadCondition()
        {
            RadComboBoxCondition.Items.Clear();
            RadComboBoxCondition.Text = string.Empty;
            RadComboBoxCondition.DataSource = new CGlobal().GetDictionary(1622);
            RadComboBoxCondition.DataTextField = "Name";
            RadComboBoxCondition.DataValueField = "Value";
            RadComboBoxCondition.DataBind();
            foreach (RadComboBoxItem item in RadComboBoxCondition.Items)
            {
                item.Selected = true;
                break;
            }
        }

        protected void LoadInUse()
        {
            RadComboBoxInUse.Items.Clear();
            RadComboBoxInUse.Text = string.Empty;
            RadComboBoxInUse.DataSource = new CGlobal().GetDictionary(1624);
            RadComboBoxInUse.DataTextField = "Name";
            RadComboBoxInUse.DataValueField = "Value";
            RadComboBoxInUse.DataBind();
            foreach (RadComboBoxItem item in RadComboBoxInUse.Items)
            {
                item.Selected = true;
                break;
            }
        }

        protected void LoadInventoryCategory()
        {
            RadComboBoxInventoryCategory.Items.Clear();
            RadComboBoxInventoryCategory.Text = string.Empty;
            RadComboBoxInventoryCategory.DataSource = new CInventory().GetInventoryCategoryList();
            RadComboBoxInventoryCategory.DataTextField = "Name";
            RadComboBoxInventoryCategory.DataValueField = "Value";
            RadComboBoxInventoryCategory.DataBind();
            foreach (RadComboBoxItem item in RadComboBoxInventoryCategory.Items)
            {
                item.Selected = true;
                break;
            }
        }

        protected void LoadInventoryCategoryItem(int inventoryCategoryId)
        {
            RadComboBoxInventoryCategoryItem.Items.Clear();
            RadComboBoxInventoryCategoryItem.Text = string.Empty;
            RadComboBoxInventoryCategoryItem.DataSource = new CInventory().GetInventoryCategoryItemList(inventoryCategoryId);
            RadComboBoxInventoryCategoryItem.DataTextField = "Name";
            RadComboBoxInventoryCategoryItem.DataValueField = "Value";
            RadComboBoxInventoryCategoryItem.DataBind();

            foreach (RadComboBoxItem item in RadComboBoxInventoryCategoryItem.Items)
            {
                item.Selected = true;
                break;
            }
        }

    }
}