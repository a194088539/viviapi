using System.Text;
using System.Xml;
using viviLib.Web;

namespace viviapi.BLL.Tools
{
    public sealed class idcards
    {
        public static IdcardInfo GetIdCardInfo(string id)
        {
            IdcardInfo idcardInfo = new IdcardInfo();
            try
            {
                Encoding encoding = Encoding.GetEncoding("GBK");
                string @string = WebClientHelper.GetString("http://www.youdao.com/smartresult-xml/search.s", "type=id&q=" + id, "GET", encoding, 5000);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(@string);
                string innerText1 = xmlDocument.GetElementsByTagName("code")[0].InnerText;
                string innerText2 = xmlDocument.GetElementsByTagName("location")[0].InnerText;
                string innerText3 = xmlDocument.GetElementsByTagName("birthday")[0].InnerText;
                string innerText4 = xmlDocument.GetElementsByTagName("gender")[0].InnerText;
                idcardInfo.code = innerText1;
                idcardInfo.location = innerText2;
                idcardInfo.birthday = innerText3;
                idcardInfo.gender = innerText4 == "m" ? "男" : "女";
                return idcardInfo;
            }
            catch
            {
                return (IdcardInfo)null;
            }
        }
    }
}
