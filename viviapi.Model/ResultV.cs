using System;

namespace OKXR.Entity
{
    public class ResultV
    {
        private DateTime _addTimer;
        private int _iD;
        private int _moneyV;
        private int _serverID;
        private string _username;

        public DateTime AddTimer
        {
            get
            {
                return this._addTimer;
            }
            set
            {
                this._addTimer = value;
            }
        }

        public int ID
        {
            get
            {
                return this._iD;
            }
            set
            {
                this._iD = value;
            }
        }

        public int MoneyV
        {
            get
            {
                return this._moneyV;
            }
            set
            {
                this._moneyV = value;
            }
        }

        public int ServerID
        {
            get
            {
                return this._serverID;
            }
            set
            {
                this._serverID = value;
            }
        }

        public string Username
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }

        public ResultV()
        {
        }

        public ResultV(int _iD, int _serverID, string _username, int _moneyV, DateTime _addTimer)
        {
            this._iD = _iD;
            this._serverID = _serverID;
            this._username = _username;
            this._moneyV = _moneyV;
            this._addTimer = _addTimer;
        }
    }
}
