using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Sales
{
    public partial class SimpleInvoiceNewPop : PageBase
    {
        private LinqDataSource _sqlDataSourceInvoiceItems;
        private RadGrid _radGridInvoiceItems;

        public SimpleInvoiceNewPop() : base((int)CConstValue.Menu.Invoice)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // find user control
            _sqlDataSourceInvoiceItems = InvoiceItemGrid1.GetSqlDataSourceInvoiceItems();
            _radGridInvoiceItems = InvoiceItemGrid1.GetRadGridInvoiceItems();
            _radGridInvoiceItems.MasterTableView.CommandItemSettings.ShowSaveChangesButton = false;
            // Simple
            InvoiceItemGrid1.SetTypeOfInvoiceCoaItem(1);

            if (!IsPostBack)
            {
                InvoiceItemGrid1.SetEditMode(true);

                var cStudent = new CStudent();
                var student = cStudent.GetStudentList(CurrentSiteLocationId);
                foreach (var stu in student)
                {
                    RadComboBoxMenu.Items.Add(new RadComboBoxItem(cStudent.GetStudentName(stu) + "(" + stu.StudentNo + ")", stu.StudentId.ToString()));
                }
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Save":
                    if (IsValid)
                    {
                        foreach (var chkItem in RadComboBoxMenu.CheckedItems)
                        {
                            var cInvoice = new CInvoice();
                            var invoice = new Erp2016.Lib.Invoice();

                            invoice.StudentId = Convert.ToInt32(chkItem.Value);

                            invoice.Status = (int)CConstValue.InvoiceStatus.Pending; // pending
                            invoice.SiteLocationId = CurrentSiteLocationId;
                            invoice.InvoiceType = (int)CConstValue.InvoiceType.Simple; //Simple Invoice(SI)

                            invoice.CreatedId = CurrentUserId;
                            invoice.CreatedDate = DateTime.Now;

                            var invoiceId = cInvoice.Add(invoice); //DB:Invoice

                            if (invoiceId > 0)
                            {
                                var cInvoiceItem = new CInvoiceItem();
                                var gridData = InvoiceItemGrid1.GetGridData();
                                gridData = gridData.Insert(0, ",");
                                var gridDataRows = gridData.Split('|');
                                foreach (var gridDataRow in gridDataRows)
                                {
                                    if (string.IsNullOrEmpty(gridDataRow))
                                        break;

                                    var gridDataRowCell = gridDataRow.Split(',');

                                    var invoiceCoaItem = gridDataRowCell[1];
                                    var standardPrice = gridDataRowCell[2];
                                    var studentPrice = gridDataRowCell[3];
                                    var agencyPrice = gridDataRowCell[4];
                                    var remark = gridDataRowCell[5];

                                    var invoiceItem = new InvoiceItem();
                                    invoiceItem.InvoiceId = invoiceId;
                                    var cInvoiceCoaItem = new CInvoiceCoaItem();
                                    invoiceItem.InvoiceCoaItemId = cInvoiceCoaItem.Get(invoiceCoaItem).InvoiceCoaItemId;
                                    if (!string.IsNullOrEmpty(standardPrice))
                                        invoiceItem.StandardPrice = Convert.ToDecimal(standardPrice.Replace("$", string.Empty));
                                    if (!string.IsNullOrEmpty(studentPrice))
                                        invoiceItem.StudentPrice = Convert.ToDecimal(studentPrice.Replace("$", string.Empty));
                                    if (!string.IsNullOrEmpty(agencyPrice))
                                        invoiceItem.AgencyPrice = Convert.ToDecimal(agencyPrice.Replace("$", string.Empty));
                                    invoiceItem.Remark = remark;

                                    invoiceItem.CreatedId = CurrentUserId;
                                    invoiceItem.CreatedDate = DateTime.Now;

                                    cInvoiceItem.Add(invoiceItem);
                                }
                            }
                        }
                        RunClientScript("Close();");
                    }
                    else
                        ShowMessage("Error to add Simple Invoice");
                    break;

                case "Cancel":
                    RunClientScript("Close();");
                    break;
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

        }

    }
}