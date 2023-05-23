using System;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.ETAPI
{
    public class LianXinRMB1 : ETAPIBase
    {
        private static int suppId = 901;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/LianXin/Bank_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/LianXin/Bank_Notify.aspx";
            }
        }

        public LianXinRMB1()
          : base(LianXinRMB1.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string BankID, bool isform)
        {
            string str1 = this.postBankUrl;
            if (string.IsNullOrEmpty(str1))
                str1 = "http://superapi.kltong.me:9180/busias/PayRequest";
            BankID = this.GetBankCode(BankID);
            orderAmt = Decimal.Round(orderAmt, 2);
            string str2 = "15";
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            string str6 = Cryptography.MD5(this.suppAccount + orderid + str2 + BankID + this.notifyUrl + orderAmt.ToString() + this.suppKey);
            string str7 = string.Empty;
            string str8;
            if (!isform)
                str8 = string.Format("{0}?MerchantID={1}&MerOrderNo={2}&CardType={3}&BankID={4}&Money={5}&CustomizeA={6}&CustomizeB={7}&CustomizeC={8}&NoticeURL={9}&NoticePage={10}&sign={11}", (object)str1, (object)this.suppAccount, (object)orderid, (object)str2, (object)BankID, (object)orderAmt, (object)str3, (object)str4, (object)str5, (object)this.notifyUrl, (object)this.returnurl, (object)str6);
            else
                str8 = string.Concat(new object[4]
                {
          (object) ("<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"" + str1 + "\">" + "<input type=\"hidden\" name=\"MerchantID\" value=\"" + this.suppAccount + "\" />" + "<input type=\"hidden\" name=\"MerOrderNo\" value=\"" + orderid + "\" />" + "<input type=\"hidden\" name=\"CardType\" value=\"" + str2 + "\" />" + "<input type=\"hidden\" name=\"BankID\" value=\"" + BankID + "\" />"),
          (object) "<input type=\"hidden\" name=\"Money\" value=\"",
          (object) orderAmt,
          (object) "\" />"
                }) + "<input type=\"hidden\" name=\"CustomizeA\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"CustomizeB\" value=\"" + str4 + "\" />" + "<input type=\"hidden\" name=\"CustomizeC\" value=\"" + str5 + "\" />" + "<input type=\"hidden\" name=\"NoticeURL\" value=\"" + this.notifyUrl + "\" />" + "<input type=\"hidden\" name=\"NoticePage\" value=\"" + this.returnurl + "\" />" + "<input type=\"hidden\" name=\"sign\" value=\"" + str6 + "\" />" + "</form>";
            return str8;
        }

        public string GetPayForm(string orderid, Decimal orderAmt, string bankCode)
        {
            string str1 = "15";
            string bankCode1 = this.GetBankCode(bankCode);
            string str2 = Cryptography.MD5(this.suppAccount + orderid + str1 + bankCode1 + this.notifyUrl + orderAmt.ToString() + this.suppKey);
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            string str6 = this.postBankUrl;
            if (string.IsNullOrEmpty(str6))
                str6 = "http://superapi.kltong.me:9180/busias/PayRequest";
            return "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + str6 + "\">" + "<input type=\"hidden\" name=\"MerchantID\" value=\"" + this.suppAccount + "\" />" + "<input type=\"hidden\" name=\"MerOrderNo\" value=\"" + orderid + "\" />" + "<input type=\"hidden\" name=\"CardType\" value=\"" + str1 + "\" />" + "<input type=\"hidden\" name=\"BankID\" value=\"" + bankCode1 + "\" />" + "<input type=\"hidden\" name=\"Money\" value=\"" + orderAmt.ToString() + "\" />" + "<input type=\"hidden\" name=\"CustomizeA\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"CustomizeB\" value=\"" + str4 + "\" />" + "<input type=\"hidden\" name=\"CustomizeC\" value=\"" + str5 + "\" />" + "<input type=\"hidden\" name=\"NoticeURL\" value=\"" + this.notifyUrl + "\" />" + "<input type=\"hidden\" name=\"returnurl\" value=\"" + this.returnurl + "\" />" + "<input type=\"hidden\" name=\"sign\" value=\"" + str2 + "\" />" + "</form>";
        }

        public void ReturnBank()
        {
            HttpRequest request = HttpContext.Current.Request;
            string supplierOrderId = request["PayOrderNo"].Trim();
            string str1 = request["MerchantID"].Trim();
            string orderId = request["MerOrderNo"].Trim();
            string str2 = request["CardNo"].Trim();
            string str3 = request["CardType"].Trim();
            string s = request["FactMoney"].Trim();
            string str4 = request["PayResult"].Trim();
            string str5 = request["CustomizeA"].Trim();
            string str6 = request["CustomizeB"].Trim();
            string str7 = request["CustomizeC"].Trim();
            string str8 = HttpUtility.UrlDecode(request["PayTime"].Trim());
            string msg = HttpUtility.UrlDecode(request["ErrorMsg"].Trim());
            string str9 = request["sign"].Trim();
            string str10 = Cryptography.MD5(supplierOrderId + str1 + orderId + str2 + str3 + s + str4 + str5 + str6 + str7 + str8 + this.suppKey);
            try
            {
                if (!(str10 == str9))
                    return;
                string opstate = "-1";
                int status = 4;
                if (str4.ToLower() == "true")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(LianXinRMB1.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), new Decimal(0), false, true);
                HttpContext.Current.Response.Write("OK");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        private string GetErrorInfo(string ErrorCode)
        {
            string str = ErrorCode;
            switch (ErrorCode)
            {
                case "-1":
                    str = "系统忙";
                    break;
                case "1":
                    str = "商户订单号无效";
                    break;
                case "2":
                    str = "银行编码错误";
                    break;
                case "3":
                    str = "商户不存在";
                    break;
                case "4":
                    str = "验证签名失败";
                    break;
                case "5":
                    str = "商户储值关闭";
                    break;
                case "6":
                    str = "金额超出限额";
                    break;
            }
            return str;
        }

        public void Notify()
        {
            HttpRequest request = HttpContext.Current.Request;
            string supplierOrderId = request["PayOrderNo"].Trim();
            string str1 = request["MerchantID"].Trim();
            string orderId = request["MerOrderNo"].Trim();
            string str2 = request["CardNo"].Trim();
            string str3 = request["CardType"].Trim();
            string s = request["FactMoney"].Trim();
            string str4 = request["PayResult"].Trim();
            string str5 = request["CustomizeA"].Trim();
            string str6 = request["CustomizeB"].Trim();
            string str7 = request["CustomizeC"].Trim();
            string str8 = HttpUtility.UrlDecode(request["PayTime"].Trim());
            string msg = HttpUtility.UrlDecode(request["ErrorMsg"].Trim());
            string str9 = request["sign"].Trim();
            string str10 = Cryptography.MD5(supplierOrderId + str1 + orderId + str2 + str3 + s + str4 + str5 + str6 + str7 + str8 + this.suppKey).ToUpper();
            try
            {
                if (!(str10 == str9))
                    return;
                string opstate = "-1";
                int status = 4;
                if (str4.ToLower() == "true")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(LianXinRMB1.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), new Decimal(0), true, false);
                HttpContext.Current.Response.Write("OK");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public string GetBankCode(string paymodeId)
        {
            string str1 = string.Empty;
            string str2;
            switch (paymodeId)
            {
                case "970":
                    str2 = "CMB";
                    break;
                case "967":
                    str2 = "ICBC";
                    break;
                case "964":
                    str2 = "ABC";
                    break;
                case "965":
                    str2 = "CCB";
                    break;
                case "963":
                    str2 = "BOC";
                    break;
                case "977":
                    str2 = "SPDB";
                    break;
                case "981":
                    str2 = "COMM";
                    break;
                case "980":
                    str2 = "CMBC";
                    break;
                case "974":
                    str2 = "SDB";
                    break;
                case "985":
                    str2 = "GDB";
                    break;
                case "962":
                    str2 = "CITIC";
                    break;
                case "982":
                    str2 = "HXB";
                    break;
                case "972":
                    str2 = "CIB";
                    break;
                case "984":
                    str2 = "GNXS";
                    break;
                case "1015":
                    str2 = "GZCB";
                    break;
                case "976":
                    str2 = "SHRCB";
                    break;
                case "989":
                    str2 = "BCCB";
                    break;
                case "988":
                    str2 = "CBHB";
                    break;
                case "990":
                    str2 = "BJRCB";
                    break;
                case "979":
                    str2 = "NJCB";
                    break;
                case "986":
                    str2 = "CEB";
                    break;
                case "987":
                    str2 = "HKBEA";
                    break;
                case "1025":
                    str2 = "NBCB";
                    break;
                case "983":
                    str2 = "HCCB";
                    break;
                case "978":
                    str2 = "SZPAB";
                    break;
                case "975":
                    str2 = "BOS";
                    break;
                case "971":
                    str2 = "PSBC";
                    break;
                default:
                    str2 = "ICBC";
                    break;
            }
            return str2;
        }
    }
}
