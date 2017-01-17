using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.AcademicRegistrar
{
    public partial class Faculty : PageBase
    {
        public Faculty() : base((int)CConstValue.Menu.Faculty)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResetForm();
            }

            LinqDataSourceFaculty.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteList)
                LinqDataSourceFaculty.WhereParameters.Add(model.SiteIdName, DbType.Int32, model.SiteId.ToString());
            LinqDataSourceFaculty.Where = UserPermissionModel.SearchWhereSiteSb.ToString();
        }

        protected void FacultyButtonClicked(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == @"New")
            {
                RadGrid1.SelectedIndexes.Clear();
                ResetForm();
            }
            else if (e.Item.Text == @"Update")
            {
                if (RadGrid1.SelectedValue != null)
                {
                    var cAgc = new CFaculty();
                    var agc = cAgc.Get(Convert.ToInt32(RadGrid1.SelectedValue));
                    agc.Name = tbFaculty.Text;
                    agc.Description = tbDescription.Text;
                    agc.IsActive = RadButtonActive.Checked;
                    agc.UpdatedId = CurrentUserId;
                    agc.UpdatedDate = DateTime.Now.Date;

                    if (cAgc.Update(agc))
                    {
                        RadGrid1.Rebind();
                        ShowMessage("'" + agc.Name + "' is updated.");
                    }
                    else
                        ShowMessage("Failed To Update.");
                }
            }
            else if (e.Item.Text == @"Save")
            {
                var cAgc = new CFaculty();
                var agc = new Erp2016.Lib.Faculty();
                agc.SiteId = CurrentSiteId;
                agc.Name = tbFaculty.Text;
                agc.Description = tbDescription.Text;
                agc.IsActive = RadButtonActive.Checked;
                agc.CreatedId = CurrentUserId;
                agc.CreatedDate = DateTime.Now;

                if (cAgc.Add(agc) > 0)
                {
                    RadGrid1.Rebind();
                    ShowMessage("'" + agc.Name + "' is added.");
                }
                else
                    ShowMessage("failed to add.");
            }
        }

        protected void GetFacultyInfo()
        {
            ResetForm();
            if (RadGrid1.SelectedValue != null)
            {
                var cAgc = new CFaculty();
                var agc = cAgc.Get(Convert.ToInt32(RadGrid1.SelectedValue));

                if (agc.FacultyId > 0)
                {
                    tbFaculty.Text = agc.Name;
                    tbDescription.Text = agc.Description;
                    RadButtonActive.Checked = agc.IsActive;

                    if (RadToolBarFaculty.FindItemByText("New") != null) RadToolBarFaculty.FindItemByText("New").Enabled = true;
                    if (RadToolBarFaculty.FindItemByText("Save") != null) RadToolBarFaculty.FindItemByText("Save").Text = @"Update";
                }
            }
        }

        protected void ResetForm()
        {
            tbFaculty.Text = "";
            tbDescription.Text = "";
            RadButtonActive.Checked = true;

            if (RadToolBarFaculty.FindItemByText("New") != null) RadToolBarFaculty.FindItemByText("New").Enabled = false;
            if (RadToolBarFaculty.FindItemByText("Update") != null) RadToolBarFaculty.FindItemByText("Update").Text = @"Save";
        }

        protected void RadGrid1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetFacultyInfo();
        }

        protected void RadGrid1_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBarFaculty.Items)
                {
                    toolbarItem.Enabled = false;
                }
            }
        }
    }
}