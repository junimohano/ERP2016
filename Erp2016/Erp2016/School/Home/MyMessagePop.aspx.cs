using System;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Home
{
    public partial class MyMessagePop : PageBase //System.Web.UI.Page
    {
        private int Id { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                var cUser = new CUser();
                RadComboBoxUserName.DataSource = cUser.GetAllUserList();
                RadComboBoxUserName.DataTextField = "Name";
                RadComboBoxUserName.DataValueField = "Value";
                RadComboBoxUserName.DataBind();

                // new
                if (Request["createOrListType"] == "0")
                {
                    foreach (RadToolBarItem item in RadToolBar1.Items)
                    {
                        if (item.Text == "Reply")
                        {
                            item.Visible = false;
                            break;
                        }
                    }
                }
                // view
                else if (Request["createOrListType"] == "1")
                {
                    var cMessage = new CMessage();
                    var message = cMessage.Get(Id);

                    if (message != null)
                    {
                        string userId = string.Empty;

                        // recieved
                        if (Request["recievedOrSent"] == "0")
                        {
                            foreach (RadToolBarItem item in RadToolBar1.Items)
                            {
                                if (item.Text == "Send")
                                {
                                    item.Visible = false;
                                    break;
                                }
                            }
                            userId = message.CreatedId.ToString();

                            // set read
                            message.IsRead = true;
                            cMessage.Update(message);
                        }
                        // sent
                        else
                        {
                            foreach (RadToolBarItem item in RadToolBar1.Items)
                            {
                                if (item.Text == "Send" || item.Text == "Reply")
                                    item.Visible = false;
                            }
                            userId = message.UserId.ToString();
                        }

                        foreach (RadComboBoxItem item in RadComboBoxUserName.Items)
                        {
                            if (item.Value == userId)
                            {
                                item.Selected = true;
                                break;
                            }
                        }

                        RadEditorContent.Content = message.Content;
                        RadTextBoxDate.Text = CGlobal.GetDateFormat(message.CreatedDate);
                        RadTextBoxIsRead.Text = message.IsRead.ToString();
                    }

                    RadComboBoxUserName.Enabled = false;
                    RadEditorContent.EditModes = EditModes.Preview;
                }
                // reply
                else
                {
                    foreach (RadToolBarItem item in RadToolBar1.Items)
                    {
                        if (item.Text == "Reply")
                        {
                            item.Visible = false;
                            break;
                        }
                    }

                    foreach (RadComboBoxItem item in RadComboBoxUserName.Items)
                    {
                        if (item.Value == Id.ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                    RadComboBoxUserName.Enabled = false;
                }
            }
        }

        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Send")
            {
                var cMessage = new CMessage();
                var message = new Message();

                message = new Message();
                message.CreatedDate = DateTime.Now;
                message.CreatedId = CurrentUserId;

                message.UserId = Convert.ToInt32(RadComboBoxUserName.SelectedValue);
                message.Content = RadEditorContent.Text;
                message.IsRead = false;

                if (cMessage.Add(message) > 0)
                    // save other tables
                    RunClientScript("Close();");
                else
                    ShowMessage("Error sending message");
            }
            // reply
            else if (e.Item.Text == "Reply")
            {
                RunClientScript("ShowPop('" + RadComboBoxUserName.SelectedValue + "', '2');");
            }
            // close
            else if (e.Item.Text == "Close")
            {
                RunClientScript("Close();");
            }
        }

    }
}