namespace com.yeepay
{
    public class SZXCallbackResult
    {
        private string errMsg;
        private string hmac;
        private string p1_MerId;
        private string pa_MP;
        private string r0_Cmd;
        private string r1_Code;
        private string r2_TrxId;
        private string rb_Order;
        private string rc_Amt;
        private string rq_CardNo;

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

        public string Pa_MP
        {
            get
            {
                return this.pa_MP;
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

        public string Rb_Order
        {
            get
            {
                return this.rb_Order;
            }
        }

        public string Rc_Amt
        {
            get
            {
                return this.rc_Amt;
            }
        }

        public string Rq_CardNo
        {
            get
            {
                return this.rq_CardNo;
            }
        }

        public SZXCallbackResult(string r0_Cmd, string r1_Code, string p1_MerId, string rb_Order, string r2_TrxId, string pa_MP, string rc_Amt, string rq_CardNo, string hmac, string errMsg)
        {
            this.r0_Cmd = r0_Cmd;
            this.r1_Code = r1_Code;
            this.p1_MerId = p1_MerId;
            this.rb_Order = rb_Order;
            this.r2_TrxId = r2_TrxId;
            this.pa_MP = pa_MP;
            this.rc_Amt = rc_Amt;
            this.rq_CardNo = rq_CardNo;
            this.hmac = hmac;
            this.errMsg = errMsg;
        }
    }
}
