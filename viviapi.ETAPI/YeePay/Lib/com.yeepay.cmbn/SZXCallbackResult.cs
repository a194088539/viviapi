namespace com.yeepay.cmbn
{
    public class SZXCallbackResult
    {
        private string errMsg;
        private string hmac;
        private string p1_MerId;
        private string p2_Order;
        private string p3_Amt;
        private string p4_FrpId;
        private string p5_CardNo;
        private string p6_confirmAmount;
        private string p7_realAmount;
        private string p8_cardStatus;
        private string p9_MP;
        private string pb_BalanceAmt;
        private string pc_BalanceAct;
        private string r0_Cmd;
        private string r1_Code;

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

        public string P2_Order
        {
            get
            {
                return this.p2_Order;
            }
        }

        public string P3_Amt
        {
            get
            {
                return this.p3_Amt;
            }
        }

        public string P4_FrpId
        {
            get
            {
                return this.p4_FrpId;
            }
        }

        public string P5_CardNo
        {
            get
            {
                return this.p5_CardNo;
            }
        }

        public string P6_confirmAmount
        {
            get
            {
                return this.p6_confirmAmount;
            }
        }

        public string P7_realAmount
        {
            get
            {
                return this.p7_realAmount;
            }
        }

        public string P8_cardStatus
        {
            get
            {
                return this.p8_cardStatus;
            }
        }

        public string P9_MP
        {
            get
            {
                return this.p9_MP;
            }
        }

        public string Pb_BalanceAmt
        {
            get
            {
                return this.pb_BalanceAmt;
            }
        }

        public string Pc_BalanceAct
        {
            get
            {
                return this.pc_BalanceAct;
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

        public SZXCallbackResult(string r0_Cmd, string r1_Code, string p1_MerId, string p2_Order, string p3_Amt, string p4_FrpId, string p5_CardNo, string p6_confirmAmount, string p7_realAmount, string p8_cardStatus, string p9_MP, string pb_BalanceAmt, string pc_BalanceAct, string hmac, string errMsg)
        {
            this.r0_Cmd = r0_Cmd;
            this.r1_Code = r1_Code;
            this.p1_MerId = p1_MerId;
            this.p2_Order = p2_Order;
            this.p3_Amt = p3_Amt;
            this.p4_FrpId = p4_FrpId;
            this.p5_CardNo = p5_CardNo;
            this.p6_confirmAmount = p6_confirmAmount;
            this.p7_realAmount = p7_realAmount;
            this.p8_cardStatus = p8_cardStatus;
            this.p9_MP = p9_MP;
            this.pb_BalanceAmt = pb_BalanceAmt;
            this.pc_BalanceAct = pc_BalanceAct;
            this.hmac = hmac;
            this.errMsg = errMsg;
        }
    }
}
