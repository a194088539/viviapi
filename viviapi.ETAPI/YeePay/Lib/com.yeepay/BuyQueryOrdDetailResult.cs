using System;

namespace com.yeepay
{
    [Serializable]
    public class BuyQueryOrdDetailResult
    {
        private string hmac;
        private string r0_Cmd;
        private string r1_Code;
        private string r2_TrxId;
        private string r3_Amt;
        private string r4_Cur;
        private string r5_Pid;
        private string r6_Order;
        private string r8_MP;
        private string rb_PayStatus;
        private string rc_RefundCount;
        private string rd_RefundAmt;

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

        public string R2_TrdId
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

        public string R8_MP
        {
            get
            {
                return this.r8_MP;
            }
        }

        public string Rb_PayStatus
        {
            get
            {
                return this.rb_PayStatus;
            }
        }

        public string Rc_RefundCount
        {
            get
            {
                return this.rc_RefundCount;
            }
        }

        public string Rd_RefundAmt
        {
            get
            {
                return this.rd_RefundAmt;
            }
        }

        public BuyQueryOrdDetailResult(string r0_Cmd, string r1_Code, string r2_TrxId, string r3_Amt, string r4_Cur, string r5_Pid, string r6_Order, string r8_MP, string rb_PayStatus, string rc_RefundCount, string rd_RefundAmt, string hmac)
        {
            this.r0_Cmd = r0_Cmd;
            this.r1_Code = r1_Code;
            this.r2_TrxId = r2_TrxId;
            this.r3_Amt = r3_Amt;
            this.r4_Cur = r4_Cur;
            this.r5_Pid = r5_Pid;
            this.r6_Order = r6_Order;
            this.r8_MP = r8_MP;
            this.rb_PayStatus = rb_PayStatus;
            this.rc_RefundCount = rc_RefundCount;
            this.rd_RefundAmt = rd_RefundAmt;
            this.hmac = hmac;
        }
    }
}
