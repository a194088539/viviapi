/// <summary>
/// MD5 的摘要说明
/// </summary>
public class MD5S
{
    /// <summary>
    /// MD5 加密转换为小写的
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string MD5(string str)
    {
        string strMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
        return strMD5.ToLower();
    }

}
