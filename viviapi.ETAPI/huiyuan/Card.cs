using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviapi.SysConfig;
using viviLib.ExceptionHandling;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.ETAPI.huiyuan
{
    public class Card : ETAPIBase
    {
        private static int suppId = 85;
        public string notify_url = RuntimeSetting.SiteDomain + "/notify/huiyuan/card.aspx";
        private char[] hexDigits = new char[16]
        {
      '0',
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      '7',
      '8',
      '9',
      'A',
      'B',
      'C',
      'D',
      'E',
      'F'
        };

        public Card()
          : base(Card.suppId)
        {
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string errorcode, out string errormsg)
        {
            errorcode = string.Empty;
            errormsg = string.Empty;
            string suppAccount = this.suppAccount;
            string str1 = _orderid;
            string str2 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string cardType = this.GetCardType(_typeId);
            string str3 = this.card_Encode(_cardno + "," + _cardpwd + "," + cardvalue.ToString());
            string str4 = cardvalue.ToString();
            string trueIp = ServerVariables.TrueIP;
            string str5 = string.Empty;
            string str6 = string.Empty;
            string str7 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string str8 = Card.md5sign(string.Format("agent_id={0}&bill_id={1}&bill_time={2}&card_type={3}&card_data={4}&card_amt={5}&notify_url={6}&time_stamp={7}|||{8}", (object)suppAccount, (object)str1, (object)str2, (object)cardType, (object)str3, (object)str4, (object)this.notify_url, (object)str7, (object)this.suppKey)).ToLower();
            string str9 = "-1";
            try
            {
                string @string = WebClientHelper.GetString(string.Format("{9}?agent_id={0}&bill_id={1}&bill_time={2}&card_type={3}&card_data={4}&card_amt={5}&notify_url={6}&time_stamp={7}&sign={8}", (object)suppAccount, (object)str1, (object)str2, (object)cardType, (object)str3, (object)str4, (object)this.notify_url, (object)str7, (object)str8, (object)this.postCardUrl), (NameValueCollection)null, "get", Encoding.GetEncoding("gbk"));
                str9 = "-1";
                if (!string.IsNullOrEmpty(@string))
                {
                    string[] strArray = @string.Split('&');
                    if (strArray.Length > 2)
                    {
                        errorcode = strArray[0].Replace("ret_code=", "");
                        str9 = "-1";
                        if (strArray[0] == "ret_code=0")
                            str9 = "0";
                        if (strArray[0] == "ret_code=99")
                        {
                            str9 = "0";
                            if (_typeId == 106)
                                new viviapi.BLL.order.reconciliation_temp().Add(new viviapi.Model.order.reconciliation_temp()
                                {
                                    count = new int?(0),
                                    orderid = _orderid,
                                    serverid = RuntimeSetting.ServerId.ToString()
                                });
                        }
                        errormsg = strArray[1];
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return str9;
        }

        public string Query(string orderid)
        {
            string suppAccount = this.suppAccount;
            string str1 = orderid;
            string str2 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string str3 = Card.md5sign(string.Format("agent_id={0}&bill_id={1}&time_stamp={2}|||{3}", (object)suppAccount, (object)str1, (object)str2, (object)this.suppKey)).ToLower();
            string str4 = string.Empty;
            string str5;
            try
            {
                str5 = WebClientHelper.GetString(string.Format("http://Service.800j.com/Consign/Query.aspx?agent_id={0}&bill_id={1}&time_stamp={2}&sign={3}", (object)suppAccount, (object)str1, (object)str2, (object)str3), (NameValueCollection)null, "get", Encoding.GetEncoding("gbk"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                str5 = ex.Message;
            }
            return str5;
        }

        public void Notify()
        {
            try
            {
                string suppKey = this.suppKey;
                string str1 = HttpContext.Current.Request.QueryString["ret_code"];
                string msg = HttpContext.Current.Request.QueryString["ret_msg"];
                string str2 = HttpContext.Current.Request.QueryString["agent_id"];
                string str3 = HttpContext.Current.Request.QueryString["bill_id"];
                string str4 = HttpContext.Current.Request.QueryString["jnet_bill_no"];
                string str5 = HttpContext.Current.Request.QueryString["bill_status"];
                string s = HttpContext.Current.Request.QueryString["card_real_amt"];
                string str6 = HttpContext.Current.Request.QueryString["card_settle_amt"];
                string str7 = HttpContext.Current.Request.QueryString["card_detail_data"];
                string str8 = HttpContext.Current.Request.QueryString["ext_param"];
                string str9 = HttpContext.Current.Request.QueryString["sign"];
                if (!(Card.md5sign(string.Format("ret_code={0}&agent_id={1}&bill_id={2}&jnet_bill_no={3}&bill_status={4}&card_real_amt={5}&card_settle_amt={6}&card_detail_data={7}|||{8}", (object)str1, (object)str2, (object)str3, (object)str4, (object)str5, (object)s, (object)str6, (object)str7, (object)this.suppKey)).ToLower() == str9.ToLower()))
                    return;
                int status = !(str1 == "0") || !(str5 == "1") ? 4 : 2;
                string opstate = status != 2 ? this.ConvertCode(str1) : "0";
                new OrderCard().ReceiveSuppResult(Card.suppId, str3, str3, status, opstate, msg, Decimal.Parse(s), new Decimal(0), str1);
                HttpContext.Current.Response.Write("ok");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                HttpContext.Current.Response.Write("error");
            }
        }

        public string ConvertCode(string suppcode)
        {
            string str = string.Empty;
            return !(suppcode == "9") && !(suppcode == "7") ? (!(suppcode == "10") ? "-11" : "-10") : "-1";
        }

        public string card_Encode(string src)
        {
            byte[] bytes1 = Encoding.GetEncoding("utf-8").GetBytes(this._suppInfo.puserkey1);
            byte[] bytes2 = Encoding.GetEncoding("utf-8").GetBytes(src);
            byte[] bytes3 = Encoding.GetEncoding("utf-8").GetBytes("123456");
            byte[] bytes4 = Des3.Des3EncodeECB(bytes1, bytes3, bytes2);
            if (bytes4 != null)
                return this.ToHexString(bytes4);
            return src;
        }

        public string ToHexString(byte[] bytes)
        {
            int length = bytes.Length;
            char[] chArray = new char[length * 2];
            for (int index = 0; index < length; ++index)
            {
                int num = (int)bytes[index];
                chArray[index * 2] = this.hexDigits[num >> 4];
                chArray[index * 2 + 1] = this.hexDigits[num & 15];
            }
            return new string(chArray);
        }

        public string GetCardType(int _type)
        {
            switch (_type)
            {
                case 103:
                    return "13";
                case 104:
                    return "35";
                case 105:
                    return "43";
                case 106:
                    return "10";
                case 108:
                    return "14";
                case 109:
                    return "47";
                case 110:
                    return "42";
                case 111:
                    return "44";
                case 112:
                    return "46";
                case 113:
                    return "15";
                default:
                    return _type.ToString();
            }
        }

        public static string md5sign(string str)
        {
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("GBK").GetBytes(str))).Replace("-", "").ToLower();
        }

        public bool Finish(string callback)
        {
            bool flag = false;
            try
            {
                if (!string.IsNullOrEmpty(callback))
                {
                    string[] strArray = callback.Split('&');
                    if (strArray.Length == 11)
                    {
                        string str1 = strArray[0];
                        string str2 = strArray[1];
                        string str3 = strArray[2];
                        string str4 = strArray[3];
                        string str5 = strArray[4];
                        string str6 = strArray[5];
                        string str7 = strArray[6];
                        string str8 = strArray[7];
                        string str9 = strArray[8];
                        string str10 = strArray[9];
                        if (strArray[10].Replace("sign=", "") == Card.md5sign(string.Format("{0}&{1}&{2}&{3}&{4}&{5}&{6}&{7}|||{8}", (object)str1, (object)str3, (object)str4, (object)str5, (object)str6, (object)str7, (object)str8, (object)str9, (object)this.suppKey)))
                        {
                            string orderId = str4.Replace("bill_id=", "");
                            string errtype = str1.Replace("ret_code=", "");
                            string str11 = str6.Replace("bill_status=", "");
                            string msg = str2.Replace("ret_msg=", "");
                            string supplierOrderId = str5.Replace("jnet_bill_no=", "");
                            string str12 = str7.Replace("card_real_amt=", "");
                            string opstate = "-1";
                            int status = 4;
                            if (errtype == "0")
                            {
                                if (str11 == "0")
                                    opstate = string.Empty;
                                else if (str11 == "1")
                                {
                                    opstate = "0";
                                    status = 2;
                                }
                            }
                            else if (errtype != "99")
                                opstate = "-1";
                            if (!string.IsNullOrEmpty(opstate))
                            {
                                OrderCard orderCard = new OrderCard();
                                Decimal result = new Decimal(0);
                                if (Decimal.TryParse(str12.Trim(), out result))
                                    orderCard.ReceiveSuppResult(Card.suppId, orderId, supplierOrderId, status, opstate, msg, result, new Decimal(0), errtype);
                                flag = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return flag;
        }
    }
}
