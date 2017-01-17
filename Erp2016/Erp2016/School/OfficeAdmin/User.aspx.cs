using System;
using System.Data;
using System.Diagnostics;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace School.OfficeAdmin
{
    public partial class User : PageBase
    {
        public User() : base((int)CConstValue.Menu.User)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResetForm();

                if (new CUser().IsUserPermission(CurrentGroupId))
                    RadToolBarUser.FindItemByText("Permission").Enabled = true;

                if (new CUser().IsUserInformation(CurrentGroupId))
                    RadToolBarUser.FindItemByText("User Information").Enabled = true;
            }

            SearchUser();

            RadComboBoxSiteLocation.OpenDropDownOnLoad = false;
        }

        private void SearchUser()
        {
            LinqDataSourceUser.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteLocationList)
                LinqDataSourceUser.WhereParameters.Add(model.SiteLocationIdName, DbType.Int32, model.SiteLocationId.ToString());
            LinqDataSourceUser.Where = UserPermissionModel.SearchWhereSiteLocationSb.ToString();
        }

        protected void StaffButtonClicked(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == @"New")
            {
                ResetForm();
            }
            else if (e.Item.Text == @"Update" && RadGridUser.SelectedValue != null)
            {
                if (IsValid)
                {
                    var cUser = new CUser();
                    var user = cUser.Get(Convert.ToInt32(RadGridUser.SelectedValue));

                    if (string.IsNullOrEmpty(tbPassWord.Text) == false)
                        user.Password = CCryptography.EncryptPlainTextToCipherText(tbPassWord.Text.Trim());
                    user.FirstName = tbFName.Text;
                    user.MidName = tbMName.Text;
                    user.LastName = tbLName.Text;

                    user.DOB = tbDOB.SelectedDate;
                    user.MaritalStatus = ddlMarital.SelectedValue;
                    user.Gender = ddlGender.SelectedValue;
                    user.Email = tbWEmail.Text.Trim();
                    user.Phone = tbWPhone.Text.Trim();

                    user.EContactName = tbEName.Text;
                    user.ERelation = tbERelation.Text;
                    user.EPhone = tbEPhone.Text;
                    user.EAddress = tbEAddress.Text;

                    // only HR or IT can modify those things.
                    if (CurrentGroupId == (int)CConstValue.UserGroupForUserInformation.HR ||
                        CurrentGroupId == (int)CConstValue.UserGroupForUserInformation.IT)
                    {
                        user.UserPositionId = Convert.ToInt32(RadComboBoxUserPosition.SelectedValue);
                        user.SINNo = Convert.ToInt32(tbSIN.Text);
                        user.SiteLocationId = Convert.ToInt32(RadComboBoxSiteLocation.SelectedValue);
                        user.EmployeeNumber = tbEmpNo.Text;
                        user.IsActive = cbActive.Checked;

                        int? supervisor = string.IsNullOrEmpty(RadComboBoxSuper.SelectedValue) ? (int?)null : Convert.ToInt32(RadComboBoxSuper.SelectedValue);

                        if (user.Supervisor != supervisor)
                        {
                            user.Supervisor = supervisor;

                            var cApp = new CApproval();
                            var appList = cApp.GetList(Convert.ToInt32(RadGridUser.SelectedValue));
                            foreach (var app in appList)
                            {
                                app.Supervisor = user.Supervisor;
                                app.IsActive = user.IsActive;

                                app.UpdatedId = CurrentUserId;
                                app.UpdatedDate = DateTime.Now;

                                cApp.Update(app);
                            }
                        }
                    }

                    user.Address1 = tbPAddess1.Text;
                    user.Address2 = tbPAddess2.Text;
                    user.City = tbPCity.Text;
                    user.Province = tbPProvince.Text;
                    user.PostalCode = tbPPostal.Text;
                    user.HomePhone = tbPHomePhone.Text;
                    user.CellPhone = tbPCell.Text;
                    user.PersonalEmail = tbPEmail.Text;

                    user.UpdatedId = CurrentUserId;
                    user.UpdatedDate = DateTime.Now;

                    if (AsyncUploadPicture.UploadedFiles.Count > 0)
                    {
                        UploadedFile file = AsyncUploadPicture.UploadedFiles[0];
                        byte[] fileData = new byte[file.InputStream.Length];
                        file.InputStream.Read(fileData, 0, (int)file.InputStream.Length);
                        user.Picture = fileData;
                    }

                    if (cUser.Update(user))
                    {
                        RadGridUser.Rebind();
                        ShowMessage("Updated Staff Info Successfully");
                    }
                    else
                    {
                        ShowMessage("Failed To Update Staff Info");
                    }
                }
            }
            else if (e.Item.Text == @"Save")
            {
                if (IsValid)
                {
                    var cUser = new CUser();
                    var user = new Erp2016.Lib.User();

                    if (CurrentSiteId != 1)
                        user.SiteLocationId = Convert.ToInt32(CurrentSiteLocationId);
                    else
                        user.SiteLocationId = Convert.ToInt32(RadComboBoxSiteLocation.SelectedValue);

                    user.LoginId = tbUserID.Text;
                    user.Password = CCryptography.EncryptPlainTextToCipherText(tbPassWord.Text.Trim());
                    user.FirstName = tbFName.Text;
                    user.MidName = tbMName.Text;
                    user.LastName = tbLName.Text;
                    user.SINNo = Convert.ToInt32(tbSIN.Text);
                    user.DOB = tbDOB.SelectedDate;
                    user.MaritalStatus = ddlMarital.SelectedValue;
                    user.Gender = ddlGender.SelectedValue;
                    user.Email = tbWEmail.Text.Trim();
                    user.Phone = tbWPhone.Text.Trim();
                    user.UserPositionId = Convert.ToInt32(RadComboBoxUserPosition.SelectedValue);
                    if (!string.IsNullOrEmpty(RadComboBoxSuper.SelectedValue))
                        user.Supervisor = Convert.ToInt32(RadComboBoxSuper.SelectedValue);

                    user.EmployeeNumber = tbEmpNo.Text;

                    user.EContactName = tbEName.Text;
                    user.ERelation = tbERelation.Text;
                    user.EPhone = tbEPhone.Text;
                    user.EAddress = tbEAddress.Text;

                    user.Address1 = tbPAddess1.Text;
                    user.Address2 = tbPAddess2.Text;
                    user.City = tbPCity.Text;
                    user.Province = tbPProvince.Text;
                    user.PostalCode = tbPPostal.Text;
                    user.HomePhone = tbPHomePhone.Text;
                    user.CellPhone = tbPCell.Text;
                    user.PersonalEmail = tbPEmail.Text;

                    user.IsActive = true;

                    user.CreatedId = CurrentUserId;
                    user.CreatedDate = DateTime.Now;

                    if (AsyncUploadPicture.UploadedFiles.Count > 0)
                    {
                        UploadedFile file = AsyncUploadPicture.UploadedFiles[0];
                        byte[] fileData = new byte[file.InputStream.Length];
                        file.InputStream.Read(fileData, 0, (int)file.InputStream.Length);
                        user.Picture = fileData;
                    }

                    int newUserId = cUser.Add(user);
                    if (newUserId > 0)
                    {
                        var tempSupervisor = user.Supervisor ?? CConstValue.UserSystemId;

                        var cApproval = new CApproval();
                        var approval = cApproval.GetAppType(tempSupervisor);
                        foreach (var a in approval)
                        {
                            var type = new Approval();

                            type.ApproveType = a.ApproveType;
                            type.UserId = newUserId;
                            type.Supervisor = a.UserId;
                            type.IsActive = true;
                            type.CreatedId = CurrentUserId;
                            type.CreatedDate = DateTime.Now;

                            cApproval.Add(type);
                        }

                        // vacation Schema
                        var cVacationSchema = new CVacationSchema();
                        var isKgic = new CSite().Get(new CSiteLocation().Get(user.SiteLocationId).SiteId).Abbreviation.ToLower() == "kgic";

                        for (var i = 1; i <= 6; i++)
                        {
                            var vacationType = 0;
                            var date = i % 2 == 0 ? DateTime.Today.AddYears(1) : DateTime.Today;
                            switch (i)
                            {
                                case 1:
                                case 2:
                                    vacationType = (int)CConstValue.VacationType.PaidVacationDay;
                                    break;
                                case 3:
                                case 4:
                                    vacationType = (int)CConstValue.VacationType.SickDay;
                                    break;
                                case 5:
                                case 6:
                                    if (isKgic == false)
                                        continue;
                                    vacationType = (int)CConstValue.VacationType.EntitlementDay;
                                    break;
                            }

                            cVacationSchema.Add(new VacationSchema
                            {
                                Date = date,
                                UserId = newUserId,
                                VacationType = vacationType,
                                TotalDays = 0,
                                CreatedId = CurrentUserId,
                                CreatedDate = DateTime.Now
                            });
                        }

                        // user permission
                        new CUserPermission().SetBasicPermission(user, CurrentUserId);

                        RadGridUser.Rebind();
                        ShowMessage(new CUser().GetUserName(user) + " has been successfully created");
                    }
                    else
                    {
                        ShowMessage("Failed To Add Staff Info");
                    }
                }
            }
            else if (e.Item.Text == "Permission")
            {
                if (RadGridUser.SelectedValue != null)
                    RunClientScript("ShowPermission(" + RadGridUser.SelectedValue + ");");
            }
            else if (e.Item.Text == "User Information")
            {
                if (RadGridUser.SelectedValue != null)
                    RunClientScript("ShowUserInformation(" + RadGridUser.SelectedValue + ");");
            }
        }

        protected void GetStaffInfo()
        {
            if (RadGridUser.SelectedValue != null)
            {
                var cUser = new CUser();
                var user = cUser.Get(Convert.ToInt32(RadGridUser.SelectedValue));

                if (user.UserId > 0)
                {
                    var cSiteLocation = new CSiteLocation();
                    var siteLocation = cSiteLocation.Get(user.SiteLocationId);

                    LoadSite(siteLocation.SiteId);
                    LoadSiteLocation(siteLocation.SiteId);
                    LoadUserGroup(siteLocation.SiteId);
                    LoadSupervisor();

                    RadComboBoxSite.SelectedValue = siteLocation.SiteId.ToString();
                    RadComboBoxSiteLocation.SelectedValue = user.SiteLocationId.ToString();
                    var cUserPosition = new CUserPosition();
                    var userPosition = cUserPosition.Get(user.UserPositionId);
                    if (userPosition != null)
                    {
                        RadComboBoxUserGroup.SelectedValue = userPosition.UserGroupId.ToString();
                        LoadUserPosition(userPosition.UserGroupId);
                        RadComboBoxUserPosition.SelectedValue = user.UserPositionId.ToString();
                    }
                    RadComboBoxSuper.SelectedValue = user.Supervisor.ToString();

                    tbUserID.Enabled = false;
                    tbUserID.Text = user.LoginId;
                    //tbPassWord.Text = user.Password;
                    tbFName.Text = user.FirstName;
                    tbMName.Text = user.MidName;
                    tbLName.Text = user.LastName;
                    tbSIN.Text = Convert.ToString(user.SINNo);
                    tbDOB.SelectedDate = user.DOB;

                    DateTime Today = DateTime.Now;
                    DateTime Dob = Convert.ToDateTime(user.DOB);

                    TimeSpan ts = Today - Dob;
                    DateTime Age = DateTime.MinValue + ts;

                    int Years = Age.Year - 1;

                    tbAge.Text = Years.ToString();

                    ddlMarital.SelectedValue = user.MaritalStatus;
                    ddlGender.SelectedValue = user.Gender;
                    tbWEmail.Text = user.Email;
                    tbWPhone.Text = user.Phone;
                    tbEmpNo.Text = user.EmployeeNumber;
                    cbActive.Checked = user.IsActive;

                    tbPAddess1.Text = user.Address1;
                    tbPAddess2.Text = user.Address2;
                    tbPCity.Text = user.City;
                    tbPProvince.Text = user.Province;
                    tbPPostal.Text = user.PostalCode;
                    tbPHomePhone.Text = user.HomePhone;
                    tbPCell.Text = user.CellPhone;
                    tbPEmail.Text = user.PersonalEmail;

                    tbEName.Text = user.EContactName;
                    tbERelation.Text = user.ERelation;
                    tbEPhone.Text = user.EPhone;
                    tbEAddress.Text = user.EAddress;

                    // pic
                    if (user.Picture != null)
                    {
                        RadBinaryImagePicture.DataValue = user.Picture.ToArray();
                        RadBinaryImagePicture.Visible = true;
                    }
                    else
                    {
                        RadBinaryImagePicture.DataValue = null;
                        RadBinaryImagePicture.Visible = false;
                    }

                    if (RadToolBarUser.FindItemByText("New") != null) RadToolBarUser.FindItemByText("New").Enabled = true;
                    if (RadToolBarUser.FindItemByText("Save") != null) RadToolBarUser.FindItemByText("Save").Text = @"Update";
                }
            }
        }

        protected void ResetForm()
        {
            LoadSite(CurrentSiteId);
            LoadSiteLocation(CurrentSiteId);
            LoadUserGroup(CurrentSiteId);
            LoadUserPosition(0);
            LoadSupervisor();

            tbUserID.Text = "";
            tbPassWord.Text = "";
            tbFName.Text = "";
            tbMName.Text = "";
            tbLName.Text = "";
            tbSIN.Text = "";
            tbDOB.SelectedDate = null;
            tbAge.Text = "";
            ddlMarital.SelectedValue = "";
            ddlGender.SelectedValue = "";
            tbWEmail.Text = "";
            tbWPhone.Text = "";

            RadComboBoxSite.SelectedValue = CurrentSiteId.ToString();
            RadComboBoxSiteLocation.SelectedValue = CurrentSiteLocationId.ToString();
            RadComboBoxUserGroup.SelectedValue = "";
            RadComboBoxUserPosition.SelectedValue = "";
            RadComboBoxSuper.SelectedValue = "";
            tbEmpNo.Text = "";
            cbActive.Checked = true;

            tbPAddess1.Text = "";
            tbPAddess2.Text = "";
            tbPCity.Text = "";
            tbPProvince.Text = "";
            tbPPostal.Text = "";
            tbPHomePhone.Text = "";
            tbPCell.Text = "";
            tbPEmail.Text = "";

            tbEName.Text = "";
            tbERelation.Text = "";
            tbEPhone.Text = "";
            tbEAddress.Text = "";

            tbUserID.Enabled = true;
            RadComboBoxSite.Enabled = true;
            RadComboBoxSiteLocation.Enabled = true;
            tbEmpNo.Enabled = true;

            RadBinaryImagePicture.DataValue = null;
            RadBinaryImagePicture.Visible = false;

            if (RadToolBarUser.FindItemByText("New") != null) RadToolBarUser.FindItemByText("New").Enabled = false;
            if (RadToolBarUser.FindItemByText("Update") != null) RadToolBarUser.FindItemByText("Update").Text = @"Save";
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
        }

        protected void LoadSite(int siteId)
        {
            var global = new CGlobal();
            RadComboBoxSite.Items.Clear();
            RadComboBoxSite.Text = string.Empty;
            RadComboBoxSite.DataSource = global.GetSiteId(siteId);
            RadComboBoxSite.DataTextField = "Name";
            RadComboBoxSite.DataValueField = "Value";
            RadComboBoxSite.DataBind();
        }

        protected void RadComboBoxSite_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSiteLocation(Convert.ToInt32(RadComboBoxSite.SelectedValue));
            RadComboBoxSiteLocation.OpenDropDownOnLoad = true;
        }

        protected void LoadUserGroup(int siteId)
        {
            var cUserGroup = new CUserGroup();
            RadComboBoxUserGroup.Text = string.Empty;
            RadComboBoxUserGroup.DataSource = cUserGroup.GetUserGroupList(siteId);
            RadComboBoxUserGroup.DataTextField = "Name";
            RadComboBoxUserGroup.DataValueField = "Value";
            RadComboBoxUserGroup.DataBind();
        }

        protected void LoadUserPosition(int userGroupId)
        {
            var cUserPosition = new CUserPosition();
            RadComboBoxUserPosition.Text = string.Empty;
            RadComboBoxUserPosition.DataSource = cUserPosition.GetUserPositionList(userGroupId);
            RadComboBoxUserPosition.DataTextField = "Name";
            RadComboBoxUserPosition.DataValueField = "Value";
            RadComboBoxUserPosition.DataBind();
        }

        protected void LoadSupervisor()
        {
            RadComboBoxSuper.Items.Clear();
            RadComboBoxSuper.Text = string.Empty;
            RadComboBoxSuper.DataSource = new CUser().GetSupervisorList();
            RadComboBoxSuper.DataTextField = "Name";
            RadComboBoxSuper.DataValueField = "Value";
            RadComboBoxSuper.DataBind();
            RadComboBoxSuper.Items.Insert(0, new RadComboBoxItem("N/A", null));
        }


        protected void Age(object o, SelectedDateChangedEventArgs e)
        {
            try
            {
                DateTime Today = DateTime.Now;
                DateTime Dob = Convert.ToDateTime(tbDOB.SelectedDate);

                TimeSpan ts = Today - Dob;
                DateTime Age = DateTime.MinValue + ts;

                int Years = Age.Year - 1;

                tbAge.Text = Years.ToString();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

        }


        protected void tbUserID_TextChanged(object sender, EventArgs e)
        {
            var user = new CUser();
            var app = user.CheckId(tbUserID.Text);

            if (app != string.Empty)
            {
                ShowMessage(tbUserID.Text + " is duplicated.");
                tbUserID.Text = "";
            }
        }

        protected void tbSIN_TextChanged(object sender, EventArgs e)
        {
            var user = new CUser();
            var app = user.CheckSIN(Convert.ToInt32(tbSIN.Text));

            if (app != string.Empty && app != "0")
            {
                ShowMessage(tbSIN.Text + " is duplicated.");
                tbSIN.Text = "";
            }
        }


        protected void RadGridUser_PreRender(object sender, EventArgs e)
        {
            if (Request["me"] != null)
            {
                // select me
                LinqDataSourceUser.WhereParameters.Clear();
                LinqDataSourceUser.WhereParameters.Add("UserId", DbType.Int32, CurrentUserId.ToString());
                LinqDataSourceUser.Where = "UserId == @UserId";

                RadGridUser.MasterTableView.Rebind();

                foreach (GridDataItem item in RadGridUser.Items)
                {
                    if (item.GetDataKeyValue("UserId").ToString() == CurrentUserId.ToString())
                    {
                        item.Selected = true;
                        GetStaffInfo();
                        RadToolBarUser.FindItemByText("New").Enabled = false;
                        RadToolBarUser.FindItemByText("Permission").Enabled = false;
                        RadToolBarUser.FindItemByText("User Information").Enabled = false;
                        RadToolBarUser.FindItemByText("Update").Enabled = true;
                        break;
                    }
                }
            }
        }

        protected void RadGridUser_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridUser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetStaffInfo();
        }

        protected void RadComboBoxUserGroup_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadUserPosition(e.Value == string.Empty ? 0 : Convert.ToInt32(e.Value));
            RadComboBoxUserPosition.Focus();
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBarUser.Items)
                {
                    if (toolbarItem.Text == "Update")
                    {
                        if (Request["me"] != null)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                    else
                        toolbarItem.Enabled = false;
                }
            }
        }
    }
}