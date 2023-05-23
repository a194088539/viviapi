using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace swiftpass.utils
{
    /// <summary>
    /// 客户端消息返回头
    /// </summary>
    public class ClientResponseHandler
    {
        /// <summary>
        /// 密钥
        /// </summary>
        private string key;

        /// <summary>
        /// 应答的参数
        /// </summary>
        protected Hashtable parameters;

        /// <summary>
        /// debug信息
        /// </summary>
        private string debugInfo;

        /// <summary>
        /// 原始内容
        /// </summary>
        protected string content;

        private string charset = "UTF-8";

        /// <summary>
        /// 获取服务器通知数据方式，进行参数获取
        /// </summary>
        public ClientResponseHandler()
        {
            parameters = new Hashtable();
        }

        /// <summary>
        /// 获取返回内容
        /// </summary>
        /// <returns></returns>
        public string getContent()
        {
            return this.content;
        }

        /// <summary>
        /// 设置返回内容
        /// </summary>
        /// <param name="content">XML内容</param>
        public virtual void setContent(string content)
        {
            this.content = content;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(content);
            XmlNode root = xmlDoc.SelectSingleNode("xml");
            XmlNodeList xnl = root.ChildNodes;

            foreach (XmlNode xnf in xnl)
            {
                this.setParameter(xnf.Name, xnf.InnerText);
            }
        }

        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        public string getKey()
        { return key; }

        /// <summary>
        /// 设置密钥
        /// </summary>
        /// <param name="key">密钥</param>
        public void setKey(string key)
        { this.key = key; }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="parameter">参数名</param>
        /// <returns></returns>
        public string getParameter(string parameter)
        {
            string s = (string)parameters[parameter];
            return (null == s) ? "" : s;
        }

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="parameter">参数名</param>
        /// <param name="parameterValue">参数值</param>
        public void setParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (parameters.Contains(parameter))
                {
                    parameters.Remove(parameter);
                }

                parameters.Add(parameter, parameterValue);
            }
        }

        /// <summary>
        /// 是否威富通签名,规则是:按参数名称a-z排序,遇到空值的参数不参加签名。
        /// </summary>
        /// <returns></returns>
        public virtual Boolean isTenpaySign()
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.getKey());
            string sign = MD5Util.GetMD5(sb.ToString(), getCharset()).ToLower();

            //debug信息
            this.setDebugInfo(sb.ToString() + " => sign:" + sign);
            return getParameter("sign").ToLower().Equals(sign);
        }

        /// <summary>
        /// 获取debug信息
        /// </summary>
        /// <returns></returns>
        public string getDebugInfo()
        { return debugInfo; }

        /// <summary>
        /// 设置debug信息
        /// </summary>
        /// <param name="debugInfo"></param>
        protected void setDebugInfo(String debugInfo)
        { this.debugInfo = debugInfo; }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <returns></returns>
        protected virtual string getCharset()
        {
            return this.charset;
        }

        /// <summary>
        /// 设置编码
        /// </summary>
        /// <param name="charset">编码</param>
        public void setCharset(String charset)
        {
            this.charset = charset;
        }

        /// <summary>
        /// 是否威富通签名,规则是:按参数名称a-z排序,遇到空值的参数不参加签名。
        /// </summary>
        /// <param name="akeys"></param>
        /// <returns></returns>
        public virtual Boolean _isTenpaySign(ArrayList akeys)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.getKey());
            string sign = MD5Util.GetMD5(sb.ToString(), getCharset()).ToLower();

            //debug信息
            this.setDebugInfo(sb.ToString() + " => sign:" + sign);
            return getParameter("sign").ToLower().Equals(sign);
        }

        /// <summary>
        /// 获取返回的所有参数
        /// </summary>
        /// <returns></returns>
        public Hashtable getAllParameters()
        {
            return this.parameters;
        }
    }
}
