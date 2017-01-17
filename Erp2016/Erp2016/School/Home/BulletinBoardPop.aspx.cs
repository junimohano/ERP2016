using System;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Home
{
    public partial class BulletinBoardPop : PageBase //System.Web.UI.Page
    {
        private int Id { get; set; }

        public BulletinBoardPop() : base((int)CConstValue.Menu.BulletinBoard)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                //var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
                //scriptManager.RegisterPostBackControl(RadButtonFileDownload);
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.BulletinBoard);

                // new
                if (Request["createOrListType"] == "0")
                {
                    var cUser = new CUser();
                    var user = cUser.Get(CurrentUserId);
                    RadTextBoxUserName.Text = cUser.GetUserName(user);
                    RadDatePickerDate.SelectedDate = DateTime.Today;
                }
                // select
                else
                {
                    var cBulletinBoard = new CBulletinBoard();
                    var bulletinBoard = cBulletinBoard.Get(Id);

                    if (bulletinBoard != null)
                    {
                        var cUser = new CUser();
                        var user = cUser.Get((int)bulletinBoard.CreatedId);
                        RadTextBoxUserName.Text = cUser.GetUserName(user);
                        RadDatePickerDate.SelectedDate = bulletinBoard.CreatedDate;
                        RadTextBoxSubject.Text = bulletinBoard.Subject;
                        RadEditorBody.Content = bulletinBoard.Body;

                        // update views
                        bulletinBoard.Views++;
                        cBulletinBoard.Update(bulletinBoard);

                        // view
                        if (CurrentUserId != bulletinBoard.CreatedId)
                        {
                            RadTextBoxSubject.Enabled = false;
                            RadEditorBody.EditModes = EditModes.Preview;
                            FileDownloadList1.SetVisibieUploadControls(false);


                            foreach (RadToolBarItem item in RadToolBar1.Items)
                            {
                                if (item.Text == "Save")
                                {
                                    item.Visible = false;
                                    break;
                                }
                            }
                        }
                    }

                    // UP LOAD
                    FileDownloadList1.GetFileDownload(Convert.ToInt32(Id));
                }
            }
        }

        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Save")
            {
                var cBulletinBoard = new CBulletinBoard();
                var bulletinBoard = cBulletinBoard.Get(Id);

                if (bulletinBoard == null)
                {
                    bulletinBoard = new BulletinBoard();
                    bulletinBoard.CreatedDate = DateTime.Now;
                    bulletinBoard.CreatedId = CurrentUserId;

                    bulletinBoard.Subject = RadTextBoxSubject.Text;
                    bulletinBoard.Body = RadEditorBody.Text;

                    bulletinBoard.Views = 0;

                    ViewState["NewIndex"] = cBulletinBoard.Add(bulletinBoard);
                }
                else
                {
                    bulletinBoard.UpdatedDate = DateTime.Now;
                    bulletinBoard.UpdatedId = CurrentUserId;

                    bulletinBoard.Subject = RadTextBoxSubject.Text;
                    bulletinBoard.Body = RadEditorBody.Content;

                    cBulletinBoard.Update(bulletinBoard);

                    ViewState["NewIndex"] = bulletinBoard.BulletinBoardId;
                }

                // save uploading file
                FileDownloadList1.SaveFile(Convert.ToInt32(ViewState["NewIndex"]));

                // save other tables
                RunClientScript("Close();");
            }
            // close
            else if (e.Item.Text == "Close")
            {
                RunClientScript("Close();");
            }
        }

    }
}