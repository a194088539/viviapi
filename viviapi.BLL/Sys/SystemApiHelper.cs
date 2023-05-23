using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using viviapi.BLL.Api;
using viviapi.BLL.Channel;
using viviapi.BLL.Sys.Transaction.YeePay;
using viviapi.BLL.User;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviLib.Security;

namespace viviapi.BLL
{
    public class SystemApiHelper
    {
        public static string vbmyapi10 = "vb1.00";
        public static string vcmyapi10 = "vc1.00";
        public static string vbmyapi20 = "vb2.00";
        public static string vcmyapi20 = "vc2.00";
        public static string vhq10 = "vhq1.00";
        public static string vhq10ApiName = "花旗支付-商户支付功能接口规范版本号1.0";
        public static string vhq10BankReceiveVerifyStr = "{0}|{1}|{2}|{3}|{4}|{5}|{6}";
        public static string vhq10NotifySuccessflag = "errCode=0";
        public static string v36010 = "v360.10";
        public static string v36010BankNotifySuccessflag = "opstate=0";
        public static string v7010 = "vb70.10";
        public static string v7010ApiName = "70Card平台用户储值接口-版本号1.0";
        public static string v7010BankReceiveVerifyStr = "userid={0}&orderid={1}&bankid={2}";
        public static string v7010BankNotifyVerifyStr = "returncode={0}&userid={1}&orderid={2}&keyvalue={3}";
        public static string v7010BankNotifySuccessflag = "ok";
        public static string vc7010 = "vc70.21";
        public static string vc7010ApiName = "70Card平台接口对接直通车-版本号2.1";
        public static string vc7010CardReceiveVerifyStr = "userid={0}&orderid={1}&typeid={2}&productid={3}&cardno={4}&cardpwd={5}&money={6}&url={7}";
        public static string vc7010CardSynchronousNotifyVerifyStr = "returncode={0}&returnorderid={1}&keyvalue={2}";
        public static string vc7010CardNotifyVerifyStr = "returncode={0}&userid={1}&orderid={2}&typeid={3}&productid={4}&cardno={5}&cardpwd={6}&money={7}&realmoney={8}&cardstatus={9}&keyvalue={10}";
        public static string vc7010CardNotifySuccessflag = "ok";
        public static string vYee10 = "vbYee.10";
        public static string vYee10ApiName = "易宝网银";
        public static string vYee10BankNotifySuccessflag = "SUCCESS";
        public static string vcYee10 = "vcYee.10";
        public static string vcYee10ApiName = "非银行卡专业版（组合支付）";
        public static string vcYee10NotifySuccessflag = "SUCCESS";
        public static string vcYee20 = "vcYee.20";
        public static string vcYee20ApiName = "易宝点卡通用接口";
        public static string vcYee20NotifySuccessflag = "SUCCESS";
        public static string veypa10 = "vyb1.00";
        public static string IPS20 = "IPS.20";
        public static string IPS20ApiName = "环迅网银";
        public static string IPS20BankNotifySuccessflag = "Y";

        public static bool BankReceiveVerify(string version, string sign, params object[] arg)
        {
            bool flag = false;
            string str1 = string.Empty;
            string str2 = string.Empty;
            if (version == SystemApiHelper.v7010)
            {
                if (Cryptography.MD5(string.Format(SystemApiHelper.v7010BankReceiveVerifyStr, arg).ToLower() + string.Format("&keyvalue={0}", arg[3])).ToLower() == sign.ToLower())
                    flag = true;
            }
            else if (version == SystemApiHelper.vhq10 && Cryptography.MD5(string.Format(SystemApiHelper.vhq10BankReceiveVerifyStr, arg)).ToLower() == sign.ToLower())
                flag = true;
            return flag;
        }

        public static bool CardReceiveVerify(string version, string sign, params object[] arg)
        {
            bool flag = false;
            string str1 = string.Empty;
            string str2 = string.Empty;
            if (version == SystemApiHelper.vc7010)
            {
                if (Cryptography.MD5(string.Format(SystemApiHelper.vc7010CardReceiveVerifyStr, arg).ToLower() + string.Format("&keyvalue={0}", arg[8]), "UTF-8") == sign)
                    flag = true;
                else if (Cryptography.MD5(string.Format(SystemApiHelper.vc7010CardReceiveVerifyStr, arg) + string.Format("&keyvalue={0}", arg[8])).ToLower() == sign)
                    flag = true;
            }
            return flag;
        }

        public static string CreateSynchronousNotifySign(string version, params object[] arg)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            if (version == SystemApiHelper.vc7010)
                str2 = Cryptography.MD5(string.Format(SystemApiHelper.vc7010CardSynchronousNotifyVerifyStr, arg)).ToLower();
            return str2;
        }

