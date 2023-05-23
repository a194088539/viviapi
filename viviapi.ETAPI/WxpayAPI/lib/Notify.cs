using System.IO;
using System.Text;
using System.Web;

namespace viviapi.ETAPI.WxPayAPI
{
    public class Notify
    {
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

        public virtual void ProcessNotify()
        {
        }
    }
}
