using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar
{
    public partial class Cancel : PageBase
    {
        public Cancel() : base((int)CConstValue.Menu.Cancel)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Cancel);
            FileDownloadList1.SetVisibieUploadControls(false);

            LinqDataSource1.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteLocationList)
                LinqDataSource1.WhereParameters.Add(model.SiteLocationIdName, DbType.Int32, model.SiteLocationId.ToString());
            LinqDataSource1.Where = UserPermissionModel.SearchWhereSiteLocationSb.ToString();
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {

            }
        }

        protected void GetInfo()
        {
            if (RadGrid1.SelectedValue != null)
            {
                var cCancel = new CCancel();
                var cancel = cCancel.Get(Convert.ToInt32(RadGrid1.SelectedValue));
                if (cancel != null)
                {
                    RadDatePickerApplyDate.SelectedDate = cancel.ApplyDate;
                    RadTextBoxComment.Text = cancel.Reason;

                }

                FileDownloadList1.GetFileDownload(Convert.ToInt32(RadGrid1.SelectedValue));
            }
        }

        protected void RadGrid1_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
        
        protected void RadGrid1_OnPreRender(object sender, EventArgs e)
        {
            GetInfo();
        }
    }
}