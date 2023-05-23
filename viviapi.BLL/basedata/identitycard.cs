namespace viviapi.BLL.basedata
{
    using DBAccess;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using viviLib.ExceptionHandling;

    public class identitycard
    {
        public static viviapi.Model.basedata.identitycard DataRowToModel(DataRow row)
        {
            viviapi.Model.basedata.identitycard identitycard = new viviapi.Model.basedata.identitycard();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    identitycard.ID = int.Parse(row["ID"].ToString());
                }
                if (row["BM"] != null)
                {
                    identitycard.BM = row["BM"].ToString();
                }
                if (row["DQ"] != null)
                {
                    identitycard.DQ = row["DQ"].ToString();
                }
            }
            return identitycard;
        }

        public static bool GetBirthdayAndSex(string identityCard, out string birthday, out string sex)
        {
            birthday = string.Empty;
            sex = string.Empty;
            try
            {
                if (identityCard.Length == 0x12)
                {
                    birthday = identityCard.Substring(6, 4) + "年" + identityCard.Substring(10, 2) + "月" + identityCard.Substring(12, 2) + "日";
                    sex = identityCard.Substring(14, 3);
                }
                if (identityCard.Length == 15)
                {
                    birthday = "19" + identityCard.Substring(6, 2) + "年" + identityCard.Substring(8, 2) + "月" + identityCard.Substring(10, 2) + "日";
                    sex = identityCard.Substring(12, 3);
                }
                if ((int.Parse(sex) % 2) == 0)
                {
                    sex = "女";
                }
                else
                {
                    sex = "男";
                }
                return true;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,BM,DQ ");
            builder.Append(" FROM base_identitycard ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" ID,BM,DQ ");
            builder.Append(" FROM base_identitycard ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
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
                builder.Append("order by T.ID desc");
            }
            builder.Append(")AS Row, T.*  from base_identitycard T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public static viviapi.Model.basedata.identitycard GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,BM,DQ from base_identitycard ");
            builder.Append(" where ID=@ID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            commandParameters[0].Value = ID;
            viviapi.Model.basedata.identitycard identitycard = new viviapi.Model.basedata.identitycard();
            DataSet set = DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), commandParameters);
            if (set.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public static viviapi.Model.basedata.identitycard GetModel(string BM)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,BM,DQ from base_identitycard ");
            builder.Append(" where BM=@BM");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@BM", SqlDbType.NVarChar, 6) };
            commandParameters[0].Value = BM;
            viviapi.Model.basedata.identitycard identitycard = new viviapi.Model.basedata.identitycard();
            DataSet set = DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), commandParameters);
            if (set.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM base_identitycard ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object obj2 = DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }
    }
}

