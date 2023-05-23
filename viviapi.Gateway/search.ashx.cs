using System;
using System.Data;
using System.Text;
using System.Web;
using viviapi.BLL.Order;
using viviapi.gateway;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.Gateway
{
    public class search : IHttpHandler
    {
        private Helper _bll = new Helper();

        public string userid
        {
            get
            {
                return WebBase.GetQueryStringString("parter", "");
            }
        }

        public string orderid
        {
            get
            {
                return WebBase.GetQueryStringString("orderid", "");
            }
        }

        public string sign
        {
            get
            {
                return WebBase.GetQueryStringString("sign", "");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string str1 = "0.00";
            string key = "";
            string str2;
            if (!this.checkParm())
            {
                str2 = "7";
            }
            else
            {
                str2 = "7";
                int o_userid = int.Parse(this.userid);
                DataRow row = (DataRow)null;
                int num = this._bll.search_check(o_userid, this.orderid, out row);
                if (num == 1 || num == 2)
                    str2 = "8";
                else if (num == 3)
                    str2 = "14";
                else if (num == 99)
                    str2 = "99";
                else if (num == 0)
                {
                    if (row == null)
                    {
                        str2 = "99";
                    }
                    else
                    {
                        key = row["apikey"].ToString();
                        if (!WebUtility.SeachMD5Check(this.orderid, this.userid, key, this.sign))
                        {
                            str2 = "3";
                        }
                        else
                        {
                            str2 = "1";
                            if (row["orderstatus"] != DBNull.Value)
                            {
                                switch (Convert.ToInt32(row["orderstatus"]))
                                {
                                    case 1:
                                        str2 = "1";
                                        break;
                                    case 2:
                                        str2 = "0";
                                        break;
                                    case 4:
                                        str2 = Convert.ToString(row["opstate"]);
                                        if (string.IsNullOrEmpty(str2))
                                            str2 = "16";
                                        break;
                                }
                            }
                            if (row["realvalue"] != DBNull.Value)
                                str1 = string.Format("{0:f2}", row["realvalue"]);
                        }
                    }
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("orderid={0}", (object)this.orderid);
            stringBuilder.AppendFormat("&opstate={0}", (object)str2);
            stringBuilder.AppendFormat("&ovalue={0}", (object)str1);
            string str3 = Cryptography.MD5(stringBuilder.ToString() + key).ToLower();
            stringBuilder.AppendFormat("&sign={0}", (object)str3);
            context.Response.Write(stringBuilder.ToString());
        }

        private bool checkParm()
        {
            if (string.IsNullOrEmpty(this.userid))
                return false;
            int result = 0;
            return int.TryParse(this.userid, out result) && !string.IsNullOrEmpty(this.orderid) && !string.IsNullOrEmpty(this.sign);
        }
    }
}
