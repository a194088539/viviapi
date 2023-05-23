using System.Web;

namespace viviLib
{
    public class SqlKey
    {
        public bool ProcessSqlStr(string Str)
        {
            bool flag = true;
            try
            {
                if (Str.Trim() != "")
                {
                    string str1 = "and |exec |insert |select |delete |update |count | * |chr |mid |master |truncate |char |declare ";
                    char[] chArray = new char[1]
                    {
            '|'
                    };
                    foreach (string str2 in str1.Split(chArray))
                    {
                        if (Str.ToLower().IndexOf(str2) >= 0)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public void StartProcessRequest()
        {
            try
            {
                string url = "default.aspx";
                if (HttpContext.Current.Request.QueryString != null)
                {
                    for (int index = 0; index < HttpContext.Current.Request.QueryString.Count; ++index)
                    {
                        if (!this.ProcessSqlStr(HttpContext.Current.Request.QueryString[HttpContext.Current.Request.QueryString.Keys[index]]))
                        {
                            HttpContext.Current.Response.Redirect(url);
                            HttpContext.Current.Response.End();
                        }
                    }
                }
                if (HttpContext.Current.Request.Form == null)
                    return;
                for (int index1 = 0; index1 < HttpContext.Current.Request.Form.Count; ++index1)
                {
                    string index2 = HttpContext.Current.Request.Form.Keys[index1];
                    if (!(index2 == "__VIEWSTATE") && !this.ProcessSqlStr(HttpContext.Current.Request.Form[index2]))
                    {
                        HttpContext.Current.Response.Redirect(url);
                        HttpContext.Current.Response.End();
                    }
                }
            }
            catch
            {
            }
        }
    }
}
