namespace com.yeepay.cmbn
{
    public class SZXResultTest
    {
        private string hmac;
        private string r0_Cmd;
        private string r1_Code;
        private string r2_TrxId;
        private string r6_Order;
        private string reqResult;
        private string reqUrl;
        private string rq_ReturnMsg;

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

        public string R6_Order
        {
            get
            {
                return this.r6_Order;
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

        public string Rq_ReturnMsg
        {
            get
            {
                return this.rq_ReturnMsg;
            }
        }

        public SZXResultTest(string r0_Cmd, string r1_Code, string r2_TrxId, string r6_Order, string rq_ReturnMsg, string hmac, string reqUrl, string reqResult, string reqPara)
        {
            this.r0_Cmd = r0_Cmd;
            this.r1_Code = r1_Code;
            this.r2_TrxId = r2_TrxId;
            this.r6_Order = r6_Order;
            this.rq_ReturnMsg = rq_ReturnMsg;
            this.hmac = hmac;
            this.reqUrl = reqUrl;
            this.reqResult = reqResult;
            if (!(r1_Code == "1"))
                return;
            this.reqResult = HttpUtils.SendRequest("http://tech.yeepay.com:8080/robot/generationCallback.action", reqPara);
        }
    }
}
