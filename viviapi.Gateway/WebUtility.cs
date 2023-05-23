using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Sys;
using viviapi.BLL.User;
using viviapi.Cache;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.gateway
{
    public class WebUtility
    {
        public static void AlertAndRedirect(Page P, string msg)
        {
            WebUtility.AlertAndRedirect(P, msg, (string)null);
        }

        public static void AlertAndRedirect(Page P, string msg, string url)
        {
            string str = string.Empty;
            string script = msg != null && msg.Length != 0 || url != null && url.Length != 0 ? (msg != null && msg.Length != 0 ? (url != null && url.Length != 0 ? string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert({0});\r\nlocation.href=\"{1}\";\r\n//--></SCRIPT>\r\n", (object)AntiXss.JavaScriptEncode(msg), (object)url) : string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert({0});\r\nlocation.href=location.href;\r\n//--></SCRIPT>\r\n", (object)AntiXss.JavaScriptEncode(msg))) : string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nlocation.href=\"{0}\";\r\n//--></SCRIPT>\r\n", (object)url)) : "\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nlocation.href=location.href;\r\n//--></SCRIPT>\r\n";
            P.ClientScript.RegisterClientScriptBlock(P.GetType(), "AlertAndRedirect", script);
        }

        public static void AlertAndClose(Page P, string msg)
        {
            string str = string.Empty;
            string script = msg != null && msg.Length != 0 ? string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert({0});\r\nwindow.close();\r\n//--></SCRIPT>\r\n", (object)AntiXss.JavaScriptEncode(msg)) : "\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nwindow.close();\r\n//--></SCRIPT>\r\n";
            P.ClientScript.RegisterClientScriptBlock(P.GetType(), "AlertAndClose", script);
        }

        public static string GetIPAddress(string ip)
        {
            return new IpList()
            {
                IP = ip
            }.IPLocation().Replace("本机地址", "局域网IP");
        }

        public static string GetIPAddressInfo(string ip)
        {
            return new IpList()
            {
                IP = ip
            }.IPAddInfo().Replace("CZ88.NET", "");
        }

        public static void ShowErrorMsg(string error)
        {
            HttpContext.Current.Response.Write(error);
            HttpContext.Current.Response.End();
        }

        public static bool BankMD5Check(string userid, string bankid, string money, string orderid, string notify_url, string key, string sign)
        {
            return Cryptography.MD5(string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}{5}", (object)userid, (object)bankid, (object)money, (object)orderid, (object)notify_url, (object)key)).ToLower() == sign;
        }

        public static bool IPSBankMD5Check(string Billno, string Currency_Type, string Amount, string Date, string OrderEncodeType, string key, string SignMD5)
        {
            return Cryptography.MD5(string.Format("billno{0}currencytype{1}amount{2}date{3}orderencodetype{4}{5}", (object)Billno, (object)Currency_Type, (object)Amount, (object)Date, (object)OrderEncodeType, (object)key)).ToLower() == SignMD5;
        }

        public static string CardMD5(string userid, string cardtype, string cardNo, string cardpass, string money, string orderid, string notify_url, string key)
        {
            return Cryptography.MD5(string.Format("userid={0}&cardtype={1}&cardno={2}&cardpass={3}&money={4}&orderid={5}&notify_url={6}{7}", (object)userid, (object)cardtype, (object)cardNo, (object)cardpass, (object)money, (object)orderid, (object)notify_url, (object)key)).ToLower();
        }

        public static bool CardMD5Check(string totalvalue, string attach, string type, string userid, string cardno, string cardpwd, string value, string orderid, string restrict, string callbackurl, string key, string sign)
        {
            try
            {
                string str = string.Empty;
                int num;
                if (string.IsNullOrEmpty(totalvalue))
                    num = cardno.Split(',').Length > 1 ? 1 : 0;
                else
                    num = 1;
                string strToEncrypt;
                if (num == 0)
                    strToEncrypt = string.Format("type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}{8}", (object)type, (object)userid, (object)cardno, (object)cardpwd, (object)value, (object)restrict, (object)orderid, (object)callbackurl, (object)key);
                else
                    strToEncrypt = string.Format("type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&totalvalue={9}&restrict={5}&attach={10}&orderid={6}&callbackurl={7}{8}", (object)type, (object)userid, (object)cardno, (object)cardpwd, (object)value, (object)restrict, (object)orderid, (object)callbackurl, (object)key, (object)totalvalue, (object)attach);
                return Cryptography.MD5(strToEncrypt).ToLower() == sign;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool SeachMD5Check(string orderid, string userid, string key, string sign)
        {
            return Cryptography.MD5(string.Format("orderid={0}&parter={1}{2}", (object)orderid, (object)userid, (object)key)).ToLower() == sign;
        }

        public static void BindBankSupplierDDL(DropDownList ddl)
        {
            ddl.Items.Clear();
            DataTable dataTable = SupplierFactory.GetList("isbank =1").Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (dataTable == null)
                return;
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
                ddl.Items.Add(new ListItem(dataRow["name"].ToString(), dataRow["code"].ToString()));
        }

        public static void BindCardSupplierDLL(DropDownList ddl)
        {
            ddl.Items.Clear();
            DataTable dataTable = SupplierFactory.GetList("iscard =1").Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (dataTable == null)
                return;
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
                ddl.Items.Add(new ListItem(dataRow["name"].ToString(), dataRow["code"].ToString()));
        }

        public static void BindSMSSupplierDLL(DropDownList ddl)
        {
            ddl.Items.Clear();
            DataTable dataTable = SupplierFactory.GetList("issms =1").Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (dataTable == null)
                return;
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
                ddl.Items.Add(new ListItem(dataRow["name"].ToString(), dataRow["code"].ToString()));
        }

        public static void BindSXSupplierDLL(DropDownList ddl)
        {
            ddl.Items.Clear();
            DataTable dataTable = SupplierFactory.GetList("issx =1").Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (dataTable == null)
                return;
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
                ddl.Items.Add(new ListItem(dataRow["name"].ToString(), dataRow["code"].ToString()));
        }

        public static bool checkUnique(int userId, string userOrder)
        {
            if (WebCache.GetCacheService().RetrieveObject(userOrder) != null)
                return false;
            return UserFactory.CheckUserOrderId(userId, userOrder);
        }

        public static bool CheckKey(string Parameters, string Mac)
        {
            return Cryptography.MD5(Parameters + Constant.ParameterEncryptionKey).ToLower() == Mac.ToLower();
        }

        public static string GetKey(string Parameters)
        {
            return Cryptography.MD5(Parameters + Constant.ParameterEncryptionKey).ToLower();
        }
    }
}
