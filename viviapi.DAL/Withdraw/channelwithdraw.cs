namespace viviapi.DAL.Withdraw
{
    using DBAccess;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class channelwithdraw
    {
        public int Add(viviapi.Model.Withdraw.channelwithdraw model)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@bankCode", SqlDbType.VarChar, 10), new SqlParameter("@bankName", SqlDbType.VarChar, 30), new SqlParameter("@supplier", SqlDbType.Int, 4), new SqlParameter("@sort", SqlDbType.Int, 4) };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.bankCode;
            parameters[2].Value = model.bankName;
            parameters[3].Value = model.supplier;
            parameters[4].Value = model.sort;
            DbHelperSQL.RunProcedure("proc_channelwithdraw_ADD", parameters, out num);
            return (int)parameters[0].Value;
        }

        public viviapi.Model.Withdraw.channelwithdraw DataRowToModel(DataRow row)
        {
            viviapi.Model.Withdraw.channelwithdraw channelwithdraw = new viviapi.Model.Withdraw.channelwithdraw();
            if (row != null)
            {
                if ((row["id"] != null) && (row["id"].ToString() != ""))
                {
                    channelwithdraw.id = int.Parse(row["id"].ToString());
                }
                if (row["bankCode"] != null)
                {
                    channelwithdraw.bankCode = row["bankCode"].ToString();
                }
                if (row["bankName"] != null)
                {
                    channelwithdraw.bankName = row["bankName"].ToString();
                }
                if ((row["supplier"] != null) && (row["supplier"].ToString() != ""))
                {
                    channelwithdraw.supplier = int.Parse(row["supplier"].ToString());
                }
                if ((row["sort"] != null) && (row["sort"].ToString() != ""))
                {
                    channelwithdraw.sort = new int?(int.Parse(row["sort"].ToString()));
                }
            }
            return channelwithdraw;
        }

        public bool Delete(int id)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            parameters[0].Value = id;
            DbHelperSQL.RunProcedure("proc_channelwithdraw_Delete", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool DeleteList(string idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from channelwithdraw ");
            builder.Append(" where id in (" + idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,bankCode,bankName,supplier,sort ");
            builder.Append(" FROM channelwithdraw ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" id,bankCode,bankName,supplier,sort ");
            builder.Append(" FROM channelwithdraw ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
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
                builder.Append("order by T.id desc");
            }
            builder.Append(")AS Row, T.*  from channelwithdraw T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public viviapi.Model.Withdraw.channelwithdraw GetModel(int id)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            parameters[0].Value = id;
            viviapi.Model.Withdraw.channelwithdraw channelwithdraw = new viviapi.Model.Withdraw.channelwithdraw();
            DataSet set = DbHelperSQL.RunProcedure("proc_channelwithdraw_GetModel", parameters, "ds");
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public viviapi.Model.Withdraw.channelwithdraw GetModelByBankName(string bankName)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@bankName", SqlDbType.VarChar, 30) };
            parameters[0].Value = bankName;
            viviapi.Model.Withdraw.channelwithdraw channelwithdraw = new viviapi.Model.Withdraw.channelwithdraw();
            DataSet set = DbHelperSQL.RunProcedure("proc_channelwithdraw_GetModelBybankName", parameters, "ds");
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM channelwithdraw ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int GetSupplier(string bankCode)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@bankCode", SqlDbType.VarChar, 10) };
            commandParameters[0].Value = bankCode;
            object obj2 = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_channelwithdraw_GetSup", commandParameters);
            if (obj2 != DBNull.Value)
            {
                return Convert.ToInt32(obj2);
            }
            return 0;
        }

        public bool Update(viviapi.Model.Withdraw.channelwithdraw model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@bankCode", SqlDbType.VarChar, 10), new SqlParameter("@bankName", SqlDbType.VarChar, 30), new SqlParameter("@supplier", SqlDbType.Int, 4), new SqlParameter("@sort", SqlDbType.Int, 4) };
            parameters[0].Value = model.id;
            parameters[1].Value = model.bankCode;
            parameters[2].Value = model.bankName;
            parameters[3].Value = model.supplier;
            parameters[4].Value = model.sort;
            DbHelperSQL.RunProcedure("proc_channelwithdraw_Update", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }
    }
}

