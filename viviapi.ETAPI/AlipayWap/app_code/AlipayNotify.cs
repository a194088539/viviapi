using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using viviapi.BLL;
using viviapi.Model;

namespace viviapi.ETAPI.AlipayWap
{
    public class Notify
    {
        private string _partner = "";
        private string _key = "";
        private string _input_charset = "";
        private string _sign_type = "";
        private string Https_veryfy_url = "https://mapi.alipay.com/gateway.do?service=notify_verify&";
        private SupplierInfo _suppInfo = SupplierFactory.GetCacheModel(101);

        public Notify()
        {
            this._partner = Config.partner.Trim();
            this._key = this._suppInfo.puserkey;
            this._input_charset = Config.input_charset.Trim().ToLower();
            this._sign_type = Config.sign_type.Trim().ToUpper();
        }

        public static string getPublicKeyStr(string Path)
        {
            StreamReader streamReader = new StreamReader(Path);
            string str = streamReader.ReadToEnd();
            streamReader.Close();
            if (str != null)
                str = str.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\r", "").Replace("\n", "");
            return str;
        }

        public bool Verify(SortedDictionary<string, string> inputPara, string notify_id, string sign)
        {
            bool signVeryfy = this.GetSignVeryfy(inputPara, sign);
            string str = "false";
            if (notify_id != null && notify_id != "")
                str = this.GetResponseTxt(notify_id);
            return str == "true" && signVeryfy;
        }

        private string GetPreSignStr(SortedDictionary<string, string> inputPara)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            return Core.CreateLinkString(Core.FilterPara(inputPara));
        }

        private bool GetSignVeryfy(SortedDictionary<string, string> inputPara, string sign)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string linkString = Core.CreateLinkString(Core.FilterPara(inputPara));
            bool flag = false;
            if (sign != null && sign != "")
            {
                switch (this._sign_type)
                {
                    case "MD5":
                        flag = AlipayMD5.Verify(linkString, sign, this._key, this._input_charset);
                        break;
                }
            }
            return flag;
        }

        private string GetResponseTxt(string notify_id)
        {
            return this.Get_Http(this.Https_veryfy_url + "partner=" + this._partner + "&notify_id=" + notify_id, 120000);
        }

        private string Get_Http(string strUrl, int timeout)
        {
            string str;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                httpWebRequest.Timeout = timeout;
                StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.Default);
                StringBuilder stringBuilder = new StringBuilder();
                while (-1 != streamReader.Peek())
                    stringBuilder.Append(streamReader.ReadLine());
                str = ((object)stringBuilder).ToString();
            }
            catch (Exception ex)
            {
                str = "错误：" + ex.Message;
            }
            return str;
        }
    }
}
