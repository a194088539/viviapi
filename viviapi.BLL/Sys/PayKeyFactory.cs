namespace viviapi.BLL.Payment
{
    using DBAccess;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PayKeyFactory
    {
        private int _pisok;
        private string _postbankurl;
        private string _postcardurl;
        private string _postsmsurl;
        private int _ptype;
        private string _purl;
        private string _puserid;
        private string _puserkey;
        private string _pusername;

        public int GetCurrentBank()
        {
            try
            {
                return (int) DataBase.ExecuteScalar(CommandType.StoredProcedure, "GetBankPayType");
            }
            catch
            {
                return 0;
            }
        }

        public int GetCurrentCardPayType(string type)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@CardType", SqlDbType.Int, 4) };
            commandParameters[0].Value = type;
            return (int) DataBase.ExecuteScalar(CommandType.StoredProcedure, "GetCardPayType", commandParameters);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM PayKey ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString()).Tables[0];
        }

        public void GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * ");
            builder.Append(" FROM PayKey ");
            builder.Append(" where ptype=@ptype ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ptype", SqlDbType.Int, 4) };
            commandParameters[0].Value = this.ptype;
            DataSet set = DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), commandParameters);
            if (set.Tables[0].Rows.Count > 0)
            {
                if (set.Tables[0].Rows[0]["ptype"].ToString() != "")
                {
                    this.ptype = int.Parse(set.Tables[0].Rows[0]["ptype"].ToString());
                }
                this.puserid = set.Tables[0].Rows[0]["puserid"].ToString();
                this.puserkey = set.Tables[0].Rows[0]["puserkey"].ToString();
                this.pusername = set.Tables[0].Rows[0]["pusername"].ToString();
                this.postBankUrl = set.Tables[0].Rows[0]["postbankUrl"].ToString();
                this.postCardUrl = set.Tables[0].Rows[0]["postcardurl"].ToString();
                this.postSmsUrl = set.Tables[0].Rows[0]["postsmsurl"].ToString();
            }
        }

        public void Update()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PayKey set ");
            builder.Append("puserid=@puserid,");
            builder.Append("puserkey=@puserkey,");
            builder.Append("pusername=@pusername,");
            builder.Append("postbankUrl=@postbankUrl,");
            builder.Append("postcardurl=@postcardurl,");
            builder.Append("postsmsurl=@postsmsurl");
            builder.Append(" where ptype=@ptype");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ptype", SqlDbType.Int, 4), new SqlParameter("@puserid", SqlDbType.VarChar, 100), new SqlParameter("@puserkey", SqlDbType.VarChar, 200), new SqlParameter("@pusername", SqlDbType.VarChar, 50), new SqlParameter("@postbankurl", SqlDbType.VarChar, 200), new SqlParameter("@postcardurl", SqlDbType.VarChar, 200), new SqlParameter("@postsmsurl", SqlDbType.VarChar, 200) };
            commandParameters[0].Value = this.ptype;
            commandParameters[1].Value = this.puserid;
            commandParameters[2].Value = this.puserkey;
            commandParameters[3].Value = this.pusername;
            commandParameters[4].Value = this.postBankUrl;
            commandParameters[5].Value = this.postCardUrl;
            commandParameters[6].Value = this.postSmsUrl;
            DataBase.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int pisok
        {
            get
            {
                return this._pisok;
            }
            set
            {
                this._pisok = value;
            }
        }

        public string postBankUrl
        {
            get
            {
                return this._postbankurl;
            }
            set
            {
                this._postbankurl = value;
            }
        }

        public string postCardUrl
        {
            get
            {
                return this._postcardurl;
            }
            set
            {
                this._postcardurl = value;
            }
        }

        public string postSmsUrl
        {
            get
            {
                return this._postsmsurl;
            }
            set
            {
                this._postsmsurl = value;
            }
        }

        public int ptype
        {
            get
            {
                return this._ptype;
            }
            set
            {
                this._ptype = value;
            }
        }

        public string purl
        {
            get
            {
                return this._purl;
            }
            set
            {
                this._purl = value;
            }
        }

        public string puserid
        {
            get
            {
                return this._puserid;
            }
            set
            {
                this._puserid = value;
            }
        }

        public string puserkey
        {
            get
            {
                return this._puserkey;
            }
            set
            {
                this._puserkey = value;
            }
        }

        public string pusername
        {
            get
            {
                return this._pusername;
            }
            set
            {
                this._pusername = value;
            }
        }
    }
}

