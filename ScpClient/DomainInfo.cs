namespace com.todaynic.ScpClient
{
    public class DomainInfo
    {
        private string m_AdminContactID;
        private ContactInfo m_adminContactInfo;
        private string m_BillingContactID;
        private ContactInfo m_BillingContactInfo;
        private string m_dtCreate;
        private string m_dtExpired;
        private string m_dtUpdate;
        private string m_Name;
        private string m_NS1;
        private string m_NS2;
        private string m_Period;
        private string m_RegistrantContactID;
        private ContactInfo m_registrantContactInfo;
        private string m_RLoginPassword;
        private string m_ROID;
        private string m_Status;
        private string m_TechContactID;
        private ContactInfo m_TechContactInfo;

        public string AdminContactID
        {
            get
            {
                return this.m_AdminContactID;
            }
            set
            {
                this.m_AdminContactID = value;
            }
        }

        public ContactInfo AdminContactInfo
        {
            get
            {
                return this.m_adminContactInfo;
            }
            set
            {
                this.m_adminContactInfo = value;
            }
        }

        public string BillingContactID
        {
            get
            {
                return this.m_BillingContactID;
            }
            set
            {
                this.m_BillingContactID = value;
            }
        }

        public ContactInfo BillingContactInfo
        {
            get
            {
                return this.m_BillingContactInfo;
            }
            set
            {
                this.m_BillingContactInfo = value;
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

        public string DtUpdate
        {
            get
            {
                return this.m_dtUpdate;
            }
            set
            {
                this.m_dtUpdate = value;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        public string NS1
        {
            get
            {
                return this.m_NS1;
            }
            set
            {
                this.m_NS1 = value;
            }
        }

        public string NS2
        {
            get
            {
                return this.m_NS2;
            }
            set
            {
                this.m_NS2 = value;
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

        public string RegistrantContactID
        {
            get
            {
                return this.m_RegistrantContactID;
            }
            set
            {
                this.m_RegistrantContactID = value;
            }
        }

        public ContactInfo RegistrantContactInfo
        {
            get
            {
                return this.m_registrantContactInfo;
            }
            set
            {
                this.m_registrantContactInfo = value;
            }
        }

        public string RLoginPassword
        {
            get
            {
                return this.m_RLoginPassword;
            }
            set
            {
                this.m_RLoginPassword = value;
            }
        }

        public string ROID
        {
            get
            {
                return this.m_ROID;
            }
            set
            {
                this.m_ROID = value;
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

        public string TechContactID
        {
            get
            {
                return this.m_TechContactID;
            }
            set
            {
                this.m_TechContactID = value;
            }
        }

        public ContactInfo TechContactInfo
        {
            get
            {
                return this.m_TechContactInfo;
            }
            set
            {
                this.m_TechContactInfo = value;
            }
        }
    }
}
