using System;
using System.IO;
using System.Text;
using System.Web;

namespace viviapi.BLL
{
    public class BaseFactory
    {
        private string _otherinfo = string.Empty;
        private int _page = 0;
        private int _pagesize = 25;
        private int _total = 0;

        public string OtherInfo
        {
            get
            {
                return this._otherinfo;
            }
            set
            {
                this._otherinfo = value;
            }
        }

        public int Page
        {
            get
            {
                return this._page;
            }
            set
            {
                this._page = value;
            }
        }

        public int PageSize
        {
            get
            {
                return this._pagesize;
            }
            set
            {
                this._pagesize = value;
            }
        }

        public int Total
        {
            get
            {
                return this._total;
            }
            set
            {
                this._total = value;
            }
        }

        public static void WriteLogs(string title, string method, string parms, string description)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("-----------------------------------------------------------------------------------------------------------\r\n");
            stringBuilder.Append("Time:" + DateTime.Now.ToString() + "\r\n");
            stringBuilder.Append("Name:" + title + "\r\n");
            stringBuilder.Append("Method:" + method + "\r\n");
            stringBuilder.Append("Parms:" + parms + "\r\n");
            stringBuilder.Append("Description:" + description + "\r\n");
            string path = HttpContext.Current.Server.MapPath("/Logs/");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            File.AppendAllText(path + DateTime.Now.ToString("yyyy-MM-dd") + ".log", stringBuilder.ToString(), Encoding.Default);
        }
    }
}
