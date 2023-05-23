namespace viviapi.BLL.Channel
{
    using DBAccess;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using viviapi.Model.Channel;

    public class Channelsupplier
    {
        public bool Add(ChannelSupplier model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into channelsupplier(");
            builder.Append("typeid,suppid,userid,isopen,payrate)");
            builder.Append(" values (");
            builder.Append("@typeid,@suppid,@userid,@isopen,@payrate)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@typeid", SqlDbType.Int, 4), new SqlParameter("@suppid", SqlDbType.Int, 4), new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@isopen", SqlDbType.Bit, 1), new SqlParameter("@payrate", SqlDbType.Decimal, 9) };
            commandParameters[0].Value = model.typeid;
            commandParameters[1].Value = model.suppid;
            commandParameters[2].Value = model.userid;
            commandParameters[3].Value = model.isopen;
            commandParameters[4].Value = model.payrate;
            return (DataBase.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public ChannelSupplier DataRowToModel(DataRow row)
        {
            ChannelSupplier supplier = new ChannelSupplier();
            if (row != null)
            {
                if ((row["typeid"] != null) && (row["typeid"].ToString() != ""))
                {
                    supplier.typeid = int.Parse(row["typeid"].ToString());
                }
                if ((row["suppid"] != null) && (row["suppid"].ToString() != ""))
                {
                    supplier.suppid = int.Parse(row["suppid"].ToString());
                }
                if ((row["userid"] != null) && (row["userid"].ToString() != ""))
                {
                    supplier.userid = int.Parse(row["userid"].ToString());
                }
                if ((row["isopen"] != null) && (row["isopen"].ToString() != ""))
                {
                    if ((row["isopen"].ToString() == "1") || (row["isopen"].ToString().ToLower() == "true"))
                    {
                        supplier.isopen = true;
                    }
                    else
                    {
                        supplier.isopen = false;
                    }
                }
                if ((row["payrate"] != null) && (row["payrate"].ToString() != ""))
                {
                    supplier.payrate = decimal.Parse(row["payrate"].ToString());
                }
            }
            return supplier;
        }

        public bool Delete(int typeid, int suppid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from channelsupplier ");
            builder.Append(" where typeid=@typeid and suppid=@suppid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@typeid", SqlDbType.Int, 4), new SqlParameter("@suppid", SqlDbType.Int, 4) };
            commandParameters[0].Value = typeid;
            commandParameters[1].Value = suppid;
            return (DataBase.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select typeid,suppid,userid,isopen,payrate ");
            builder.Append(" FROM channelsupplier ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public static DataSet GetList(int typeid, int userid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT A.[typeid]\r\n      ,B.[id]     \r\n\t  ,B.[name]\r\n\t  ,B.[name1],B.code,[userid]\r\n      ,isnull([isopen],0) as isopen\r\n      ,isnull([payrate],0)*100 as payrate\r\n      ,isnull(isdefault,0) as isdefault\r\n  FROM supplier B  LEFT JOIN  [channelsupplier] A\r\n\t\tON A.suppid = B.code and A.typeid = @typeid and A.userid=@userid\r\n  where B.iscard = 1 ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@typeid", SqlDbType.Int, 4), new SqlParameter("@userid", SqlDbType.Int, 4) };
            commandParameters[0].Value = typeid;
            commandParameters[1].Value = 0;
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" typeid,suppid,userid,isopen,payrate ");
            builder.Append(" FROM channelsupplier ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public static DataSet GetList1(int typeid, int userid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT suppid,name1,payrate,isnull(isdefault,0) as isdefault  from V_channelsupplier where typeid=@typeid and userid=@userid and isopen =1");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@typeid", SqlDbType.Int, 4), new SqlParameter("@userid", SqlDbType.Int, 4) };
            commandParameters[0].Value = typeid;
            commandParameters[1].Value = 0;
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.suppid desc");
            }
            builder.Append(")AS Row, T.*  from channelsupplier T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public ChannelSupplier GetModel(int typeid, int suppid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 typeid,suppid,userid,isopen,payrate from channelsupplier ");
            builder.Append(" where typeid=@typeid and suppid=@suppid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@typeid", SqlDbType.Int, 4), new SqlParameter("@suppid", SqlDbType.Int, 4) };
            commandParameters[0].Value = typeid;
            commandParameters[1].Value = suppid;
            ChannelSupplier supplier = new ChannelSupplier();
            DataSet set = DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), commandParameters);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public static decimal GetPayRate(int typeid, int suppid)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT payrate from channelsupplier where typeid=@typeid and userid=0 and suppid =@suppid");
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@typeid", SqlDbType.Int, 4), new SqlParameter("@suppid", SqlDbType.Int, 4) };
                commandParameters[0].Value = typeid;
                commandParameters[1].Value = suppid;
                object obj2 = DataBase.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
                if (obj2 == DBNull.Value)
                {
                    return 0M;
                }
                return Convert.ToDecimal(obj2);
            }
            catch (Exception)
            {
                return 0M;
            }
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM channelsupplier ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object obj2 = DataBase.ExecuteScalar(CommandType.Text, builder.ToString());
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public static bool Insert(ChannelSupplier model)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@typeid", SqlDbType.Int, 4), new SqlParameter("@suppid", SqlDbType.Int, 4), new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@isopen", SqlDbType.Bit, 1), new SqlParameter("@payrate", SqlDbType.Decimal, 9), new SqlParameter("@isdefault", SqlDbType.Bit, 1) };
            commandParameters[0].Value = model.typeid;
            commandParameters[1].Value = model.suppid;
            commandParameters[2].Value = model.userid;
            commandParameters[3].Value = model.isopen;
            commandParameters[4].Value = model.payrate;
            commandParameters[5].Value = model.isdefault;
            return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channelsupplier_Insert", commandParameters) > 0);
        }

        public bool Update(ChannelSupplier model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update channelsupplier set ");
            builder.Append("userid=@userid,");
            builder.Append("isopen=@isopen,");
            builder.Append("payrate=@payrate");
            builder.Append(" where typeid=@typeid and suppid=@suppid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@isopen", SqlDbType.Bit, 1), new SqlParameter("@payrate", SqlDbType.Decimal, 9), new SqlParameter("@typeid", SqlDbType.Int, 4), new SqlParameter("@suppid", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.userid;
            commandParameters[1].Value = model.isopen;
            commandParameters[2].Value = model.payrate;
            commandParameters[3].Value = model.typeid;
            commandParameters[4].Value = model.suppid;
            return (DataBase.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }
    }
}

