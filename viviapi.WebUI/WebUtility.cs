namespace viviapi.WebUI
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviLib.Security;

    public class WebUtility
    {
        public static void AlertAndClose(Page P, string msg)
        {
            string script = string.Empty;
            if ((msg == null) || (msg.Length == 0))
            {
                script = "\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nwindow.close();\r\n//--></SCRIPT>\r\n";
            }
            else
            {
                script = string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert({0});\r\nwindow.close();\r\n//--></SCRIPT>\r\n", AntiXss.JavaScriptEncode(msg));
            }
            P.ClientScript.RegisterClientScriptBlock(P.GetType(), "AlertAndClose", script);
        }

        public static void AlertAndRedirect(Page P, string msg)
        {
            AlertAndRedirect(P, msg, null);
        }

        public static void AlertAndRedirect(Page P, string msg, string url)
        {
            string script = string.Empty;
            if (((msg == null) || (msg.Length == 0)) && ((url == null) || (url.Length == 0)))
            {
                script = "\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nlocation.href=location.href;\r\n//--></SCRIPT>\r\n";
            }
            else if ((msg == null) || (msg.Length == 0))
            {
                script = string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nlocation.href=\"{0}\";\r\n//--></SCRIPT>\r\n", url);
            }
            else if ((url == null) || (url.Length == 0))
            {
                script = string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert({0});\r\nlocation.href=location.href;\r\n//--></SCRIPT>\r\n", AntiXss.JavaScriptEncode(msg));
            }
            else
            {
                script = string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert({0});\r\nlocation.href=\"{1}\";\r\n//--></SCRIPT>\r\n", AntiXss.JavaScriptEncode(msg), url);
            }
            P.ClientScript.RegisterClientScriptBlock(P.GetType(), "AlertAndRedirect", script);
        }

        public static void BindBankSupplierDDL(DropDownList ddl)
        {
            ddl.Items.Clear();
            string where = "isbank =1";
            DataTable table = SupplierFactory.GetList(where).Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    ddl.Items.Add(new ListItem(row["name"].ToString(), row["code"].ToString()));
                }
            }
        }

        public static void BindBquestionDDL(DropDownList ddl)
        {
            ddl.Items.Clear();
            DataTable cacheList = new Question().GetCacheList();
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (cacheList != null)
            {
                foreach (DataRow row in cacheList.Rows)
                {
                    ddl.Items.Add(new ListItem(row["question"].ToString(), row["id"].ToString()));
                }
            }
        }

        public static void BindCardSupplierDLL(DropDownList ddl)
        {
            ddl.Items.Clear();
            string where = "iscard =1";
            DataTable table = SupplierFactory.GetList(where).Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    ddl.Items.Add(new ListItem(row["name"].ToString(), row["code"].ToString()));
                }
            }
        }

        public static void BindSMSSupplierDLL(DropDownList ddl)
        {
            ddl.Items.Clear();
            string where = "issms =1";
            DataTable table = SupplierFactory.GetList(where).Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    ddl.Items.Add(new ListItem(row["name"].ToString(), row["code"].ToString()));
                }
            }
        }

        public static void BindSXSupplierDLL(DropDownList ddl)
        {
            ddl.Items.Clear();
            string where = "issx =1";
            DataTable table = SupplierFactory.GetList(where).Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    ddl.Items.Add(new ListItem(row["name"].ToString(), row["code"].ToString()));
                }
            }
        }

        public static string GetIPAddress(string ip)
        {
            try
            {
                IpList list = new IpList();
                list.IP = ip;
                return list.IPLocation().Replace("localhost", "192.168.2.110");
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetIPAddressInfo(string ip)
        {
            IpList list = new IpList();
            list.IP = ip;
            //return list.IPAddInfo().Replace("localhost", "");
            return list.IPAddInfo();
        }

        public static string GetPayModeViewName(int pmode)
        {
            string str = string.Empty;
            switch (pmode)
            {
                case 1:
                    return "银行帐户";

                case 2:
                    return "支付宝";

                case 3:
                    return "财付通";
            }
            return str;
        }

        public static string GetsupplierName(object obj)
        {
            try
            {
                if ((obj == DBNull.Value) || (obj == null))
                {
                    return string.Empty;
                }
                return SupplierFactory.GetModelByCode(int.Parse(obj.ToString())).name;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}

