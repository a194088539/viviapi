using System;

namespace com.yeepay
{
    [Serializable]
    public class BuyCallbackResult
    {
        private string errMsg;
        private string hmac;
        private string p1_MerId;
        private string r0_Cmd;
        private string r1_Code;
        private string r2_TrxId;
        private string r3_Amt;
        private string r4_Cur;
        private string r5_Pid;
        private string r6_Order;
        private string r7_Uid;
        private string r8_MP;
        private string r9_BType;
        private string rp_PayDate;

        public string ErrMsg
        {
            get
            {
                return this.errMsg;
            }
        }

        public string Hmac
        {
            get
            {
                return this.hmac;
            }
        }

        public string P1_MerId
        {
            get
            {
                return this.p1_MerId;
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

        public string R7_Uid
        {
            get
            {
                return this.r7_Uid;
            }
        }

        public string R8_MP
        {
            get
            {
                return this.r8_MP;
            }
        }

        public string R9_BType
        {
            get
            {
                return this.r9_BType;
            }
        }

        public string Rp_PayDate
        {
            get
            {
                return this.rp_PayDate;
            }
        }

        public BuyCallbackResult(string p1_MerId, string r0_Cmd, string r1_Code, string r2_TrxId, string r3_Amt, string r4_Cur, string r5_Pid, string r6_Order, string r7_Uid, string r8_MP, string r9_BType, string rp_PayDate, string hmac, string errMsg)
        {
            this.p1_MerId = p1_MerId;
            this.r0_Cmd = r0_Cmd;
            this.r1_Code = r1_Code;
            this.r2_TrxId = r2_TrxId;
            this.r3_Amt = r3_Amt;
            this.r4_Cur = r4_Cur;
            this.r5_Pid = r5_Pid;
            this.r6_Order = r6_Order;
            this.r7_Uid = r7_Uid;
            this.r8_MP = r8_MP;
            this.r9_BType = r9_BType;
            this.rp_PayDate = rp_PayDate;
            this.hmac = hmac;
            this.errMsg = errMsg;
        }
    }
}
