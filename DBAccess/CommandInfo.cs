namespace DBAccess
{
    using System;
    using System.Data.Common;
    using System.Data.SqlClient;

    public class CommandInfo
    {
        public string CommandText;
        public DBAccess.EffentNextType EffentNextType;
        public object OriginalData;
        public DbParameter[] Parameters;
        public object ShareObject;

        private event EventHandler _solicitationEvent;

        public event EventHandler SolicitationEvent;

        public CommandInfo()
        {
            this.ShareObject = null;
            this.OriginalData = null;
            this.EffentNextType = DBAccess.EffentNextType.None;
        }

        public CommandInfo(string sqlText, SqlParameter[] para)
        {
            this.ShareObject = null;
            this.OriginalData = null;
            this.EffentNextType = DBAccess.EffentNextType.None;
            this.CommandText = sqlText;
            this.Parameters = para;
        }

        public CommandInfo(string sqlText, SqlParameter[] para, DBAccess.EffentNextType type)
        {
            this.ShareObject = null;
            this.OriginalData = null;
            this.EffentNextType = DBAccess.EffentNextType.None;
            this.CommandText = sqlText;
            this.Parameters = para;
            this.EffentNextType = type;
        }

        public void OnSolicitationEvent()
        {
            if (this._solicitationEvent != null)
            {
                this._solicitationEvent(this, new EventArgs());
            }
        }
    }
}

