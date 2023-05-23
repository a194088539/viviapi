namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviapi.WebComponents;
    using viviapi.WebComponents.Web;
    using viviLib.Security;
    using viviLib.Text;
    using viviLib.Web;

    public class modiemail : UserPageBase
    {
        protected Button btnSave;
        protected HtmlInputText txtemail;
        protected HtmlInputText txtnewemail;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            string queryStringString = WebBase.GetQueryStringString("email", string.Empty);
            string inputData = WebBase.GetQueryStringString("newemail", string.Empty);
            if (queryStringString != UserFactory.CurrentMember.Email)
            {
                msg = "当前邮件账号输入不正确 请重新输入";
            }
            else if (!PageValidate.IsEmail(inputData))
            {
                msg = "新邮箱格式不正确;";
            }
            else
            {
                EmailCheckInfo model = new EmailCheckInfo();
                model.userid = UserFactory.CurrentMember.ID;
                model.status = EmailCheckStatus.提交中;
                model.addtime = new DateTime?(DateTime.Now);
                model.checktime = new DateTime?(DateTime.Now);
                model.email = inputData;
                model.typeid = EmailCheckType.认证;
                model.Expired = DateTime.Now.AddDays(7.0);
                int num = new EmailCheck().Add(model);
                if (num > 0)
                {
                    string parms = HttpUtility.UrlEncode(Cryptography.RijndaelEncrypt(string.Format("id={0}&", num)));
                    string checkEmailUrl = this.GetCheckEmailUrl(parms);
                    StringBuilder builder = new StringBuilder();
                    builder.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");
                    builder.Append("<html>");
                    builder.Append("<head>");
                    builder.Append("<title>" + UserFactory.CurrentMember.UserName + "邮件验证</title>");
                    builder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
                    builder.Append("<style>BODY {\tPADDING-RIGHT: 10px; PADDING-LEFT: 10px; FONT-SIZE: 14px; PADDING-BOTTOM: 8px; MARGIN: 0px; LINE-HEIGHT: 25px; PADDING-TOP: 8px; FONT-FAMILY: arial,verdana,sans-serif}");
                    builder.Append("PRE {PADDING-RIGHT: 10px; PADDING-LEFT: 10px; FONT-SIZE: 14px; PADDING-BOTTOM: 8px; MARGIN: 0px; LINE-HEIGHT: 25px; PADDING-TOP: 8px; FONT-FAMILY: arial,verdana,sans-serif; WORD-WRAP: break-word}");
                    builder.Append(".rm_line {BORDER-TOP: #f1f1f1 2px solid; FONT-SIZE: 0px; MARGIN: 15px 0px}");
                    builder.Append(".atchImg IMG {BORDER-RIGHT: #c3d9ff 2px solid; BORDER-TOP: #c3d9ff 2px solid; BORDER-LEFT: #c3d9ff 2px solid; BORDER-BOTTOM: #c3d9ff 2px solid}");
                    builder.Append(".lnkTxt {COLOR: #0066cc}");
                    builder.Append(".rm_PicArea * {FONT-WEIGHT: 700; FONT-SIZE: 14px; FONT-FAMILY: Arial, sans-serif}");
                    builder.Append(".fbk3 {COLOR: #333; LINE-HEIGHT: 160%}");
                    builder.Append(".fTip {FONT-WEIGHT: normal; FONT-SIZE: 11px}");
                    builder.Append("</style>");
                    builder.Append("<meta content=\"MSHTML 6.00.6000.17092\" name=\"GENERATOR\">");
                    builder.Append("</head>");
                    builder.Append("<body>");
                    builder.Append("<table cellspacing=\"0\" width=\"100%\" bgcolor=\"#f9f9f9\" border=\"0\">");
                    builder.Append("<tbody>");
                    builder.Append("<tr>");
                    builder.Append("<td>");
                    builder.Append("<table style='font-size: 12px; margin: 20px auto' cellspacing='0' cellpadding='0'");
                    builder.Append("width='700' border='0'>");
                    builder.Append("<tbody>");
                    builder.Append("<tr>");
                    builder.Append("<td colspan='2'>");
                    builder.Append("<h1>");
                    builder.Append(" <a title='" + base.SiteName + "' href='" + base.webInfo.Domain + "' target='_blank'>");
                    builder.Append("<img src='" + base.webInfo.Domain + base.webInfo.LogoPath + "' border='0'></a></h1>");
                    builder.Append("</td>");
                    builder.Append("</tr>");
                    builder.Append(" <tr>");
                    builder.Append("<td style='padding-left: 10px' valign='center' colspan='2' height='40'>");
                    builder.Append("<font style='font-size: 14px'><strong>Hi, " + UserFactory.CurrentMember.UserName + "</strong></font></td>");
                    builder.Append("</tr>");
                    builder.Append("<tr>");
                    builder.Append("<td style='padding-left: 10px' valign='center' colspan='2' height='40'>");
                    builder.Append("<font style='font-size: 14px'>" + base.SiteName + "客户中心。</font></td>");
                    builder.Append("</tr>");
                    builder.Append("<tr>");
                    builder.Append("<td style='padding-left: 10px' colspan='2' height='40'>");
                    builder.Append("<font style='font-size: 14px'>请点击下面的链接完成邮件验证：</font></td>");
                    builder.Append("</tr>");
                    builder.Append("<tr>");
                    builder.Append(" <td style='padding-left: 10px' colspan='2'>");
                    builder.AppendFormat(" <font style='font-size: 14px'><a style='color: #0000cc; text-decoration: underline' href='{0}' target='_blank'>{0}</a></font></td>", checkEmailUrl);
                    builder.Append("</tr>");
                    builder.Append("<tr>");
                    builder.Append(" <td style='padding-left: 10px; height: 40px;' colspan='2'>");
                    builder.Append("<span style='font-size: 12px; color: #999'>如果以上链接无法点击，请将上面的地址复制到你的浏览器(如IE)的地址栏进入。</span></td>");
                    builder.Append("</tr>");
                    builder.Append(" <tr>");
                    builder.Append(" <td style='border-top: #ededed 1px solid; padding-left: 10px' colspan='2' height='35'>");
                    builder.Append("   <span style='font-weight: bold; font-size: 14px; color: #f60'>" + base.SiteName + "</span></td>");
                    builder.Append("</tr>");
                    builder.Append("</tbody>");
                    builder.Append("</table>");
                    builder.Append(" </td>");
                    builder.Append(" </tr>");
                    builder.Append(" </tbody>");
                    builder.Append("</table>");
                    builder.Append("</body>");
                    builder.Append("</html>");
                    EmailHelper helper = new EmailHelper(base.SiteName, queryStringString, queryStringString + "邮件验证", builder.ToString(), true, Encoding.GetEncoding("gbk"));
                    if (helper.Send())
                    {
                        msg = "true";
                    }
                    else
                    {
                        msg = "邮件发送失败，请联系管理员";
                    }
                }
            }
            base.AlertAndRedirect(msg);
        }

        private string GetCheckEmailUrl(string parms)
        {
            return (base.webInfo.Domain + "/Merchant/Ajax/mailCheckReceive.ashx?parms=" + parms);
        }

        private void InitForm()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitForm();
            }
        }
    }
}

