using System;

namespace com.yeepay
{
    [Serializable]
    public class BuyRefundOrdResult
    {
        private string hmac;
        private string r0_Cmd;
        private string r1_Code;
        private string r2_TrxId;
        private string r3_Amt;
        private string r4_Cur;

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

        public BuyRefundOrdResult(string r0_Cmd, string r1_Code, string r2_TrxId, string r3_Amt, string r4_Cur, string hmac)
        {
            this.r0_Cmd = r0_Cmd;
            this.r1_Code = r1_Code;
            this.r2_TrxId = r2_TrxId;
            this.r3_Amt = r3_Amt;
            this.r4_Cur = r4_Cur;
            this.hmac = hmac;
        }
    }
}
