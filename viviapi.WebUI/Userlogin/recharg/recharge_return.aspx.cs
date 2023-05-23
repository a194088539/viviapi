namespace viviapi.WebUI.Userlogin.recharg
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.WebComponents.Web;
    using viviLib.Security;

    public class recharge_return : UserPageBase
    {
        protected HtmlForm form1;
        protected Literal Literal1;
        protected Literal litphone;
        protected string successAmt = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            bool flag = false;
            string aPIKey = UserFactory.GetModel(base.currentUser.ID).APIKey;
            string str2 = base.Request["orderid"];
            string str3 = base.Request["opstate"];
            string str4 = base.Request["ovalue"];
            string str5 = base.Request["sign"];
            string str6 = base.Request["sysorderid"];
            string str7 = base.Request["systime"];
            string str8 = base.Request["attach"];
            string str9 = base.Request["msg"];
            string str11 = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", new object[] { str2, str3, str4, aPIKey }));
            if (str5 == str11)
            {
                this.successAmt = str4;
                if (str3.Equals("0") || str3.Equals("-3"))
                {
                    this.Literal1.Text = "<img src=\"../images/onebit_34.png\" width=\"46\" height=\"41\" />";
                    this.litphone.Text = "恭喜您!充值成功";
                    flag = true;
                }
                else if (str3.Equals("-1"))
                {
                    this.Literal1.Text = "<img src=\"../images/onebit_36.png\" width=\"46\" height=\"41\" />";
                    this.litphone.Text = "卡号密码错误";
                }
                else if (str3.Equals("-2"))
                {
                    this.Literal1.Text = "<img src=\"../images/onebit_36.png\" width=\"46\" height=\"41\" />";
                    this.litphone.Text = "卡实际面值和提交时面值不符，卡内实际面值未使用";
                }
                else if (str3.Equals("-4"))
                {
                    this.Literal1.Text = "<img src=\"../images/onebit_36.png\" width=\"46\" height=\"41\" />";
                    this.litphone.Text = "卡在提交之前已经被使";
                }
                else if (str3.Equals("-5"))
                {
                    this.Literal1.Text = "<img src=\"../images/onebit_36.png\" width=\"46\" height=\"41\" />";
                    this.litphone.Text = "充值失败，详情请看订单明细";
                }
            }
            if (flag)
            {
            }
        }
    }
}

