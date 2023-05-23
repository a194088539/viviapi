using System;

namespace com.yeepay
{
    [Serializable]
    public class QueryOrdResult
    {
        private string returnAllPara;
        private string returnAmt;
        private string returnCode;
        private string returnMP;
        private string returnOrder;
        private string returnPayStatus;
        private string returnPid;
        private string returnRefundAmt;
        private string returnRefundCount;
        private string returnStatus;
        private string returnTrxId;

        public string ReturnAllPara
        {
            get
            {
                return this.returnAllPara;
            }
        }

        public string ReturnAmt
        {
            get
            {
                return this.returnAmt;
            }
        }

        public string ReturnCode
        {
            get
            {
                return this.returnCode;
            }
        }

        public string ReturnMP
        {
            get
            {
                return this.returnMP;
            }
        }

        public string ReturnOrder
        {
            get
            {
                return this.returnOrder;
            }
        }

        public string ReturnPayStatus
        {
            get
            {
                return this.returnPayStatus;
            }
        }

        public string ReturnPid
        {
            get
            {
                return this.returnPid;
            }
        }

        public string ReturnRefundAmt
        {
            get
            {
                return this.returnRefundAmt;
            }
        }

        public string ReturnRefundCount
        {
            get
            {
                return this.returnRefundCount;
            }
        }

        public string ReturnStatus
        {
            get
            {
                return this.returnStatus;
            }
        }

        public string ReturnTrxId
        {
            get
            {
                return this.returnTrxId;
            }
        }

        public QueryOrdResult(string returnCode, string returnTrxId, string returnAmt, string returnPid, string returnOrder, string returnStatus, string returnAllPara, string returnMP, string returnRefundCount)
        {
            this.returnCode = returnCode;
            this.returnTrxId = returnTrxId;
            this.returnAmt = returnAmt;
            this.returnPid = returnPid;
            this.returnOrder = returnOrder;
            this.returnStatus = returnStatus;
            this.returnAllPara = returnAllPara;
            this.returnMP = returnMP;
            this.returnPayStatus = returnStatus;
            this.returnRefundCount = returnRefundCount;
            this.returnRefundAmt = returnAmt;
        }
    }
}
