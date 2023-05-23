using com.todaynic.ScpClient;
using System;
using System.Text;
using viviapi.Model;
using viviapi.SysConfig;
using viviLib.Text;
using viviLib.Web;

namespace viviapi.BLL.Tools
{
    public class SMS
    {
        public static bool Send(string mobile, string msg, string type)
        {
            return new SMSClient(RuntimeSetting.HostName, Convert.ToInt32(RuntimeSetting.HostPort), RuntimeSetting.SMSUser, RuntimeSetting.SMSPassword).sendSMS(mobile, msg, type);
        }

        public static string SendSmsWithCheck(string mobile, string msg, string type)
        {
            string smsUser = RuntimeSetting.SMSUser;
            string smsPassword = RuntimeSetting.SMSPassword;
            string hostPort = RuntimeSetting.HostPort;
            string hostName = RuntimeSetting.HostName;
            int informationNumber = SysConfig.MaxInformationNumber;
            return SMS.SendSmsWithCheck(smsUser, smsPassword, informationNumber, mobile, msg, type);
        }

        public static string SendJXTWithCheck(string mobile, string msg, string type)
        {
            return SMS.SendJXTWithCheck(RuntimeSetting.SMUID, RuntimeSetting.SMPWD, RuntimeSetting.JXTURL, SysConfig.MaxInformationNumber, mobile, msg, type);
        }

        public static string SendJXTWithCheck(string uid, string pwd, string URL, int maxSendTimes, string mobile, string msg, string type)
        {
            string str = string.Empty;
            if (!PageValidate.IsMobile(mobile))
            {
                str = "您输入的手机号码不正确！请重新输入。";
            }
            else
            {
                bool flag = false;
                if (!PhoneValidFactory.isLimited(mobile))
                    flag = true;
                else if (PhoneValidFactory.SendCount(mobile) < maxSendTimes)
                    flag = true;
                if (!flag)
                    str = "抱歉，你输入的手机发送次数已达到最大允许次数！";
                else if (SMS.SendtoPost(uid, pwd, mobile, msg, type))
                    PhoneValidFactory.Add(new PhoneValidLog()
                    {
                        phone = mobile,
                        sendTime = DateTime.Now,
                        code = msg,
                        clientIP = ServerVariables.TrueIP
                    });
                else
                    str = "验证码发送失败，请联系管理员！";
            }
            return str;
        }

        public static bool SendtoPost(string uid, string pwd, string mobile, string msg, string ext)
        {
            string str = string.Empty;
            string jxturl = RuntimeSetting.JXTURL;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("id={0}", (object)uid);
            stringBuilder.AppendFormat("&pwd={0}", (object)pwd);
            stringBuilder.AppendFormat("&to={0}", (object)mobile);
            stringBuilder.AppendFormat("&content={0}", (object)msg);
            stringBuilder.AppendFormat("&time={0}", (object)ext);
            return WebClientHelper.GetString(jxturl, stringBuilder.ToString(), "POST", Encoding.GetEncoding("gb2312"), 10000).Split('/')[0] == "000";
        }

        public static string SendSmsWithCheck(string sn, string pwd, int maxSendTimes, string mobile, string msg, string type)
        {
            string str = string.Empty;
            if (!PageValidate.IsMobile(mobile))
            {
                str = "您输入的手机号码不正确！请重新输入。";
            }
            else
            {
                bool flag = false;
                if (!PhoneValidFactory.isLimited(mobile))
                    flag = true;
                else if (PhoneValidFactory.SendCount(mobile) < maxSendTimes)
                    flag = true;
                if (!flag)
                    str = "抱歉，你输入的手机发送次数已达到最大允许次数！";
                else if (SMS.SendtoSupp(sn, pwd, mobile, msg, type))
                    PhoneValidFactory.Add(new PhoneValidLog()
                    {
                        phone = mobile,
                        sendTime = DateTime.Now,
                        code = msg,
                        clientIP = ServerVariables.TrueIP
                    });
                else
                    str = "验证码发送失败，请联系管理员！";
            }
            return str;
        }

        public static bool SendtoSupp(string name, string pwd, string mobile, string msg, string ext)
        {
            return new SMSClient(RuntimeSetting.HostName, Convert.ToInt32(RuntimeSetting.HostPort), name, pwd).sendSMS(mobile, msg, ext);
        }
    }
}
