using System;
using System.Web.UI;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace App_Data
{
    public partial class FileDownloadList : UserControl
    {
        private int UploadType
        {
            get
            {
                return (int)ViewState["UploadTypeIndex"];
            }
            set { ViewState["UploadTypeIndex"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
            scriptManager.RegisterPostBackControl(RadButtonFileDownload);
        }

        public void InitFileDownloadList(int uploadType)
        {
            UploadType = uploadType;
        }

        public void SetVisibieUploadControls(bool isVisibleDeleteButton)
        {
            RadButtonFileDelete.Visible = isVisibleDeleteButton;
            AsyncUpload.Visible = isVisibleDeleteButton;
        }

        public void GetFileDownload(int downloadIndex)
        {
            var uUploadFile = new CUploadFile();
            var fileList = uUploadFile.GetUploadFiles(UploadType, downloadIndex);
            RadListBoxFileInfo.Items.Clear();
            foreach (var file in fileList)
            {
                RadListBoxFileInfo.Items.Add(new RadListBoxItem(file.Name, file.UploadFileId.ToString()));
            }
        }

        public void SaveFile(int newIndex)
        {
            // delete list of hidden files in listbox
            var cUploadFile = new CUploadFile();
            foreach (RadListBoxItem item in RadListBoxFileInfo.Items)
            {
                if (item.Enabled == false)
                {
                    cUploadFile.DelUploadFile(Convert.ToInt32(item.Value));
                }
            }

            // upload on basepage
            ((PageBase)Page).UploadFiles(AsyncUpload, UploadType, newIndex);
        }


        protected void RadButtonFileDownload_Click(object sender, EventArgs e)
        {
            if (RadListBoxFileInfo.SelectedItems.Count > 0)
            {
                ((PageBase)Page).DownloadFile(Convert.ToInt32(RadListBoxFileInfo.SelectedItems[0].Value));
            }
        }

        protected void RadButtonFileDelete_Click(object sender, EventArgs e)
        {
            foreach (var item in RadListBoxFileInfo.SelectedItems)
            {
                item.Enabled = false;
            }
        }

        public int GetFileInfoCountForHire()
        {
            return RadListBoxFileInfo.Items.Count;
        }

        public RadAsyncUpload GetAsyncUpload()
        {
            return AsyncUpload;
        }
    }
}