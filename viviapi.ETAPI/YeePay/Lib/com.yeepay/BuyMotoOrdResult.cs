using System;

namespace com.yeepay
{
    [Serializable]
    public class BuyMotoOrdResult
    {
        private string hmac;
        private string r1_Code;
        private string r3_Amt;
        private string r4_Cur;
        private string r5_Pid;
        private string r6_Order;
        private string rd_MotoId;
        private string re_OrderIndex;

        public string Hmac
        {
            get
            {
                return this.hmac;
            }
        }

        public string R1_Code
        {
            get
            {
                return this.r1_Code;
            }
        }

        public string R3_Amt
        {
            get
            {
                return this.r3_Amt;
            }
        }

        public string R5_Pid
        {
            get
            {
                return this.r5_Pid;
            }
        }

        public string R6_Order
        {
            get
            {
                return this.r6_Order;
            }
        }

        public string Rd_MotoId
        {
            get
            {
                return this.rd_MotoId;
            }
        }

        public string Re_OrderIndex
        {
            get
            {
                return this.re_OrderIndex;
            }
        }

        public BuyMotoOrdResult(string r1_Code, string rd_MotoId, string r3_Amt, string r4_Cur, string r5_Pid, string r6_Order, string re_OrderIndex, string hmac)
        {
            this.r1_Code = r1_Code;
            this.rd_MotoId = rd_MotoId;
            this.r3_Amt = r3_Amt;
            this.r4_Cur = r4_Cur;
            this.r5_Pid = r5_Pid;
            this.r6_Order = r6_Order;
            this.re_OrderIndex = re_OrderIndex;
            this.hmac = hmac;
        }
    }
}
