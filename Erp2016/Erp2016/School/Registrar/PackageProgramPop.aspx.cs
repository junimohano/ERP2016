using System;
using System.Collections.Generic;
using System.Text;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace School.Registrar
{
    public partial class PackageProgramPop : PageBase
    {
        public PackageProgramPop() : base((int)CConstValue.Menu.PackageProgram)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.PackageProgram);

                hfId.Value = Request["id"];
                hfType.Value = Request["type"];

                ResetForm();
                LoadFaculty();

                // new
                if (hfType.Value == "0")
                {
                    GetSiteLocation(false);
                    mainToolBar.FindItemByText("Request").Enabled = false;
                }
                // modify
                else
                {
                    GetSiteLocation(true);

                    var cP = new CPackageProgram().GetViewPackageProgram(Convert.ToInt32(hfId.Value));
                    RadTextBoxPackageProgramName.Text = cP.PackageProgramName;

                    var cProgram = new CProgram();
                    var program = cProgram.Get((int)cP.ProgramId);
                    if (program != null)
                    {
                        if (program.ProgramGroupId != null)
                        {
                            var programGroup = new CProgramGroup().Get(Convert.ToInt32(program.ProgramGroupId));
                            if (programGroup != null)
                            {
                                RadComboBoxFaculty.SelectedValue = programGroup.FacultyId.ToString();
                                LoadProgramGroup(RadComboBoxFaculty.SelectedValue);
                            }
                            RadComboBoxProgramGroup.SelectedValue = program.ProgramGroupId.ToString();
                            LoadProgram(RadComboBoxProgramGroup.SelectedValue);
                        }
                        RadComboBoxProgram.SelectedValue = program.ProgramId.ToString();
                    }

                    RadDatePickerStartDate.SelectedDate = cP.StartDate;
                    RadDatePickerEndDate.SelectedDate = cP.EndDate;
                    RadTextBoxDescription.Text = cP.Description;
                    //btnToggleActive.Checked = (bool)cP.IsActive;
                    //btnToggleActive.Visible = true;

                    // UP LOAD
                    FileDownloadList1.GetFileDownload(Convert.ToInt32(hfId.Value));
                }
            }

            RadComboBoxFaculty.OpenDropDownOnLoad = false;
            RadComboBoxProgramGroup.OpenDropDownOnLoad = false;
            RadComboBoxProgram.OpenDropDownOnLoad = false;
        }

        private void ResetForm()
        {
            LoadFaculty();
            LoadProgramGroup(null);
            LoadProgram(null);
        }
        
        private void GetSiteLocation(bool isModify)
        {
            RadComboBoxSiteLocation.Items.Clear();
            List<SiteLocation> siteLocationList = new List<SiteLocation>();

            if (isModify)
            {
                var cPackageProgramSiteLocation = new CPackageProgramSiteLocation();
                var packageProgramSiteLocation = cPackageProgramSiteLocation.GetPackageProgramSiteLocationList(Convert.ToInt32(hfId.Value));
                if (packageProgramSiteLocation.Count > 0)
                {
                    var siteLocation = new CSiteLocation().Get(packageProgramSiteLocation[0].SiteLocationId);
                    siteLocationList = new CSiteLocation().GetSiteLocationBySiteId(siteLocation.SiteId);

                    RadTextBoxSite.Text = (new CSite()).Get(siteLocation.SiteId).Abbreviation;
                }

                foreach (var siteLocation in siteLocationList)
                {
                    RadComboBoxSiteLocation.Items.Add(new RadComboBoxItem(siteLocation.Name, siteLocation.SiteLocationId.ToString()));
                }

                foreach (var packageProgramSiteLo in packageProgramSiteLocation)
                {
                    foreach (RadComboBoxItem siteLocation in RadComboBoxSiteLocation.Items)
                    {
                        if (packageProgramSiteLo.SiteLocationId == Convert.ToInt32(siteLocation.Value))
                        {
                            siteLocation.Checked = true;
                        }
                    }
                }
            }
            else
            {
                var cSiteLocation = new CSiteLocation();
                siteLocationList = cSiteLocation.GetSiteLocationBySiteId(CurrentSiteId);
                foreach (var siteLocation in siteLocationList)
                {
                    RadComboBoxSiteLocation.Items.Add(new RadComboBoxItem(siteLocation.Name, siteLocation.SiteLocationId.ToString()));
                }

                RadTextBoxSite.Text = (new CSite()).Get(CurrentSiteId).Abbreviation;
            }

            RadComboBoxSiteLocation_OnSelectedIndexChanged(null, null);
        }

        protected void mainToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "TempSave":
                case "Request":
                    if (IsValid)
                    {
                        var cProgram = new CPackageProgram();
                        Erp2016.Lib.PackageProgram packageProgram1 = null;
                        // new
                        if (hfType.Value == "0")
                        {
                            packageProgram1 = new Erp2016.Lib.PackageProgram();
                            packageProgram1.CreatedId = CurrentUserId;
                            packageProgram1.CreatedDate = DateTime.Now;
                        }
                        // modify
                        else
                            packageProgram1 = cProgram.GetPackageProgram(Convert.ToInt32(hfId.Value));

                        packageProgram1.ProgramId = Convert.ToInt32(RadComboBoxProgram.SelectedValue);
                        packageProgram1.PackageProgramName = RadTextBoxPackageProgramName.Text;
                        packageProgram1.Description = RadTextBoxDescription.Text;
                        packageProgram1.StartDate = RadDatePickerStartDate.SelectedDate;
                        packageProgram1.EndDate = RadDatePickerEndDate.SelectedDate;

                        int packageProgramId;

                        // new
                        if (hfType.Value == "0")
                        {
                            packageProgram1.IsActive = false;
                            packageProgramId = cProgram.Add(packageProgram1);
                        }
                        // modify
                        else
                        {
                            packageProgram1.UpdatedId = CurrentUserId;
                            packageProgram1.UpdatedDate = DateTime.Now;

                            //packageProgram.IsActive = btnToggleActive.Checked;
                            cProgram.Update(packageProgram1);

                            packageProgramId = packageProgram1.PackageProgramId;
                        }

                        var cPackageProgramSiteLocation = new CPackageProgramSiteLocation();
                        cPackageProgramSiteLocation.DelPackageProgramSiteLocation(packageProgramId);

                        foreach (var siteLocation in RadComboBoxSiteLocation.CheckedItems)
                        {
                            var packageProgramSiteLocation = new PackageProgramSiteLocation()
                            {
                                CreatedId = CurrentUserId,
                                PackageProgramId = packageProgramId,
                                SiteLocationId = Convert.ToInt32(siteLocation.Value),
                                CreatedDate = DateTime.Now
                            };
                            cPackageProgramSiteLocation.Add(packageProgramSiteLocation);
                        }

                        FileDownloadList1.SaveFile(packageProgramId);

                        if (e.Item.Text == "TempSave")
                        {
                            RunClientScript("Close();");
                        }
                        else
                        {
                            var cApprovalHistory = new CApprovalHistory();
                            cApprovalHistory.DelApprovalHistory((int)CConstValue.Approval.Package, packageProgramId);

                            // approve request 
                            var approval = new CApproval().ApproveRequstCreate((int)CConstValue.Approval.Package, CurrentUserId, packageProgramId);
                            if (approval > 0)
                            {
                                var cP = new CPackageProgram();
                                var packageProgram = cP.GetPackageProgram(packageProgramId);
                                packageProgram.ApprovalStatus = approval;
                                packageProgram.ApprovalId = CurrentUserId;
                                packageProgram.ApprovalDate = DateTime.Now;
                                //packageProgram.ApprovalMemo = "";
                                cP.Update(packageProgram);

                                new CMail().SendMail(CConstValue.Approval.Package, CConstValue.MailStatus.ToApproveUser, packageProgram.PackageProgramId, string.Empty, CurrentUserId);

                                RunClientScript("Close();");
                            }
                            else
                                ShowMessage("error requesting");
                        }
                    }
                    break;

                case "Close":
                    RunClientScript("Close();");
                    break;
            }
        }

        protected void RadComboBoxSiteLocation_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var sb = new StringBuilder();
            var collection = RadComboBoxSiteLocation.CheckedItems;

            if (collection.Count != 0)
            {
                sb.Append("<h4>Checked SiteLocation List</h4>");
                foreach (var item in collection)
                    sb.Append("<label>" + item.Text + "</label>");

                itemsClientSide.Text = sb.ToString();
            }
            else
            {
                itemsClientSide.Text = string.Empty;
            }
        }

        protected void RadComboBoxFaculty_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadProgramGroup(RadComboBoxFaculty.SelectedValue);

            RadComboBoxProgramGroup.OpenDropDownOnLoad = true;
        }

        protected void RadComboBoxProgramGroup_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadProgram(RadComboBoxProgramGroup.SelectedValue);

            RadComboBoxProgram.OpenDropDownOnLoad = true;
        }

        protected void LoadFaculty()
        {
            RadComboBoxFaculty.Items.Clear();
            RadComboBoxFaculty.Text = string.Empty;
            RadComboBoxFaculty.DataSource = new CFaculty().GetFacultyList(CurrentSiteId);
            RadComboBoxFaculty.DataTextField = "Name";
            RadComboBoxFaculty.DataValueField = "Value";
            RadComboBoxFaculty.DataBind();
            RadComboBoxFaculty.Items.Insert(0, new RadComboBoxItem("N/A", null));
        }

        protected void LoadProgramGroup(string facultyId)
        {
            RadComboBoxProgramGroup.Items.Clear();
            RadComboBoxProgramGroup.Text = string.Empty;
            if (!string.IsNullOrEmpty(facultyId))
                RadComboBoxProgramGroup.DataSource = new CProgramGroup().GetProgramGroupList(CurrentSiteId, Convert.ToInt32(facultyId));
            else
                RadComboBoxProgramGroup.DataSource = new CProgramGroup().GetProgramGroupList(CurrentSiteId);

            RadComboBoxProgramGroup.DataTextField = "Name";
            RadComboBoxProgramGroup.DataValueField = "Value";
            RadComboBoxProgramGroup.DataBind();
            RadComboBoxProgramGroup.Items.Insert(0, new RadComboBoxItem("N/A", null));
        }

        protected void LoadProgram(string programGroupId)
        {
            RadComboBoxProgram.Items.Clear();
            RadComboBoxProgram.Text = string.Empty;
            if (!string.IsNullOrEmpty(programGroupId))
                RadComboBoxProgram.DataSource = new CProgram().GetProgramList(CurrentSiteLocationId, Convert.ToInt32(programGroupId));
            else
                RadComboBoxProgram.DataSource = new CProgram().GetProgramList(CurrentSiteLocationId);

            RadComboBoxProgram.DataTextField = "Name";
            RadComboBoxProgram.DataValueField = "Value";
            RadComboBoxProgram.DataBind();
        }
    }
}