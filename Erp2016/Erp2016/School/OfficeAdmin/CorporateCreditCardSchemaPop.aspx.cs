using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class CorporateCreditCardSchemaPop : PageBase
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public CorporateCreditCardSchemaPop() : base((int)CConstValue.Menu.CorporateCreditCardSchema)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["id"]);
            Type = Request["type"];

            if (!IsPostBack)
            {
                ResetForm();

                // new
                if (Type == "0")
                {
                    //
                }
                // modify
                else
                {
                    var corporateCreditCardSchema = new CCorporateCreditCardSchema().Get(Id);
                    if (corporateCreditCardSchema != null)
                    {
                        LinqDataSourceUser.WhereParameters.Clear();
                        LinqDataSourceUser.WhereParameters.Add("UserId", DbType.Int32, corporateCreditCardSchema.UserId.ToString());
                        LinqDataSourceUser.Where = "UserId == @UserId";

                        RadGridUser.MasterTableView.Rebind();
                        foreach (GridDataItem item in RadGridUser.Items)
                        {
                            item.Selected = true;
                            break;
                        }
                        GetCorporateCreditCardSchema();
                    }
                }
            }

        }

        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Save")
            {
                if (IsValid)
                {
                    if (RadGridUser.SelectedValue != null)
                    {
                        var cCorporateCreditCardSchema = new CCorporateCreditCardSchema();
                        var corporateCreditCardSchema = cCorporateCreditCardSchema.GetByUserId(Convert.ToInt32(RadGridUser.SelectedValue));

                        bool isNew = false;
                        if (corporateCreditCardSchema == null)
                        {
                            corporateCreditCardSchema = new Erp2016.Lib.CorporateCreditCardSchema();
                            corporateCreditCardSchema.CreatedId = CurrentUserId;
                            corporateCreditCardSchema.CreatedDate = DateTime.Now;
                            isNew = true;
                        }

                        corporateCreditCardSchema.UserId = Convert.ToInt32(RadGridUser.SelectedValue);
                        corporateCreditCardSchema.CreditCardNumber = RadTextBoxCreditCardNumber.Text;
                        corporateCreditCardSchema.CreditLimit = (decimal)RadNumericTextBoxCreditLimit.Value;

                        // new
                        if (isNew)
                        {
                            cCorporateCreditCardSchema.Add(corporateCreditCardSchema);
                        }
                        // modify
                        else
                        {
                            corporateCreditCardSchema.UpdatedId = CurrentUserId;
                            corporateCreditCardSchema.UpdatedDate = DateTime.Now;
                            cCorporateCreditCardSchema.Update(corporateCreditCardSchema);
                        }

                        RunClientScript("Close();");
                    }
                }
            }
        }

        protected void ResetForm()
        {
            RadTextBoxCreditCardNumber.Text = string.Empty;
            RadNumericTextBoxCreditLimit.Value = 0;
        }

        protected void RadGridUser_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        private void GetCorporateCreditCardSchema()
        {
            if (RadGridUser.SelectedValue != null)
            {
                var cCorporateCreditCardSchema = new CCorporateCreditCardSchema().GetByUserId(Convert.ToInt32(RadGridUser.SelectedValue));
                if (cCorporateCreditCardSchema != null)
                {
                    RadTextBoxCreditCardNumber.Text = cCorporateCreditCardSchema.CreditCardNumber;
                    RadNumericTextBoxCreditLimit.Value = (double)cCorporateCreditCardSchema.CreditLimit;
                }
            }
        }

        protected void RadGridUser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetCorporateCreditCardSchema();
        }
    }
}