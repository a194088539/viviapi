namespace com.todaynic.ScpClient
{
    public class EmailInfo
    {
        private string m_AttachVHostID;
        private string m_Domain;
        private string m_dtCreate;
        private string m_dtExpired;
        private string m_EmailID;
        private string m_MailServer;
        private string m_Password;
        private string m_Period;
        private string m_ProductID;
        private string m_Space;
        private string m_Status;

        public string AttachVHostID
        {
            get
            {
                return this.m_AttachVHostID;
            }
            set
            {
                this.m_AttachVHostID = value;
            }
        }

        public string Domain
        {
            get
            {
                return this.m_Domain;
            }
            set
            {
                this.m_Domain = value;
            }
        }

        public string DtCreate
        {
            get
            {
                return this.m_dtCreate;
            }
            set
            {
                this.m_dtCreate = value;
            }
        }

        public string DtExpired
        {
            get
            {
                return this.m_dtExpired;
            }
            set
            {
                this.m_dtExpired = value;
            }
        }

        public string EmailID
        {
            get
            {
                return this.m_EmailID;
            }
            set
            {
                this.m_EmailID = value;
            }
        }

        public string MailServer
        {
            get
            {
                return this.m_MailServer;
            }
            set
            {
                this.m_MailServer = value;
            }
        }

        public string Password
        {
            get
            {
                return this.m_Password;
            }
            set
            {
                this.m_Password = value;
            }
        }

        public string Period
        {
            get
            {
                return this.m_Period;
            }
            set
            {
                this.m_Period = value;
            }
        }

        public string ProductID
        {
            get
            {
                return this.m_ProductID;
            }
            set
            {
                this.m_ProductID = value;
            }
        }

        public string Space
        {
            get
            {
                return this.m_Space;
            }
            set
            {
                this.m_Space = value;
            }
        }

        public string Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                this.m_Status = value;
            }
        }
    }
}
