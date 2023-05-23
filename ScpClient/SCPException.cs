using System;

namespace com.todaynic.ScpClient
{
    public class SCPException : Exception
    {
        private SCPReply m_Reply;

        public string ResultCode
        {
            get
            {
                return this.m_Reply.getResultCode();
            }
        }

        public SCPException(SCPReply reply)
          : base(reply.getResultMessage())
        {
            this.m_Reply = reply;
        }
    }
}
