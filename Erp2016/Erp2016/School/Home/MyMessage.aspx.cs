using System;
using System.Data;
using Telerik.Web.UI;

namespace School.Home
{
    public partial class MyMessage : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }

            GetRecievedMessage();
            GetSentMessage();
        }

        private void GetRecievedMessage()
        {
            LinqDataSourceRecievedMessage.WhereParameters.Clear();
            LinqDataSourceRecievedMessage.WhereParameters.Add("UserId", DbType.Int32, CurrentUserId.ToString());
            LinqDataSourceRecievedMessage.Where = "UserId == @UserId";
        }

        private void GetSentMessage()
        {
            LinqDataSourceSentMessage.WhereParameters.Clear();
            LinqDataSourceSentMessage.WhereParameters.Add("CreatedId", DbType.Int32, CurrentUserId.ToString());
            LinqDataSourceSentMessage.Where = "CreatedId == @CreatedId";
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            // refresh
            RadGridRecievedMessage.Rebind();
            RadGridSentMessage.Rebind();
        }

        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "New Message")
            {
                RunClientScript("ShowPop('" + CurrentUserId + "', '0', '0');");
            }
        }

        protected void RadGridRecievedMessage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            RunClientScript("ShowPop('" + RadGridRecievedMessage.SelectedValue + "', '1', '0');");
        }

        protected void RadGridSentMessage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            RunClientScript("ShowPop('" + RadGridSentMessage.SelectedValue + "', '1', '1');");
        }

        protected void RadGridSentMessage_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;
                foreach (GridColumn column in (sender as RadGrid).Columns)
                    gridItem[column.UniqueName].ToolTip = gridItem[column.UniqueName].Text;
            }
        }

        protected void RadGridSentMessage_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridRecievedMessage_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}