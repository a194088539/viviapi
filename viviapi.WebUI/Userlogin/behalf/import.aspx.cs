namespace viviapi.WebUI.Userlogin.behalf
{
    using Aspose.Cells;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.Settled;
    using viviapi.Cache;
    using viviapi.ETAPI;
    using viviapi.Model.Settled;
    using viviapi.WebComponents.Web;
    using viviLib.Security;
    using viviLib.Web;

    public class import : UserPageBase
    {
        private viviapi.BLL.Withdraw.settledAgentSummary _bll = new viviapi.BLL.Withdraw.settledAgentSummary();
        private viviapi.BLL.Withdraw.channelwithdraw _chnlbll = new viviapi.BLL.Withdraw.channelwithdraw();
        private TocashSchemeInfo _scheme = null;
        private viviapi.BLL.Withdraw.settledAgent _setAntBLL = new viviapi.BLL.Withdraw.settledAgent();
        protected Button btnupload;
        protected HtmlGenericControl callinfo;
        protected CheckBox cbx_sure;
        protected FileUpload file_data;
        protected HtmlForm form1;
        //protected TextBox TextEmail;
        protected TextBox txtcashpwd;

        protected void btnupload_Click(object sender, EventArgs e)
        {
            string str = this.txtcashpwd.Text.Trim();
            //string str2 = this.TextEmail.Text.Trim();
            string parmValue = this.GetParmValue("phone");
            string objId = "PHONE_VALID_" + parmValue;
            string str5 = (string)WebCache.GetCacheService().RetrieveObject(objId);
            if (base.currentUser.isagentDistribution == 0)
            {
                base.AlertAndRedirect("未开通此功能！请先提交申请！");
            }
            else if (this.scheme == null)
            {
                base.AlertAndRedirect("未设置提现方案，请联系商务！");
            }
            else
            {
                if (string.IsNullOrEmpty(str))
                {
                    base.AlertAndRedirect("请输入您的提现密码");
                }
                else if (Cryptography.MD5(str) != base.currentUser.Password2)
                {
                    base.AlertAndRedirect("提现密码不正确");
                }
                //else if (string.IsNullOrEmpty(str2))
                //{
                //base.AlertAndRedirect("邮箱验证码不能为空");
                //}
                //else if (str2 != str5)
                //{
                //base.AlertAndRedirect("邮箱验证码不正确!");
                //}
                string str6 = this.check();
                Stream fileContent = this.file_data.FileContent;
                BinaryReader reader = new BinaryReader(fileContent);
                string str7 = "";
                try
                {
                    str7 = reader.ReadByte().ToString();
                    str7 = str7 + reader.ReadByte().ToString();
                }
                catch
                {
                }
                if ((str7 != "8075") && (str7 != "208207"))
                {
                    base.AlertAndRedirect("对不起，文件格式不正确。");
                }
                else if (!string.IsNullOrEmpty(str6))
                {
                    base.AlertAndRedirect(str6);
                }
                else
                {
                    try
                    {
                        LoadOptions loadOptions = new LoadOptions(LoadFormat.Xlsx);
                        Workbook workbook = new Workbook(fileContent, loadOptions);
                        Aspose.Cells.Cells cells = workbook.Worksheets[0].Cells;
                        DataTable table = cells.ExportDataTable(9, 0, cells.MaxDataRow, 7);
                        reader.Close();
                        fileContent.Close();
                        if ((table == null) || (table.Rows.Count <= 0))
                        {
                            base.AlertAndRedirect("无代发数据.你检查文件格式是否修改");
                        }
                        else
                        {
                            viviapi.Model.Withdraw.settledAgentSummary summarymodel = new viviapi.Model.Withdraw.settledAgentSummary();
                            summarymodel.userid = base.UserId;
                            summarymodel.lotno = this._bll.Generatelotno();
                            List<viviapi.Model.Withdraw.settledAgent> itemlist = new List<viviapi.Model.Withdraw.settledAgent>();
                            int num2 = 0;
                            int num3 = 0;
                            decimal result = 0M;
                            decimal tamount = 0M;
                            decimal num6 = 0M;
                            decimal chargeleastofeach = 0M;
                            int supplier = 0;
                            string bankName = string.Empty;
                            string bankCode = string.Empty;
                            string str10 = string.Empty;
                            string str11 = string.Empty;
                            string str12 = string.Empty;
                            string s = string.Empty;
                            string str14 = string.Empty;
                            foreach (DataRow row in table.Rows)
                            {
                                str14 = string.Empty;
                                bool flag = false;
                                bankName = string.Empty;
                                bankCode = string.Empty;
                                if ((row[1] == null) || (row[1] == DBNull.Value))
                                {
                                    str14 = " 目标银行为空";
                                    flag = true;
                                }
                                else
                                {
                                    bankName = row[1].ToString();
                                    viviapi.Model.Withdraw.channelwithdraw modelByBankName = this._chnlbll.GetModelByBankName(bankName);
                                    if (modelByBankName == null)
                                    {
                                        flag = true;
                                        str14 = " 不支持目标银行";
                                    }
                                    else
                                    {
                                        supplier = modelByBankName.supplier;
                                        bankCode = modelByBankName.bankCode;
                                    }
                                }
                                str10 = string.Empty;
                                if (bankName != "财付通")
                                {
                                    if ((row[2] == null) || (row[2] == DBNull.Value))
                                    {
                                        str14 = str14 + " 开户网点为空";
                                        flag = true;
                                    }
                                    else
                                    {
                                        str10 = row[2].ToString();
                                        if (string.IsNullOrEmpty(str10))
                                        {
                                            str14 = str14 + " 开户网点为空";
                                            flag = true;
                                        }
                                    }
                                }
                                if ((row[3] == null) || (row[3] == DBNull.Value))
                                {
                                    str11 = string.Empty;
                                    str14 = str14 + " 开户名为空";
                                    flag = true;
                                }
                                else
                                {
                                    str11 = row[3].ToString();
                                }
                                if ((row[4] == null) || (row[4] == DBNull.Value))
                                {
                                    str12 = string.Empty;
                                    str14 = str14 + " 账号为空";
                                    flag = true;
                                }
                                else
                                {
                                    str12 = row[4].ToString();
                                }
                                result = 0M;
                                chargeleastofeach = 0M;
                                if ((row[5] == null) || (row[5] == DBNull.Value))
                                {
                                    str14 = str14 + " 代发金额为空";
                                    flag = true;
                                }
                                else
                                {
                                    s = row[5].ToString();
                                    if (!decimal.TryParse(s, out result))
                                    {
                                        str14 = str14 + " 代发金额格式不正确";
                                        flag = true;
                                    }
                                    else
                                    {
                                        tamount += result;
                                        chargeleastofeach = this.scheme.chargerate * result;
                                        if (chargeleastofeach < this.scheme.chargeleastofeach)
                                        {
                                            chargeleastofeach = this.scheme.chargeleastofeach;
                                        }
                                        else if (chargeleastofeach > this.scheme.chargemostofeach)
                                        {
                                            chargeleastofeach = this.scheme.chargemostofeach;
                                        }
                                        num6 += chargeleastofeach;
                                    }
                                }
                                if (((!string.IsNullOrEmpty(bankName) && !string.IsNullOrEmpty(str12)) && !string.IsNullOrEmpty(str11)) && !string.IsNullOrEmpty(s))
                                {
                                    num2++;
                                    viviapi.Model.Withdraw.settledAgent item = new viviapi.Model.Withdraw.settledAgent();
                                    item.lotno = summarymodel.lotno;
                                    item.serial = num2;
                                    item.amount = result;
                                    item.bankAccount = str12;
                                    item.bankAccountName = str11;
                                    item.bankBranch = str10;
                                    item.bankName = bankName;
                                    item.bankCode = bankCode;
                                    if ((row[6] != null) && (row[6] != DBNull.Value))
                                    {
                                        item.ext1 = row[6].ToString();
                                    }
                                    item.charge = chargeleastofeach;
                                    item.mode = 2;
                                    item.out_trade_no = summarymodel.lotno;
                                    item.remark = str14;
                                    item.return_url = string.Empty;
                                    item.service = string.Empty;
                                    item.input_charset = string.Empty;
                                    item.sign_type = string.Empty;
                                    item.userid = base.UserId;
                                    item.trade_no = this._setAntBLL.GenerateTradeNo(base.UserId, item.serial);
                                    item.is_cancel = flag;
                                    item.suppid = 0;
                                    if (!flag && (this.scheme.vaiInterface == 1))
                                    {
                                        item.suppid = supplier;
                                        if (this.scheme.tranRequiredAudit == 0)
                                        {
                                            item.audit_status = 2;
                                            item.auditTime = new DateTime?(DateTime.Now);
                                            item.auditUser = 0;
                                            item.auditUserName = "自动审核";
                                        }
                                    }
                                    itemlist.Add(item);
                                }
                            }
                            summarymodel.qty = num2;
                            summarymodel.amt = tamount;
                            summarymodel.fee = num6;
                            if ((tamount <= 0M) || (num2 <= 0))
                            {
                                base.AlertAndRedirect("无有效代发数据，请检查文件");
                            }
                            else
                            {
                                str6 = string.Empty;
                                switch (this._bll.ChkParms(base.UserId, tamount))
                                {
                                    case 1:
                                        str6 = "用户状态不正常。";
                                        break;

                                    case 2:
                                        str6 = "商户未签约当前产品。";
                                        break;

                                    case 3:
                                        str6 = "未设置提现方案。";
                                        break;

                                    case 4:
                                        str6 = "不能低于最小允许金额。";
                                        break;

                                    case 5:
                                        str6 = "不能超过最大允许金额。";
                                        break;

                                    case 6:
                                        str6 = "提现金额超过余额。";
                                        break;

                                    case 0x63:
                                        str6 = "未知错误。";
                                        break;
                                }
                                if (!string.IsNullOrEmpty(str6))
                                {
                                    summarymodel.status = 3;
                                    summarymodel.success = 4;
                                    summarymodel.audit_status = 3;
                                    summarymodel.auditTime = new DateTime?(DateTime.Now);
                                    summarymodel.auditUser = new int?(base.UserId);
                                    summarymodel.auditUserName = "system";
                                    summarymodel.remark = str6;
                                    foreach (viviapi.Model.Withdraw.settledAgent agent2 in itemlist)
                                    {
                                        agent2.is_cancel = true;
                                        agent2.remark = str6;
                                    }
                                }
                                else
                                {
                                    foreach (viviapi.Model.Withdraw.settledAgent agent2 in itemlist)
                                    {
                                        if (!agent2.is_cancel)
                                        {
                                            num3++;
                                        }
                                    }
                                    if (num3 == 0)
                                    {
                                        summarymodel.status = 3;
                                        summarymodel.success = 4;
                                    }
                                }
                                if (this._bll.Insert(summarymodel, itemlist) <= 0)
                                {
                                    base.AlertAndRedirect("上传出错。");
                                }
                                else
                                {
                                    if ((this.scheme.vaiInterface == 1) && (this.scheme.tranRequiredAudit == 0))
                                    {
                                        foreach (viviapi.Model.Withdraw.settledAgent agent2 in itemlist)
                                        {
                                            if (agent2.audit_status == 2)
                                            {
                                                Withdraw.InitDistribution2(agent2);
                                            }
                                        }
                                    }
                                    base.AlertAndRedirect("上传成功", "importlist.aspx");
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        base.AlertAndRedirect(exception.Message);
                    }
                }
            }
        }

        protected void cbx_sure_CheckedChanged(object sender, EventArgs e)
        {
            this.sure();
        }

        private string check()
        {
            if (!this.file_data.HasFile)
            {
                return "请选择要上传的文件";
            }
            bool flag = false;
            string str = Path.GetExtension(this.file_data.FileName).ToLower();
            string[] strArray = new string[] { ".xls", ".xlsx" };
            for (int i = 0; i < strArray.Length; i++)
            {
                if (str == strArray[i])
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                return "对不起，文件格式不正确。";
            }
            if (this.file_data.PostedFile.ContentLength > 0x400000)
            {
                return "对不起，文件不可大于4M。";
            }
            return "";
        }

        private string GetParmValue(string caption)
        {
            return WebBase.GetFormString(caption, "");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.sure();
            }
        }

        private void sure()
        {
            bool flag = this.cbx_sure.Checked;
            this.file_data.Enabled = flag;
            this.btnupload.Enabled = flag;
        }

        protected TocashSchemeInfo scheme
        {
            get
            {
                if (this._scheme == null)
                {
                    this._scheme = TocashScheme.GetModelByUser(2, base.UserId);
                }
                return this._scheme;
            }
        }
    }
}

