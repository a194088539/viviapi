namespace viviapi.BLL.Payment
{
    using DBAccess;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Pay_PriceFactory
    {
        private int _id;
        private string _pay_100;
        private string _pay_101;
        private string _pay_102;
        private string _pay_103;
        private string _pay_104;
        private string _pay_105;
        private string _pay_106;
        private string _pay_107;
        private string _pay_108;
        private string _pay_109;
        private string _pay_110;
        private string _pay_111;
        private string _pay_112;
        private string _pay_113;
        private string _pay_114;
        private string _pay_300;
        private string _pay_name;
        private int _pri_type;

        public static double Get_Pay_Price(int Member_Type, int ugourp, int paytype)
        {
            string s = string.Empty;
            string str2 = "Pay_" + paytype.ToString();
            string commandText = "select * from Pay_Price where Pri_Type = " + Member_Type;
            SqlDataReader reader = DataBase.ExecuteReader(CommandType.Text, commandText);
            if (reader.Read())
            {
                s = reader[str2].ToString();
            }
            reader.Close();
            if (s == "")
            {
                s = "0";
            }
            return double.Parse(s);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM Pay_Price ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public void GetModel(int pid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * ");
            builder.Append(" FROM Pay_Price ");
            builder.Append(" where Pri_Type=@Pri_Type ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Pri_Type", SqlDbType.Int, 4) };
            commandParameters[0].Value = pid;
            DataSet set = DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), commandParameters);
            if (set.Tables[0].Rows.Count > 0)
            {
                if (set.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
                }
                if (set.Tables[0].Rows[0]["Pri_Type"].ToString() != "")
                {
                    this.Pri_Type = int.Parse(set.Tables[0].Rows[0]["Pri_Type"].ToString());
                }
                this.Pay_100 = set.Tables[0].Rows[0]["Pay_100"].ToString();
                this.Pay_101 = set.Tables[0].Rows[0]["Pay_101"].ToString();
                this.Pay_102 = set.Tables[0].Rows[0]["Pay_102"].ToString();
                this.Pay_103 = set.Tables[0].Rows[0]["Pay_103"].ToString();
                this.Pay_104 = set.Tables[0].Rows[0]["Pay_104"].ToString();
                this.Pay_105 = set.Tables[0].Rows[0]["Pay_105"].ToString();
                this.Pay_106 = set.Tables[0].Rows[0]["Pay_106"].ToString();
                this.Pay_107 = set.Tables[0].Rows[0]["Pay_107"].ToString();
                this.Pay_108 = set.Tables[0].Rows[0]["Pay_108"].ToString();
                this.Pay_109 = set.Tables[0].Rows[0]["Pay_109"].ToString();
                this.Pay_110 = set.Tables[0].Rows[0]["Pay_110"].ToString();
                this.Pay_111 = set.Tables[0].Rows[0]["Pay_111"].ToString();
                this.Pay_112 = set.Tables[0].Rows[0]["Pay_112"].ToString();
                this.Pay_113 = set.Tables[0].Rows[0]["Pay_113"].ToString();
                this.Pay_300 = set.Tables[0].Rows[0]["Pay_300"].ToString();
                this.Pay_114 = set.Tables[0].Rows[0]["Pay_114"].ToString();
                this.Pay_Name = set.Tables[0].Rows[0]["Pay_Name"].ToString();
            }
        }

        public void Update()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Pay_Price set ");
            builder.Append("Pay_101=@Pay_101,");
            builder.Append("Pay_102=@Pay_102,");
            builder.Append("Pay_103=@Pay_103,");
            builder.Append("Pay_104=@Pay_104,");
            builder.Append("Pay_105=@Pay_105,");
            builder.Append("Pay_106=@Pay_106,");
            builder.Append("Pay_107=@Pay_107,");
            builder.Append("Pay_108=@Pay_108,");
            builder.Append("Pay_109=@Pay_109,");
            builder.Append("Pay_110=@Pay_110,");
            builder.Append("Pay_111=@Pay_111,");
            builder.Append("Pay_112=@Pay_112,");
            builder.Append("Pay_113=@Pay_113,");
            builder.Append("Pay_300=@Pay_300,");
            builder.Append("Pay_114=@Pay_114,");
            builder.Append("Pay_100=@Pay_100,");
            builder.Append("Pay_Name=@Pay_Name");
            builder.Append(" where Pri_Type=@Pri_Type ");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@Pri_Type", SqlDbType.Int, 4), new SqlParameter("@Pay_101", SqlDbType.VarChar, 50), new SqlParameter("@Pay_102", SqlDbType.VarChar, 50), new SqlParameter("@Pay_103", SqlDbType.VarChar, 50), new SqlParameter("@Pay_104", SqlDbType.VarChar, 50), new SqlParameter("@Pay_105", SqlDbType.VarChar, 50), new SqlParameter("@Pay_106", SqlDbType.VarChar, 50), new SqlParameter("@Pay_107", SqlDbType.VarChar, 50), new SqlParameter("@Pay_108", SqlDbType.VarChar, 50), new SqlParameter("@Pay_109", SqlDbType.VarChar, 50), new SqlParameter("@Pay_110", SqlDbType.VarChar, 50), new SqlParameter("@Pay_111", SqlDbType.VarChar, 50), new SqlParameter("@Pay_112", SqlDbType.VarChar, 50), new SqlParameter("@Pay_113", SqlDbType.VarChar, 50), new SqlParameter("@Pay_300", SqlDbType.VarChar, 50), new SqlParameter("@Pay_114", SqlDbType.VarChar, 50), 
                new SqlParameter("@Pay_100", SqlDbType.VarChar, 50), new SqlParameter("@Pay_Name", SqlDbType.VarChar, 50)
             };
            commandParameters[0].Value = this.Pri_Type;
            commandParameters[1].Value = this.Pay_101;
            commandParameters[2].Value = this.Pay_102;
            commandParameters[3].Value = this.Pay_103;
            commandParameters[4].Value = this.Pay_104;
            commandParameters[5].Value = this.Pay_105;
            commandParameters[6].Value = this.Pay_106;
            commandParameters[7].Value = this.Pay_107;
            commandParameters[8].Value = this.Pay_108;
            commandParameters[9].Value = this.Pay_109;
            commandParameters[10].Value = this.Pay_110;
            commandParameters[11].Value = this.Pay_111;
            commandParameters[12].Value = this.Pay_112;
            commandParameters[13].Value = this.Pay_113;
            commandParameters[14].Value = this.Pay_300;
            commandParameters[15].Value = this.Pay_114;
            commandParameters[0x10].Value = this.Pay_100;
            commandParameters[0x11].Value = this.Pay_Name;
            DataBase.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string Pay_100
        {
            get
            {
                return this._pay_100;
            }
            set
            {
                this._pay_100 = value;
            }
        }

        public string Pay_101
        {
            get
            {
                return this._pay_101;
            }
            set
            {
                this._pay_101 = value;
            }
        }

        public string Pay_102
        {
            get
            {
                return this._pay_102;
            }
            set
            {
                this._pay_102 = value;
            }
        }

        public string Pay_103
        {
            get
            {
                return this._pay_103;
            }
            set
            {
                this._pay_103 = value;
            }
        }

        public string Pay_104
        {
            get
            {
                return this._pay_104;
            }
            set
            {
                this._pay_104 = value;
            }
        }

        public string Pay_105
        {
            get
            {
                return this._pay_105;
            }
            set
            {
                this._pay_105 = value;
            }
        }

        public string Pay_106
        {
            get
            {
                return this._pay_106;
            }
            set
            {
                this._pay_106 = value;
            }
        }

        public string Pay_107
        {
            get
            {
                return this._pay_107;
            }
            set
            {
                this._pay_107 = value;
            }
        }

        public string Pay_108
        {
            get
            {
                return this._pay_108;
            }
            set
            {
                this._pay_108 = value;
            }
        }

        public string Pay_109
        {
            get
            {
                return this._pay_109;
            }
            set
            {
                this._pay_109 = value;
            }
        }

        public string Pay_110
        {
            get
            {
                return this._pay_110;
            }
            set
            {
                this._pay_110 = value;
            }
        }

        public string Pay_111
        {
            get
            {
                return this._pay_111;
            }
            set
            {
                this._pay_111 = value;
            }
        }

        public string Pay_112
        {
            get
            {
                return this._pay_112;
            }
            set
            {
                this._pay_112 = value;
            }
        }

        public string Pay_113
        {
            get
            {
                return this._pay_113;
            }
            set
            {
                this._pay_113 = value;
            }
        }

        public string Pay_114
        {
            get
            {
                return this._pay_114;
            }
            set
            {
                this._pay_114 = value;
            }
        }

        public string Pay_300
        {
            get
            {
                return this._pay_300;
            }
            set
            {
                this._pay_300 = value;
            }
        }

        public string Pay_Name
        {
            get
            {
                return this._pay_name;
            }
            set
            {
                this._pay_name = value;
            }
        }

        public int Pri_Type
        {
            get
            {
                return this._pri_type;
            }
            set
            {
                this._pri_type = value;
            }
        }
    }
}

