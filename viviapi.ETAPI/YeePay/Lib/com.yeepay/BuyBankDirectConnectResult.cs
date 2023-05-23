using System;

namespace com.yeepay
{
    [Serializable]
    public class BuyBankDirectConnectResult
    {
        private string hmac;
        private string r0_Cmd;
        private string r1_Code;
        private string r2_TrxId;
        private string r3_Amt;
        private string r4_Cur;
        private string r6_Order;
        private string r8_MP;
        private string reqResult;
        private string reqUrl;
        private string ro_BankOrderId;

        public string Hmac
        {
            get
            {
                return this.hmac;
            }
        }

        public string R0_Cmd
        {
            get
            {
                return this.r0_Cmd;
            }
        }

        public string R1_Code
        {
            get
            {
                return this.r1_Code;
            }
        }

        public string R2_TrxId
        {
            get
            {
                return this.r2_TrxId;
            }
        }

        public string R3_Amt
        {
            get
            {
                return this.r3_Amt;
            }
        }

        public string R4_Cur
        {
            get
            {
                return this.r4_Cur;
            }
        }

        public string R6_Order
        {
            get
            {
                return this.r6_Order;
            }
        }

        public string R8_MP
        {
            get
            {
                return this.r8_MP;
            }
        }

        public string ReqResult
        {
            get
            {
                return this.reqResult;
            }
        }

        public string ReqUrl
        {
            get
            {
                return this.reqUrl;
            }
        }

        public string Ro_BankOrderId
        {
            get
            {
                return this.ro_BankOrderId;
            }
        }

        public BuyBankDirectConnectResult(string r0_Cmd, string r1_Code, string r2_TrxId, string r3_Amt, string r4_Cur, string r6_Order, string ro_BankOrderId, string r8_MP, string hmac, string reqUrl, string reqResult)
        {
            this.r0_Cmd = r0_Cmd;
            this.r1_Code = r1_Code;
            this.r2_TrxId = r2_TrxId;
            this.r3_Amt = r3_Amt;
            this.r4_Cur = r4_Cur;
            this.r6_Order = r6_Order;
            this.ro_BankOrderId = ro_BankOrderId;
            this.r8_MP = r8_MP;
            this.hmac = hmac;
            this.reqUrl = reqUrl;
            this.reqResult = reqResult;
        }
    }
}