        public static string GetBankBackUrl(OrderBankInfo orderinfo, bool isNotify)
        {
            string str1 = string.Empty;
            string str2 = !isNotify ? orderinfo.returnurl : orderinfo.notifyurl;
            if (orderinfo == null || string.IsNullOrEmpty(str2))
                return str2;
            UserInfo baseModel = UserFactory.GetBaseModel(orderinfo.userid);
            if (baseModel == null)
                return str2;
            string apiKey = baseModel.APIKey;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string version = orderinfo.version;
            string userorder1 = orderinfo.userorder;
            StringBuilder stringBuilder1 = new StringBuilder();
            if (string.IsNullOrEmpty(version) || version == SystemApiHelper.v36010 || version == SystemApiHelper.vbmyapi10)
            {
                string opstate = orderinfo.opstate;
                string str5 = Decimal.Round(orderinfo.realvalue.Value, 2).ToString();
                string str6 = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", (object)userorder1, (object)opstate, (object)str5, (object)apiKey));
                stringBuilder1.AppendFormat("orderid={0}", (object)HttpUtility.UrlEncode(userorder1));
                stringBuilder1.AppendFormat("&opstate={0}", (object)HttpUtility.UrlEncode(opstate));
                stringBuilder1.AppendFormat("&ovalue={0}", (object)HttpUtility.UrlEncode(str5));
                StringBuilder stringBuilder2 = stringBuilder1;
                string format1 = "&systime={0}";
                DateTime dateTime = orderinfo.completetime.Value;
                string str7 = HttpUtility.UrlEncode(dateTime.ToString("yyyy/MM/dd HH:mm:ss"));
                stringBuilder2.AppendFormat(format1, (object)str7);
                if (version == SystemApiHelper.vbmyapi10)
                {
                    stringBuilder1.AppendFormat("&sysorderid={0}", (object)HttpUtility.UrlEncode(orderinfo.orderid));
                    StringBuilder stringBuilder3 = stringBuilder1;
                    string format2 = "&completiontime={0}";
                    dateTime = orderinfo.completetime.Value;
                    string str8 = HttpUtility.UrlEncode(dateTime.ToString("yyyy/MM/dd HH:mm:ss"));
                    stringBuilder3.AppendFormat(format2, (object)str8);
                }
                stringBuilder1.AppendFormat("&attach={0}", (object)HttpUtility.UrlEncode(orderinfo.attach, Encoding.GetEncoding("GB2312")));
                stringBuilder1.AppendFormat("&msg={0}", (object)HttpUtility.UrlEncode(orderinfo.msg, Encoding.GetEncoding("GB2312")));
                stringBuilder1.AppendFormat("&sign={0}", (object)HttpUtility.UrlEncode(str6));
                str2 = str2 + "?" + stringBuilder1.ToString();
            }
            else if (version == SystemApiHelper.vbmyapi20)
                str2 = PayBank20.CreateNotifyUrl(orderinfo, isNotify, apiKey);
            else if (version == SystemApiHelper.v7010)
            {
                string str5 = "11";
                if (orderinfo.status == 2 || orderinfo.status == 8)
                    str5 = "1";
                string str6 = Decimal.Round(orderinfo.realvalue.Value, 2).ToString();
                string str7 = Cryptography.MD5(string.Format(SystemApiHelper.v7010BankNotifyVerifyStr, (object)str5, (object)orderinfo.userid, (object)userorder1, (object)apiKey));
                stringBuilder1.AppendFormat("returncode={0}", (object)HttpUtility.UrlEncode(str5));
                stringBuilder1.AppendFormat("&userid={0}", (object)HttpUtility.UrlEncode(orderinfo.userid.ToString()));
                stringBuilder1.AppendFormat("&orderid={0}", (object)HttpUtility.UrlEncode(userorder1));
                stringBuilder1.AppendFormat("&money={0}", (object)HttpUtility.UrlEncode(str6));
                stringBuilder1.AppendFormat("&sign={0}", (object)HttpUtility.UrlEncode(str7));
                stringBuilder1.AppendFormat("&ext={0}", (object)HttpUtility.UrlEncode(orderinfo.attach, Encoding.GetEncoding("GB2312")));
                str2 = str2.IndexOf("?") <= 0 ? str2 + "?" + stringBuilder1.ToString() : str2 + "&" + stringBuilder1.ToString();
            }
            else if (version == SystemApiHelper.vYee10)
            {
                string str5 = "0";
                string str6 = orderinfo.userid.ToString();
                string str7 = "Buy";
                if (orderinfo.status == 2 || orderinfo.status == 8)
                    str5 = "1";
                string orderid = orderinfo.orderid;
                string str8 = Decimal.Round(orderinfo.realvalue.Value, 2).ToString();
                string str9 = "RMB";
                string cusSubject = orderinfo.cus_subject;
                string userorder2 = orderinfo.userorder;
                string str10 = "";
                string attach = orderinfo.attach;
                string str11 = "1";
                if (isNotify)
                    str11 = "2";
                string str12 = Digest.HmacSign("" + str6 + str7 + str5 + orderid + str8 + str9 + cusSubject + userorder2 + str10 + attach + str11, apiKey);
                stringBuilder1.AppendFormat("p1_MerId={0}", (object)SystemApiHelper.FormatQueryString(str6));
                stringBuilder1.AppendFormat("&r0_Cmd={0}", (object)SystemApiHelper.FormatQueryString(str7));
                stringBuilder1.AppendFormat("&r1_Code={0}", (object)SystemApiHelper.FormatQueryString(str5));
                stringBuilder1.AppendFormat("&r2_TrxId={0}", (object)SystemApiHelper.FormatQueryString(orderid));
                stringBuilder1.AppendFormat("&r3_Amt={0}", (object)SystemApiHelper.FormatQueryString(str8));
                stringBuilder1.AppendFormat("&r4_Cur={0}", (object)SystemApiHelper.FormatQueryString("RMB"));
                stringBuilder1.AppendFormat("&r5_Pid={0}", (object)SystemApiHelper.FormatQueryString(cusSubject));
                stringBuilder1.AppendFormat("&r6_Order={0}", (object)SystemApiHelper.FormatQueryString(userorder2));
                stringBuilder1.AppendFormat("&r7_Uid={0}", (object)SystemApiHelper.FormatQueryString(str10));
                stringBuilder1.AppendFormat("&r8_MP={0}", (object)SystemApiHelper.FormatQueryString(attach));
                stringBuilder1.AppendFormat("&r9_BType={0}", (object)SystemApiHelper.FormatQueryString(str11));
                stringBuilder1.AppendFormat("&rb_BankId={0}", (object)SystemApiHelper.FormatQueryString(orderinfo.paymodeId));
                stringBuilder1.AppendFormat("&ro_BankOrderId={0}", (object)SystemApiHelper.FormatQueryString(orderinfo.supplierOrder));
                stringBuilder1.AppendFormat("&rp_PayDate={0}", (object)SystemApiHelper.FormatQueryString(orderinfo.completetime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                stringBuilder1.AppendFormat("&ro_BankOrderId={0}", (object)SystemApiHelper.FormatQueryString(orderinfo.supplierOrder));
                stringBuilder1.AppendFormat("&rq_CardNo={0}", (object)SystemApiHelper.FormatQueryString(string.Empty));
                stringBuilder1.AppendFormat("&ru_Trxtime={0}", (object)SystemApiHelper.FormatQueryString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                stringBuilder1.AppendFormat("&hmac={0}", (object)SystemApiHelper.FormatQueryString(str12));
                str2 = str2.IndexOf("?") <= 0 ? str2 + "?" + stringBuilder1.ToString() : str2 + "&" + stringBuilder1.ToString();
            }
            else if (version == SystemApiHelper.vhq10)
            {
                Decimal num = Decimal.Round(orderinfo.refervalue, 2);
                string str5 = num.ToString();
                num = Decimal.Round(orderinfo.realvalue.Value, 2);
                string str6 = num.ToString();
                string str7 = "116";
                if (orderinfo.status == 2 || orderinfo.status == 8)
                    str7 = "0";
                string str8 = Cryptography.MD5(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", (object)orderinfo.userid, (object)orderinfo.userorder, (object)string.Empty, (object)string.Empty, (object)str5, (object)orderinfo.cus_field2, (object)apiKey));
                stringBuilder1.AppendFormat("P_UserId={0}", (object)orderinfo.userid);
                stringBuilder1.AppendFormat("&P_OrderId={0}", (object)orderinfo.userorder);
                stringBuilder1.AppendFormat("&P_CardId={0}", (object)string.Empty);
                stringBuilder1.AppendFormat("&P_CardPass={0}", (object)string.Empty);
                stringBuilder1.AppendFormat("&P_FaceValue={0}", (object)str5);
                stringBuilder1.AppendFormat("&P_ChannelId={0}", (object)orderinfo.cus_field2);
                stringBuilder1.AppendFormat("&P_PayMoney={0}", (object)str6);
                stringBuilder1.AppendFormat("&P_Subject={0}", (object)orderinfo.cus_subject);
                stringBuilder1.AppendFormat("&P_Price={0}", (object)orderinfo.cus_price);
                stringBuilder1.AppendFormat("&P_Quantity={0}", (object)orderinfo.cus_quantity);
                stringBuilder1.AppendFormat("&P_Description={0}", (object)orderinfo.cus_description);
                stringBuilder1.AppendFormat("&P_Notic={0}", (object)orderinfo.attach);
                stringBuilder1.AppendFormat("&P_ErrCode={0}", (object)str7);
                stringBuilder1.AppendFormat("&P_ErrMsg={0}", (object)orderinfo.msg);
                stringBuilder1.AppendFormat("&P_PostKey={0}", (object)str8);
                str2 = str2.IndexOf("?") <= 0 ? str2 + "?" + stringBuilder1.ToString() : str2 + "&" + stringBuilder1.ToString();
            }
            else if (version == SystemApiHelper.IPS20)
            {
                string userorder2 = orderinfo.userorder;
                string str5 = Decimal.Round(orderinfo.realvalue.Value, 2).ToString();
                string str6 = "RMB";
                string str7 = orderinfo.completetime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string str8 = "Y";
                if (orderinfo.status == 2 || orderinfo.status == 8)
                    str8 = "N";
                string attach1 = orderinfo.attach;
                string attach2 = orderinfo.attach;
                string orderid = orderinfo.orderid;
                string str9 = "MD5";
                string str10 = FormsAuthentication.HashPasswordForStoringInConfigFile(userorder2 + str6 + str5 + str7 + str8 + orderid + str9 + apiKey, "MD5").ToLower();
                stringBuilder1.AppendFormat("billno={0}", (object)userorder2);
                stringBuilder1.AppendFormat("&amount={0}", (object)str5);
                stringBuilder1.AppendFormat("&currency_type={0}", (object)str6);
                stringBuilder1.AppendFormat("&P_CardPass={0}", (object)string.Empty);
                stringBuilder1.AppendFormat("&date={0}", (object)str7);
                stringBuilder1.AppendFormat("&succ={0}", (object)str8);
                stringBuilder1.AppendFormat("&msg={0}", (object)attach1);
                stringBuilder1.AppendFormat("&attach={0}", (object)orderinfo.cus_price);
                stringBuilder1.AppendFormat("&ipsbillno={0}", (object)orderid);
                stringBuilder1.AppendFormat("&retEncodeType={0}", (object)string.Empty);
                stringBuilder1.AppendFormat("&signature={0}", (object)str10);
                str2 = str2.IndexOf("?") <= 0 ? str2 + "?" + stringBuilder1.ToString() : str2 + "&" + stringBuilder1.ToString();
            }
            return str2;
        }

        private static string FormatQueryString(string value)
        {
            return HttpUtility.UrlEncode(value, Encoding.GetEncoding("GB2312"));
        }

        public static string GetCardBackUrl(OrderCardInfo orderinfo)
        {
            string str1 = string.Empty;
            string str2 = orderinfo.notifyurl;
            if (orderinfo == null || string.IsNullOrEmpty(str2))
                return str2;
            UserInfo baseModel = UserFactory.GetBaseModel(orderinfo.userid);
            if (baseModel == null)
                return str2;
            string apiKey = baseModel.APIKey;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string version = orderinfo.version;
            string userorder1 = orderinfo.userorder;
            Decimal num = new Decimal(0);
            Decimal? realvalue = orderinfo.realvalue;
            if (realvalue.HasValue)
            {
                realvalue = orderinfo.realvalue;
                num = Decimal.Round(realvalue.Value, 0);
            }
            DateTime now = DateTime.Now;
            string str5 = now.ToString("yyyy/MM/dd HH:mm:ss");
            DateTime? completetime = orderinfo.completetime;
            if (completetime.HasValue)
            {
                completetime = orderinfo.completetime;
                now = completetime.Value;
                str5 = now.ToString("yyyy/MM/dd HH:mm:ss");
            }
            StringBuilder stringBuilder1 = new StringBuilder();
            if (string.IsNullOrEmpty(version) || version == SystemApiHelper.v36010 || version == SystemApiHelper.vcmyapi10)
            {
                if (orderinfo.ismulticard == 0)
                {
                    string paramValue = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", (object)orderinfo.userorder, (object)orderinfo.opstate, (object)num, (object)apiKey));
                    stringBuilder1.AppendFormat("orderid={0}", (object)HttpUtility.UrlEncode(orderinfo.userorder));
                    if (orderinfo.opstate == "10")
                        orderinfo.opstate = "-1";
                    stringBuilder1.AppendFormat("&opstate={0}", (object)HttpUtility.UrlEncode(orderinfo.opstate));
                    stringBuilder1.AppendFormat("&ovalue={0}", (object)HttpUtility.UrlEncode(num.ToString()));
                    StringBuilder stringBuilder2 = stringBuilder1;
                    string format1 = "&systime={0}";
                    completetime = orderinfo.completetime;
                    now = completetime.Value;
                    string str6 = HttpUtility.UrlEncode(now.ToString("yyyy/MM/dd HH:mm:ss"));
                    stringBuilder2.AppendFormat(format1, (object)str6);
                    if (version == SystemApiHelper.vcmyapi10)
                    {
                        stringBuilder1.AppendFormat("&sysorderid={0}", (object)HttpUtility.UrlEncode(orderinfo.orderid));
                        StringBuilder stringBuilder3 = stringBuilder1;
                        string format2 = "&completiontime={0}";
                        completetime = orderinfo.completetime;
                        now = completetime.Value;
                        string str7 = HttpUtility.UrlEncode(now.ToString("yyyy/MM/dd HH:mm:ss"));
                        stringBuilder3.AppendFormat(format2, (object)str7);
                    }
                    stringBuilder1.AppendFormat("&attach={0}", (object)SystemApiHelper.UrlEncode(orderinfo.attach));
                    stringBuilder1.AppendFormat("&msg={0}", (object)SystemApiHelper.UrlEncode(orderinfo.userViewMsg));
                    stringBuilder1.AppendFormat("&sign={0}", (object)SystemApiHelper.UrlEncode(paramValue));
                    str2 = str2 + "?" + stringBuilder1.ToString();
                }
                else if (orderinfo.ismulticard == 1)
                {
                    string paramValue = Cryptography.MD5(string.Format("orderid={0}&cardno={1}&opstate={2}&ovalue={3}&ototalvalue={4}&attach={5}&msg={6}{7}", (object)orderinfo.userorder, (object)orderinfo.cardNo, (object)orderinfo.returnopstate, (object)orderinfo.returnovalue, (object)num, (object)orderinfo.attach, (object)orderinfo.msg, (object)apiKey));
                    stringBuilder1.AppendFormat("orderid={0}", (object)HttpUtility.UrlEncode(orderinfo.userorder));
                    stringBuilder1.AppendFormat("&cardno={0}", (object)HttpUtility.UrlEncode(orderinfo.cardNo));
                    stringBuilder1.AppendFormat("&opstate={0}", (object)HttpUtility.UrlEncode(orderinfo.returnopstate));
                    stringBuilder1.AppendFormat("&ovalue={0}", (object)HttpUtility.UrlEncode(orderinfo.returnovalue));
                    stringBuilder1.AppendFormat("&ototalvalue={0}", (object)HttpUtility.UrlEncode(num.ToString()));
                    stringBuilder1.AppendFormat("&attach={0}", (object)SystemApiHelper.UrlEncode(orderinfo.attach));
                    stringBuilder1.AppendFormat("&msg={0}", (object)SystemApiHelper.UrlEncode(orderinfo.msg));
                    stringBuilder1.AppendFormat("&ekaorderid={0}", (object)HttpUtility.UrlEncode(orderinfo.orderid));
                    StringBuilder stringBuilder2 = stringBuilder1;
                    string format1 = "&ekatime={0}";
                    completetime = orderinfo.completetime;
                    now = completetime.Value;
                    string str6 = HttpUtility.UrlEncode(now.ToString("yyyy/MM/dd HH:mm:ss"));
                    stringBuilder2.AppendFormat(format1, (object)str6);
                    if (version == SystemApiHelper.vbmyapi10)
                    {
                        stringBuilder1.AppendFormat("&sysorderid={0}", (object)HttpUtility.UrlEncode(orderinfo.orderid));
                        StringBuilder stringBuilder3 = stringBuilder1;
                        string format2 = "&completiontime={0}";
                        completetime = orderinfo.completetime;
                        now = completetime.Value;
                        string str7 = HttpUtility.UrlEncode(now.ToString("yyyy/MM/dd HH:mm:ss"));
                        stringBuilder3.AppendFormat(format2, (object)str7);
                    }
                    stringBuilder1.AppendFormat("&sign={0}", (object)SystemApiHelper.UrlEncode(paramValue));
                    str2 = str2 + "?" + stringBuilder1.ToString();
                }
            }
            else if (version == SystemApiHelper.vcmyapi20)
                str2 = SellCard20.CreateNotifyUrl(orderinfo, apiKey);
            else if (version == SystemApiHelper.vc7010)
            {
                string opstate = orderinfo.opstate;
                string str6 = "0";
                string paramValue1;
                if (opstate == "0")
                {
                    str6 = "1";
                    paramValue1 = "1";
                }
                else
                    paramValue1 = "11";
                string paramValue2 = Decimal.Round(orderinfo.refervalue, 0).ToString();
                string paramValue3 = SystemApiHelper.Get70Paycardno(orderinfo.typeId);
                string paramValue4 = SystemApiHelper.Get70Paycardno(orderinfo.typeId) + paramValue2;
                string paramValue5 = Cryptography.MD5(string.Format(SystemApiHelper.vc7010CardNotifyVerifyStr, (object)paramValue1, (object)orderinfo.userid, (object)userorder1, (object)paramValue3, (object)paramValue4, (object)orderinfo.cardNo, (object)orderinfo.cardPwd, (object)paramValue2, (object)num, (object)str6, (object)apiKey)).ToLower();
                stringBuilder1.AppendFormat("returncode={0}", (object)SystemApiHelper.UrlEncode(paramValue1));
                stringBuilder1.AppendFormat("&userid={0}", (object)SystemApiHelper.UrlEncode(orderinfo.userid.ToString()));
                stringBuilder1.AppendFormat("&orderid={0}", (object)SystemApiHelper.UrlEncode(userorder1));
                stringBuilder1.AppendFormat("&typeid={0}", (object)SystemApiHelper.UrlEncode(paramValue3));
                stringBuilder1.AppendFormat("&productid={0}", (object)SystemApiHelper.UrlEncode(paramValue4));
                stringBuilder1.AppendFormat("&cardno={0}", (object)SystemApiHelper.UrlEncode(orderinfo.cardNo));
                stringBuilder1.AppendFormat("&cardpwd={0}", (object)SystemApiHelper.UrlEncode(orderinfo.cardPwd));
                stringBuilder1.AppendFormat("&money={0}", (object)SystemApiHelper.UrlEncode(paramValue2));
                stringBuilder1.AppendFormat("&realmoney={0}", (object)SystemApiHelper.UrlEncode(num.ToString()));
                stringBuilder1.AppendFormat("&cardstatus={0}", (object)SystemApiHelper.UrlEncode(str6.ToString()));
                stringBuilder1.AppendFormat("&sign={0}", (object)SystemApiHelper.UrlEncode(paramValue5));
                stringBuilder1.AppendFormat("&ext={0}", (object)SystemApiHelper.UrlEncode(orderinfo.attach));
                if (paramValue1 == "0")
                    stringBuilder1.AppendFormat("&errtype={0}", (object)string.Empty);
                else
                    stringBuilder1.AppendFormat("&errtype={0}", (object)SystemApiHelper.Get70errtype(orderinfo.supplierId, orderinfo.errtype));
                str2 = str2.IndexOf("?") <= 0 ? str2 + "?" + stringBuilder1.ToString() : str2 + "&" + stringBuilder1.ToString();
            }
            else if (version == SystemApiHelper.vcYee10)
            {
                bool flag1 = orderinfo.cus_field4 == "true";
                string paramValue1 = "ChargeCardDirect";
                string paramValue2 = "0";
                if (orderinfo.status == 2)
                    paramValue2 = "1";
                string paramValue3 = orderinfo.userid.ToString();
                string userorder2 = orderinfo.userorder;
                string paramValue4 = num.ToString();
                string yeeCardcardno = SystemApiHelper.GetYeeCardcardno(orderinfo.typeId);
                string cardNo = orderinfo.cardNo;
                string paramValue5 = num.ToString();
                string paramValue6 = num.ToString();
                string paramValue7 = orderinfo.opstate;
                if (paramValue7 == "-1")
                    paramValue7 = "1006";
                if (flag1 && paramValue7 == "0" && orderinfo.refervalue > num)
                {
                    paramValue7 = "1";
                    paramValue2 = "2";
                }
                if (orderinfo.ismulticard == 1)
                {
                    paramValue5 = orderinfo.returnovalue;
                    paramValue6 = orderinfo.returnovalue;
                    string returnopstate = orderinfo.returnopstate;
                    bool flag2 = flag1 && orderinfo.refervalue > num;
                    if (flag2)
                        paramValue2 = "2";
                    string[] strArray = returnopstate.Split(',');
                    paramValue7 = string.Empty;
                    foreach (string str6 in strArray)
                        paramValue7 = !(str6 == "0") ? (!(str6 == "-1") ? paramValue7 + str6 + "," : paramValue7 + "1006,") : (!flag2 ? paramValue7 + str6 + "," : paramValue7 + "1,");
                    if (!string.IsNullOrEmpty(paramValue7))
                        paramValue7 = paramValue7.Substring(0, paramValue7.Length - 1);
                }
                string attach = orderinfo.attach;
                string paramValue8 = "0M";
                string paramValue9 = "";
                string paramValue10 = Digest.HmacSign("" + paramValue1 + paramValue2 + paramValue3 + userorder2 + paramValue4 + yeeCardcardno + cardNo + paramValue5 + paramValue6 + paramValue7 + attach + paramValue8 + paramValue9, apiKey);
                stringBuilder1.AppendFormat("r0_Cmd={0}", (object)SystemApiHelper.UrlEncode(paramValue1));
                stringBuilder1.AppendFormat("&r1_Code={0}", (object)SystemApiHelper.UrlEncode(paramValue2));
                stringBuilder1.AppendFormat("&p1_MerId={0}", (object)SystemApiHelper.UrlEncode(paramValue3));
                stringBuilder1.AppendFormat("&p2_Order={0}", (object)SystemApiHelper.UrlEncode(userorder2));
                stringBuilder1.AppendFormat("&p3_Amt={0}", (object)SystemApiHelper.UrlEncode(paramValue4));
                stringBuilder1.AppendFormat("&p4_FrpId={0}", (object)SystemApiHelper.UrlEncode(yeeCardcardno));
                stringBuilder1.AppendFormat("&p5_CardNo={0}", (object)SystemApiHelper.UrlEncode(cardNo));
                stringBuilder1.AppendFormat("&p6_confirmAmount={0}", (object)SystemApiHelper.UrlEncode(paramValue5));
                stringBuilder1.AppendFormat("&p7_realAmount={0}", (object)SystemApiHelper.UrlEncode(paramValue6));
                stringBuilder1.AppendFormat("&p8_cardStatus={0}", (object)SystemApiHelper.UrlEncode(paramValue7));
                stringBuilder1.AppendFormat("&p9_MP={0}", (object)SystemApiHelper.UrlEncode(attach));
                stringBuilder1.AppendFormat("&pb_BalanceAmt={0}", (object)SystemApiHelper.UrlEncode(paramValue8));
                stringBuilder1.AppendFormat("&pc_BalanceAct={0}", (object)SystemApiHelper.UrlEncode(paramValue9));
                stringBuilder1.AppendFormat("&hmac={0}", (object)SystemApiHelper.UrlEncode(paramValue10));
                str2 = str2.IndexOf("?") <= 0 ? str2 + "?" + stringBuilder1.ToString() : str2 + "&" + stringBuilder1.ToString();
            }
            else if (version == SystemApiHelper.vcYee20)
            {
                string paramValue1 = "AnnulCard";
                string paramValue2 = "0";
                if (orderinfo.status == 2)
                    paramValue2 = "1";
                string userorder2 = orderinfo.userorder;
                string orderid = orderinfo.orderid;
                string attach = orderinfo.attach;
                string paramValue3 = num.ToString();
                string cardNo = orderinfo.cardNo;
                string str6 = orderinfo.userid.ToString();
                string paramValue4 = Digest.HmacSign(paramValue1 + paramValue2 + str6 + userorder2 + orderid + attach + paramValue3, apiKey);
                stringBuilder1.AppendFormat("r0_Cmd={0}", (object)SystemApiHelper.UrlEncode(paramValue1));
                stringBuilder1.AppendFormat("&r1_Code={0}", (object)SystemApiHelper.UrlEncode(paramValue2));
                stringBuilder1.AppendFormat("&rb_Order={0}", (object)SystemApiHelper.UrlEncode(userorder2));
                stringBuilder1.AppendFormat("&r2_TrxId={0}", (object)SystemApiHelper.UrlEncode(orderid));
                stringBuilder1.AppendFormat("&pa_MP={0}", (object)SystemApiHelper.UrlEncode(attach));
                stringBuilder1.AppendFormat("&rc_Amt={0}", (object)SystemApiHelper.UrlEncode(paramValue3));
                stringBuilder1.AppendFormat("&rq_CardNo={0}", (object)SystemApiHelper.UrlEncode(cardNo));
                stringBuilder1.AppendFormat("&hmac={0}", (object)SystemApiHelper.UrlEncode(paramValue4));
                str2 = str2.IndexOf("?") <= 0 ? str2 + "?" + stringBuilder1.ToString() : str2 + "&" + stringBuilder1.ToString();
            }
            return str2;
        }

        public static string Get70errtype(int suppid, string errtype)
        {
            string str = string.Empty;
            if (suppid == 60866 || suppid == 70)
                str = errtype;
            else if (suppid == 80)
            {
                switch (errtype)
                {
                    case "7":
                        str = "1001";
                        break;
                    case "1008":
                        str = "1002";
                        break;
                    case "2018":
                        str = "1003";
                        break;
                    case "2005":
                        str = "1005";
                        break;
                    case "2009":
                        str = "1007";
                        break;
                    case "2013":
                        str = "1006";
                        break;
                    case "2019":
                        str = "1008";
                        break;
                    case "10000":
                        str = "1009";
                        break;
                }
            }
            else if (suppid == 102)
            {
                switch (errtype)
                {
                    case "7":
                        str = "1001";
                        break;
                    case "1008":
                        str = "1002";
                        break;
                    case "1007":
                        str = "1003";
                        break;
                    case "1002":
                    case "1010":
                    case "2005":
                    case "2006":
                        str = "1005";
                        break;
                    case "2007":
                        str = "1007";
                        break;
                    case "2013":
                        str = "1006";
                        break;
                    case "1006":
                    case "1003":
                    case "2008":
                    case "2009":
                    case "2010":
                    case "2011":
                    case "2012":
                    case "2014":
                        str = "1008";
                        break;
                    case "10000":
                        str = "1009";
                        break;
                }
            }
            return str;
        }

        public static string UrlEncode(string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
                return string.Empty;
            return HttpUtility.UrlEncode(paramValue, Encoding.GetEncoding("GB2312"));
        }

        public static string ConverBankCode(string version, string bankcode)
        {
            string str1 = string.Empty;
            if (!(version == SystemApiHelper.v7010))
                return str1;
            string str2;
            switch (bankcode)
            {
                case "1001":
                    str2 = "940";
                    break;
                case "1002":
                    str2 = "967";
                    break;
                case "1005":
                    str2 = "964";
                    break;
                case "1003":
                    str2 = "965";
                    break;
                case "1052":
                    str2 = "963";
                    break;
                case "1004":
                    str2 = "947";
                    break;
                case "1020":
                    str2 = "981";
                    break;
                case "1006":
                    str2 = "980";
                    break;
                case "1008":
                    str2 = "944";
                    break;
                case "1027":
                    str2 = "985";
                    break;
                case "1021":
                    str2 = "962";
                    break;
                case "1025":
                    str2 = "982";
                    break;
                case "1009":
                    str2 = "942";
                    break;
                case "1032":
                    str2 = "989";
                    break;
                case "1022":
                    str2 = "986";
                    break;
                case "1010":
                    str2 = "948";
                    break;
                case "1024":
                    str2 = "945";
                    break;
                case "1028":
                    str2 = "941";
                    break;
                case "1101":
                    str2 = "992";
                    break;
                case "1102":
                    str2 = "993";
                    break;
                default:
                    str2 = "967";
                    break;
            }
            return str2;
        }

        public static string ConverCardCode(string version, string cardtype, string cardno)
        {
            string str = string.Empty;
            if (!(version == SystemApiHelper.vc7010))
                return str;
            switch (cardtype)
            {
                case "cm":
                    str = "103";
                    break;
                case "sd":
                    str = "104";
                    if (ChannelType.IsShengFuTong(cardno))
                    {
                        str = "210";
                        break;
                    }
                    break;
                case "zt":
                    str = "105";
                    break;
                case "jw":
                    str = "106";
                    break;
                case "qq":
                    str = "107";
                    break;
                case "cc":
                    str = "108";
                    break;
                case "jy":
                    str = "109";
                    break;
                case "wy":
                    str = "110";
                    break;
                case "wm":
                    str = "111";
                    break;
                case "sh":
                    str = "112";
                    break;
                case "dx":
                    str = "113";
                    break;
                case "gy":
                    str = "115";
                    break;
                case "zy":
                    str = "117";
                    break;
                case "tx":
                    str = "118";
                    break;
                case "th":
                    str = "119";
                    break;
            }
            return str;
        }

        public static string Get70Paycardno(int _type)
        {
            string str = string.Empty;
            switch (_type)
            {
                case 103:
                    return "cm";
                case 104:
                case 210:
                    return "sd";
                case 105:
                    return "zt";
                case 106:
                    return "jw";
                case 107:
                    return "qq";
                case 108:
                    return "cc";
                case 109:
                    return "jy";
                case 110:
                    return "wy";
                case 111:
                    return "wm";
                case 112:
                    return "sh";
                case 113:
                    return "dx";
                case 115:
                    return "gy";
                case 117:
                    return "zy";
                case 118:
                    return "tx";
                case 119:
                    return "th";
                default:
                    return str;
            }
        }

        public static string GetYeeCardcardno(int _type)
        {
            string str = string.Empty;
            switch (_type)
            {
                case 103:
                    return "SZX";
                case 104:
                    return "SNDACARD";
                case 105:
                    return "ZHENGTU";
                case 106:
                    return "JUNNET";
                case 107:
                    return "QQCARD";
                case 108:
                    return "UNICOM";
                case 109:
                    return "JIUYOU";
                case 110:
                    return "NETEASE";
                case 111:
                    return "WANMEI";
                case 112:
                    return "SOHU";
                case 113:
                    return "TELECOM";
                case 117:
                    return "ZONGYOU";
                case 118:
                    return "TIANXIA";
                case 119:
                    return "TIANHONG";
                default:
                    return str;
            }
        }









        public static string ConverProductId(string version, string prodcutid)
        {
            string str = string.Empty;
            if (version == SystemApiHelper.vc7010)
            {
                switch (prodcutid)
                {
                    case "cm10":
                        str = "001310";
                        break;
                    case "cm20":
                        str = "001320";
                        break;
                    case "cm30":
                        str = "001330";
                        break;
                    case "cm50":
                        str = "001350";
                        break;
                    case "cm100":
                        str = "0013100";
                        break;
                    case "cm300":
                        str = "0013300";
                        break;
                    case "cm500":
                        str = "0013500";
                        break;
                    case "sd25":
                        str = "000225";
                        break;
                    case "sd30":
                        str = "000230";
                        break;
                    case "sd35":
                        str = "000235";
                        break;
                    case "sd45":
                        str = "000245";
                        break;
                    case "sd50":
                        str = "000250";
                        break;
                    case "sd100":
                        str = "0002100";
                        break;
                    case "zt10":
                        str = "000710";
                        break;
                    case "zt20":
                        str = "000720";
                        break;
                    case "zt30":
                        str = "000730";
                        break;
                    case "zt50":
                        str = "000750";
                        break;
                    case "zt60":
                        str = "000760";
                        break;
                    case "zt100":
                        str = "0007100";
                        break;
                    case "zt300":
                        str = "0007300";
                        break;
                    case "jw4":
                        str = "00034";
                        break;
                    case "jw5":
                        str = "00035";
                        break;
                    case "jw6":
                        str = "00036";
                        break;
                    case "jw10":
                        str = "000310";
                        break;
                    case "jw15":
                        str = "000315";
                        break;
                    case "jw30":
                        str = "000330";
                        break;
                    case "jw50":
                        str = "000350";
                        break;
                    case "jw100":
                        str = "0003100";
                        break;
                    case "qq5":
                        str = "00015";
                        break;
                    case "qq10":
                        str = "000110";
                        break;
                    case "qq15":
                        str = "000115";
                        break;
                    case "qq30":
                        str = "000130";
                        break;
                    case "qq60":
                        str = "000160";
                        break;
                    case "qq100":
                        str = "0001100";
                        break;
                    case "cc20":
                        str = "001420";
                        break;
                    case "cc30":
                        str = "001430";
                        break;
                    case "cc50":
                        str = "001450";
                        break;
                    case "cc100":
                        str = "0014100";
                        break;
                    case "cc300":
                        str = "0014300";
                        break;
                    case "cc500":
                        str = "0014500";
                        break;
                    case "jy5":
                        str = "00085";
                        break;
                    case "jy10":
                        str = "000810";
                        break;
                    case "jy30":
                        str = "000830";
                        break;
                    case "jy50":
                        str = "000850";
                        break;
                    case "wy10":
                        str = "000910";
                        break;
                    case "wy15":
                        str = "000915";
                        break;
                    case "wy30":
                        str = "000930";
                        break;
                    case "wm15":
                        str = "000515";
                        break;
                    case "wm30":
                        str = "000530";
                        break;
                    case "wm50":
                        str = "000550";
                        break;
                    case "wm100":
                        str = "0005100";
                        break;
                    case "sh5":
                        str = "00065";
                        break;
                    case "sh10":
                        str = "000610";
                        break;
                    case "sh15":
                        str = "000615";
                        break;
                    case "sh30":
                        str = "000630";
                        break;
                    case "sh40":
                        str = "000640";
                        break;
                    case "sh100":
                        str = "0006100";
                        break;
                    case "dx50":
                        str = "001250";
                        break;
                    case "dx100":
                        str = "0012100";
                        break;
                    case "gy10":
                        str = "001610";
                        break;
                    case "gy20":
                        str = "001620";
                        break;
                    case "gy30":
                        str = "001630";
                        break;
                    case "gy50":
                        str = "001650";
                        break;
                    case "gy100":
                        str = "0016100";
                        break;
                    case "zy10":
                        str = "002110";
                        break;
                    case "zy15":
                        str = "002115";
                        break;
                    case "zy30":
                        str = "002130";
                        break;
                    case "zy50":
                        str = "002150";
                        break;
                    case "zy100":
                        str = "0021100";
                        break;
                    case "tx10":
                        str = "002210";
                        break;
                    case "tx20":
                        str = "002220";
                        break;
                    case "tx30":
                        str = "002230";
                        break;
                    case "tx40":
                        str = "002240";
                        break;
                    case "tx50":
                        str = "002250";
                        break;
                    case "tx60":
                        str = "002260";
                        break;
                    case "tx70":
                        str = "002270";
                        break;
                    case "tx80":
                        str = "002280";
                        break;
                    case "tx90":
                        str = "002290";
                        break;
                    case "tx100":
                        str = "0022100";
                        break;
                    case "th5":
                        str = "00235";
                        break;
                    case "th10":
                        str = "002310";
                        break;
                    case "th15":
                        str = "002315";
                        break;
                    case "th30":
                        str = "002330";
                        break;
                    case "th50":
                        str = "002350";
                        break;
                }
            }
            return str;
        }

        public static bool CheckCallBackIsSuccess(string version, string callbackText)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(callbackText))
                return flag;
            string str = SystemApiHelper.Successflag(version);
            if (callbackText.StartsWith(str) || callbackText.ToLower().StartsWith(str))
                flag = true;
            return flag;
        }


        public static bool NewBankMD5Check(string version, string partner, string orderid, string payamount, string payip, string notifyurl, string returnurl, string paytype, string remark, string key, string sign)
        {
            return (Cryptography.MD5(string.Format("version={0}&partner={1}&orderid={2}&payamount={3}&payip={4}&notifyurl={5}&returnurl={6}&paytype={7}&remark={8}&key={9}", new object[] { version, partner, orderid, payamount, payip, notifyurl, returnurl, paytype, remark, key })).ToLower() == sign);
        }

        public static string NewBankNoticeUrl(OrderBankInfo orderinfo, bool isNotify)
        {
            string notifyurl = string.Empty;
            if (isNotify)
            {
                notifyurl = orderinfo.notifyurl;
            }
            else
            {
                notifyurl = orderinfo.returnurl;
            }
            if ((orderinfo != null) && !string.IsNullOrEmpty(notifyurl))
            {
                string opstate;
                string str9;
                UserInfo baseModel = UserFactory.GetBaseModel(orderinfo.userid);
                if (baseModel == null)
                {
                    return notifyurl;
                }
                string aPIKey = baseModel.APIKey;
                string str = string.Empty;
                string version = orderinfo.version;
                string userorder = orderinfo.userorder;
                StringBuilder builder = new StringBuilder();
                if ((string.IsNullOrEmpty(version) || (version == v36010)) || (version == vbmyapi10))
                {
                    opstate = orderinfo.opstate;
                    string str8 = decimal.Round(orderinfo.realvalue.Value, 2).ToString();
                    str = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", new object[] { userorder, opstate, str8, aPIKey }));
                    builder.AppendFormat("orderid={0}", HttpUtility.UrlEncode(userorder));
                    builder.AppendFormat("&opstate={0}", HttpUtility.UrlEncode(opstate));
                    builder.AppendFormat("&ovalue={0}", HttpUtility.UrlEncode(str8));
                    builder.AppendFormat("&systime={0}", HttpUtility.UrlEncode(orderinfo.completetime.Value.ToString("yyyy/MM/dd HH:mm:ss")));
                    if (version == vbmyapi10)
                    {
                        builder.AppendFormat("&sysorderid={0}", HttpUtility.UrlEncode(orderinfo.orderid));
                        builder.AppendFormat("&completiontime={0}", HttpUtility.UrlEncode(orderinfo.completetime.Value.ToString("yyyy/MM/dd HH:mm:ss")));
                    }
                    builder.AppendFormat("&attach={0}", HttpUtility.UrlEncode(orderinfo.attach, Encoding.GetEncoding("GB2312")));
                    builder.AppendFormat("&msg={0}", HttpUtility.UrlEncode(orderinfo.msg, Encoding.GetEncoding("GB2312")));
                    builder.AppendFormat("&sign={0}", HttpUtility.UrlEncode(str));
                    return (notifyurl + "?" + builder.ToString());
                }
                if (version == vbmyapi20)
                {
                    return PayBank20.CreateNotifyUrl(orderinfo, isNotify, aPIKey);
                }
                if (version == veypa10)
                {
                    opstate = "0";
                    if ((orderinfo.status == 2) || (orderinfo.status == 8))
                    {
                        opstate = "2";
                    }
                    str9 = decimal.Round(orderinfo.realvalue.Value, 2).ToString();
                    string str10 = orderinfo.completetime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                    string format = "version={0}&partner={1}&orderid={2}&payamount={3}&opstate={4}&orderno={5}&eypaltime={6}&message={7}&paytype={8}&remark={9}&key={10}";
                    format = string.Format(format, new object[] { "1.0", orderinfo.userid, userorder, str9, opstate, orderinfo.orderid, str10, "success", orderinfo.paymodeId, orderinfo.attach, aPIKey });
                    MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                    str = BitConverter.ToString(provider.ComputeHash(Encoding.Default.GetBytes(format))).Replace("-", "").ToLower();
                    return (((((((((((("<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + notifyurl + "\">") + "<input type=\"hidden\" name=\"version\" value=\"1.0\" />") + "<input type=\"hidden\" name=\"partner\" value=\"" + orderinfo.userid.ToString() + "\" />") + "<input type=\"hidden\" name=\"orderid\" value=\"" + userorder + "\" />") + "<input type=\"hidden\" name=\"payamount\" value=\"" + str9 + "\" />") + "<input type=\"hidden\" name=\"opstate\" value=\"" + opstate + "\" />") + "<input type=\"hidden\" name=\"eypaltime\" value=\"" + str10 + "\" />") + "<input type=\"hidden\" name=\"message\" value=\"success\" />") + "<input type=\"hidden\" name=\"paytype\" value=\"" + orderinfo.paymodeId + "\" />") + "<input type=\"hidden\" name=\"remark\" value=\"" + orderinfo.attach + "\" />") + "<input type=\"hidden\" name=\"sign\" value=\"" + str + "\" />") + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>");
                }
                if (version == v7010)
                {
                    string str13 = "11";
                    if ((orderinfo.status == 2) || (orderinfo.status == 8))
                    {
                        str13 = "1";
                    }
                    str9 = decimal.Round(orderinfo.realvalue.Value, 2).ToString();
                    str = Cryptography.MD5(string.Format(v7010BankNotifyVerifyStr, new object[] { str13, orderinfo.userid, userorder, aPIKey }));
                    builder.AppendFormat("returncode={0}", HttpUtility.UrlEncode(str13));
                    builder.AppendFormat("&userid={0}", HttpUtility.UrlEncode(orderinfo.userid.ToString()));
                    builder.AppendFormat("&orderid={0}", HttpUtility.UrlEncode(userorder));
                    builder.AppendFormat("&money={0}", HttpUtility.UrlEncode(str9));
                    builder.AppendFormat("&sign={0}", HttpUtility.UrlEncode(str));
                    builder.AppendFormat("&ext={0}", HttpUtility.UrlEncode(orderinfo.attach, Encoding.GetEncoding("GB2312")));
                    if (notifyurl.IndexOf("?") > 0)
                    {
                        notifyurl = notifyurl + "&" + builder.ToString();
                    }
                    else
                    {
                        notifyurl = notifyurl + "?" + builder.ToString();
                    }
                    return notifyurl;
                }
                if (version == vYee10)
                {
                    string str14 = "0";
                    string str15 = orderinfo.userid.ToString();
                    string str16 = "Buy";
                    if ((orderinfo.status == 2) || (orderinfo.status == 8))
                    {
                        str14 = "1";
                    }
                    string orderid = orderinfo.orderid;
                    string str18 = decimal.Round(orderinfo.realvalue.Value, 2).ToString();
                    string str19 = "RMB";
                    string str20 = orderinfo.cus_subject;
                    string str21 = orderinfo.userorder;
                    string str22 = "";
                    string attach = orderinfo.attach;
                    string str24 = "1";
                    if (isNotify)
                    {
                        str24 = "2";
                    }
                    string str26 = Digest.HmacSign(((((("" + str15) + str16 + str14) + orderid + str18) + str19 + str20) + str21 + str22) + attach + str24, aPIKey);
                    builder.AppendFormat("p1_MerId={0}", FormatQueryString(str15));
                    builder.AppendFormat("&r0_Cmd={0}", FormatQueryString(str16));
                    builder.AppendFormat("&r1_Code={0}", FormatQueryString(str14));
                    builder.AppendFormat("&r2_TrxId={0}", FormatQueryString(orderid));
                    builder.AppendFormat("&r3_Amt={0}", FormatQueryString(str18));
                    builder.AppendFormat("&r4_Cur={0}", FormatQueryString("RMB"));
                    builder.AppendFormat("&r5_Pid={0}", FormatQueryString(str20));
                    builder.AppendFormat("&r6_Order={0}", FormatQueryString(str21));
                    builder.AppendFormat("&r7_Uid={0}", FormatQueryString(str22));
                    builder.AppendFormat("&r8_MP={0}", FormatQueryString(attach));
                    builder.AppendFormat("&r9_BType={0}", FormatQueryString(str24));
                    builder.AppendFormat("&rb_BankId={0}", FormatQueryString(orderinfo.paymodeId));
                    builder.AppendFormat("&ro_BankOrderId={0}", FormatQueryString(orderinfo.supplierOrder));
                    builder.AppendFormat("&rp_PayDate={0}", FormatQueryString(orderinfo.completetime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                    builder.AppendFormat("&ro_BankOrderId={0}", FormatQueryString(orderinfo.supplierOrder));
                    builder.AppendFormat("&rq_CardNo={0}", FormatQueryString(string.Empty));
                    builder.AppendFormat("&ru_Trxtime={0}", FormatQueryString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    builder.AppendFormat("&hmac={0}", FormatQueryString(str26));
                    if (notifyurl.IndexOf("?") > 0)
                    {
                        notifyurl = notifyurl + "&" + builder.ToString();
                    }
                    else
                    {
                        notifyurl = notifyurl + "?" + builder.ToString();
                    }
                    return notifyurl;
                }
                if (version == vhq10)
                {
                    string str27 = decimal.Round(orderinfo.refervalue, 2).ToString();
                    string str28 = decimal.Round(orderinfo.realvalue.Value, 2).ToString();
                    string str29 = "116";
                    if ((orderinfo.status == 2) || (orderinfo.status == 8))
                    {
                        str29 = "0";
                    }
                    str = Cryptography.MD5(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", new object[] { orderinfo.userid, orderinfo.userorder, string.Empty, string.Empty, str27, orderinfo.cus_field2, aPIKey }));
                    builder.AppendFormat("P_UserId={0}", orderinfo.userid);
                    builder.AppendFormat("&P_OrderId={0}", orderinfo.userorder);
                    builder.AppendFormat("&P_CardId={0}", string.Empty);
                    builder.AppendFormat("&P_CardPass={0}", string.Empty);
                    builder.AppendFormat("&P_FaceValue={0}", str27);
                    builder.AppendFormat("&P_ChannelId={0}", orderinfo.cus_field2);
                    builder.AppendFormat("&P_PayMoney={0}", str28);
                    builder.AppendFormat("&P_Subject={0}", orderinfo.cus_subject);
                    builder.AppendFormat("&P_Price={0}", orderinfo.cus_price);
                    builder.AppendFormat("&P_Quantity={0}", orderinfo.cus_quantity);
                    builder.AppendFormat("&P_Description={0}", orderinfo.cus_description);
                    builder.AppendFormat("&P_Notic={0}", orderinfo.attach);
                    builder.AppendFormat("&P_ErrCode={0}", str29);
                    builder.AppendFormat("&P_ErrMsg={0}", orderinfo.msg);
                    builder.AppendFormat("&P_PostKey={0}", str);
                    if (notifyurl.IndexOf("?") > 0)
                    {
                        notifyurl = notifyurl + "&" + builder.ToString();
                    }
                    else
                    {
                        notifyurl = notifyurl + "?" + builder.ToString();
                    }
                }
            }
            return notifyurl;
        }

        public static string NewConverBankCode(string bankcode)
        {
            switch (bankcode)
            {
                case "CMB":
                    return "940";

                case "ICBC":
                    return "967";

                case "ABC":
                    return "964";

                case "CCB":
                    return "965";

                case "BOC":
                    return "963";

                case "SPDB":
                    return "947";

                case "BOCM":
                    return "981";

                case "CMBC":
                    return "980";

                case "SDD":
                    return "944";

                case "CGB":
                    return "985";

                case "CTITC":
                    return "962";

                case "HXB":
                    return "982";

                case "CIB":
                    return "942";

                case "BCCB":
                    return "989";

                case "CEB":
                    return "986";

                case "SDB":
                    return "948";

                case "SHBANK":
                    return "945";

                case "PSBC":
                    return "941";

                case "1101":
                    return "992";

                case "1102":
                    return "993";

                case "UNION":
                    return "996";

                case "SHNS":
                    return "946";

                case "BOHAI":
                    return "998";

                case "ALIPAY":
                    return "992";

                case "TENPAY":
                    return "993";

                case "WECHAT":
                    return "1004";
            }
            return "UNION";
        }


        public static string Successflag(string version)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(version) || version == SystemApiHelper.vbmyapi10 || version == SystemApiHelper.v36010 || version == SystemApiHelper.vcmyapi10)
                str = SystemApiHelper.v36010BankNotifySuccessflag;
            else if (version == SystemApiHelper.vcmyapi20)
                str = SellCard20.Successflag;
            else if (version == SystemApiHelper.v7010)
                str = SystemApiHelper.v7010BankNotifySuccessflag;
            else if (version == SystemApiHelper.vc7010)
                str = SystemApiHelper.vc7010CardNotifySuccessflag;
            else if (version == SystemApiHelper.vYee10)
                str = SystemApiHelper.vYee10BankNotifySuccessflag;
            else if (version == SystemApiHelper.vcYee10)
                str = SystemApiHelper.vcYee10NotifySuccessflag;
            else if (version == SystemApiHelper.vcYee20)
                str = SystemApiHelper.vcYee20NotifySuccessflag;
            else if (version == SystemApiHelper.vhq10)
                str = SystemApiHelper.vhq10NotifySuccessflag;
            return str;
        }

        public static string GetVersionName(string version)
        {
            string str = string.Empty;
            if (version == SystemApiHelper.v7010)
                str = SystemApiHelper.v7010ApiName;
            if (version == SystemApiHelper.vcmyapi20)
                str = SellCard20.VersionName;
            if (version == SystemApiHelper.vbmyapi20)
                str = PayBank20.VersionName;
            else if (version == SystemApiHelper.vc7010)
                str = SystemApiHelper.vc7010ApiName;
            else if (version == SystemApiHelper.vYee10)
                str = SystemApiHelper.vYee10ApiName;
            else if (version == SystemApiHelper.vcYee10)
                str = SystemApiHelper.vcYee10ApiName;
            else if (version == SystemApiHelper.vcYee20)
                str = SystemApiHelper.vcYee20ApiName;
            else if (version == SystemApiHelper.IPS20)
                str = SystemApiHelper.IPS20ApiName;
            return str;
        }

        public static int CodeMapping(int typeid)
        {
            int num = 0;
            switch (typeid)
            {
                case 103:
                    num = 13;
                    break;
                case 104:
                    num = 2;
                    break;
                case 105:
                    num = 7;
                    break;
                case 106:
                    num = 3;
                    break;
                case 107:
                    num = 1;
                    break;
                case 108:
                    num = 14;
                    break;
                case 109:
                    num = 8;
                    break;
                case 110:
                    num = 9;
                    break;
                case 111:
                    num = 5;
                    break;
                case 112:
                    num = 6;
                    break;
                case 113:
                    num = 12;
                    break;
                case 115:
                    num = 16;
                    break;
                case 116:
                    num = 15;
                    break;
                case 117:
                    num = 21;
                    break;
                case 118:
                    num = 22;
                    break;
                case 119:
                    num = 23;
                    break;
                case 200:
                    num = 17;
                    break;
                case 201:
                    num = 18;
                    break;
                case 202:
                    num = 19;
                    break;
                case 203:
                    num = 20;
                    break;
                case 204:
                    num = 10;
                    break;
                case 205:
                    num = 11;
                    break;
                case 210:
                    num = 28;
                    break;
            }
            return num;
        }

        public static int ConvertChannelCode(string ver, string typeid)
        {
            int num = 0;
            if (ver == SystemApiHelper.vhq10)
            {
                switch (typeid)
                {
                    case "1":
                        num = 102;
                        break;
                    case "2":
                        num = 101;
                        break;
                    case "3":
                        num = 100;
                        break;
                    case "4":
                        num = 107;
                        break;
                    case "5":
                        num = 104;
                        break;
                    case "6":
                        num = 106;
                        break;
                    case "7":
                        num = 111;
                        break;
                    case "8":
                        num = 112;
                        break;
                    case "9":
                        num = 105;
                        break;
                    case "10":
                        num = 109;
                        break;
                    case "11":
                        num = 110;
                        break;
                    case "12":
                        num = 117;
                        break;
                    case "13":
                        num = 113;
                        break;
                    case "14":
                        num = 103;
                        break;
                    case "15":
                        num = 108;
                        break;
                    case "16":
                        num = 116;
                        break;
                    case "17":
                        num = 115;
                        break;
                    case "18":
                        num = 118;
                        break;
                    case "19":
                        num = 119;
                        break;
                }
            }
            return num;
        }
    }
}
