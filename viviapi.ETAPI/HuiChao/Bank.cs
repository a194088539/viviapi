using System;
using System.Text;
namespace viviapi.ETAPI.HuiChao
{
    public class Bank : ETAPIBase
    {


        private static int suppId = 10031;

        internal string notify_url
        {
            get
            {
                return this.SiteDomain + "/notify/HuiChao/Bank_Notify.aspx";
            }
        }

        public Bank()
            : base(suppId)
        {
        }

        public static string GetBankCode(string paymodeId)
        {
            string str = "";
            switch (paymodeId)
            {
                case "970":
                    str = "3085840";
                    break;
                case "967":
                    str = "1021000";
                    break;
                case "964":
                    str = "1031000";
                    break;
                case "965":
                    str = "1051000";
                    break;
                case "963":
                    str = "1041000";
                    break;
                case "981":
                    str = "3012900";
                    break;
                case "980":
                    str = "3051000";
                    break;
                case "974":
                    str = "3071000";
                    break;
                case "985":
                    str = "3065810";
                    break;
                case "962":
                    str = "3021000";
                    break;
                //case "982":
                //    str = "HXB-NET-B2C";
                //    break;
                case "972":
                    str = "3093910";
                    break;
                case "971":
                    str = "4031000";
                    break;
                case "989":
                    str = "3131000";
                    break;
                //case "988":
                //    str = "CBHB-NET-B2C";
                //    break;
                //case "990":
                //    str = "BJRCB-NET-B2C";
                //    break;
                case "979":
                    str = "3133010";
                    break;
                case "986":
                    str = "3031000";
                    break;
                case "987":
                    str = "5021000";
                    break;
                case "997":
                    str = "4031000";
                    break;
                case "978":
                    str = "3071000";
                    break;
                //case "968":
                //    str = "CZ-NET-B2C";
                //    break;
                case "975":
                    str = "3222900";
                    break;
                case "977":
                    str = "3102900";
                    break;
            }
            return str;
        }


        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            //            String xmlInfo = "<?xml version=\"1.0\" encoding=\"GBK\"?>" +
            //                                    @"<yspay>
            //                                    <head>
            //                                      <Ver>1.0</Ver>
            //                                      <Src>【商户号】</Src>
            //                                      <MsgCode>S3001</MsgCode>
            //                                      <Time>20120911182020</Time>
            //                                    </head>
            //                                    <body>
            //                                    <Order>
            //	                                    <OrderId>L00000000000000039</OrderId> 
            //	                                    <BusiCode>01000010</BusiCode> 
            //	                                    <ShopDate>20140507</ShopDate> 
            //	                                    <Cur>CNY</Cur> 
            //	                                    <Amount>5000</Amount>
            //	                                    <Note>测试报文</Note> 
            //	                                    <ExtraData>测试order_id</ExtraData> 
            //	                                    <Remark>remark</Remark> 
            //                                        <BankType>1051000</BankType> 
            //	                                    <BankAccountType></BankAccountType> 
            //                                        <Timeout></Timeout> 
            //	                                    <SupportCards></SupportCards> 
            //                                    </Order>
            //                                    <Payee>
            //	                                    <UserCode>test_src</UserCode> 
            //	                                    <Name>测试商户名称</Name> 
            //	                                    <PhoneNum></PhoneNum> 
            //	                                    <Amount>5000</Amount> 
            //                                    </Payee>
            //                                    <Payer>
            //	                                    <UserCode></UserCode> 
            //	                                    <Name></Name> 
            //                                    </Payer>
            //                                    <Notice>
            //	                                    <PgUrl>http://www.baidu.com</PgUrl> 
            //	                                    <BgUrl>http://www.baidu.com</BgUrl>
            //                                    </Notice>
            //                                    </body>
            //                                    </yspay>";
            String xmlInfo = "<?xml version=\"1.0\" encoding=\"GBK\"?>" +
                                    @"<yspay>
                                    <head>
                                      <Ver>1.0</Ver>
                                      <Src>【商户号】</Src>
                                      <MsgCode>【报文编号】</MsgCode>
                                      <Time>【发送时间】</Time>
                                    </head>
                                    <body>
                                    <Order>
	                                    <OrderId>【订单号】</OrderId> 
	                                    <BusiCode>【业务代码】</BusiCode> 
	                                    <ShopDate>【商户日期】</ShopDate> 
	                                    <Cur>CNY</Cur> 
	                                    <Amount>【金额】</Amount>
	                                    <Note>【订单描述】</Note> 
	                                    <ExtraData>【订单号】</ExtraData> 
	                                    <Remark>【备注】</Remark> 
                                        <BankType>【银行行别】</BankType> 
	                                    <BankAccountType>【账户类型】</BankAccountType> 
                                        <Timeout>【订单有效时间】</Timeout> 
	                                    
