using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviapi.gateway
{
    public class JuBao : PageBase
    {
        protected HtmlForm form1;
        protected HtmlInputText txtUserName;
        protected HtmlInputText txtEmail;
        protected HtmlInputText txtMoblie;
        protected HtmlTextArea txtUrl;
        protected HtmlSelect ddlType;
        protected HtmlTextArea txtReason;
        protected HtmlGenericControl lblInfo;
        protected Button btnSub;
        protected Button btnSearch;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSub_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (this.txtUserName.Value.Trim().Length == 0)
                msg += "name不能为空！\\n";
            if (this.txtEmail.Value.Trim().Length == 0)
                msg += "email不能为空！\\n";
            if (this.txtMoblie.Value.Trim().Length == 0)
                msg += "tel不能为空！\\n";
            if (this.txtUrl.Value.Trim().Length == 0)
                msg += "url不能为空！\\n";
            if (this.txtReason.Value.Trim().Length == 0)
                msg += "remark不能为空！\\n";
            if (msg != "")
            {
                this.AlertAndRedirect(msg);
            }
            else
            {
                string str1 = this.txtUserName.Value;
                string str2 = this.txtEmail.Value;
                string str3 = this.txtMoblie.Value;
                string str4 = this.txtUrl.Value;
                int num1 = int.Parse(this.ddlType.Value);
                string str5 = this.txtReason.Value;
                DateTime now1 = DateTime.Now;
                int num2 = 1;
                DateTime now2 = DateTime.Now;
                int num3 = 0;
                string str6 = string.Empty;
                string str7 = "J" + DateTime.Now.Ticks.ToString();
                string trueIp = ServerVariables.TrueIP;
                string str8 = string.Empty;
                string str9 = string.Empty;
                JuBaoInfo model = new JuBaoInfo();
                model.name = str1;
                model.email = str2;
                model.tel = str3;
                model.url = str4;
                model.type = (JuBaoEnum)num1;
                model.remark = str5;
                model.addtime = new DateTime?(now1);
                model.status = (JuBaoStatusEnum)num2;
                model.checktime = new DateTime?(now2);
                model.check = new int?(num3);
                model.checkremark = str6;
                model.pwd = str7;
                model.field1 = trueIp;
                model.field2 = str8;
                model.field3 = str9;
                if (new viviapi.BLL.JuBao().Add(model) > 0)
                    this.lblInfo.InnerText = "举报成功，等候处理。请复制并保存查询密码：" + model.pwd;
                else
                    this.lblInfo.InnerText = "举报失败";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("ShowJuBao.aspx");
        }
    }
}
