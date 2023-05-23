using System;
using System.IO;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.WxPayAPI
{
    public class NativeNotify : ETAPIBase
    {
        private static int suppId = 99;

        public NativeNotify()
          : base(NativeNotify.suppId)
        {
        }

        public WxPayData GetNotifyData(HttpContext httpContext)
        {
            Stream inputStream = httpContext.Request.InputStream;
            byte[] numArray = new byte[1024];
            StringBuilder stringBuilder = new StringBuilder();
            int count;
            while ((count = inputStream.Read(numArray, 0, 1024)) > 0)
                stringBuilder.Append(Encoding.UTF8.GetString(numArray, 0, count));
            inputStream.Flush();
            inputStream.Close();
            inputStream.Dispose();
            Log.Info(this.GetType().ToString(), "Receive data from WeChat : " + ((object)stringBuilder).ToString());
            WxPayData wxPayData1 = new WxPayData();
            try
            {
                wxPayData1.FromXml(((object)stringBuilder).ToString());
            }
            catch (WxPayException ex)
            {
                WxPayData wxPayData2 = new WxPayData();
                wxPayData2.SetValue("return_code", (object)"FAIL");
                wxPayData2.SetValue("return_msg", (object)ex.Message);
                Log.Error(this.GetType().ToString(), "Sign check error : " + wxPayData2.ToXml());
                httpContext.Response.Write(wxPayData2.ToXml());
                httpContext.Response.End();
            }
            Log.Info(this.GetType().ToString(), "Check sign success");
            return wxPayData1;
        }

        public void ProcessNotify(HttpContext httpContext)
        {
            WxPayData notifyData = this.GetNotifyData(httpContext);
            string orderId = notifyData.GetValue("out_trade_no").ToString();
            string supplierOrderId = notifyData.GetValue("transaction_id").ToString();
            string s = notifyData.GetValue("total_fee").ToString();
            string str = notifyData.GetValue("result_code").ToString();
            try
            {
                if (!(str == "SUCCESS"))
                    return;
                int status = 2;
                string opstate = "0";
                new OrderBank().DoBankComplete(NativeNotify.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s) / new Decimal(100), new Decimal(0), false, true);
                WxPayData wxPayData = new WxPayData();
                wxPayData.SetValue("return_code", (object)"SUCCESS");
                wxPayData.SetValue("return_msg", (object)"OK");
                Log.Info(this.GetType().ToString(), "UnifiedOrder success , send data to WeChat : " + wxPayData.ToXml());
                httpContext.Response.Write(wxPayData.ToXml());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
    }
}
