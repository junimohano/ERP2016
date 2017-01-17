using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;
using System.Collections.Generic;

namespace School.AcademicRegistrar
{
    public partial class ProgramClassRoom : PageBase
    {
        public ProgramClassRoom() : base((int)CConstValue.Menu.ProgramClassRoom)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResetForm();
            }

            LinqDataSource1.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteLocationList)
                LinqDataSource1.WhereParameters.Add(model.SiteLocationIdName, DbType.Int32, model.SiteLocationId.ToString());
            LinqDataSource1.Where = UserPermissionModel.SearchWhereSiteLocationSb.ToString();
        }

        protected void ToolbarClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "New")
            {
                Grid.SelectedIndexes.Clear();
                ResetForm();
            }
            if (e.Item.Text == "Save")
            {
                var cRoom = new CProgramClassRoom();
                var room = new Erp2016.Lib.ProgramClassRoom();

                room.SiteLocationId = CurrentSiteLocationId;
                room.Name = tbName.Text;
                room.Number = tbNumber.Text;
                room.Floor = tbFloor.Text;

                if (tbCapacity.Value != null)
                    room.Capacity = Convert.ToInt32(tbCapacity.Value);
                else
                    room.Capacity = null;

                room.CreatedId = CurrentUserId;
                room.CreatedDate = DateTime.Now;
                room.Description = RadTextBoxDescription.Text;
                room.IsActive = RadButtonActive.Checked;

                int result = cRoom.Add(room);
                if (result > 0)
                {
                    SetProgramClassRoomDetails(result);

                    ShowMessage("New class room is added");

                    Grid.Rebind();
                }
                else
                {
                    ShowMessage("Failed to add class room, please try again");
                }
            }
            if (e.Item.Text == "Update")
            {
                if (Grid.SelectedValue != null)
                {
                    var cRoom = new CProgramClassRoom();
                    var room = cRoom.Get(Convert.ToInt32(Grid.SelectedValue));

                    room.Name = tbName.Text;
                    room.Number = tbNumber.Text;
                    room.Floor = tbFloor.Text;

                    if (tbCapacity.Value != null)
                        room.Capacity = Convert.ToInt32(tbCapacity.Value);
                    else
                        room.Capacity = null;

                    room.Description = RadTextBoxDescription.Text;
                    room.IsActive = RadButtonActive.Checked;

                    if (cRoom.Update(room))
                    {
                        SetProgramClassRoomDetails(room.ProgramClassRoomId);

                        ShowMessage("Classroom updated");

                        Grid.Rebind();
                    }
                    else
                    {
                        ShowMessage("Failed to update class room, please try again");
                    }
                }
            }
        }

        protected void GetClassRoom()
        {
            ResetForm();
            if (Grid.SelectedValue != null)
            {
                var cRoom = new CProgramClassRoom();
                var room = cRoom.Get(Convert.ToInt32(Grid.SelectedValue));
                if (room.ProgramClassRoomId > 0)
                {
                    tbName.Text = room.Name;
                    tbNumber.Text = room.Number;
                    tbFloor.Text = room.Floor;
                    tbCapacity.Value = room.Capacity;
                    RadButtonActive.Checked = room.IsActive;
                    RadTextBoxDescription.Text = room.Description;
                    
                    RadListBoxComponents.DataSource = new CProgramClassRoom().GetClassRoomItemList(room.ProgramClassRoomId);
                    RadListBoxComponents.DataBind();

                    RadToolBar2.FindItemByText("New").Enabled = true;
                    if (RadToolBar2.FindItemByText("Save") != null) RadToolBar2.FindItemByText("Save").Text = "Update";
                }
            }
        }

        private void SetProgramClassRoomDetails(int programClassRoomId)
        {
            var list = new List<CListModel>();
            foreach (RadListBoxItem item in RadListBoxComponents.Items)
            {
                if ((item.FindControl("RadButtonSelect") as RadButton).Checked)
                {
                    list.Add(new CListModel()
                    {
                        Name = (item.FindControl("RadTextBoxName") as RadTextBox).Text,
                        Comment = (item.FindControl("RadTextBoxComment") as RadTextBox).Text,
                        Value = (item.FindControl("RadTextBoxValue") as RadTextBox).Text,
                        Selected = true
                    });
                }
            }
            new CProgramClassRoom().SetClassRoomDetails(list, programClassRoomId, CurrentUserId);
        }

        protected void ResetForm()
        {
            tbName.Text = "";
            tbNumber.Text = "";
            tbFloor.Text = "";
            tbCapacity.Value = null;
            RadButtonActive.Checked = true;
            RadTextBoxDescription.Text = string.Empty;
            
            RadListBoxComponents.DataSource = new CProgramClassRoom().GetClassRoomItemList(0);
            RadListBoxComponents.DataBind();

            RadToolBar2.FindItemByText("New").Enabled = false;
            if (RadToolBar2.FindItemByText("Update") != null) RadToolBar2.FindItemByText("Update").Text = "Save";
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
                foreach (RadToolBarItem toolbarItem in RadToolBar2.Items)
                {
                    toolbarItem.Enabled = false;
                }
            }
        }

        protected void Grid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetClassRoom();
        }
    }
}