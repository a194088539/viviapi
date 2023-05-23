namespace viviapi.WebUI.Managements
{
    using DBAccess;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;

    public class Console_CleanUpData : Page
    {
        protected Button btnCleanUp;
        protected CheckBoxList cb_stat;
        protected CheckBoxList cb_where;
        protected CheckBoxList cbl_clearType;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected Label lbmsg;
        protected TextBox txtcaozuo;

        protected void btndel_Click(object sender, EventArgs e)
        {
            if (!ManageFactory.SecPwdVaild(this.txtcaozuo.Text.Trim()))
            {
                this.lbmsg.Text = "二级密码不正确，请重新输入!";
            }
            else
            {
                DateTime minValue = DateTime.MinValue;
                DateTime.TryParse(this.EtimeBox.Text, out minValue);
                TimeSpan span = (TimeSpan)(DateTime.Now - minValue);
                if (span.TotalDays < 7.0)
                {
                    minValue = DateTime.Now.AddDays(-7.0);
                }
                bool flag = false;
                foreach (ListItem item in this.cbl_clearType.Items)
                {
                    if (item.Selected && (item.Value == "order"))
                    {
                        flag = true;
                    }
                }
                StringBuilder builder = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                if (flag)
                {
                    bool flag2 = false;
                    bool flag3 = false;
                    bool flag4 = false;
                    foreach (ListItem item in this.cb_where.Items)
                    {
                        if (item.Selected)
                        {
                            if (item.Value == "bank")
                            {
                                builder.Append("\r\ndeclare @t table(orderid varchar(30))\r\ninsert into @t select orderid from v_orderbank where addtime<@addtime {0}\r\ndelete from orderbankamt where orderid in (select orderid from @t)\r\ndelete from orderbanknotify where orderid in (select orderid from @t)\r\ndelete from orderbank where orderid in (select orderid from @t)");
                                flag2 = true;
                            }
                            else if (item.Value == "card")
                            {
                                builder.Append("\r\ndeclare @t1 table(orderid varchar(30))\r\ninsert into @t1 select orderid from v_ordercard where addtime<@addtime {0}\r\ndelete from ordercardamt where orderid in (select orderid from @t1)\r\ndelete from ordercardnotify where orderid in (select orderid from @t1)\r\ndelete from  ordercard where orderid in (select orderid from @t1)");
                                flag3 = true;
                            }
                            else if (item.Value == "sms")
                            {
                                flag4 = true;
                            }
                        }
                    }
                    if (builder.Length > 0)
                    {
                        bool flag5 = false;
                        builder2.Append(" and status in (");
                        foreach (ListItem item in this.cb_stat.Items)
                        {
                            if (item.Selected)
                            {
                                if (!flag5)
                                {
                                    builder2.Append(item.Value);
                                }
                                else
                                {
                                    builder2.Append("," + item.Value);
                                }
                                flag5 = true;
                            }
                        }
                        builder2.Append(")");
                        if (!flag5)
                        {
                            builder = new StringBuilder();
                        }
                        else
                        {
                            builder.Replace("{0}", builder2.ToString());
                        }
                    }
                }
                if (builder.Length > 0)
                {
                    try
                    {
                        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@addtime", SqlDbType.DateTime, 8) };
                        commandParameters[0].Value = minValue;
                        if (DataBase.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0)
                        {
                            this.lbmsg.Text = "清理成功";
                        }
                        else
                        {
                            this.lbmsg.Text = "清理失败";
                        }
                    }
                    catch (Exception exception)
                    {
                        this.lbmsg.Text = exception.Message;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageFactory.CheckSecondPwd();
            if (!base.IsPostBack)
            {
                this.EtimeBox.Text = DateTime.Now.AddDays(-7.0).ToString("yyyy-MM-dd");
                this.EtimeBox.Attributes.Add("onFocus", string.Format("WdatePicker({{maxDate:'{0}'}})", DateTime.Now.AddDays(-7.0).ToString("yyyy-MM-dd 00:00:00")));
            }
        }
    }
}

