using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace viviapi.ETAPI.tongyi
{
    public class Util
    {
        public static Dictionary<string, object> JsonToDictionary(string jsonData)
        {
            return new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(jsonData);
        }

        public static string DictionaryToJson(Dictionary<string, object> dic)
        {
            return new JavaScriptSerializer().Serialize((object)dic);
        }

        public static string DictionaryToJson(Dictionary<string, string> dic)
        {
            return new JavaScriptSerializer().Serialize((object)dic);
        }

        public static Dictionary<string, string> JsonToDictionary1(string jsonData)
        {
            return new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsonData);
        }

        public static NameValueCollection GetQueryString(string queryString)
        {
            return Util.GetQueryString(queryString, (Encoding)null, true);
        }

        public static string CreateLinkString1(Dictionary<string, string> dicArray)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dicArray)
                stringBuilder.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            int length = stringBuilder.Length;
            stringBuilder.Remove(length - 1, 1);
            return ((object)stringBuilder).ToString();
        }

        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            Dictionary<string, string> dictionary = Util.SortDictionaryAsc(dicArray);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
                stringBuilder.Append(keyValuePair.Key + "=" + HttpUtility.UrlEncode(keyValuePair.Value) + "&");
            int length = stringBuilder.Length;
            stringBuilder.Remove(length - 1, 1);
            return ((object)stringBuilder).ToString();
        }

        public static string CreateLinkString(Hashtable sParaTemp)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string str in (IEnumerable)sParaTemp.Keys)
                stringBuilder.Append(str + "=" + Util.UrlEncode(sParaTemp[(object)str].ToString(), "UTF-8") + "&");
            int length = stringBuilder.Length;
            stringBuilder.Remove(length - 1, 1);
            return ((object)stringBuilder).ToString();
        }

        public static Dictionary<string, string> SortDictionaryAsc(Dictionary<string, string> dic)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>)dic);
            list.Sort((Comparison<KeyValuePair<string, string>>)((s1, s2) => s1.Key.CompareTo(s2.Key)));
            Enumerable.OrderBy<KeyValuePair<string, string>, string>((IEnumerable<KeyValuePair<string, string>>)dic, (Func<KeyValuePair<string, string>, string>)(objDic => objDic.Key));
            string[] array = new string[dic.Keys.Count];
            dic.Keys.CopyTo(array, 0);
            Array.Sort<string>(array);
            dic.Clear();
            foreach (KeyValuePair<string, string> keyValuePair in list)
                dic.Add(keyValuePair.Key, keyValuePair.Value);
            return dic;
        }

        public static string CreateLinkString2(Dictionary<string, string> dicArray)
        {
            Dictionary<string, string> dictionary = Util.SortDictionaryAsc(dicArray);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
                stringBuilder.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            int length = stringBuilder.Length;
            stringBuilder.Remove(length - 1, 1);
            return ((object)stringBuilder).ToString();
        }

        public static string CreateLinkString3(Dictionary<string, string> dicArray)
        {
            Dictionary<string, string> dictionary = Util.SortDictionaryAsc(dicArray);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                if (keyValuePair.Value != null && keyValuePair.Value != "")
                    stringBuilder.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            }
            int length = stringBuilder.Length;
            stringBuilder.Remove(length - 1, 1);
            return ((object)stringBuilder).ToString();
        }

        public static string BuildFormHtml(Dictionary<string, string> sParaTemp, string gateway, string strMethod, string strButtonValue)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">");
            stringBuilder.Append("<html><head><title>跳转中...</title>");
            stringBuilder.Append("</head><body>");
            stringBuilder.Append("<form id='demosubmit' name='demosubmit' action='" + gateway + "' method='" + strMethod.ToLower().Trim() + "'>");
            foreach (KeyValuePair<string, string> keyValuePair in sParaTemp)
                stringBuilder.Append("<input type='hidden' name='" + keyValuePair.Key + "' value='" + keyValuePair.Value + "'/>");
            stringBuilder.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
            stringBuilder.Append("<script>document.forms['demosubmit'].submit();</script> ");
            stringBuilder.Append("</body></html>");
            return ((object)stringBuilder).ToString();
        }

        public static string BuildFormRUL(Dictionary<string, string> sParaTemp)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in sParaTemp)
                stringBuilder.Append("<input type='hidden' name='" + keyValuePair.Key + "' value='" + keyValuePair.Value + "'/>");
            return ((object)stringBuilder).ToString();
        }

        public static string BuildFormHtml1(string url, string strMethod, string strButtonValue)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">");
            stringBuilder.Append("<html><head><title>跳转中...</title>");
            stringBuilder.Append("<script src=\"http://jh.yizhibank.com/js/callalipay.js\"></script>");
            stringBuilder.Append("<script type=\"text/javascript\">var aliPay ='" + url + "';callappjs.callAlipay(aliPay);</script>");
            stringBuilder.Append("</head><body>");
            stringBuilder.Append("</body></html>");
            return ((object)stringBuilder).ToString();
        }

        public static string BuildFormHtml(Hashtable sParaTemp, string gateway, string strMethod, string strButtonValue)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">");
            stringBuilder.Append("<html><head><title>跳转中...</title>");
            stringBuilder.Append("</head><body>");
            stringBuilder.Append("<form id='demosubmit' name='demosubmit' action='" + gateway + "' method='" + strMethod.ToLower().Trim() + "'>");
            foreach (string str in (IEnumerable)sParaTemp.Keys)
                stringBuilder.Append("<input type='hidden' name='" + (object)str + "' value='" + (string)sParaTemp[(object)str] + "'/>");
            stringBuilder.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
            stringBuilder.Append("<script>document.forms['demosubmit'].submit();</script> ");
            stringBuilder.Append("</body></html>");
            return ((object)stringBuilder).ToString();
        }

        public static string CreateLinkStringUrlencode(Dictionary<string, string> dicArray, Encoding code)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dicArray)
                stringBuilder.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            int length = stringBuilder.Length;
            stringBuilder.Remove(length - 1, 1);
            return ((object)stringBuilder).ToString();
        }

        public static Dictionary<string, string> FilterPara(Dictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> keyValuePair in dicArrayPre)
            {
                string key = keyValuePair.Key;
                string str = keyValuePair.Value;
                if (!(keyValuePair.Key.ToLower() == "signature") && str != null && !(str == ""))
                    dictionary.Add(key, str);
            }
            return dictionary;
        }

        public static Dictionary<string, object> FilterPara1(Dictionary<string, object> dicArrayPre)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> keyValuePair in dicArrayPre)
            {
                string key = keyValuePair.Key;
                string str = keyValuePair.Value.ToString();
                if (!(keyValuePair.Key.ToLower() == "signature") && (str != null || !(str == "")))
                    dictionary.Add(key, (object)str);
            }
            return dictionary;
        }

        public static string getParamSrc(Dictionary<string, string> paramsMap)
        {
            string[] array = new string[paramsMap.Keys.Count];
            paramsMap.Keys.CopyTo(array, 0);
            Array.Sort<string>(array, new Comparison<string>(string.CompareOrdinal));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string index in array)
            {
                if (paramsMap[index] != null && paramsMap[index] != "")
                    stringBuilder.Append(index + "=" + paramsMap[index] + "&");
            }
            return ((object)stringBuilder).ToString().Substring(0, ((object)stringBuilder).ToString().Length - 1);
        }

        public static string getParamSrc1(Dictionary<string, object> paramsMap)
        {
            string[] array = new string[paramsMap.Keys.Count];
            paramsMap.Keys.CopyTo(array, 0);
            Array.Sort<string>(array, new Comparison<string>(string.CompareOrdinal));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string index in array)
            {
                if (paramsMap[index].ToString() != null && paramsMap[index].ToString() != "")
                    stringBuilder.Append(paramsMap[index].ToString() + "#");
            }
            return ((object)stringBuilder).ToString().Substring(0, ((object)stringBuilder).ToString().Length - 1);
        }

        public static NameValueCollection GetQueryString(string queryString, Encoding encoding, bool isEncoded)
        {
            NameValueCollection nameValueCollection = new NameValueCollection((IEqualityComparer)StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrEmpty(queryString))
            {
                int length = queryString.Length;
                for (int index1 = 0; index1 < length; ++index1)
                {
                    int startIndex = index1;
                    int num = -1;
                    for (; index1 < length; ++index1)
                    {
                        switch (queryString[index1])
                        {
                            case '=':
                                if (num < 0)
                                {
                                    num = index1;
                                    break;
                                }
                                else
                                    break;
                            case '&':
                                goto label_8;
                        }
                    }
                label_8:
                    string str = (string)null;
                    string index2;
                    if (num >= 0)
                    {
                        index2 = queryString.Substring(startIndex, num - startIndex);
                        str = queryString.Substring(num + 1, index1 - num - 1);
                    }
                    else
                        index2 = queryString.Substring(startIndex, index1 - startIndex);
                    nameValueCollection[index2] = !isEncoded ? str : Util.MyUrlDeCode(str, encoding);
                    if (index1 == length - 1 && (int)queryString[index1] == 38)
                        nameValueCollection[index2] = string.Empty;
                }
            }
            return nameValueCollection;
        }

        public static string GetJsonValue(JEnumerable<JToken> jToken, string key)
        {
            IEnumerator enumerator = (IEnumerator)jToken.GetEnumerator();
            while (enumerator.MoveNext())
            {
                JToken jtoken = (JToken)enumerator.Current;
                if (jtoken is JObject || ((JProperty)jtoken).Value is JObject)
                    return Util.GetJsonValue(jtoken.Children(), key);
                if (((JProperty)jtoken).Name == key)
                    return ((object)((JProperty)jtoken).Value).ToString();
            }
            return (string)null;
        }

        public static string MyUrlDeCode(string str, Encoding encoding)
        {
            if (encoding == null)
            {
                Encoding utF8 = Encoding.UTF8;
                string str1 = HttpUtility.UrlEncode(HttpUtility.UrlDecode(str.ToUpper(), utF8), utF8).ToUpper();
                encoding = !(str == str1) ? Encoding.GetEncoding("gb2312") : Encoding.UTF8;
            }
            return HttpUtility.UrlDecode(str, encoding);
        }

        public static string UrlEncode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            string str;
            try
            {
                str = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));
            }
            catch (Exception ex)
            {
                str = HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
                Console.WriteLine((object)ex);
            }
            return str;
        }

        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            string str;
            try
            {
                str = HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));
            }
            catch (Exception ex)
            {
                str = HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
                Console.WriteLine((object)ex);
            }
            return str;
        }

        public static uint UnixStamp()
        {
            return Convert.ToUInt32((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds);
        }

        public static string BuildRandomStr(int length)
        {
            string str = new Random().Next().ToString();
            if (str.Length > length)
                str = str.Substring(0, length);
            else if (str.Length < length)
            {
                for (int index = length - str.Length; index > 0; --index)
                    str.Insert(0, "0");
            }
            return str;
        }

        public static Dictionary<string, string> loadCfg()
        {
            string path = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ApplicationBase) + (object)Path.DirectorySeparatorChar + "config" + (string)(object)Path.DirectorySeparatorChar + "config.properties";
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            using (StreamReader streamReader = new StreamReader(path))
            {
                while (streamReader.Peek() >= 0)
                {
                    string str1 = streamReader.ReadLine();
                    if (!str1.StartsWith("#"))
                    {
                        int length = str1.IndexOf("=");
                        string key = str1.Substring(0, length);
                        string str2 = str1.Substring(length + 1, str1.Length - (length + 1));
                        if (!dictionary.ContainsKey(key) && !string.IsNullOrEmpty(str2))
                            dictionary.Add(key, str2);
                    }
                }
            }
            return dictionary;
        }

        public static void writeFile(string title, Hashtable _param)
        {
            string path = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ApplicationBase) + (object)Path.DirectorySeparatorChar + "result.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    streamWriter.WriteLine("=====================" + title + "=====================");
                    foreach (DictionaryEntry dictionaryEntry in _param)
                        streamWriter.WriteLine("key:" + dictionaryEntry.Key.ToString() + " value:" + dictionaryEntry.Value.ToString());
                }
            }
            else
            {
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine("=====================" + title + "=====================");
                    foreach (DictionaryEntry dictionaryEntry in _param)
                        streamWriter.WriteLine("key:" + dictionaryEntry.Key.ToString() + " value:" + dictionaryEntry.Value.ToString());
                }
            }
        }

        public static void writeFile1(string title, string str)
        {
            string path = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ApplicationBase) + (object)Path.DirectorySeparatorChar + "result.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    streamWriter.WriteLine("=====================" + title + "=====================");
                    streamWriter.WriteLine("key:" + str);
                }
            }
            else
            {
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine("=====================" + title + "=====================");
                    streamWriter.WriteLine("key:" + str);
                }
            }
        }

        public static string random()
        {
            char[] chArray = new char[62]
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
        'a',
        'b',
        'c',
        'd',
        'e',
        'f',
        'g',
        'h',
        'i',
        'j',
        'k',
        'l',
        'm',
        'n',
        'o',
        'p',
        'q',
        'r',
        's',
        't',
        'u',
        'v',
        'w',
        'x',
        'y',
        'z',
        'A',
        'B',
        'C',
        'D',
        'E',
        'F',
        'G',
        'H',
        'I',
        'J',
        'K',
        'L',
        'M',
        'N',
        'O',
        'P',
        'Q',
        'R',
        'S',
        'T',
        'U',
        'V',
        'W',
        'X',
        'Y',
        'Z'
            };
            StringBuilder stringBuilder = new StringBuilder(32);
            Random random = new Random();
            for (int index = 0; index < 32; ++index)
                stringBuilder.Append(chArray[random.Next(62)]);
            return ((object)stringBuilder).ToString();
        }

        public static string Nmrandom()
        {
            string str = "";
            Random random = new Random();
            for (int index = 0; index < 16; ++index)
                str = str + random.Next(0, 9).ToString();
            return str;
        }

        public static string toXml(Hashtable _params)
        {
            StringBuilder stringBuilder = new StringBuilder("<xml>");
            foreach (DictionaryEntry dictionaryEntry in _params)
            {
                string str = dictionaryEntry.Key.ToString();
                stringBuilder.Append("<").Append(str).Append("><![CDATA[").Append(dictionaryEntry.Value.ToString()).Append("]]></").Append(str).Append(">");
            }
            return ((object)stringBuilder.Append("</xml>")).ToString();
        }
    }
}

