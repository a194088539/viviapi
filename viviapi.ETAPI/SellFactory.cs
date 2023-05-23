using System;
using System.Web;
using viviapi.BLL;
using viviapi.BLL.Sys;
using viviapi.ETAPI.Alipay;
using viviapi.ETAPI.Huida;
using viviapi.ETAPI.KuaiQian;
using viviapi.ETAPI.ShenZhouFu;
using viviapi.Model;
using viviapi.SysConfig;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.ETAPI
{
    public class SellFactory
    {
        public static string SellCard(SupplierCode supp, string orderid, int cardtype, string cardno, string cardpass, string attach, int cardfacevalue, out string supporderid, out string supperrorcode, out string errormsg)
        {
            supporderid = string.Empty;
            supperrorcode = string.Empty;
            errormsg = string.Empty;
            string str = string.Empty;
            try
            {
                if (supp == SupplierCode.Card51esales)
                    str = new viviapi.ETAPI.Card51esales.Card().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out supporderid, out errormsg);
                if (supp == SupplierCode.Mi55)
                    str = new viviapi.ETAPI.Mi55.Card().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out supporderid, out errormsg);
                else if (supp == SupplierCode.Cared70)
                    str = new Cared70().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out supporderid, out errormsg);
                else if (supp == SupplierCode.OfCard)
                    str = new OfCard().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out supperrorcode, out errormsg);
                else if (supp == SupplierCode.HuiYuan)
                    str = new viviapi.ETAPI.huiyuan.Card().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out supperrorcode, out errormsg);
                else if (supp == SupplierCode.ShenZhouFu)
                    str = new card().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out errormsg);
                else if (supp == SupplierCode.Jiexun)
                    str = new Jiexun().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out errormsg);
                else if (supp == SupplierCode.LianXinCard)
                    str = new LianXinCard().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out supporderid, out errormsg);
                else if (supp == SupplierCode.YeePay)
                    str = new viviapi.ETAPI.YeePay.Card().GetPayUrl(orderid, cardno, cardpass, cardtype, cardfacevalue, out errormsg);
                else if (supp == SupplierCode.EkaCard)
                    str = new EkaCard().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out supporderid, out errormsg);
                else if (supp == SupplierCode.LongBaoPay)
                    str = new viviapi.ETAPI.Longbao.Card().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out supporderid, out supperrorcode, out errormsg);
                else if (supp == SupplierCode.Youka)
                    str = new viviapi.ETAPI.Youka.Card().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out supporderid, out supperrorcode, out errormsg);
                else if (supp == SupplierCode.Shengpay)
                    str = new viviapi.ETAPI.Shengpay.Card().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out errormsg);
                else if (supp == SupplierCode.KuaiQian)
                {
                    if (cardtype == 103 || cardtype == 108 || cardtype == 113 || cardtype == 106)
                        str = new ShenZhouXing().GetPayUrl(orderid, cardno, cardpass, cardtype, cardfacevalue, out errormsg);
                }
                else if (supp == SupplierCode.LianXinCard1)
                    str = new LianXinCard1().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out supporderid, out errormsg);
                else if (supp == SupplierCode.LianXinCard2)
                    str = new LianXinCard2().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out supporderid, out errormsg);
                else if (supp == SupplierCode.Card60866)
                    str = new Card60866().CardSend(orderid, cardno, cardpass, cardtype, cardfacevalue, out supporderid, out supperrorcode, out errormsg);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return str;
        }

        public static bool OnlineBankPay(int suppid, string orderid, Decimal orderAmt, string bankcode)
        {
            if (SupplierFactory.GetCacheModel(suppid) == null)
                return false;
            if (PaymentSetting.showjubao)
            {
                string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + Constant.ParameterEncryptionKey;
                HttpContext.Current.Response.Redirect(string.Format("/GoPay.aspx?suppId={0}&orderAmt={1}&orderid={2}&sign={3}&bankcode={4}", (object)suppid, (object)orderAmt, (object)orderid, (object)Cryptography.MD5(strToEncrypt), (object)bankcode), true);
                return false;
            }
            string str1 = "get";
            string str2 = string.Empty;
            if (suppid == 70 || suppid == 90)
            {
                string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + Constant.ParameterEncryptionKey;
                str2 = string.Format("/postToBank.aspx?suppId={0}&orderAmt={1}&orderid={2}&sign={3}&bankcode={4}", (object)suppid, (object)orderAmt, (object)orderid, (object)Cryptography.MD5(strToEncrypt), (object)bankcode);
            }
            else if (suppid == 70)
            {
                str2 = new YZCHRMB().PayBank(orderid, orderAmt, bankcode, true);

                str1 = "post";
            }
            else if (suppid == 10001)
            {
                str2 = new YaFu.Bank().PayBank(orderid, orderAmt, bankcode);

                if (!str2.Contains("error"))
                {
                    string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + bankcode + str2.Replace("&", "qfand") + Constant.ParameterEncryptionKey;
                    str2 = string.Format("/PCCode/index.aspx?suppId={0}&orderAmt={1}&orderid={2}&bankcode={3}&payurl={4}&sign={5}", suppid, orderAmt, orderid, bankcode, str2.Replace("&", "qfand"), Cryptography.MD5(strToEncrypt));
                    str1 = "get";
                }
                else
                {
                    str1 = "post";
                    HttpContext.Current.Response.Write(str2);
                }

            }
            else if (suppid == 10002)
            {
                str2 = new TongLian.Code().PayBank(orderid, orderAmt, bankcode);

                if (!str2.Contains("error"))
                {
                    string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + bankcode + str2.Replace("&", "qfand") + Constant.ParameterEncryptionKey;
                    str2 = string.Format("/PCCode/index.aspx?suppId={0}&orderAmt={1}&orderid={2}&bankcode={3}&payurl={4}&sign={5}", suppid, orderAmt, orderid, bankcode, str2.Replace("&", "qfand"), Cryptography.MD5(strToEncrypt));
                    str1 = "get";
                }
                else
                {
                    str1 = "post";
                    HttpContext.Current.Response.Write(str2);
                }

            }
            else if (suppid == 10003)
            {
                if (bankcode != "992" && bankcode != "1006" && bankcode != "1007" && bankcode != "1004" && bankcode != "1008" && bankcode != "1009")
                {
                    str2 = new TongLian2.Bank().PayBank(orderid, orderAmt, bankcode);
                    str1 = "post";
                }
                else
                {
                    str2 = new TongLian2.Code().PayBank(orderid, orderAmt, bankcode);

                    if (!str2.Contains("error"))
                    {
                        string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + bankcode + str2.Replace("&", "qfand") + Constant.ParameterEncryptionKey;
                        str2 = string.Format("/PCCode/index.aspx?suppId={0}&orderAmt={1}&orderid={2}&bankcode={3}&payurl={4}&sign={5}", suppid, orderAmt, orderid, bankcode, str2.Replace("&", "qfand"), Cryptography.MD5(strToEncrypt));
                        str1 = "get";
                    }
                    else
                    {
                        str1 = "post";
                        HttpContext.Current.Response.Write(str2);
                    }

                }
            }
            else if (suppid == 10004)//信付宝微信网银
            {
                if (bankcode != "992" && bankcode != "1006" && bankcode != "1007" && bankcode != "1004" && bankcode != "1008" && bankcode != "1009")
                {
                    str2 = new XinFuBao.Bank().PayBank(orderid, orderAmt, bankcode);
                    str1 = "post";
                }
                else
                {
                    string url = new XinFuBao.Code().PayBank(orderid, orderAmt, bankcode);
                    if (!url.Contains("error"))
                    {
                        string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + bankcode + url + Constant.ParameterEncryptionKey;
                        str2 = string.Format("/PCCode/index.aspx?suppId={0}&orderAmt={1}&orderid={2}&bankcode={3}&payurl={4}&wx=0&sign={5}", suppid, orderAmt, orderid, bankcode, url.Replace("&", "qfand"), Cryptography.MD5(strToEncrypt));
                        str1 = "get";
                    }
                    else
                    {
                        HttpContext.Current.Response.Write(url);
                        HttpContext.Current.Response.End();
                    }
                }
            }
            else if (suppid == 10005)//信付宝京东QQ
            {
                string url = new XinFuBao.Code2().PayBank(orderid, orderAmt, bankcode);
                if (!url.Contains("error"))
                {
                    string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + bankcode + url + Constant.ParameterEncryptionKey;
                    str2 = string.Format("/PCCode/index.aspx?suppId={0}&orderAmt={1}&orderid={2}&bankcode={3}&payurl={4}&wx=0&sign={5}", suppid, orderAmt, orderid, bankcode, url.Replace("&", "qfand"), Cryptography.MD5(strToEncrypt));
                    str1 = "get";
                }
                else
                {
                    HttpContext.Current.Response.Write(url);
                    HttpContext.Current.Response.End();
                }
            }
            else if (suppid == 10006)
            {
                str2 = new viviapi.ETAPI.Yt.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10008)
            {
                if (bankcode != "1004" && bankcode != "992" && bankcode != "1008" && bankcode != "2001" && bankcode != "2003" && bankcode != "2006")
                {
                    str2 = new viviapi.ETAPI.Qingyifu.Bank().PayBank(orderid, orderAmt, bankcode);
                    str1 = "post";
                }
                else
                {
                    string url = new viviapi.ETAPI.Qingyifu.Bank().PayBank(orderid, orderAmt, bankcode);
                    if (!url.Contains("error"))
                    {
                        string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + bankcode + url + Constant.ParameterEncryptionKey;
                        str2 = string.Format("/PCCode/index.aspx?suppId={0}&orderAmt={1}&orderid={2}&bankcode={3}&payurl={4}&wx=0&sign={5}", suppid, orderAmt, orderid, bankcode, url.Replace("&", "qfand"), Cryptography.MD5(strToEncrypt));
                        str1 = "get";
                    }
                    else
                    {
                        HttpContext.Current.Response.Write(url);
                        HttpContext.Current.Response.End();
                    }
                }
            }
            else if (suppid == 10009)
            {
                str2 = new viviapi.ETAPI.Huida.pay().PayBank(orderid, orderAmt, bankcode, HttpContext.Current);
                str1 = "post";
            }
            else if (suppid == 10010)
            {
                str2 = new Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10011)
            {
                str2 = new viviapi.ETAPI.Yutou.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10012)
            {
                string url = new weifutong.Bank().PayBank(orderid, orderAmt, bankcode);
                if (!url.Contains("error"))
                {
                    string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + bankcode + url + Constant.ParameterEncryptionKey;
                    str2 = string.Format("/QRCode/Index.aspx?suppId={0}&orderAmt={1}&orderid={2}&bankcode={3}&payurl={4}&sign={5}", suppid, orderAmt, orderid, bankcode, url, Cryptography.MD5(strToEncrypt));
                    str1 = "get";
                }
                else
                {
                    HttpContext.Current.Response.Write(url);
                    HttpContext.Current.Response.End();

                }
            }
            else if (suppid == 10013)
            {
                str2 = new Youqi.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10014)
            {
                str2 = new Changf.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10015)
            {
                str2 = new viviapi.ETAPI.Dinpay.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10016)
            {
                str2 = new viviapi.ETAPI.Yingmin.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "get";
            }
            else if (suppid == 101)
            {
                if (bankcode == "101")
                {
                    string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + Constant.ParameterEncryptionKey;
                    str2 = string.Format("/postToBank.aspx?suppId={0}&orderAmt={1}&orderid={2}&sign={3}&bankcode={4}", (object)suppid, (object)orderAmt, (object)orderid, (object)Cryptography.MD5(strToEncrypt), (object)bankcode);
                }
                else
                {
                    str2 = new AliPayMApi().GetPayForm(orderid, orderAmt, bankcode);
                    str1 = "post";
                }
            }
            else if (suppid == 101)
            {
                str2 = new viviapi.ETAPI.AlipayWap.Bank().PayBankApp(orderid, orderAmt, bankcode);
                str1 = "get";
            }
            else if (suppid == 10018)
            {
                str2 = new viviapi.ETAPI.Youka.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "get";
            }
            else if (suppid == 10019)
            {
                str2 = new AliPayBase().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10020)
            {
                str2 = new viviapi.ETAPI.Ddbill.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10021)
            {
                str2 = new Gopay().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10022)
            {
                str2 = new IPS70().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10023)
            {
                str2 = new IPSCode().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10024)
            {
                str2 = new viviapi.ETAPI.Mobaopay.pay().PayBank(orderid, orderAmt, bankcode, HttpContext.Current);
                str1 = "post";
            }
            else if (suppid == 10025)
            {
                str2 = new viviapi.ETAPI.YeePay.RMB().GetPayForm(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10026)
            {
                str2 = new viviapi.ETAPI.KuaiQian.RMB().GetPayUrl(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10027)
            {
                str2 = new viviapi.ETAPI.Heepay.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10028)
            {
                str2 = new viviapi.ETAPI.Baofoo.Bank().PayBank(orderid, orderAmt, bankcode, false);
                str1 = "post";
            }

            else if (suppid == 10030)
            {
                str2 = new viviapi.ETAPI.Ebatong.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10031)
            {
                str2 = new HuiChao.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10032)
            {
                if (bankcode != "992" && bankcode != "1004" && bankcode != "1008" && bankcode != "2001" && bankcode != "2003" && bankcode != "1007" && bankcode != "1006" && bankcode != "1009" && bankcode != "2002" && bankcode != "2005" && bankcode != "2008")
                {
                    str2 = new xunyou.Bank().PayBank(orderid, orderAmt, bankcode);
                    str1 = "post";
                }
                else
                {
                    string url = new xunyou.Scan().PayScan(orderid, orderAmt, bankcode);
                    if (!url.Contains("error"))
                    {
                        string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + bankcode + url + Constant.ParameterEncryptionKey;
                        str2 = string.Format("/PCCode/index.aspx?suppId={0}&orderAmt={1}&orderid={2}&bankcode={3}&payurl={4}&wx=0&sign={5}", suppid, orderAmt, orderid, bankcode, url.Replace("&", "qfand"), Cryptography.MD5(strToEncrypt));
                        str1 = "get";
                    }
                    else
                    {
                        HttpContext.Current.Response.Write(url);
                        HttpContext.Current.Response.End();
                    }
                }
            }
            else if (suppid == 10033)
            {
                str2 = new suiszf.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10034)
            {
                str2 = new viviapi.ETAPI._51upay.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10035)
            {
                str2 = new viviapi.ETAPI._19pay.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10036)
            {
                if (bankcode != "1000" && bankcode != "1004" && bankcode != "992" && bankcode != "1008" && bankcode != "1009" && bankcode != "2001" && bankcode != "2006" && bankcode != "2007")
                {
                    str2 = new viviapi.ETAPI.ruilianpay.Bank().PayBank(orderid, orderAmt, bankcode);
                    str1 = "post";
                }
                else
                {
                    str2 = new viviapi.ETAPI.ruilianpay.Bank().PayCode(orderid, orderAmt, bankcode);
                    str1 = "post";
                }
            }
            else if (suppid == 10037)
            {
                str2 = new viviapi.ETAPI.yeepaycdn.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10038)
            {
                if (bankcode != "992" && bankcode != "1004" && bankcode != "1008" && bankcode != "2001")
                {
                    str2 = new shukenet.Bank().PayBankH5(orderid, orderAmt, bankcode);
                    str1 = "post";
                }
                else
                {
                    str2 = new shukenet.Bank().PayBank(orderid, orderAmt, bankcode);
                    str1 = "post";
                }
            }
            else if (suppid == 10039)
            {
                str2 = new viviapi.ETAPI.haiou.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "get";
            }
            else if (suppid == 10040)
            {
                str2 = new viviapi.ETAPI.epay.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10041)
            {
                if (bankcode == "992" || bankcode == "1004" || (bankcode == "1008" || bankcode == "2001") || bankcode == "2003")
                {
                    string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + Constant.ParameterEncryptionKey;
                    str2 = string.Format("/PRCode/Index.aspx?suppId={0}&orderAmt={1}&orderid={2}&sign={3}&bankcode={4}", (object)suppid, (object)orderAmt, (object)orderid, (object)Cryptography.MD5(strToEncrypt), (object)bankcode);
                }
                else
                    str2 = new viviapi.ETAPI.tongyi.WuYou2API().UnifiedOrder(orderid, orderAmt, bankcode);
            }
            else if (suppid == 10042)
            {
                str2 = new viviapi.ETAPI.skpay.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            if (str1 == "get")
                HttpContext.Current.Response.Redirect(str2, true);
            else
                HttpContext.Current.Response.Write(str2);
            return true;
        }

        public static bool OnlineBankPayApp(int suppid, string orderid, Decimal orderAmt, string bankcode)
        {
            if (SupplierFactory.GetCacheModel(suppid) == null)
                return false;
            //if (PaymentSetting.showjubao)
            //{
            //    string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + Constant.ParameterEncryptionKey;
            //    HttpContext.Current.Response.Redirect(string.Format("/GoPay.aspx?suppId={0}&orderAmt={1}&orderid={2}&sign={3}&bankcode={4}", (object)suppid, (object)orderAmt, (object)orderid, (object)Cryptography.MD5(strToEncrypt), (object)bankcode), true);
            //    return false;
            //}
            //else
            //{
            string str1 = "get";
            string str2 = string.Empty;
            #region
            if (suppid == 70 || suppid == 90)
            {
                string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + Constant.ParameterEncryptionKey;
                str2 = string.Format("/postToBank.aspx?suppId={0}&orderAmt={1}&orderid={2}&sign={3}&bankcode={4}", (object)suppid, (object)orderAmt, (object)orderid, (object)Cryptography.MD5(strToEncrypt), (object)bankcode);
            }
            //else if (suppid == 700)
            //{
            //    str2 = new viviapi.ETAPI.Longbao.Bank().PayBankApp(orderid, orderAmt, bankcode, false);
            //    str1 = "get";
            //}
            else if (suppid == 101)
            {
                str2 = new viviapi.ETAPI.AlipayWap.Bank().PayBankApp(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            //else if (suppid == 1111)
            //{
            //    str2 = new viviapi.ETAPI.UnionpayWap.Bank().PayBank(orderid, orderAmt, bankcode);
            //    str1 = "post";
            //}
            //else if (suppid == 99)
            //{
            //    string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + Constant.ParameterEncryptionKey;
            //    str2 = string.Format("/PCCode/?suppId={0}&orderAmt={1}&orderid={2}&sign={3}&bankcode={4}", (object)suppid, (object)orderAmt, (object)orderid, (object)Cryptography.MD5(strToEncrypt), (object)bankcode);
            //}

            //else if (suppid == 1006)
            //{
            //    str2 = new ecpss().PayBankApp(orderid, orderAmt, bankcode);
            //    str1 = "post";
            //}

            //else if (suppid == 6000)//易宝付
            //{
            //    str2 = new ebfpay.Bank().PayBank(orderid, orderAmt, bankcode);
            //    str1 = "get";
            //}
            //else if (suppid == 6002)//星期天
            //{

            //    str2 = new qitian.Bank().PayBankApp(orderid, orderAmt, bankcode);
            //    str1 = "get";
            //    //if (!url.Contains("error"))
            //    //{
            //    //    string strToEncrypt = suppid.ToString() + orderAmt.ToString() + orderid + bankcode + url + Constant.ParameterEncryptionKey;
            //    //    str2 = string.Format("/PCCode/index.aspx?suppId={0}&orderAmt={1}&orderid={2}&bankcode={3}&payurl={4}&sign={5}", suppid, orderAmt, orderid, bankcode, url, Cryptography.MD5(strToEncrypt));
            //    //    str1 = "get";
            //    //}
            //    //else
            //    //{
            //    //    HttpContext.Current.Response.Write(url);
            //    //    HttpContext.Current.Response.End();

            //    //}
            //}
            //else if (suppid == 6001)//立马支付
            //{
            //    str2 = new lima.Bank().PayBank(orderid, orderAmt, bankcode);
            //    str1 = "get";
            //}
            #endregion
            if (suppid == 10001)//
            {
                str2 = new YaFu.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "get";
            }
            else if (suppid == 10002)//
            {
                str2 = new TongLian.Code().PayBank(orderid, orderAmt, bankcode);
                str1 = "get";
            }
            else if (suppid == 10003)//
            {
                str2 = new TongLian2.Code().PayBank(orderid, orderAmt, bankcode);
                str1 = "get";
            }
            else if (suppid == 10008)
            {
                str2 = new Qingyifu.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10009)
            {
                str2 = new pay().PayBankApp(orderid, orderAmt, bankcode, HttpContext.Current);
                str1 = "post";
            }
            else if (suppid == 10010)
            {
                str2 = new Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10013)
            {
                str2 = new Youqi.Bank().PayBankApp(orderid, orderAmt, bankcode);
                str1 = "post";
            }
            else if (suppid == 10018)
            {
                str2 = new viviapi.ETAPI.Youka.Bank().PayBank(orderid, orderAmt, bankcode);
                str1 = "get";
            }
            if (suppid == 10016)
            {
                str2 = new Yingmin.Bank().PayBankApp(orderid, orderAmt, bankcode);
                str1 = "get";
            }
            else if (suppid == 10032)
            {
                str2 = new xunyou.Scan().PayScan(orderid, orderAmt, bankcode);
                str1 = "get";
            }
            else if (suppid == 10041)
                str2 = new viviapi.ETAPI.tongyi.WuYou2API().UnifiedOrder(orderid, orderAmt, bankcode);
            if (str1 == "get")
                HttpContext.Current.Response.Redirect(str2, true);
            else
                HttpContext.Current.Response.Write(str2);
            return true;
            //}
        }

        public static void ReqDistribution(viviapi.Model.distribution info)
        {
            string msg = "";
            bool flag;
            if (info.suppid == 10002)
            {
                msg = new viviapi.ETAPI.TongLian.daifu().DoTrans(info);
            }
            else if (info.suppid == 10004)
            {
                msg = new viviapi.ETAPI.XinFuBao.DaiFu(10004).DoTrans(info);
            }
            else if (info.suppid == 10005)
            {
                msg = new viviapi.ETAPI.XinFuBao.DaiFu(10005).DoTrans(info);
            }
            else if (info.suppid == 10008)
            {
                msg = new viviapi.ETAPI.Qingyifu.DaiFu(10008).DoTrans(info);
            }
            else if (info.suppid == 10041)
            {
                flag = new viviapi.ETAPI.tongyi.WuYou2gw(10041).DoTrans(info);
            }
            else if (info.suppid == 10032)
            {
                msg = new viviapi.ETAPI.xunyou.DaiFu(10032).DoTrans(info);
            }

            //bool flag = false;
            if (info.suppid == 100)
            {
                flag = new viviapi.ETAPI.tenpay.distribution.gw().DoTrans(info);
            }
            else
            {
                if (info.suppid != 10030)
                    return;
                flag = new viviapi.ETAPI.Ebatong.distribution.gw().DoTrans(info);
            }
        }
    }
}
