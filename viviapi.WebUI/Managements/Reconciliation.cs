namespace viviapi.WebUI.Managements
{
    using Newtonsoft.Json;
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;
    using viviapi.BLL;
    using viviapi.ETAPI;
    using viviapi.ETAPI.ShenZhouFu;
    using viviapi.Model;
    using viviapi.WebComponents.Web;

    public class Reconciliation : ManagePageBase
    {
        protected Button btn_search;
        protected DropDownList ddlsupp;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Repeater rptOrders;
        protected TextBox txtorders;

        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtorders.Text.Trim()))
            {
                base.AlertAndRedirect("请输入订单号");
            }
            else
            {
                DataRow row;
                DataTable table = new DataTable();
                table.Columns.Add("orderid", typeof(string));
                table.Columns.Add("supporder", typeof(string));
                table.Columns.Add("realamt", typeof(string));
                table.Columns.Add("result", typeof(string));
                table.Columns.Add("status", typeof(string));
                table.Columns.Add("coin", typeof(string));
                table.Columns.Add("cardtype", typeof(string));
                string str = string.Empty;
                string selectedValue = this.ddlsupp.SelectedValue;
                string[] strArray = this.txtorders.Text.Split(new char[] { '\n' });
                string orderids = string.Empty;
                foreach (string str4 in strArray)
                {
                    string[] strArray2;
                    switch (selectedValue)
                    {
                        case "70":
                            row = table.NewRow();
                            row["orderid"] = str4;
                            str = new Cared70().Query(str4);
                            if (!string.IsNullOrEmpty(str))
                            {
                                try
                                {
                                    strArray2 = str.Split(new char[] { '&' });
                                    row["status"] = strArray2[0].Replace("returncode=", "");
                                    row["realamt"] = strArray2[1].Replace("realmoney=", "");
                                    row["result"] = strArray2[2].Replace("message=", "");
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                row["result"] = "查询失败";
                            }
                            table.Rows.Add(row);
                            break;

                        case "60866":
                            row = table.NewRow();
                            row["orderid"] = str4;
                            str = new Card60866().Query(str4);
                            if (!string.IsNullOrEmpty(str))
                            {
                                try
                                {
                                    strArray2 = str.Split(new char[] { '&' });
                                    row["status"] = strArray2[0].Replace("returncode=", "");
                                    row["realamt"] = strArray2[1].Replace("realmoney=", "");
                                    row["result"] = strArray2[2].Replace("message=", "");
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                row["result"] = "查询失败";
                            }
                            table.Rows.Add(row);
                            break;

                        case "700":
                            row = table.NewRow();
                            row["orderid"] = str4;
                            str = new viviapi.ETAPI.Longbao.Card().Query(str4);
                            if (!string.IsNullOrEmpty(str))
                            {
                                try
                                {
                                    strArray2 = str.Split(new char[] { '&' });
                                    row["status"] = strArray2[0].Replace("opstate=", "");
                                    row["realamt"] = strArray2[1].Replace("ovalue=", "");
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                row["result"] = "查询失败";
                            }
                            table.Rows.Add(row);
                            goto Label_06B2;

                        case "80":
                            row = table.NewRow();
                            row["orderid"] = str4;
                            str = new OfCard().Query(str4);
                            if (!string.IsNullOrEmpty(str))
                            {
                                try
                                {
                                    XmlDocument document = new XmlDocument();
                                    document.LoadXml(str);
                                    string innerText = document.GetElementsByTagName("billid")[0].InnerText;
                                    string str6 = document.GetElementsByTagName("result")[0].InnerText;
                                    string str7 = document.GetElementsByTagName("info")[0].InnerText;
                                    string str8 = document.GetElementsByTagName("value")[0].InnerText;
                                    string str9 = document.GetElementsByTagName("accountvalue")[0].InnerText;
                                    row["supporder"] = innerText;
                                    row["realamt"] = str8;
                                    row["result"] = str7;
                                    row["status"] = str6;
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                row["result"] = "查询失败";
                            }
                            table.Rows.Add(row);
                            goto Label_06B2;

                        case "81":
                            row = table.NewRow();
                            row["orderid"] = str4;
                            str = new viviapi.ETAPI.huiyuan.Card().Query(str4);
                            if (!string.IsNullOrEmpty(str))
                            {
                                try
                                {
                                    strArray2 = str.Split(new char[] { '&' });
                                    if (strArray2.Length == 11)
                                    {
                                        string str10 = strArray2[0];
                                        string str11 = strArray2[1];
                                        string str12 = strArray2[2];
                                        string str13 = strArray2[3];
                                        string str14 = strArray2[4];
                                        string str15 = strArray2[5];
                                        string str16 = strArray2[6];
                                        string str17 = strArray2[7];
                                        string str18 = strArray2[8];
                                        string str19 = strArray2[9];
                                        string str20 = strArray2[10];
                                        row["supporder"] = str14;
                                        row["realamt"] = str16;
                                        row["result"] = str11;
                                        row["status"] = str11;
                                    }
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                row["result"] = "查询失败";
                            }
                            table.Rows.Add(row);
                            goto Label_06B2;

                        case "86":
                            orderids = orderids + str4 + "|";
                            goto Label_06B2;
                    }
                Label_06B2:;
                }
                if (selectedValue == "86")
                {
                    orderids = orderids.Substring(0, orderids.Length - 1);
                    str = new card().Query(orderids);
                    if (!string.IsNullOrEmpty(str))
                    {
                        queryresult queryresult = (queryresult)JsonConvert.DeserializeObject(str, typeof(queryresult));
                        if (queryresult == null)
                        {
                            row = table.NewRow();
                            row["orderid"] = orderids;
                            row["result"] = "查询失败";
                            table.Rows.Add(row);
                        }
                        else if (queryresult.queryResult == "001")
                        {
                            row = table.NewRow();
                            row["orderid"] = orderids;
                            row["result"] = "参数错误";
                            table.Rows.Add(row);
                        }
                        else if (queryresult.queryResult == "002")
                        {
                            row = table.NewRow();
                            row["orderid"] = orderids;
                            row["result"] = "商户不存在";
                            table.Rows.Add(row);
                        }
                        else if (queryresult.queryResult == "003")
                        {
                            row = table.NewRow();
                            row["orderid"] = orderids;
                            row["result"] = "md5校验失败";
                            table.Rows.Add(row);
                        }
                        else
                        {
                            foreach (orderitem orderitem in queryresult.orders)
                            {
                                row = table.NewRow();
                                row["orderid"] = orderitem.orderId;
                                row["supporder"] = orderitem.cardNo;
                                row["realamt"] = orderitem.payMoney;
                                if (orderitem.payStatus == "1")
                                {
                                    row["status"] = "成功";
                                }
                                else if (orderitem.payStatus == "0")
                                {
                                    row["status"] = "失败";
                                }
                                else if (orderitem.payStatus == "2")
                                {
                                    row["status"] = "处理中";
                                }
                                row["result"] = "查询成功";
                                table.Rows.Add(row);
                            }
                        }
                    }
                    else
                    {
                        row = table.NewRow();
                        row["orderid"] = orderids;
                        row["result"] = "查询失败";
                        table.Rows.Add(row);
                    }
                }
                this.rptOrders.DataSource = table;
                this.rptOrders.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
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

