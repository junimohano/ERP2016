using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace App_Data
{
    public partial class InvoiceItemGrid : UserControl
    {
        public int InvoiceId
        {
            get
            {
                return (int)ViewState["RadGridInvoiceItemsSeletedValue"];
            }
            set { ViewState["RadGridInvoiceItemsSeletedValue"] = value; }
        }

        //public bool IsAgencyPrice
        //{
        //    get
        //    {
        //        return ViewState["IsAgencyPrice"] == null ? false : (bool)ViewState["IsAgencyPrice"];
        //    }
        //    set { ViewState["IsAgencyPrice"] = value; }
        //}

        //public void SetEnableAgencyPrice(bool isEnableAgencyPrice)
        //{
        //    IsAgencyPrice = isEnableAgencyPrice;
        //}

        public RadGrid GetRadGridInvoiceItems()
        {
            return RadGridInvoiceItems;
        }

        public LinqDataSource GetSqlDataSourceInvoiceItems()
        {
            return sqlDataSourceInvoiceItems;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            sqlDataSourceInvoiceItems.WhereParameters.Clear();
            sqlDataSourceInvoiceItems.WhereParameters.Add("InvoiceItemId", DbType.Int32, 0.ToString());
            sqlDataSourceInvoiceItems.Where = "InvoiceItemId== @InvoiceItemId";
        }

        protected void RadGridInvoiceItems_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Delete")
                {
                    command.NewValues["InvoiceId"] = InvoiceId;

                    var standardPrice = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["StandardPrice"]))) ? 0 : Convert.ToDecimal(command.NewValues["StandardPrice"]);
                    var studentPrice = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["StudentPrice"]))) ? 0 : Convert.ToDecimal(command.NewValues["StudentPrice"]);
                    var agencyPrice = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["AgencyPrice"]))) ? 0 : Convert.ToDecimal(command.NewValues["AgencyPrice"]);

                    var invoiceCoaItem = (new CInvoiceCoaItem()).Get(Convert.ToInt32(command.NewValues["InvoiceCoaItemId"]));
                    if (invoiceCoaItem != null)
                    {
                        standardPrice = Math.Abs(standardPrice) * invoiceCoaItem.ItemType;
                        studentPrice = Math.Abs(studentPrice) * invoiceCoaItem.ItemType;
                        agencyPrice = Math.Abs(agencyPrice) * invoiceCoaItem.ItemType;
                    }

                    command.NewValues["StandardPrice"] = standardPrice;
                    command.NewValues["StudentPrice"] = studentPrice;
                    command.NewValues["AgencyPrice"] = agencyPrice;

                    command.NewValues["CreatedId"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["CreatedId"]))) ? 0 : Convert.ToInt32(command.NewValues["CreatedId"]);
                    command.NewValues["CreatedDate"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["CreatedDate"]))) ? DateTime.Now : command.NewValues["CreatedDate"];
                }
            }

            ViewState["IsSaveChanged"] = true;
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
                if ((dataItem["AgencyPrice"].FindControl("lblAgencyPrice") as Label).Text.Contains("-"))
                    (dataItem["AgencyPrice"].FindControl("lblAgencyPrice") as Label).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                // it can't because it works at javascript
                //var footer = (GridFooterItem)e.Item;
                //if ((footer["StandardPrice"].FindControl("TotalStandard") as RadNumericTextBox).Text.Contains("-"))
                //    (footer["StandardPrice"].FindControl("TotalStandard") as RadNumericTextBox).Style["color"] = CConstValue.NagativeColorName;
                //if ((footer["StudentPrice"].FindControl("TotalStudent") as RadNumericTextBox).Text.Contains("-"))
                //    (footer["StudentPrice"].FindControl("TotalStudent") as RadNumericTextBox).Style["color"] = CConstValue.NagativeColorName;
                //if ((footer["AgencyPrice"].FindControl("TotalAgency") as RadNumericTextBox).Text.Contains("-"))
                //    (footer["AgencyPrice"].FindControl("TotalAgency") as RadNumericTextBox).Style["color"] = CConstValue.NagativeColorName;
            }
        }

        public void Rebind()
        {
            RadGridInvoiceItems.Rebind();
        }

        public void SetEditMode(bool isEnable)
        {
            var delete = RadGridInvoiceItems.MasterTableView.GetColumn("DeleteColumn");
            delete.Visible = isEnable;

            if (isEnable)
            {
                RadGridInvoiceItems.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                RadGridInvoiceItems.MasterTableView.EditMode = GridEditMode.Batch;
            }
            else
            {
                RadGridInvoiceItems.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                RadGridInvoiceItems.MasterTableView.EditMode = GridEditMode.InPlace;
            }
        }

        public void ValidateInvoiceItems()
        {
            if (ViewState["IsSaveChanged"] != null)
            {
                // init
                ViewState["IsSaveChanged"] = null;

                // ===========
                // validation
                // ===========
                var cInvoice = new CInvoice();
                var invoice = cInvoice.Get(InvoiceId);

                var cInvoiceItem = new CInvoiceItem();
                var invoiceItem = cInvoiceItem.GetInvoiceItems(InvoiceId);

                decimal calCommissionFee = 0;
                foreach (InvoiceItem item in invoiceItem)
                {
                    // direct Student
                    if (invoice.AgencyId == null)
                    {
                        if (item.AgencyPrice != 0)
                        {
                            // update price which isnot zero
                            item.AgencyPrice = 0;
                            cInvoiceItem.Update(item);
                        }
                    }
                    // from Agency
                    else
                    {
                        switch (item.InvoiceCoaItemId)
                        {
                            // tuition
                            case 1:
                                calCommissionFee = (decimal)item.AgencyPrice;
                                break;

                            // scholarship
                            case 2:
                            // promotion Credit
                            case 3:
                            // Advertising fee
                            case 4:
                            // Commision - Incentive
                            case 5:
                                calCommissionFee += (decimal)item.AgencyPrice;
                                break;


                            // Commission - Tuition
                            case 6:
                                var cAgency = new CAgency();
                                var agency = cAgency.Get((int)invoice.AgencyId);
                                if (agency.CommissionRateBasic != null)
                                {
                                    // update calculated commissionFee
                                    item.AgencyPrice = calCommissionFee * ((decimal)agency.CommissionRateBasic / -100);
                                    cInvoiceItem.Update(item);
                                }
                                break;
                        }
                    }

                    // get standardPrice
                    decimal maximumPrice = (decimal)(item.StudentPrice > item.AgencyPrice ?
                        (item.StudentPrice < 0 ? 0 : item.StudentPrice) : (item.AgencyPrice < 0 ? 0 : item.AgencyPrice));

                    decimal? tempStandardPrice = 0;
                    // tuition
                    if (item.InvoiceCoaItemId == 1)
                    {
                        if (item.StandardPrice > 0)
                            tempStandardPrice = item.StandardPrice;
                        else
                            tempStandardPrice = item.StandardPrice > maximumPrice ? item.StandardPrice : maximumPrice;
                    }
                    // others
                    else
                        tempStandardPrice = item.StandardPrice > maximumPrice ? item.StandardPrice : maximumPrice;

                    if (item.StandardPrice != tempStandardPrice)
                    {
                        item.StandardPrice = tempStandardPrice;
                        cInvoiceItem.Update(item);
                    }
                }
            }
        }

        public string GetGridData()
        {
            return HiddenFieldGridData.Value;
        }

        //protected void tbAgencyPrice_OnLoad(object sender, EventArgs e)
        //{
        //    (sender as RadNumericTextBox).ReadOnly = IsAgencyPrice;
        //}

        public void SetTypeOfInvoiceCoaItem(int invoiceType)
        {
            string paramName;
            switch (invoiceType)
            {
                // General
                case (int)CConstValue.InvoiceType.General:
                    paramName = "GeneralFlag";
                    break;

                // Simple
                case (int)CConstValue.InvoiceType.Simple:
                    paramName = "SimpleFlag";
                    break;

                // Manual
                case (int)CConstValue.InvoiceType.Manual:
                    paramName = "ManualFlag";
                    break;

                // Refund
                default:
                    paramName = "RefundFlag";
                    break;

            }
            sqlDataSourceInvoiceCoaItems.WhereParameters.Clear();
            sqlDataSourceInvoiceCoaItems.WhereParameters.Add(paramName, DbType.Boolean, bool.TrueString);
            sqlDataSourceInvoiceCoaItems.Where = paramName + "== @" + paramName;

        }

        protected void RadGridInvoiceItems_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            PageBase.SetFilterCheckListItems(e);
        }
    }
}