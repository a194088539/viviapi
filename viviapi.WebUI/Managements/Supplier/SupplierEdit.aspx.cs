namespace viviapi.WebUI.Managements
{
    using System;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public partial class SupplierEdit : ManagePageBase
    {
        public SupplierInfo _ItemInfo = null;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int num = int.Parse(this.txtcode.Text);
            string text = this.txtname.Text;
            string str2 = this.txtlogourl.Text;
            bool flag = this.chkisbank.Checked;
            bool flag2 = this.chkiscard.Checked;
            bool flag3 = this.chkissms.Checked;
            bool flag4 = this.chkissx.Checked;
            bool flag5 = this.chkiwap.Checked;
            bool flag6 = this.chkiali.Checked;
            bool flag7 = this.chkiwx.Checked;
            bool flag8 = this.chkisdistribution.Checked;
            string str3 = this.txtdistributionUrl.Text;
            string str4 = this.txtQueryCardUrl.Text.Trim();
            string str5 = this.txtpuserid.Text;
            string str6 = this.txtpuserkey.Text;
            string str7 = this.txtpusername.Text;
            string str8 = this.txtpuserid1.Text;
            string str9 = this.txtpuserkey1.Text;
            string str10 = this.txtpuserid2.Text;
            string str11 = this.txtpuserkey2.Text;
            string str12 = this.txtpuserid3.Text;
            string str13 = this.txtpuserkey3.Text;
            string str14 = this.txtpuserid4.Text;
            string str15 = this.txtpuserkey4.Text;
            string str16 = this.txtpuserid5.Text;
            string str17 = this.txtpuserkey5.Text;
            string str18 = this.txtpurl.Text;
            string str19 = this.txtpbakurl.Text;
            string str20 = this.txtpostBankUrl.Text;
            string str21 = this.txtpostCardUrl.Text;
            string str22 = this.txtpostSMSUrl.Text;
            string str23 = this.txtdesc.Text;
            int num2 = int.Parse(this.txtsort.Text);
            bool flag9 = true;
            bool flag10 = true;
            this.ItemInfo.code = new int?(num);
            this.ItemInfo.name = text;
            this.ItemInfo.logourl = str2;
            this.ItemInfo.isbank = new bool?(flag);
            this.ItemInfo.iscard = new bool?(flag2);
            this.ItemInfo.issms = new bool?(flag3);
            this.ItemInfo.issx = new bool?(flag4);
            this.ItemInfo.iswap = new bool?(flag5);
            this.ItemInfo.isali = new bool?(flag6);
            this.ItemInfo.iswx = new bool?(flag7);
            this.ItemInfo.puserid = str5;
            this.ItemInfo.puserkey = str6;
            this.ItemInfo.pusername = str7;
            this.ItemInfo.puserid1 = str8;
            this.ItemInfo.puserkey1 = str9;
            this.ItemInfo.puserid2 = str10;
            this.ItemInfo.puserkey2 = str11;
            this.ItemInfo.puserid3 = str12;
            this.ItemInfo.puserkey3 = str13;
            this.ItemInfo.puserid4 = str14;
            this.ItemInfo.puserkey4 = str15;
            this.ItemInfo.puserid5 = str16;
            this.ItemInfo.puserkey5 = str17;
            this.ItemInfo.purl = str18;
            this.ItemInfo.pbakurl = str19;
            this.ItemInfo.postBankUrl = str20;
            this.ItemInfo.postCardUrl = str21;
            this.ItemInfo.postSMSUrl = str22;
            this.ItemInfo.desc = str23;
            this.ItemInfo.sort = new int?(num2);
            this.ItemInfo.release = new bool?(flag9);
            this.ItemInfo.issys = new bool?(flag10);
            this.ItemInfo.jumpUrl = this.txtJumpUrl.Text.Trim();
            this.ItemInfo.isdistribution = flag8;
            this.ItemInfo.distributionUrl = str3;
            this.ItemInfo.queryCardUrl = str4;
            if (!this.isUpdate)
            {
                if (SupplierFactory.Add(this.ItemInfo) > 0)
                {
                    base.AlertAndRedirect("保存成功！", "SupplierList.aspx");
                }
                else
                {
                    base.AlertAndRedirect("保存失败！");
                }
            }
            else if (SupplierFactory.Update(this.ItemInfo))
            {
                base.AlertAndRedirect("更新成功！", "SupplierList.aspx");
            }
            else
            {
                base.AlertAndRedirect("更新失败！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageFactory.CheckSecondPwd();
            this.setPower();
            if (!base.IsPostBack)
            {
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
            if (this.isUpdate && (this.ItemInfo != null))
            {
                this.txtcode.Text = this.ItemInfo.code.ToString();
                this.txtname.Text = this.ItemInfo.name;
                this.txtlogourl.Text = this.ItemInfo.logourl;
                this.chkisbank.Checked = this.ItemInfo.isbank.Value;
                this.chkiscard.Checked = this.ItemInfo.iscard.Value;
                this.chkissms.Checked = this.ItemInfo.issms.Value;
                this.chkissx.Checked = this.ItemInfo.issx.Value;
                this.chkisdistribution.Checked = this.ItemInfo.isdistribution;
                this.chkiwap.Checked = this.ItemInfo.iswap.Value;
                this.chkiali.Checked = this.ItemInfo.isali.Value;
                this.chkiwx.Checked = this.ItemInfo.iswx.Value;
                this.txtpuserid.Text = this.ItemInfo.puserid;
                this.txtpuserkey.Text = this.ItemInfo.puserkey;
                this.txtpusername.Text = this.ItemInfo.pusername;
                this.txtpuserid1.Text = this.ItemInfo.puserid1;
                this.txtpuserkey1.Text = this.ItemInfo.puserkey1;
                this.txtpuserid2.Text = this.ItemInfo.puserid2;
                this.txtpuserkey2.Text = this.ItemInfo.puserkey2;
                this.txtpuserid3.Text = this.ItemInfo.puserid3;
                this.txtpuserkey3.Text = this.ItemInfo.puserkey3;
                this.txtpuserid4.Text = this.ItemInfo.puserid4;
                this.txtpuserkey4.Text = this.ItemInfo.puserkey4;
                this.txtpuserid5.Text = this.ItemInfo.puserid5;
                this.txtpuserkey5.Text = this.ItemInfo.puserkey5;
                this.txtpurl.Text = this.ItemInfo.purl;
                this.txtpbakurl.Text = this.ItemInfo.pbakurl;
                this.txtJumpUrl.Text = this.ItemInfo.jumpUrl;
                this.txtpostBankUrl.Text = this.ItemInfo.postBankUrl;
                this.txtpostCardUrl.Text = this.ItemInfo.postCardUrl;
                this.txtQueryCardUrl.Text = this.ItemInfo.queryCardUrl;
                this.txtpostSMSUrl.Text = this.ItemInfo.postSMSUrl;
                this.txtdistributionUrl.Text = this.ItemInfo.distributionUrl;
                this.txtdesc.Text = this.ItemInfo.desc;
                this.txtsort.Text = this.ItemInfo.sort.ToString();
            }
        }

        public bool isUpdate
        {
            get
            {
                return (this.ItemInfoId > 0);
            }
        }

        public SupplierInfo ItemInfo
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        this._ItemInfo = SupplierFactory.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new SupplierInfo();
                    }
                }
                return this._ItemInfo;
            }
        }

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }
    }
}

