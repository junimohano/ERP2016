using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.AcademicRegistrar
{
    public partial class Program : PageBase
    {
        public Program() : base((int)CConstValue.Menu.Program)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var global = new CGlobal();
                //ddlHours.DataSource = global.GetDictionary(150);
                //ddlHours.DataTextField = "Name";
                //ddlHours.DataValueField = "Value";
                //ddlHours.DataBind();

                ddlProgramType.DataSource = global.GetDictionary(1285);
                ddlProgramType.DataTextField = "Name";
                ddlProgramType.DataValueField = "Value";
                ddlProgramType.DataBind();

                ddlComType.DataSource = global.GetDictionary(1286);
                ddlComType.DataTextField = "Name";
                ddlComType.DataValueField = "Value";
                ddlComType.DataBind();
                ResetForm();
            }

            RefreshProgramList();
            RefreshTuition();
            RadComboBoxProgramGroup.OpenDropDownOnLoad = false;
        }

        private void GetSiteLocation()
        {
            RadComboBoxSiteLocation.Items.Clear();
            List<SiteLocation> siteLocationList = new List<SiteLocation>();
            if (Grid.SelectedValue != null)
            {
                var cProgramSiteLocation = new CProgramSiteLocation();
                var programSiteLocation = cProgramSiteLocation.GetProgramSiteLocationList(Convert.ToInt32(Grid.SelectedValue));
                if (programSiteLocation.Count > 0)
                {
                    var siteLocation = new CSiteLocation().Get(programSiteLocation[0].SiteLocationId);
                    siteLocationList = new CSiteLocation().GetSiteLocationBySiteId(siteLocation.SiteId);

                    RadTextBoxSite.Text = (new CSite()).Get(siteLocation.SiteId).Abbreviation;
                }

                foreach (var siteLocation in siteLocationList)
                {
                    RadComboBoxSiteLocation.Items.Add(new RadComboBoxItem(siteLocation.Name, siteLocation.SiteLocationId.ToString()));
                }

                foreach (var agencySiteLo in programSiteLocation)
                {
                    foreach (RadComboBoxItem siteLocation in RadComboBoxSiteLocation.Items)
                    {
                        if (agencySiteLo.SiteLocationId == Convert.ToInt32(siteLocation.Value))
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

        private void RefreshProgramList()
        {
            LinqDataSourceProgram.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteList)
                LinqDataSourceProgram.WhereParameters.Add(model.SiteIdName, DbType.Int32, model.SiteId.ToString());
            LinqDataSourceProgram.Where = UserPermissionModel.SearchWhereSiteSb.ToString();
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
        
        protected void ToolbarClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "New")
            {
                Grid.SelectedIndexes.Clear();
                ResetForm();
            }
            else if (e.Item.Text == "Save")
            {
                var cProg = new CProgram();
                var prog = new Erp2016.Lib.Program();

                if (string.IsNullOrEmpty(RadComboBoxProgramGroup.SelectedValue) == false)
                    prog.ProgramGroupId = Convert.ToInt32(RadComboBoxProgramGroup.SelectedValue);

                prog.ProgramFullName = tbProgramFullName.Text;
                prog.ProgramWebName = tbProgramWebName.Text;
                prog.ProgramShortName = tbProgramShortName.Text;
                prog.ProgramDescription = tbDescript.Text;

                if (string.IsNullOrEmpty(ddlProgramType.SelectedValue) == false)
                    prog.ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
                else
                    prog.ProgramType = null;

                if (string.IsNullOrEmpty(ddlComType.SelectedValue) == false)
                    prog.UisType = Convert.ToInt32(ddlComType.SelectedValue);
                else
                    prog.UisType = null;

                prog.EarningCredit = Convert.ToDecimal(tbEarningCredit.Value);

                //if (tbProgramWeek.Text != "")
                //    prog.TotalWeeks = Convert.ToInt32(tbProgramWeek.Text);
                //else
                //    prog.TotalWeeks = null;

                //if (tbProgramSemester.Text != "")
                //    prog.TotalSemester = Convert.ToInt32(tbProgramSemester.Text);
                //else
                //    prog.TotalSemester = null;

                //if (tbProgramMonth.Text != "")
                //    prog.TotalMonth = Convert.ToInt32(tbProgramMonth.Text);
                //else
                //    prog.TotalMonth = null;

                //if (tbProgramHoursDay.Text != "")
                //    prog.HoursOfDay = Convert.ToInt32(tbProgramHoursDay.Text);
                //else
                //    prog.HoursOfDay = null;

                //prog.HoursOfWeek = Convert.ToInt32(ddlHours.SelectedValue);
                prog.EstimatedStartDate = tbProgramStartDate.SelectedDate;
                prog.AdmissionRequirement = tbProgramAdmission.Text;
                prog.DiplomaCertificationRequirement = tbProgramDiploma.Text;

                if (string.IsNullOrEmpty(tbPracticum.Text) == false)
                    prog.PracticumWeeks = Convert.ToInt32(tbPracticum.Text);
                else
                    prog.PracticumWeeks = null;

                if (string.IsNullOrEmpty(tbIntership.Text) == false)
                    prog.IntershipWeeks = Convert.ToInt32(tbIntership.Text);
                else
                    prog.IntershipWeeks = null;

                prog.IsActive = RadButtonActive.Checked;
                prog.ActiveDate = tbProgramActiveDate.SelectedDate;
                prog.InActiveDate = tbProgramInActiveDate.SelectedDate;
                prog.CreatedId = CurrentUserId;
                prog.CreatedDate = DateTime.Now;
                prog.UpdatedId = CurrentUserId;
                prog.UpdatedDate = DateTime.Now;

                int programId = cProg.Add(prog);
                if (programId > 0)
                {
                    var cProgramSiteLocation = new CProgramSiteLocation();
                    cProgramSiteLocation.DelProgramSiteLocationList(programId);

                    foreach (var siteLocation in RadComboBoxSiteLocation.CheckedItems)
                    {
                        var programSiteLocation = new ProgramSiteLocation()
                        {
                            CreatedId = CurrentUserId,
                            ProgramId = programId,
                            SiteLocationId = Convert.ToInt32(siteLocation.Value),
                            CreatedDate = DateTime.Now
                        };
                        cProgramSiteLocation.Add(programSiteLocation);
                    }

                    // other info fee                   
                    var cProgramOtherFeeInfo = new CProgramOtherFeeInfo();
                    var programOtherFeeInfo = new ProgramOtherFeeInfo();

                    for (var i = 1; i < 19; i++)
                    {
                        var fee = (RadNumericTextBox)RadPaneProgram.FindControl("tbFee" + i);

                        programOtherFeeInfo.ProgramId = prog.ProgramId;

                        decimal feeValue = (decimal)(fee.Value ?? 0);

                        switch (i)
                        {
                            case 1:
                                programOtherFeeInfo.RegFee = feeValue;
                                //oid.InvoiceCoaItemId = 7;
                                break;
                            case 2:
                                programOtherFeeInfo.JRegFee = feeValue;
                                //oid.InvoiceCoaItemId = 7;
                                break;
                            case 3:
                                programOtherFeeInfo.AcademicFee = feeValue;
                                //oid.InvoiceCoaItemId = 11;
                                break;
                            case 4:
                                programOtherFeeInfo.MaterialFee = feeValue;
                                //oid.InvoiceCoaItemId = 11;
                                break;
                            case 5:
                                programOtherFeeInfo.TestFee = feeValue;
                                //oid.InvoiceCoaItemId = 31;
                                break;
                            case 6:
                                programOtherFeeInfo.PracticeFee = feeValue;
                                //oid.InvoiceCoaItemId = 27;
                                break;
                            case 7:
                                programOtherFeeInfo.ExamFee = feeValue;
                                //oid.InvoiceCoaItemId = 31;
                                break;
                            case 8:
                                programOtherFeeInfo.AdminFee = feeValue;
                                //oid.InvoiceCoaItemId = 13;
                                break;
                            case 9:
                                programOtherFeeInfo.InternFee = feeValue;
                                //oid.InvoiceCoaItemId = 27;
                                break;
                            case 10:
                                programOtherFeeInfo.LCFee = feeValue;
                                //oid.InvoiceCoaItemId = 27;
                                break;
                            case 11:
                                programOtherFeeInfo.SDFee = feeValue;
                                //oid.InvoiceCoaItemId = 27;
                                break;
                            case 12:
                                programOtherFeeInfo.UPFee = feeValue;
                                //oid.InvoiceCoaItemId = 27;
                                break;
                            case 13:
                                programOtherFeeInfo.ACFee = feeValue;
                                //oid.InvoiceCoaItemId = 13;
                                break;
                            case 14:
                                programOtherFeeInfo.CFee = feeValue;
                                //oid.InvoiceCoaItemId = 45;
                                break;
                            case 15:
                                programOtherFeeInfo.SupplyFee = feeValue;
                                //oid.InvoiceCoaItemId = 11;
                                break;
                            case 16:
                                programOtherFeeInfo.UniformFee = feeValue;
                                //oid.InvoiceCoaItemId = 11;
                                break;
                            case 17:
                                programOtherFeeInfo.UAGFee = feeValue;
                                //oid.InvoiceCoaItemId = 13;
                                break;
                            case 18:
                                programOtherFeeInfo.OtherFee = feeValue;
                                //oid.InvoiceCoaItemId = 0;
                                break;
                        }
                    }
                    programOtherFeeInfo.Comment = tbFeeComment.Text;
                    programOtherFeeInfo.CreatedId = CurrentUserId;
                    programOtherFeeInfo.CreatedDate = DateTime.Now;

                    cProgramOtherFeeInfo.Add(programOtherFeeInfo);

                    // other info
                    var cOtid = new CProgramOtherInfo();
                    var otid = new ProgramOtherInfo();

                    otid.ProgramId = prog.ProgramId;
                    otid.LocalCRC = tbLocalCRC.Text;
                    otid.DoctorNote = tbDoctorNote.Text;
                    otid.Noc = tbNoc.Text;
                    otid.Hrsdc = tbHrsdc.Text;
                    otid.Reference2 = tbReference2.Text;
                    otid.Reference3 = tbReference3.Text;
                    otid.Eng10 = tbEng10.Text;
                    otid.Math10 = tbMath10.Text;
                    otid.Sience11 = tbSience11.Text;
                    otid.Eng12 = tbEng12.Text;
                    otid.Bio12 = tbBio12.Text;
                    otid.SSience = tbSSience.Text;
                    otid.SMath = tbSMath.Text;
                    otid.SEng = tbSEng.Text;
                    otid.SLBio = tbSLBio.Text;
                    otid.SLChemi = tbSLChemi.Text;
                    otid.Immun = tbImmun.Text;
                    otid.HelpB = tbHelpB.Text;
                    otid.Comment = tbOthercomment.Text;
                    otid.CreatedId = CurrentUserId;
                    otid.CreatedDate = DateTime.Now;
                    otid.UpdatedId = CurrentUserId;
                    otid.UpdatedDate = otid.CreatedDate;

                    if (cOtid.Add(otid) > 0)
                    {
                        ShowMessage("New program is added");
                        Grid.Rebind();
                    }
                }
                else
                {
                    ShowMessage("Failed to add program, please try again");
                }
            }
            else if (e.Item.Text == "Update")
            {
                if (Grid.SelectedValue != null)
                {
                    var cProg = new CProgram();
                    var prog = cProg.Get(Convert.ToInt32(Grid.SelectedValue));

                    if (RadComboBoxProgramGroup.SelectedValue != null)
                        prog.ProgramGroupId = Convert.ToInt32(RadComboBoxProgramGroup.SelectedValue);

                    prog.ProgramFullName = tbProgramFullName.Text;
                    prog.ProgramWebName = tbProgramWebName.Text;
                    prog.ProgramShortName = tbProgramShortName.Text;
                    prog.ProgramDescription = tbDescript.Text;

                    if (ddlProgramType.SelectedValue != "")
                        prog.ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
                    else
                        prog.ProgramType = null;

                    if (ddlComType.SelectedValue != "")
                        prog.UisType = Convert.ToInt32(ddlComType.SelectedValue);
                    else
                        prog.UisType = null;

                    prog.EarningCredit = Convert.ToDecimal(tbEarningCredit.Value);

                    //if (tbProgramWeek.Text != "")
                    //    prog.TotalWeeks = Convert.ToInt32(tbProgramWeek.Text);
                    //else
                    //    prog.TotalWeeks = null;

                    //if (tbProgramSemester.Text != "")
                    //    prog.TotalSemester = Convert.ToInt32(tbProgramSemester.Text);
                    //else
                    //    prog.TotalSemester = null;

                    //if (tbProgramMonth.Text != "")
                    //    prog.TotalMonth = Convert.ToInt32(tbProgramMonth.Text);
                    //else
                    //    prog.TotalMonth = null;

                    //if (tbProgramHoursDay.Text != "")
                    //    prog.HoursOfDay = Convert.ToInt32(tbProgramHoursDay.Text);
                    //else
                    //    prog.HoursOfDay = null;

                    //prog.HoursOfWeek = Convert.ToInt32(ddlHours.SelectedValue);
                    prog.EstimatedStartDate = tbProgramStartDate.SelectedDate;
                    prog.AdmissionRequirement = tbProgramAdmission.Text;
                    prog.DiplomaCertificationRequirement = tbProgramDiploma.Text;

                    if (tbPracticum.Text != "")
                        prog.PracticumWeeks = Convert.ToInt32(tbPracticum.Text);
                    else
                        prog.PracticumWeeks = null;

                    if (tbIntership.Text != "")
                        prog.IntershipWeeks = Convert.ToInt32(tbIntership.Text);
                    else
                        prog.IntershipWeeks = null;

                    prog.IsActive = RadButtonActive.Checked;
                    prog.ActiveDate = tbProgramActiveDate.SelectedDate;
                    prog.InActiveDate = tbProgramInActiveDate.SelectedDate;
                    prog.UpdatedId = CurrentUserId;
                    prog.UpdatedDate = DateTime.Now;

                    if (cProg.Update(prog))
                    {
                        var cProgramSiteLocation = new CProgramSiteLocation();
                        cProgramSiteLocation.DelProgramSiteLocationList(prog.ProgramId);

                        foreach (var siteLocation in RadComboBoxSiteLocation.CheckedItems)
                        {
                            var programSiteLocation = new ProgramSiteLocation()
                            {
                                CreatedId = CurrentUserId,
                                ProgramId = prog.ProgramId,
                                SiteLocationId = Convert.ToInt32(siteLocation.Value),
                                CreatedDate = DateTime.Now
                            };
                            cProgramSiteLocation.Add(programSiteLocation);
                        }

                        var cProgramOtherFeeInfo = new CProgramOtherFeeInfo();
                        var programOtherFeeInfo = cProgramOtherFeeInfo.Get(Convert.ToInt32(Grid.SelectedValue));
                        if (programOtherFeeInfo != null)
                        {
                            for (var i = 1; i < 19; i++)
                            {
                                var fee = (RadNumericTextBox)RadPaneProgram.FindControl("tbFee" + i);
                                decimal feeValue = (decimal)(fee.Value ?? 0);

                                switch (i)
                                {
                                    case 1:
                                        programOtherFeeInfo.RegFee = feeValue;
                                        break;
                                    case 2:
                                        programOtherFeeInfo.JRegFee = feeValue;
                                        break;
                                    case 3:
                                        programOtherFeeInfo.AcademicFee = feeValue;
                                        break;
                                    case 4:
                                        programOtherFeeInfo.MaterialFee = feeValue;
                                        break;
                                    case 5:
                                        programOtherFeeInfo.TestFee = feeValue;
                                        break;
                                    case 6:
                                        programOtherFeeInfo.PracticeFee = feeValue;
                                        break;
                                    case 7:
                                        programOtherFeeInfo.ExamFee = feeValue;
                                        break;
                                    case 8:
                                        programOtherFeeInfo.AdminFee = feeValue;
                                        break;
                                    case 9:
                                        programOtherFeeInfo.InternFee = feeValue;
                                        break;
                                    case 10:
                                        programOtherFeeInfo.LCFee = feeValue;
                                        break;
                                    case 11:
                                        programOtherFeeInfo.SDFee = feeValue;
                                        break;
                                    case 12:
                                        programOtherFeeInfo.UPFee = feeValue;
                                        break;
                                    case 13:
                                        programOtherFeeInfo.ACFee = feeValue;
                                        break;
                                    case 14:
                                        programOtherFeeInfo.CFee = feeValue;
                                        break;
                                    case 15:
                                        programOtherFeeInfo.SupplyFee = feeValue;
                                        break;
                                    case 16:
                                        programOtherFeeInfo.UniformFee = feeValue;
                                        break;
                                    case 17:
                                        programOtherFeeInfo.UAGFee = feeValue;
                                        break;
                                    case 18:
                                        programOtherFeeInfo.OtherFee = feeValue;
                                        break;
                                }
                            }

                            programOtherFeeInfo.Comment = tbFeeComment.Text;
                            programOtherFeeInfo.UpdatedId = CurrentUserId;
                            programOtherFeeInfo.UpdatedDate = DateTime.Now;

                            cProgramOtherFeeInfo.Update(programOtherFeeInfo);
                        }

                        var cOtid = new CProgramOtherInfo();
                        var otid = cOtid.Get(Convert.ToInt32(Grid.SelectedValue));
                        if (otid != null)
                        {
                            otid.ProgramId = prog.ProgramId;
                            otid.LocalCRC = tbLocalCRC.Text;
                            otid.DoctorNote = tbDoctorNote.Text;
                            otid.Noc = tbNoc.Text;
                            otid.Hrsdc = tbHrsdc.Text;
                            otid.Reference2 = tbReference2.Text;
                            otid.Reference3 = tbReference3.Text;
                            otid.Eng10 = tbEng10.Text;
                            otid.Math10 = tbMath10.Text;
                            otid.Sience11 = tbSience11.Text;
                            otid.Eng12 = tbEng12.Text;
                            otid.Bio12 = tbBio12.Text;
                            otid.SSience = tbSSience.Text;
                            otid.SMath = tbSMath.Text;
                            otid.SEng = tbSEng.Text;
                            otid.SLBio = tbSLBio.Text;
                            otid.SLChemi = tbSLChemi.Text;
                            otid.Immun = tbImmun.Text;
                            otid.HelpB = tbHelpB.Text;
                            otid.Comment = tbOthercomment.Text;
                            otid.UpdatedId = CurrentUserId;
                            otid.UpdatedDate = DateTime.Now;

                            cOtid.Update(otid);
                        }

                        ShowMessage("Program updated");
                        RefreshProgramList();

                    }
                    else
                    {
                        ShowMessage("Failed to update program, please try again");
                    }
                }
            }
        }

        protected void SelectProgram(object sender, EventArgs e)
        {
            GetProgram();
            GetSiteLocation();
        }

        protected void GetProgram()
        {
            ResetForm();
            if (Grid.SelectedValue != null)
            {
                var pid = (new CProgram()).Get(Convert.ToInt32(Grid.SelectedValue));
                var otherId = (new CProgramOtherInfo()).Get(pid.ProgramId);

                if (pid.ProgramGroupId != null)
                {
                    var programGroup = new CProgramGroup().Get(Convert.ToInt32(pid.ProgramGroupId));
                    if (programGroup != null)
                    {
                        RadComboBoxFaculty.SelectedValue = programGroup.FacultyId.ToString();
                        LoadProgramGroup(RadComboBoxFaculty.SelectedValue);
                    }
                    RadComboBoxProgramGroup.SelectedValue = pid.ProgramGroupId.ToString();
                }

                tbProgramFullName.Text = pid.ProgramFullName;

                if (pid.ProgramWebName == null)
                    tbProgramWebName.Text = "N/A";
                else
                    tbProgramWebName.Text = pid.ProgramWebName;

                if (pid.ProgramShortName == null)
                    tbProgramShortName.Text = "N/A";
                else
                    tbProgramShortName.Text = pid.ProgramShortName;

                if (pid.ProgramDescription == null)
                    tbDescript.Text = "N/A";
                else
                    tbDescript.Text = pid.ProgramDescription;

                ddlProgramType.SelectedValue = pid.ProgramType.ToString();
                ddlComType.SelectedValue = pid.UisType.ToString();
                tbEarningCredit.Text = pid.EarningCredit.ToString();

                //tbProgramType

                //tbProgramWeek.Text = pid.TotalWeeks.ToString();
                //tbProgramSemester.Text = pid.TotalSemester.ToString();
                //tbProgramMonth.Text = pid.TotalMonth.ToString();
                //tbProgramHoursDay.Text = pid.HoursOfDay.ToString();
                //ddlHours.SelectedValue = pid.HoursOfWeek.ToString();

                tbProgramStartDate.SelectedDate = pid.EstimatedStartDate;

                if (pid.AdmissionRequirement == null)
                    tbProgramAdmission.Text = "N/A";
                else
                    tbProgramAdmission.Text = pid.AdmissionRequirement;

                if (pid.DiplomaCertificationRequirement == null)
                    tbProgramDiploma.Text = "N/A";
                else
                    tbProgramDiploma.Text = pid.DiplomaCertificationRequirement;

                tbPracticum.Text = Convert.ToString(pid.PracticumWeeks);
                tbIntership.Text = Convert.ToString(pid.IntershipWeeks);
                RadButtonActive.Checked = Convert.ToBoolean(pid.IsActive);
                tbProgramActiveDate.SelectedDate = pid.ActiveDate;
                tbProgramInActiveDate.SelectedDate = pid.InActiveDate;

                // OTHER FEE
                var programOtherFeeInfo = (new CProgramOtherFeeInfo()).Get(pid.ProgramId);

                if (programOtherFeeInfo != null)
                    tbFeeComment.Text = programOtherFeeInfo.Comment;

                for (var i = 1; i < 19; i++)
                {
                    var fee = (RadNumericTextBox)RadPaneProgram.FindControl("tbFee" + i);

                    if (programOtherFeeInfo == null)
                    {
                        fee.Value = 0;
                        continue;
                    }

                    switch (i)
                    {
                        case 1:
                            fee.Value = (double)(programOtherFeeInfo.RegFee ?? 0);
                            break;
                        case 2:
                            fee.Value = (double)(programOtherFeeInfo.JRegFee ?? 0);
                            break;
                        case 3:
                            fee.Value = (double)(programOtherFeeInfo.AcademicFee ?? 0);
                            break;
                        case 4:
                            fee.Value = (double)(programOtherFeeInfo.MaterialFee ?? 0);
                            break;
                        case 5:
                            fee.Value = (double)(programOtherFeeInfo.TestFee ?? 0);
                            break;
                        case 6:
                            fee.Value = (double)(programOtherFeeInfo.PracticeFee ?? 0);
                            break;
                        case 7:
                            fee.Value = (double)(programOtherFeeInfo.ExamFee ?? 0);
                            break;
                        case 8:
                            fee.Value = (double)(programOtherFeeInfo.AdminFee ?? 0);
                            break;
                        case 9:
                            fee.Value = (double)(programOtherFeeInfo.InternFee ?? 0);
                            break;
                        case 10:
                            fee.Value = (double)(programOtherFeeInfo.LCFee ?? 0);
                            break;
                        case 11:
                            fee.Value = (double)(programOtherFeeInfo.SDFee ?? 0);
                            break;
                        case 12:
                            fee.Value = (double)(programOtherFeeInfo.UPFee ?? 0);
                            break;
                        case 13:
                            fee.Value = (double)(programOtherFeeInfo.ACFee ?? 0);
                            break;
                        case 14:
                            fee.Value = (double)(programOtherFeeInfo.CFee ?? 0);
                            break;
                        case 15:
                            fee.Value = (double)(programOtherFeeInfo.SupplyFee ?? 0);
                            break;
                        case 16:
                            fee.Value = (double)(programOtherFeeInfo.UniformFee ?? 0);
                            break;
                        case 17:
                            fee.Value = (double)(programOtherFeeInfo.UAGFee ?? 0);
                            break;
                        case 18:
                            fee.Value = (double)(programOtherFeeInfo.OtherFee ?? 0);
                            break;
                    }
                }

                // OTHER INFO
                tbLocalCRC.Text = otherId.LocalCRC;
                tbDoctorNote.Text = otherId.DoctorNote;
                tbNoc.Text = otherId.Noc;
                tbHrsdc.Text = otherId.Hrsdc;
                tbReference2.Text = otherId.Reference2;
                tbReference3.Text = otherId.Reference3;
                tbEng10.Text = otherId.Eng10;
                tbMath10.Text = otherId.Math10;
                tbSience11.Text = otherId.Sience11;
                tbEng12.Text = otherId.Eng12;
                tbBio12.Text = otherId.Bio12;
                tbSSience.Text = otherId.SSience;
                tbSMath.Text = otherId.SMath;
                tbSEng.Text = otherId.SEng;
                tbSLBio.Text = otherId.SLBio;
                tbSLChemi.Text = otherId.SLChemi;
                tbImmun.Text = otherId.Immun;
                tbHelpB.Text = otherId.HelpB;
                tbOthercomment.Text = otherId.Comment;

                RadToolBarProgram.FindItemByText("New").Enabled = true;
                //RadToolBarProgram.FindItemByText("Delete").Enabled = true;
                if (RadToolBarProgram.FindItemByText("Save") != null) RadToolBarProgram.FindItemByText("Save").Text = "Update";
            }
        }

        protected void ResetForm()
        {
            LoadFaculty();
            LoadProgramGroup(null);

            tbProgramFullName.Text = "";
            tbProgramWebName.Text = "";
            tbProgramShortName.Text = "";
            tbDescript.Text = "";
            //tbProgramWeek.Text = "";
            //tbProgramSemester.Text = "";
            //tbProgramMonth.Text = "";
            //tbProgramHoursDay.Text = "";
            //ddlHours.SelectedValue = "";
            tbProgramStartDate.SelectedDate = null;
            tbProgramAdmission.Text = "";
            tbProgramDiploma.Text = "";
            tbPracticum.Text = "";
            tbIntership.Text = "";
            RadButtonActive.Checked = true;
            tbProgramActiveDate.SelectedDate = null;
            tbProgramInActiveDate.SelectedDate = null;

            // other fee info
            for (var i = 1; i < 19; i++)
            {
                var fee = (RadNumericTextBox)RadPaneProgram.FindControl("tbFee" + i);
                fee.Value = null;
            }
            tbFeeComment.Text = "";

            // other info
            tbLocalCRC.Text = "";
            tbDoctorNote.Text = "";
            tbNoc.Text = "";
            tbHrsdc.Text = "";
            tbReference2.Text = "";
            tbReference3.Text = "";
            tbEng10.Text = "";
            tbMath10.Text = "";
            tbSience11.Text = "";
            tbEng12.Text = "";
            tbBio12.Text = "";
            tbSSience.Text = "";
            tbSMath.Text = "";
            tbSEng.Text = "";
            tbSLBio.Text = "";
            tbSLChemi.Text = "";
            tbImmun.Text = "";
            tbHelpB.Text = "";
            tbOthercomment.Text = "";

            RadToolBarProgram.FindItemByText("New").Enabled = false;
            //RadToolBarProgram.FindItemByText("Delete").Enabled = false;
            if (RadToolBarProgram.FindItemByText("Update") != null) RadToolBarProgram.FindItemByText("Update").Text = "Save";

            GetSiteLocation();
        }

        protected void RadGridTuition_OnPreRender(object sender, EventArgs e)
        {
            RefreshTuition();
        }

        private void RefreshTuition()
        {
            sqlDataSource1.WhereParameters.Clear();
            if (RadComboBoxCountryMarket.SelectedValue == null)
                sqlDataSource1.WhereParameters.Add("CountryMarketId", DbType.Int32, 0.ToString());
            else
                sqlDataSource1.WhereParameters.Add("CountryMarketId", DbType.Int32, RadComboBoxCountryMarket.SelectedValue);

            if (Grid.SelectedValue == null)
                sqlDataSource1.WhereParameters.Add("ProgramId", DbType.Int32, 0.ToString());
            else
                sqlDataSource1.WhereParameters.Add("ProgramId", DbType.Int32, Grid.SelectedValue.ToString());

            sqlDataSource1.Where = "CountryMarketId == @CountryMarketId AND ProgramId == @ProgramId";
        }

        protected void RadGridTuition_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;

                //if (Grid.SelectedValue != null && RadComboBoxCountryMarket.SelectedValue != null)
                //{
                //    vwProgramTuition tuition = (vwProgramTuition)dataItem.DataItem;

                //    CProgramTuition cProgramTuition = new CProgramTuition();
                //    var tuitionList = cProgramTuition.GetTuitionsBelongToHoursByWeeks(tuition.SiteLocationId, tuition.ProgramId, (int)tuition.CountryMarketId, Convert.ToInt32(tuition.Weeks));

                //    foreach (ProgramTuition p in tuitionList)
                //    {
                //        if (p.HrsStatus == 5)
                //            dataItem["5hrs"].Text = p.HrsStatus.ToString();
                //        else if (p.HrsStatus == 10)
                //            dataItem["10hrs"].Text = p.HrsStatus.ToString();
                //        else if (p.HrsStatus == 15)
                //            dataItem["15hrs"].Text = p.HrsStatus.ToString();
                //        else if (p.HrsStatus == 20)
                //            dataItem["20hrs"].Text = p.HrsStatus.ToString();
                //        else if (p.HrsStatus == 24.5)
                //            dataItem["24.5hrs"].Text = p.HrsStatus.ToString();
                //        else if (p.HrsStatus == 25)
                //            dataItem["25hrs"].Text = p.HrsStatus.ToString();
                //        else if (p.HrsStatus == 28)
                //            dataItem["28hrs"].Text = p.HrsStatus.ToString();
                //        else if (p.HrsStatus == 30)
                //            dataItem["30hrs"].Text = p.HrsStatus.ToString();
                //    }
                //}

                //if ((dataItem["5hrs"].FindControl("RadNumericTextBox5hrs") as Label).Text.Contains("-"))
                //    (dataItem["StandardPrice"].FindControl("lblStandardPrice") as Label).Style["color"] = Color.OrangeRed.Name;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
                //(footer["StudentPrice"].FindControl("TotalStudent") as RadNumericTextBox).Value = Double.Parse(studentSum.ToString());
            }
        }

        protected void RadGridTuition_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Delete")
                {
                    //List<ProgramTuition> programTuitionList = new List<ProgramTuition>();

                    //foreach (GridDataItem item in RadGridTuition.Items)
                    //{
                    //    // hours index
                    //    for (int i = 4; i <= 11; i++)
                    //    {
                    //        if (item.Cells[i].Text != string.Empty && item.Cells[i].Text != "&nbsp;")
                    //        {
                    //            programTuitionList.Add(new ProgramTuition()
                    //            {
                    //                SiteLocationId = CurrentSiteLocationId,
                    //                IsActive = true,
                    //                Weeks = Convert.ToInt32(item.Cells[4].Text),
                    //                UpdatedDate = DateTime.Now,
                    //                UpdatedId = CurrentUserId,
                    //                ProgramId = Convert.ToInt32(Grid.SelectedValue),
                    //                HrsStatus = Convert.ToInt32(item.Cells[i].Text),
                    //                CountryMarketId = Convert.ToInt32(RadComboBoxCountryMarket.SelectedValue),
                    //                Tuition = Convert.ToInt32(item.Cells[i].Text)
                    //            });
                    //        }
                    //    }
                    //}

                    //if (programTuitionList.Count > 0)
                    //{

                    //    CProgramTuition programTuition = new CProgramTuition();
                    //    // del
                    //    programTuition.DelProgramTuition(CurrentSiteLocationId, Convert.ToInt32(Grid.SelectedValue), Convert.ToInt32(RadComboBoxCountryMarket.SelectedValue));
                    //    // add
                    //    programTuition.SetProgramTuitionList(programTuitionList);
                    //}

                    command.NewValues["ProgramId"] = Convert.ToInt32(Grid.SelectedValue);
                    command.NewValues["CountryMarketId"] = Convert.ToInt32(RadComboBoxCountryMarket.SelectedValue);
                    command.NewValues["IsActive"] = true;
                    command.NewValues["CreatedId"] = CurrentUserId;
                    command.NewValues["CreatedDate"] = DateTime.Now;
                    return;
                }
            }
        }

        protected void RadComboBoxCountryMarket_OnPreRender(object sender, EventArgs e)
        {
            if (Grid.SelectedValue == null)
            {
                RadComboBoxCountryMarket.Enabled = false;
                RadGridTuition.Visible = false;
            }
            else {
                RadComboBoxCountryMarket.Enabled = true;
                RadGridTuition.Visible = true;
            }
        }

        protected void Grid_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBarProgram.Items)
                {
                    toolbarItem.Enabled = false;
                }

                RadGridTuition.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                RadGridTuition.MasterTableView.EditMode = GridEditMode.InPlace;
                var delete = RadGridTuition.MasterTableView.GetColumn("DeleteColumn");
                delete.Visible = false;
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
            tbProgramFullName.Focus();
        }

        protected void RadGridTuition_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}