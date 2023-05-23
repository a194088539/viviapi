namespace viviapi.WebUI.Userlogin.Ajax
{
    using Newtonsoft.Json;
    using System;
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviapi.Model.Order;
    using viviLib.Web;

    public class OrderJson_new : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
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
            string s = string.Empty;
            OrderJsonResult result = new OrderJsonResult();
            result.Success = string.Empty;
            result.paymoney = "0";
            result.errorMsg = string.Empty;
            if (UserFactory.CurrentMember != null)
            {
                OrderCardInfo model = new OrderCard().GetModel(this.oid);
                if ((model != null) && (model.userid == UserFactory.CurrentMember.ID))
                {
                    result.Success = this.GetViewStatusName(model.status);
                    result.paymoney = this.GetViewSuccessAmt(model.status, model.realvalue);
                    result.errorMsg = model.msg;
                }
            }
            s = JsonConvert.SerializeObject(result, Formatting.Indented);
            context.Response.ContentType = "application/json";
            context.Response.Write(s);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public long oid
        {
            get
            {
                return WebBase.GetQueryStringInt64("oid", 0L);
            }
        }
    }
}