                                    </Order>
                                    <Payee>
	                                    <UserCode>【商户号】</UserCode> 
	                                    <Name>【客户名】</Name> 
	                                    <PhoneNum></PhoneNum> 
	                                    <Amount>5000</Amount> 
                                    </Payee>
                                    
                                    <Notice>
	                                    <PgUrl>【前端回调页面URL】</PgUrl> 
	                                    <BgUrl>【后台回调URL】</BgUrl>
                                    </Notice>
                                    </body>
                                    </yspay>";
            string bianhao = "S3101";
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            xmlInfo = xmlInfo.Replace("【商户号】", this.suppAccount).Replace("【报文编号】", bianhao).Replace("【发送时间】", time).Replace("【订单号】", orderid).Replace("【业务代码】", "").Replace("【商户日期】", "").Replace("【金额】", (orderAmt * 100).ToString()).Replace("【订单描述】", "").Replace("【订单号】", orderid).Replace("【备注】", "").Replace("【银行行别】", GetBankCode(bankcode)).Replace("【账户类型】", "").Replace("【订单有效时间】", "10080").Replace("【客户名】", suppUserName).Replace("【前端回调页面URL】", notify_url).Replace("【后台回调URL】", "");

            string check = "";
            string msg = "";
            //通过证书签名
            check = Sign.signData(xmlInfo);
            //签名之后再base64编码
            msg = Convert.ToBase64String(Encoding.GetEncoding("GBK").GetBytes(xmlInfo));

            return "<form id=\"form1\" method=\"post\" action=\"" + base.postBankUrl + "\"><input type=\"hidden\" name=\"src\" value=\"" + suppAccount + "\"/><input type=\"hidden\" name=\"msgCode\" value=\"" + bianhao + "\"/> <input type=\"hidden\" name=\"msgId\" value=\"" + time + "\"/><input type=\"hidden\" name=\"check\" value=\"" + check + "\"/><input type=\"hidden\" name=\"msg\" value=\"" + xmlInfo + "\"/></div>   </form><script>document.getElementById(\"form1\").submit();</script>";
        }

        public void Return()
        {
            //string suppAccount = this.suppAccount;
            //string suppKey = this.suppKey;
            //string opstate = "-1";
            //int status = 4;
            //string str = string.Empty;
            //BuyCallbackResult buyCallbackResult = Buy.VerifyCallback(suppAccount, suppKey, FormatQueryString.GetQueryString("r0_Cmd"), FormatQueryString.GetQueryString("r1_Code"), FormatQueryString.GetQueryString("r2_TrxId"), FormatQueryString.GetQueryString("r3_Amt"), FormatQueryString.GetQueryString("r4_Cur"), FormatQueryString.GetQueryString("r5_Pid"), FormatQueryString.GetQueryString("r6_Order"), FormatQueryString.GetQueryString("r7_Uid"), FormatQueryString.GetQueryString("r8_MP"), FormatQueryString.GetQueryString("r9_BType"), FormatQueryString.GetQueryString("rp_PayDate"), FormatQueryString.GetQueryString("hmac"));
            //if (!string.IsNullOrEmpty(buyCallbackResult.ErrMsg))
            //    return;
            //string msg = "支付失败";
            //if (buyCallbackResult.R1_Code == "1")
            //{
            //    msg = "支付成功";
            //    opstate = "0";
            //    status = 2;
            //}
            //Decimal result = new Decimal(0);
            //Decimal.TryParse(buyCallbackResult.R3_Amt, out result);
            //OrderBank orderBank = new OrderBank();
            //if (buyCallbackResult.R9_BType == "1")
            //    orderBank.DoBankComplete(RMB.suppId, buyCallbackResult.R6_Order, buyCallbackResult.R2_TrxId, status, opstate, msg, result, new Decimal(0), false, true);
            //else if (buyCallbackResult.R9_BType == "2" || buyCallbackResult.R9_BType == "3")
            //{
            //    orderBank.DoBankComplete(RMB.suppId, buyCallbackResult.R6_Order, buyCallbackResult.R2_TrxId, status, opstate, msg, result, new Decimal(0), true, false);
            //    HttpContext.Current.Response.Write("SUCCESS");
            //}
        }
    }
}
