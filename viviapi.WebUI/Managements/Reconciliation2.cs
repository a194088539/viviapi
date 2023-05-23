namespace viviapi.WebUI.Managements
{
    using Newtonsoft.Json;
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Order;
    using viviapi.ETAPI;
    using viviapi.ETAPI.ShenZhouFu;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Logging;

    public class Reconciliation2 : ManagePageBase
    {
        protected Button btn_Reconciliation;
        protected Button btn_search;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Repeater rptOrders;
        protected TextBox StimeBox;

        private void Bind()
        {
            DateTime sdt = DateTime.Parse(this.StimeBox.Text);
            DateTime edt = DateTime.Parse(this.EtimeBox.Text);
            DataTable table = Dal.GetFailOrders2(sdt, edt);
            this.rptOrders.DataSource = table;
            this.rptOrders.DataBind();
        }

        protected void btn_Reconciliation_Click(object sender, EventArgs e)
        {
            this.ProcessNotify();
            base.AlertAndRedirect("执行完成。");
            this.Bind();
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            this.Bind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.AddHours(-24.0).ToString("yyyy-MM-dd HH:mm:ss");
                this.EtimeBox.Text = DateTime.Now.AddSeconds(-2.0).ToString("yyyy-MM-dd HH:mm:ss");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})");
            }
        }

        private void ProcessNotify()
        {
            DateTime sdt = DateTime.Parse(this.StimeBox.Text);
            DateTime edt = DateTime.Parse(this.EtimeBox.Text);
            DataTable table = Dal.GetFailOrders2(sdt, edt);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int num = Convert.ToInt32(row["supplierID"]);
                    string orderid = row["orderid"].ToString();
                    string callback = string.Empty;
                    switch (num)
                    {
                        case 70:
                            {
                                Cared70 cared = new Cared70();
                                callback = cared.Query(orderid);
                                cared.Finish(orderid, callback);
                                break;
                            }
                        case 80:
                            {
                                OfCard card = new OfCard();
                                callback = card.Query(orderid);
                                card.Finish(callback);
                                break;
                            }
                        case 0x55:
                            {
                                viviapi.ETAPI.huiyuan.Card card2 = new viviapi.ETAPI.huiyuan.Card();
                                callback = card2.Query(orderid);
                                if (!string.IsNullOrEmpty(callback))
                                {
                                    bool flag = card2.Finish(callback);
                                }
                                break;
                            }
                        case 0x56:
                            {
                                card card3 = new card();
                                callback = card3.Query(orderid);
                                if (!string.IsNullOrEmpty(callback))
                                {
                                    queryresult queryresult = (queryresult)JsonConvert.DeserializeObject(callback, typeof(queryresult));
                                    if ((queryresult != null) && (queryresult.queryResult == "000"))
                                    {
                                        foreach (orderitem orderitem in queryresult.orders)
                                        {
                                            card3.Finish(orderitem.orderId, orderitem.cardNo, orderitem.payStatus, orderitem.payMoney);
                                        }
                                    }
                                }
                                break;
                            }
                        case 700:
                            {
                                viviapi.ETAPI.Longbao.Card card4 = new viviapi.ETAPI.Longbao.Card();
                                callback = card4.Query(orderid);
                                if (!string.IsNullOrEmpty(callback))
                                {
                                    card4.Finish(callback);
                                }
                                break;
                            }
                        case 0xedc2:
                            {
                                Card60866 card5 = new Card60866();
                                callback = card5.Query(orderid);
                                LogHelper.Write(callback);
                                card5.Finish(orderid, callback);
                                break;
                            }
                    }
                }
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.System))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }
    }
}

