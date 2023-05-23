using System;

namespace viviapi.Model.User
{
    [Serializable]
    public class UserAccessTimeInfo
    {
        private DateTime _lastaccesstime = DateTime.MinValue;
        private int _userid;

        public int userid
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }

        public DateTime lastAccesstime
        {
            get
            {
                return this._lastaccesstime;
            }
            set
            {
                this._lastaccesstime = value;
            }
        }
    }
}
