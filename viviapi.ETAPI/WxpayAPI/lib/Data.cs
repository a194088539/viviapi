using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using viviapi.BLL;
using viviapi.Model;

namespace viviapi.ETAPI.WxPayAPI
{
    public class WxPayData
    {
        private SupplierInfo suppInfo = SupplierFactory.GetCacheModel(99);
        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();

        public void SetValue(string key, object value)
        {
            this.m_values[key] = value;
        }

        public object GetValue(string key)
        {
            object obj = (object)null;
            this.m_values.TryGetValue(key, out obj);
            return obj;
        }

        public bool IsSet(string key)
        {
            object obj = (object)null;
            this.m_values.TryGetValue(key, out obj);
            return null != obj;
        }

        public string ToXml()
        {
            if (0 == this.m_values.Count)
            {
                Log.Error(this.GetType().ToString(), "WxPayData数据为空!");
                throw new WxPayException("WxPayData数据为空!");
            }
            else
            {
                string str = "<xml>";
                foreach (KeyValuePair<string, object> keyValuePair in this.m_values)
                {
                    if (keyValuePair.Value == null)
                    {
                        Log.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                        throw new WxPayException("WxPayData内部含有值为null的字段!");
                    }
                    else if (keyValuePair.Value.GetType() == typeof(int))
                        str = str + (object)"<" + keyValuePair.Key + ">" + (string)keyValuePair.Value + "</" + keyValuePair.Key + ">";
                    else if (keyValuePair.Value.GetType() == typeof(string))
                    {
                        str = str + (object)"<" + keyValuePair.Key + "><![CDATA[" + (string)keyValuePair.Value + "]]></" + keyValuePair.Key + ">";
                    }
                    else
                    {
                        Log.Error(this.GetType().ToString(), "WxPayData字段数据类型错误!");
                        throw new WxPayException("WxPayData字段数据类型错误!");
                    }
                }
                return str + "</xml>";
            }
        }

        public SortedDictionary<string, object> FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                Log.Error(this.GetType().ToString(), "将空的xml串转换为WxPayData不合法!");
                throw new WxPayException("将空的xml串转换为WxPayData不合法!");
            }
            else
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xml);
                foreach (XmlElement xmlElement in xmlDocument.FirstChild.ChildNodes)
                    this.m_values[xmlElement.Name] = (object)xmlElement.InnerText;
                try
                {
                    if (this.m_values["return_code"] != "SUCCESS")
                        return this.m_values;
                    this.CheckSign();
                }
                catch (WxPayException ex)
                {
                    throw new WxPayException(ex.Message);
                }
                return this.m_values;
            }
        }

        public string ToUrl()
        {
            string str = "";
            foreach (KeyValuePair<string, object> keyValuePair in this.m_values)
            {
                if (keyValuePair.Value == null)
                {
                    Log.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                    throw new WxPayException("WxPayData内部含有值为null的字段!");
                }
                else if (keyValuePair.Key != "sign" && keyValuePair.Value.ToString() != "")
                    str = str + (object)keyValuePair.Key + "=" + (string)keyValuePair.Value + "&";
            }
            return str.Trim('&');
        }

        public string ToPrintStr()
        {
            string str = "";
            foreach (KeyValuePair<string, object> keyValuePair in this.m_values)
            {
                if (keyValuePair.Value == null)
                {
                    Log.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                    throw new WxPayException("WxPayData内部含有值为null的字段!");
                }
                else
                    str = str + string.Format("{0}={1}<br>", (object)keyValuePair.Key, (object)keyValuePair.Value.ToString());
            }
            Log.Debug(this.GetType().ToString(), "Print in Web Page : " + str);
            return str;
        }

        public string MakeSign()
        {
            byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(this.ToUrl() + "&key=" + this.suppInfo.puserkey));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in hash)
                stringBuilder.Append(num.ToString("x2"));
            return ((object)stringBuilder).ToString().ToUpper();
        }

        public bool CheckSign()
        {
            if (!this.IsSet("sign"))
            {
                Log.Error(this.GetType().ToString(), "WxPayData签名存在但不合法!");
                throw new WxPayException("WxPayData签名存在但不合法!");
            }
            else if (this.GetValue("sign") == null || this.GetValue("sign").ToString() == "")
            {
                Log.Error(this.GetType().ToString(), "WxPayData签名存在但不合法!");
                throw new WxPayException("WxPayData签名存在但不合法!");
            }
            else
            {
                if (this.MakeSign() == this.GetValue("sign").ToString())
                    return true;
                Log.Error(this.GetType().ToString(), "WxPayData签名验证错误!");
                throw new WxPayException("WxPayData签名验证错误!");
            }
        }

        public SortedDictionary<string, object> GetValues()
        {
            return this.m_values;
        }
    }
}
