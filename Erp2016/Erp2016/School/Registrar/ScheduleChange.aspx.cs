using System;
using System.Data;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar
{
    public partial class ScheduleChange : PageBase
    {
        public ScheduleChange() : base((int)CConstValue.Menu.ScheduleChange)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.ScheduleChange);
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
                var cC = new CScheduleChange();
                var c = cC.Get(Convert.ToInt32(RadGrid1.SelectedValue));
                if (c != null)
                {
                    RadDatePickerApplyDate.SelectedDate = c.ApplyDate;
                    RadDatePickerStartDate.SelectedDate = c.StartDate;
                    RadDatePickerEndDate.SelectedDate = c.EndDate;
                    RadTextBoxComment.Text = c.Reason;
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
            if (!IsPostBack)
            {
                foreach (GridColumn column in RadGrid1.MasterTableView.Columns)
                {
                    if (column.DataType == typeof(DateTime))
                        column.FilterControlWidth = Unit.Pixel(50);
                    else
                        column.FilterControlWidth = Unit.Percentage(10);
                }
            }

            GetInfo();
        }
    }
}