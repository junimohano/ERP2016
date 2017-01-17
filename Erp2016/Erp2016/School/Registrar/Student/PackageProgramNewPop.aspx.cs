using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace School.Registrar.Student
{
    public partial class PackageProgramNewPop : PageBase
    {
        public int Id { get; set; }

        private RadGrid _radGridInvoiceItems;

        public PackageProgramNewPop() : base((int)CConstValue.Menu.Student)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // find user control
            _radGridInvoiceItems = InvoiceItemGrid1.GetRadGridInvoiceItems();
            // connect event of invoice Items.
            _radGridInvoiceItems.PreRender += _radGridInvoiceItems_PreRender;
            _radGridInvoiceItems.MasterTableView.DataSourceID = null;
            _radGridInvoiceItems.DataSourceID = null;
            // just view
            InvoiceItemGrid1.SetEditMode(false);

            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                var cStudent = new CStudent();
                var student = cStudent.Get(Id);

                LoadAgency(student.SiteLocationId);
                var cCountry = new CCountry().Get((int)student.CountryId);
                var cCountryMarket = new CCountryMarket().Get((int)cCountry.CountryMarketId);
                ViewState["CountryMarketId"] = cCountry.CountryMarketId;

                ttName1.Text = cStudent.GetStudentName(student) + " [" + student.StudentNo + "]";
                ttName2.Text = cCountryMarket.Name;

                // Package Program
                ddlPackageProgram.DataSource = new CPackageProgram().GetPackageProgramBySiteIdAndCountryId(student.SiteLocationId);
                ddlPackageProgram.DataTextField = "Name";
                ddlPackageProgram.DataValueField = "Value";
                ddlPackageProgram.DataBind();
                if (ddlPackageProgram.Items.Count > 0)
                    SetPackageProgramData(ddlPackageProgram.Items[0].Value);
            }

            ddlPackageProgram.OpenDropDownOnLoad = false;
        }

        private void _radGridInvoiceItems_PreRender(object sender, EventArgs e)
        {
            ResetGridInvoice();
        }

        protected void LoadAgency(int siteLocationId)
        {
            ddlAgency.Items.Clear();
            ddlAgency.Text = string.Empty;
            ddlAgency.DataSource = new CAgency().GetAgency(siteLocationId);
            ddlAgency.DataTextField = "Name";
            ddlAgency.DataValueField = "Value";
            ddlAgency.DataBind();
            ddlAgency.Items.Insert(0, new RadComboBoxItem("N/A (Direct Student)", null));
        }

        private bool GetAgencyRate()
        {
            if (!string.IsNullOrEmpty(ddlAgency.SelectedValue))
            {
                var cAgency = new CAgency();
                var agency = cAgency.Get(Convert.ToInt32(ddlAgency.SelectedValue));
                if (agency != null)
                    tbCommissionRate.Value = RadButtonAgencyRateBasic.Checked ? agency.CommissionRateBasic : agency.CommissionRateSeasonal;
                else
                    tbCommissionRate.Value = 0;
                return true;
            }

            tbCommissionRate.Value = 0;
            return false;
        }

        protected void ddlAgency_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (GetAgencyRate())
                ddlPackageProgram.OpenDropDownOnLoad = true;
        }

        protected void ddlPackageProgram_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetPackageProgramData(e.Value);

            tbPrgTuition.Focus();
        }

        private void SetPackageProgramData(string value)
        {
            var split = value.Split(',');
            if (string.IsNullOrEmpty(split[0])) return;

            var packageProgramId = Convert.ToInt32(split[0]);
            ViewState["PackageProgramId"] = packageProgramId;

            var programId = Convert.ToInt32(split[1]);

            var cCP = new CProgram();
            var cP = cCP.Get(programId);

            if (cP != null)
            {
                // program Group
                if (cP.ProgramGroupId != null)
                {
                    var cProgramGroup = (new CProgramGroup()).Get((int)cP.ProgramGroupId);
                    if (cProgramGroup != null)
                    {
                        radTextBoxProgramGroup.Text = cProgramGroup.Name;

                        // faculty
                        if (cProgramGroup.FacultyId != null)
                        {
                            var cFaculty = (new CFaculty()).Get((int)cProgramGroup.FacultyId);
                            radTextBoxFaculty.Text = cFaculty.Name;
                        }
                    }
                }

                // program name
                radTextBoxProgramName.Text = cP.ProgramFullName;
            }

            var cPackageProgram = new CPackageProgram();
            var packageProgram = cPackageProgram.GetPackageProgram(packageProgramId);
            tbPrgStartDate.SelectedDate = packageProgram.StartDate;
            tbPrgEndDate.SelectedDate = packageProgram.EndDate;

            var standardTuition = cPackageProgram.GetStandardTuition(packageProgramId).ToString();

            tbPrgStandardTuition.Text = standardTuition;
            tbPrgTuition.Text = standardTuition;
        }

        protected void ToolbarButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == @"Save")
            {
                if (IsValid)
                {
                    if (!string.IsNullOrEmpty(ddlAgency.SelectedValue) && (tbCommissionRate.Value == 0 || tbCommissionRate.Value == null))
                    {
                        ShowMessage("Commision Rate should be written.");
                        return;
                    }

                    var programId = Convert.ToInt32(ddlPackageProgram.SelectedValue.Split(',')[1]);
                    var packageProgramId = Convert.ToInt32(ddlPackageProgram.SelectedValue.Split(',')[0]);

                    var cProgramReg = new CProgramRegistration();
                    var programReg = new ProgramRegistration();

                    programReg.StudentId = Id;
                    programReg.ProgramId = programId;
                    programReg.PackageProgramId = packageProgramId;

                    programReg.StartDate = tbPrgStartDate.SelectedDate;
                    programReg.EndDate = tbPrgEndDate.SelectedDate;

                    programReg.ProgramRegistrationType = 9;

                    programReg.CreatedId = CurrentUserId;
                    programReg.CreatedDate = DateTime.Now;

                    var proRegId = cProgramReg.Add(programReg); //DB:ProgramRegistration

                    if (proRegId > 0)
                    {
                        // add basic invoice first, then homestay or dormitory if exists.
                        var cInvoice = new CInvoice();
                        var invoice = new Invoice();

                        invoice.ProgramRegistrationId = proRegId;
                        invoice.StudentId = Id;
                        if (!string.IsNullOrEmpty(ddlAgency.SelectedValue))
                        {
                            invoice.AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);
                            invoice.IsAgencySeasonalRate = RadButtonAgencyRateSeasonal.Checked;
                            invoice.AgencyRate = tbCommissionRate.Value;
                        }
                        invoice.SiteLocationId = CurrentSiteLocationId;
                        invoice.InvoiceType = (int)CConstValue.InvoiceType.General; //General Invoice(IN)
                        invoice.Status = (int)CConstValue.InvoiceStatus.Pending; // Pending
                        invoice.CreatedId = CurrentUserId;
                        invoice.CreatedDate = DateTime.Now;

                        var invoiceId = cInvoice.Add(invoice); //DB:Invoice

                        if (invoiceId > 0)
                        {
                            var invoiceItems = new List<InvoiceItem>();
                            var homestayInvoiceItems = new List<InvoiceItem>();
                            var dormitoryInvoiceItems = new List<InvoiceItem>();
                            var airportInvoiceItems = new List<InvoiceItem>();

                            var cInvoiceItem = new CInvoiceItem();

                            foreach (GridDataItem item in _radGridInvoiceItems.Items)
                            {
                                var invoiceCoaItemId = (RadDropDownList)item.FindControl("ddlInvoiceItems");
                                var standardPrice = (Label)item.FindControl("lblStandardPrice");
                                var studentPrice = (Label)item.FindControl("lblStudentPrice");
                                var agencyPrice = (Label)item.FindControl("lblAgencyPrice");

                                var invoiceItem = new InvoiceItem();
                                invoiceItem.InvoiceId = invoiceId;

                                invoiceItem.InvoiceCoaItemId = Convert.ToInt32(invoiceCoaItemId.SelectedValue);
                                invoiceItem.StandardPrice = Convert.ToDecimal(standardPrice.Text.Replace("$", string.Empty));
                                invoiceItem.StudentPrice = Convert.ToDecimal(studentPrice.Text.Replace("$", string.Empty));
                                invoiceItem.AgencyPrice = Convert.ToDecimal(agencyPrice.Text.Replace("$", string.Empty));

                                invoiceItem.CreatedId = CurrentUserId;
                                invoiceItem.CreatedDate = DateTime.Now;

                                switch (invoiceItem.InvoiceCoaItemId)
                                {
                                    case (int)CConstValue.InvoiceCoaItemForHomestay.HomestayBasic:
                                    case (int)CConstValue.InvoiceCoaItemForHomestay.HomestayBasicDiscount:
                                    case (int)CConstValue.InvoiceCoaItemForHomestay.HomestayPlacement:
                                    case (int)CConstValue.InvoiceCoaItemForHomestay.HomestayPlacementDiscount:
                                    case (int)CConstValue.InvoiceCoaItemForHomestay.HomestayInternetGuarantee:
                                    case (int)CConstValue.InvoiceCoaItemForHomestay.HomestayOtherDiscount:
                                        homestayInvoiceItems.Add(invoiceItem);
                                        break;

                                    case (int)CConstValue.InvoiceCoaItemForDormitory.DormitoryBasic:
                                    case (int)CConstValue.InvoiceCoaItemForDormitory.DormitoryBasicDiscount:
                                    case (int)CConstValue.InvoiceCoaItemForDormitory.DormitoryPlacement:
                                    case (int)CConstValue.InvoiceCoaItemForDormitory.DormitoryPlacementDiscount:
                                    case (int)CConstValue.InvoiceCoaItemForDormitory.DormitoryKeyDeposit:
                                    case (int)CConstValue.InvoiceCoaItemForDormitory.DormitoryKeyDepositDiscount:
                                        dormitoryInvoiceItems.Add(invoiceItem);
                                        break;

                                    case (int)CConstValue.InvoiceCoaItem.AirportPickup:
                                    case (int)CConstValue.InvoiceCoaItem.AirportPickupDiscount:
                                    case (int)CConstValue.InvoiceCoaItem.AirportDropoff:
                                    case (int)CConstValue.InvoiceCoaItem.AirportDropoffDiscount:
                                    case (int)CConstValue.InvoiceCoaItem.AirportPickupAndDropoff:
                                    case (int)CConstValue.InvoiceCoaItem.AirportPickupAndDropoffDiscount:
                                        airportInvoiceItems.Add(invoiceItem);
                                        break;

                                    default:
                                        invoiceItems.Add(invoiceItem);
                                        break;
                                }
                            }

                            // add invoiceItems except for homestay and dormitory
                            if (cInvoiceItem.Add(invoiceItems) == false)
                                ShowMessage("Error : add invoiceItem");

                            // add homestay if exist.
                            if (homestayInvoiceItems.Count > 0)
                            {
                                var newHomestayRegistrationId = new CHomestayStudentRequest().Add(new HomestayStudentBasic()
                                {
                                    HomestayStudentStatus = 0, // Pending
                                    StudentId = Id,
                                    PlacedUserId = 0,
                                    CreatedUserId = CurrentUserId,
                                    CreatedDate = DateTime.Now
                                });

                                if (newHomestayRegistrationId > 0)
                                {
                                    var invoiceForHomestay = new Invoice();
                                    invoiceForHomestay.HomestayRegistrationId = newHomestayRegistrationId;
                                    invoiceForHomestay.StudentId = Id;
                                    if (!string.IsNullOrEmpty(ddlAgency.SelectedValue))
                                    {
                                        invoiceForHomestay.AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);
                                        invoiceForHomestay.IsAgencySeasonalRate = RadButtonAgencyRateSeasonal.Checked;
                                        invoiceForHomestay.AgencyRate = tbCommissionRate.Value;
                                    }
                                    invoiceForHomestay.SiteLocationId = CurrentSiteLocationId;
                                    invoiceForHomestay.InvoiceType = (int)CConstValue.InvoiceType.Homestay;
                                    invoiceForHomestay.Status = (int)CConstValue.InvoiceStatus.Pending;
                                    invoiceForHomestay.CreatedId = CurrentUserId;
                                    invoiceForHomestay.CreatedDate = DateTime.Now;

                                    var invoiceForHomestayId = cInvoice.Add(invoiceForHomestay);
                                    if (invoiceForHomestayId > 0)
                                    {
                                        foreach (var h in homestayInvoiceItems)
                                            h.InvoiceId = invoiceForHomestayId;
                                        foreach (var a in airportInvoiceItems)
                                            a.InvoiceId = invoiceForHomestayId;

                                        // merge between homestay Items and airport Items
                                        homestayInvoiceItems.AddRange(airportInvoiceItems);

                                        if (cInvoiceItem.Add(homestayInvoiceItems) == false)
                                            ShowMessage("Error : add invoiceHomestayItem");
                                    }
                                    else
                                        ShowMessage("Error : add Homestay");
                                }
                                else
                                    ShowMessage("Error : add Homestay registration");
                            }

                            // add dormitory if exist.
                            if (dormitoryInvoiceItems.Count > 0)
                            {
                                var newDormitoryRegistrationId = new CDormitoryRegistrations().Add(new DormitoryRegistration()
                                {
                                    DormitoryStudentStatus = 0, // Pending
                                    StudentId = Id,
                                    PlacedUserId = 0,
                                    CreatedId = CurrentUserId,
                                    CreatedDate = DateTime.Now
                                });

                                if (newDormitoryRegistrationId > 0)
                                {
                                    var invoiceForDormitory = new Invoice();
                                    invoiceForDormitory.DormitoryRegistrationId = newDormitoryRegistrationId;
                                    invoiceForDormitory.StudentId = Id;
                                    if (!string.IsNullOrEmpty(ddlAgency.SelectedValue))
                                    {
                                        invoiceForDormitory.AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);
                                        invoiceForDormitory.IsAgencySeasonalRate = RadButtonAgencyRateSeasonal.Checked;
                                        invoiceForDormitory.AgencyRate = tbCommissionRate.Value;
                                    }
                                    invoiceForDormitory.SiteLocationId = CurrentSiteLocationId;
                                    invoiceForDormitory.InvoiceType = (int)CConstValue.InvoiceType.Dormitory;
                                    invoiceForDormitory.Status = (int)CConstValue.InvoiceStatus.Pending;
                                    invoiceForDormitory.CreatedId = CurrentUserId;
                                    invoiceForDormitory.CreatedDate = DateTime.Now;

                                    var invoiceForDormitoryId = cInvoice.Add(invoiceForDormitory);
                                    if (invoiceForDormitoryId > 0)
                                    {
                                        foreach (var h in dormitoryInvoiceItems)
                                            h.InvoiceId = invoiceForDormitoryId;
                                        foreach (var a in airportInvoiceItems)
                                            a.InvoiceId = invoiceForDormitoryId;

                                        // merge between dormitory Items and airport Items
                                        dormitoryInvoiceItems.AddRange(airportInvoiceItems);

                                        if (cInvoiceItem.Add(dormitoryInvoiceItems) == false)
                                            ShowMessage("Error : add invoiceDormitoryItem");
                                    }
                                    else
                                        ShowMessage("Error : add Dormitory");
                                }
                                else
                                    ShowMessage("Error : add Dormitory registration");
                            }

                            RunClientScript("Close();");
                        }
                        else
                        {
                            ShowMessage("failed to update inqury (Invoice)");
                        }
                    }
                    else
                    {
                        ShowMessage("failed to update inqury (Program Registragion)");
                    }
                }
            }
        }

        public DataTable GetInvoiceData()
        {
            DataTable table = new DataTable();
            table.Columns.Add("InvoiceCoaItemId", typeof(int));
            table.Columns.Add("StandardPrice", typeof(decimal));
            table.Columns.Add("StudentPrice", typeof(decimal));
            table.Columns.Add("AgencyPrice", typeof(decimal));
            table.Columns.Add("Remark", typeof(string));
            table.Columns.Add("InvoiceItemId", typeof(int));

            var packageProgramList = (new CPackageProgram()).GetPackageProgramDetail(Convert.ToInt32(ViewState["PackageProgramId"]));
            foreach (PackageProgramDetail pDetail in packageProgramList)
            {
                // tuition
                if (pDetail.InvoiceCoaItemId == (int)CConstValue.InvoiceCoaItem.TuitionBasic)
                {
                    // tuition
                    table.Rows.Add(pDetail.InvoiceCoaItemId, pDetail.StandardPrice, tbPrgTuition.Value, tbPrgTuition.Value, string.Empty, 0);

                    // set comission
                    if (tbCommissionRate.Value.ToString() != "0" && tbCommissionRate.Value.ToString() != string.Empty)
                    {
                        table.Rows.Add((int)CConstValue.InvoiceCoaItem.CommissionTuition, 0, 0, pDetail.AgencyPrice * ((decimal)tbCommissionRate.Value / -100), string.Empty, 0);
                    }
                }
                // anything else
                else
                    table.Rows.Add(pDetail.InvoiceCoaItemId, pDetail.StandardPrice, pDetail.StudentPrice, pDetail.AgencyPrice, string.Empty, 0);
            }

            // agency check
            if (string.IsNullOrEmpty(ddlAgency.SelectedValue))
            {
                foreach (DataRow dr in table.Rows)
                {
                    dr["AgencyPrice"] = 0;
                }
            }

            return table;
        }

        private void ResetGridInvoice()
        {
            _radGridInvoiceItems.DataSource = GetInvoiceData();
            _radGridInvoiceItems.Rebind();
        }

        protected void RadGridInvoiceItems_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                if ((dataItem["StandardPrice"].FindControl("lblStandardPrice") as Label).Text.Contains("-"))
                    (dataItem["StandardPrice"].FindControl("lblStandardPrice") as Label).Style["color"] = CConstValue.NagativeColorName;
                if ((dataItem["StudentPrice"].FindControl("lblStudentPrice") as Label).Text.Contains("-"))
                    (dataItem["StudentPrice"].FindControl("lblStudentPrice") as Label).Style["color"] = CConstValue.NagativeColorName;
                if ((dataItem["StudentPrice"].FindControl("lblAgencyPrice") as Label).Text.Contains("-"))
                    (dataItem["AgencyPrice"].FindControl("lblAgencyPrice") as Label).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
            }
        }

        protected void RadButtonAgencyRateBasic_OnCheckedChanged(object sender, EventArgs e)
        {
            GetAgencyRate();
        }

        protected void RadButtonAgencyRateSeasonal_OnCheckedChanged(object sender, EventArgs e)
        {
            GetAgencyRate();
        }
    }
}