using System;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Systems
{
    public partial class Dict : PageBase
    {
        public Dict() : base((int)CConstValue.Menu.Dictionary)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void TypeChange(object sender, EventArgs e)
        {
            var ddlSearchType = (RadComboBox)sender;

            if (ddlSearchType.SelectedValue != "")
            {
                GetDicts(Convert.ToInt32(ddlSearchType.SelectedValue));
                ddlType.SelectedValue = ddlSearchType.SelectedValue;
                Reset();
            }
        }

        protected void GetDicts(int typeid)
        {
            CDict cDict = new CDict();
            lbDicts.DataSource = cDict.GetDictList(Convert.ToInt32(typeid), false, true);
            lbDicts.DataValueField = "Comment";
            lbDicts.DataBind();
        }


        protected void SelectDict(object sender, EventArgs e)
        {
            if (lbDicts.SelectedValue != "")
            {
                var dict = (new CDict()).Get(Convert.ToInt32(lbDicts.SelectedValue));

                hfID.Value = dict.DictId.ToString();
                tbName.Text = dict.Name;
                tbValue.Text = dict.Value.ToString();
                ddlType.SelectedValue = dict.DictType.ToString();
                tbAbbreviation.Text = dict.Abbreviation;
                tbComment.Text = dict.Comment;

                btDelete.Visible = true;
            }
        }

        protected void NewClick(object sender, EventArgs e)
        {
            Reset();
            lbDicts.SelectedIndex = -1;
        }

        protected void SaveClick(object sender, EventArgs e)
        {
            if (hfID.Value == "")
            {
                var cDict = new CDict();
                var dict = new Erp2016.Lib.Dict();
                dict.Name = tbName.Text;
                try
                {
                    dict.Value = Convert.ToInt32(tbValue.Text);
                }
                catch (Exception)
                {
                }
                dict.DictType = Convert.ToInt32(ddlType.SelectedValue);

                dict.Abbreviation = tbAbbreviation.Text;
                dict.Comment = tbComment.Text;
                dict.CreatedId = CurrentUserId;
                dict.CreatedDate = DateTime.Now;

                if (cDict.Add(dict) > 0)
                {
                    GetDicts(dict.DictType);
                    hfID.Value = dict.DictId.ToString();
                    btDelete.Visible = true;
                    lbDicts.SelectedValue = dict.DictId.ToString();
                }
            }
            else
            {
                var cDict = new CDict();
                var dict = cDict.Get(Convert.ToInt32(hfID.Value));

                dict.Name = tbName.Text;
                try
                {
                    dict.Value = Convert.ToInt32(tbValue.Text);
                }
                catch (Exception)
                {
                }
                dict.DictType = Convert.ToInt32(ddlType.SelectedValue);

                dict.Abbreviation = tbAbbreviation.Text;
                dict.Comment = tbComment.Text;

                dict.UpdatedId = CurrentUserId;
                dict.UpdatedDate = DateTime.Now;

                if (cDict.Update(dict))
                {
                    GetDicts(dict.DictType);
                    lbDicts.SelectedValue = dict.DictId.ToString();
                }
            }
        }

        protected void DeleteClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfID.Value))
            {
                var cDict = new CDict();
                var dict = cDict.Get(Convert.ToInt32(hfID.Value));

                var type = dict.DictType;

                if (cDict.Delete(dict))
                {
                    Reset();
                    GetDicts(type);
                }
            }
        }

        protected void Reset()
        {
            hfID.Value = "";
            tbName.Text = "";
            tbValue.Text = "";

            var ddlSearchType = (RadComboBox)RadToolBar1.Items[1].FindControl("ddlSearchType");

            if (ddlSearchType.SelectedValue == "")
            {
                ddlType.SelectedIndex = 0;
            }
            else
            {
                ddlType.SelectedValue = ddlSearchType.SelectedValue;
            }

            tbAbbreviation.Text = "";
            tbComment.Text = "";
            btDelete.Visible = false;
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                btSave.Enabled = false;
                btDelete.Enabled = false;
                btNew.Enabled = false;
            }
        }
    }
}