namespace viviapi.WebUI.Managements
{
    using System;
    using System.Data;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.Model;
    using viviapi.Model.Channel;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public partial class ChannelEdit : ManagePageBase
    {
        public ChannelInfo _ItemInfo = null;
        public ChannelTypeInfo _typeInfo = null;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string text = this.txtcode.Text;
            int result = -1;
            int.TryParse(this.rblOpen.SelectedValue, out result);
            int num2 = -1;
            int.TryParse(this.ddlSupp.SelectedValue, out num2);
            string str2 = this.txtmodeName.Text;
            int num3 = int.Parse(this.txtfaceValue.Text);
            int num4 = 0;
            int.TryParse(this.ddlType.SelectedValue, out num4);
            int num5 = int.Parse(this.txtsort.Text);
            this.model.code = text;
            this.model.typeId = num4;
            this.model.modeName = str2;
            this.model.faceValue = num3;
            if (result == -1)
            {
                this.model.isOpen = null;
            }
            else
            {
                this.model.isOpen = new int?(result);
            }
            if (num2 == -1)
            {
                this.model.supplier = null;
            }
            else
            {
                this.model.supplier = new int?(num2);
            }
            this.model.addtime = DateTime.Now;
            this.model.sort = new int?(num5);
            this.model.modeEnName = this.txtenmodeName.Text;
            string url = "ChannelList.aspx";
            if (this.Session["selecttype"] != null)
            {
                url = url + "?typeid=" + this.Session["selecttype"].ToString();
            }
            if (!this.isUpdate)
            {
                if (viviapi.BLL.Channel.Channel.Add(this.model) > 0)
                {
                    base.AlertAndRedirect("保存成功！", url);
                }
                else
                {
                    base.AlertAndRedirect("保存失败！");
                }
            }
            else if (viviapi.BLL.Channel.Channel.Update(this.model))
            {
                base.AlertAndRedirect("更新成功！", url);
            }
            else
            {
                base.AlertAndRedirect("更新失败！");
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlTypeSupp.SelectedValue = "";
            int result = 0;
            int.TryParse(this.ddlType.SelectedValue, out result);
            if (result > 0)
            {
                ChannelTypeInfo modelByTypeId = ChannelType.GetModelByTypeId(result);
                if (modelByTypeId != null)
                {
                    this.ddlTypeSupp.SelectedValue = modelByTypeId.supplier.ToString();
                    this.rblTypeOpen.SelectedValue = ((int)modelByTypeId.isOpen).ToString();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            ManageFactory.CheckSecondPwd();
            if (!base.IsPostBack)
            {
                this.ddlType.Items.Add(new ListItem("---全部类别---", ""));
                DataTable table = ChannelType.GetList(null).Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    this.ddlType.Items.Add(new ListItem(row["modetypename"].ToString(), row["typeId"].ToString()));
                }
                DataTable table2 = SupplierFactory.GetList(string.Empty).Tables[0];
                this.ddlTypeSupp.Items.Add(new ListItem("--请选择--", ""));
                foreach (DataRow row in table2.Rows)
                {
                    this.ddlTypeSupp.Items.Add(new ListItem(row["name"].ToString(), row["code"].ToString()));
                }
                this.ddlSupp.Items.Add(new ListItem("--默认--", "-1"));
                foreach (DataRow row in table2.Rows)
                {
                    this.ddlSupp.Items.Add(new ListItem(row["name"].ToString(), row["code"].ToString()));
                }
                this.ShowInfo();
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Interfaces))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        private void ShowInfo()
        {
            if (this.isUpdate && (this.model != null))
            {
                this.txtcode.Text = this.model.code;
                this.ddlType.SelectedValue = this.model.typeId.ToString();
                this.txtmodeName.Text = this.model.modeName;
                this.txtfaceValue.Text = this.model.faceValue.ToString();
                this.rblTypeOpen.SelectedValue = ((int)this.typeInfo.isOpen).ToString();
                this.txtsort.Text = this.model.sort.ToString();
                this.txtenmodeName.Text = this.model.modeEnName;
                this.ddlTypeSupp.SelectedValue = this.typeInfo.supplier.ToString();
                if (this.model.supplier.HasValue)
                {
                    this.ddlSupp.SelectedValue = this.model.supplier.Value.ToString();
                }
                if (this.model.isOpen.HasValue)
                {
                    this.rblOpen.SelectedValue = this.model.isOpen.Value.ToString();
                }
            }
            this.ddlTypeSupp.Enabled = false;
            this.rblTypeOpen.Enabled = false;
        }

        public bool isUpdate
        {
            get
            {
                return (this.ItemInfoId > 0);
            }
        }

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public ChannelInfo model
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        this._ItemInfo = viviapi.BLL.Channel.Channel.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new ChannelInfo();
                    }
                }
                return this._ItemInfo;
            }
        }

        public ChannelTypeInfo typeInfo
        {
            get
            {
                if (this._typeInfo == null)
                {
                    if (this.model != null)
                    {
                        this._typeInfo = ChannelType.GetModelByTypeId(this.model.typeId);
                    }
                    else
                    {
                        this._typeInfo = new ChannelTypeInfo();
                    }
                }
                return this._typeInfo;
            }
        }
    }
}

