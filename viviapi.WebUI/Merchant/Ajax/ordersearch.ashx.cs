namespace viviapi.WebUI.Merchant.Ajax
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviapi.Model.Order;
    using viviapi.Model.User;
    using viviLib.Data;

    public class ordersearch : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        public UsersAmtInfo _currentUserAmt = null;

        public string GetViewStatusName(object status)
        {
            if (status == DBNull.Value)
            {
                return string.Empty;
            }
            if (Convert.ToInt32(status) == 8)
            {
                return "失败";
            }
            return Enum.GetName(typeof(OrderStatusEnum), status);
        }

        public string GetViewSuccessAmt(object status, object amt)
        {
            if (((status != DBNull.Value) && (amt != DBNull.Value)) && (Convert.ToInt32(status) == 2))
            {
                return decimal.Round(Convert.ToDecimal(amt), 2).ToString();
            }
            return "0";
        }

        public void ProcessRequest(HttpContext context)
        {
            string s = "";
            if (UserFactory.CurrentMember == null)
            {
                s = "登录信息失效，请重新登录";
            }
            else
            {
                List<SearchParam> searchParams = new List<SearchParam>();
                searchParams.Add(new SearchParam("userid", UserFactory.CurrentMember.ID));
                OrderCard card = new OrderCard();
                DataTable table = card.PageSearch(searchParams, 8, 1, string.Empty).Tables[1];
                StringBuilder builder = new StringBuilder();
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        builder.AppendFormat("<tr>\r\n\t\t\t\t<td height=\"30\" align=\"center\" bgcolor=\"#FFFFFF\">{0}</td>\r\n\t\t\t\t<td height=\"30\" align=\"center\" bgcolor=\"#FFFFFF\">{1:0.00}</td>\r\n\t\t\t\t<td id=\"paymoney{6}\" height=\"30\" align=\"center\" bgcolor=\"#FFFFFF\">{2:0.00}</td>\r\n\t\t\t\t<td id=\"orderzt{6}\" height=\"30\" align=\"center\" bgcolor=\"#FFFFFF\">{3}</td>\r\n\t\t\t\t<td id=\"errorMsg{6}\" height=\"30\" align=\"center\" bgcolor=\"#FFFFFF\">{4}</td>\r\n\t\t\t\t<td height=\"30\" align=\"center\" bgcolor=\"#FFFFFF\"> {5}</td>\r\n\t\t\t\t<td height=\"30\" align=\"center\" bgcolor=\"#FFFFFF\"><button class=\"button_01\" id=\"sub{6}\" style=\"margin-right:0\" type=\"button\" onClick=\"checkflag('{6}')\">刷新</button></td>\r\n\t\t\t</tr>", new object[] { row["cardno"], row["refervalue"], this.GetViewSuccessAmt(row["status"], row["realvalue"]), this.GetViewStatusName(row["status"]), row["msg"], row["addtime"], row["ID"] });
                    }
                }
                s = builder.ToString();
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(s);
        }

        public UsersAmtInfo currentUserAmt
        {
            get
            {
                if ((this._currentUserAmt == null) && (UserFactory.CurrentMember != null))
                {
                    this._currentUserAmt = UsersAmt.GetModel(UserFactory.CurrentMember.ID);
                }
                return this._currentUserAmt;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

