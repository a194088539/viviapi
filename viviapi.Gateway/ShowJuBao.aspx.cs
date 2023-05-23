using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.WebComponents.Web;

namespace viviapi.gateway
{
    public class ShowJuBao : PageBase
    {
        protected HtmlForm form1;
        protected HtmlInputText txtKey;
        protected Button btnQuery;
        protected HtmlGenericControl lblMsg;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string pwd = this.txtKey.Value.Trim();
            if (string.IsNullOrEmpty(pwd))
            {
                this.lblMsg.InnerText = "请输入查询密码";
            }
            else
            {
                JuBaoInfo modelByPwd = new viviapi.BLL.JuBao().GetModelByPwd(pwd);
                if (modelByPwd == null)
                    this.lblMsg.InnerText = "不存在此记录!";
                else if (modelByPwd.status == JuBaoStatusEnum.等待回复)
                    this.lblMsg.InnerText = "投诉 处理中!";
                else
                    this.lblMsg.InnerText = "处理结果：" + modelByPwd.checkremark;
            }
        }
    }
}
