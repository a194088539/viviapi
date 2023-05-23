using DBAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace viviapi.BLL.Settled
{
    public class transferscheme
    {
        public bool Exists(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select count(1) from transferscheme");
            stringBuilder.Append(" where id=@id");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            return DbHelperSQL.Exists(stringBuilder.ToString(), sqlParameterArray);
        }

        public int Add(viviapi.Model.Settled.transferscheme model)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("insert into transferscheme(");
            stringBuilder.Append("schemename,minamtlimitofeach,maxamtlimitofeach,dailymaxtimes,dailymaxamt,monthmaxtimes,monthmaxamt,chargerate,chargeleastofeach,chargemostofeach,isdefault)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@schemename,@minamtlimitofeach,@maxamtlimitofeach,@dailymaxtimes,@dailymaxamt,@monthmaxtimes,@monthmaxamt,@chargerate,@chargeleastofeach,@chargemostofeach,@isdefault)");
            stringBuilder.Append(";select @@IDENTITY");
            SqlParameter[] sqlParameterArray = new SqlParameter[11]
            {
        new SqlParameter("@schemename", SqlDbType.NVarChar, 50),
        new SqlParameter("@minamtlimitofeach", SqlDbType.Decimal, 9),
        new SqlParameter("@maxamtlimitofeach", SqlDbType.Decimal, 9),
        new SqlParameter("@dailymaxtimes", SqlDbType.Int, 4),
        new SqlParameter("@dailymaxamt", SqlDbType.Decimal, 9),
        new SqlParameter("@monthmaxtimes", SqlDbType.Int, 4),
        new SqlParameter("@monthmaxamt", SqlDbType.Decimal, 9),
        new SqlParameter("@chargerate", SqlDbType.Decimal, 9),
        new SqlParameter("@chargeleastofeach", SqlDbType.Decimal, 9),
        new SqlParameter("@chargemostofeach", SqlDbType.Decimal, 9),
        new SqlParameter("@isdefault", SqlDbType.TinyInt, 1)
            };
            sqlParameterArray[0].Value = (object)model.schemename;
            sqlParameterArray[1].Value = (object)model.minamtlimitofeach;
            sqlParameterArray[2].Value = (object)model.maxamtlimitofeach;
            sqlParameterArray[3].Value = (object)model.dailymaxtimes;
            sqlParameterArray[4].Value = (object)model.dailymaxamt;
            sqlParameterArray[5].Value = (object)model.monthmaxtimes;
            sqlParameterArray[6].Value = (object)model.monthmaxamt;
            sqlParameterArray[7].Value = (object)model.chargerate;
            sqlParameterArray[8].Value = (object)model.chargeleastofeach;
            sqlParameterArray[9].Value = (object)model.chargemostofeach;
            sqlParameterArray[10].Value = (object)model.isdefault;
            object single = DbHelperSQL.GetSingle(stringBuilder.ToString(), sqlParameterArray);
            if (single == null)
                return 0;
            return Convert.ToInt32(single);
        }

        public bool Update(viviapi.Model.Settled.transferscheme model)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("update transferscheme set ");
            stringBuilder.Append("schemename=@schemename,");
            stringBuilder.Append("minamtlimitofeach=@minamtlimitofeach,");
            stringBuilder.Append("maxamtlimitofeach=@maxamtlimitofeach,");
            stringBuilder.Append("dailymaxtimes=@dailymaxtimes,");
            stringBuilder.Append("dailymaxamt=@dailymaxamt,");
            stringBuilder.Append("monthmaxtimes=@monthmaxtimes,");
            stringBuilder.Append("monthmaxamt=@monthmaxamt,");
            stringBuilder.Append("chargerate=@chargerate,");
            stringBuilder.Append("chargeleastofeach=@chargeleastofeach,");
            stringBuilder.Append("chargemostofeach=@chargemostofeach,");
            stringBuilder.Append("isdefault=@isdefault");
            stringBuilder.Append(" where id=@id");
            SqlParameter[] sqlParameterArray = new SqlParameter[12]
            {
        new SqlParameter("@schemename", SqlDbType.NVarChar, 50),
        new SqlParameter("@minamtlimitofeach", SqlDbType.Decimal, 9),
        new SqlParameter("@maxamtlimitofeach", SqlDbType.Decimal, 9),
        new SqlParameter("@dailymaxtimes", SqlDbType.Int, 4),
        new SqlParameter("@dailymaxamt", SqlDbType.Decimal, 9),
        new SqlParameter("@monthmaxtimes", SqlDbType.Int, 4),
        new SqlParameter("@monthmaxamt", SqlDbType.Decimal, 9),
        new SqlParameter("@chargerate", SqlDbType.Decimal, 9),
        new SqlParameter("@chargeleastofeach", SqlDbType.Decimal, 9),
        new SqlParameter("@chargemostofeach", SqlDbType.Decimal, 9),
        new SqlParameter("@isdefault", SqlDbType.TinyInt, 1),
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)model.schemename;
            sqlParameterArray[1].Value = (object)model.minamtlimitofeach;
            sqlParameterArray[2].Value = (object)model.maxamtlimitofeach;
            sqlParameterArray[3].Value = (object)model.dailymaxtimes;
            sqlParameterArray[4].Value = (object)model.dailymaxamt;
            sqlParameterArray[5].Value = (object)model.monthmaxtimes;
            sqlParameterArray[6].Value = (object)model.monthmaxamt;
            sqlParameterArray[7].Value = (object)model.chargerate;
            sqlParameterArray[8].Value = (object)model.chargeleastofeach;
            sqlParameterArray[9].Value = (object)model.chargemostofeach;
            sqlParameterArray[10].Value = (object)model.isdefault;
            sqlParameterArray[11].Value = (object)model.id;
            return DbHelperSQL.ExecuteSql(stringBuilder.ToString(), sqlParameterArray) > 0;
        }

        public bool Delete(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("delete from transferscheme ");
            stringBuilder.Append(" where id=@id");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            return DbHelperSQL.ExecuteSql(stringBuilder.ToString(), sqlParameterArray) > 0;
        }

        public bool DeleteList(string idlist)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("delete from transferscheme ");
            stringBuilder.Append(" where id in (" + idlist + ")  ");
            return DbHelperSQL.ExecuteSql(stringBuilder.ToString()) > 0;
        }

        public viviapi.Model.Settled.transferscheme GetModel(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select  top 1 id,schemename,minamtlimitofeach,maxamtlimitofeach,dailymaxtimes,dailymaxamt,monthmaxtimes,monthmaxamt,chargerate,chargeleastofeach,chargemostofeach,isdefault from transferscheme ");
            stringBuilder.Append(" where id=@id");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            viviapi.Model.Settled.transferscheme transferscheme = new viviapi.Model.Settled.transferscheme();
            DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count > 0)
                return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
            return (viviapi.Model.Settled.transferscheme)null;
        }

        public Decimal GetUserMonthTotalAmt(int userid)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[3]
            {
        new SqlParameter("@year", SqlDbType.Int, 4),
        new SqlParameter("@month", SqlDbType.Int, 4),
        new SqlParameter("@userid", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)DateTime.Now.Month;
            sqlParameterArray[1].Value = (object)DateTime.Now.Year;
            sqlParameterArray[2].Value = (object)userid;
            object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_transfer_getusermonthtotalamt", sqlParameterArray);
            if (obj == DBNull.Value)
                return new Decimal(0);
            return Convert.ToDecimal(obj);
        }

        public viviapi.Model.Settled.transferscheme DataRowToModel(DataRow row)
        {
            viviapi.Model.Settled.transferscheme transferscheme = new viviapi.Model.Settled.transferscheme();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                    transferscheme.id = int.Parse(row["id"].ToString());
                if (row["schemename"] != null)
                    transferscheme.schemename = row["schemename"].ToString();
                if (row["minamtlimitofeach"] != null && row["minamtlimitofeach"].ToString() != "")
                    transferscheme.minamtlimitofeach = Decimal.Parse(row["minamtlimitofeach"].ToString());
                if (row["maxamtlimitofeach"] != null && row["maxamtlimitofeach"].ToString() != "")
                    transferscheme.maxamtlimitofeach = Decimal.Parse(row["maxamtlimitofeach"].ToString());
                if (row["dailymaxtimes"] != null && row["dailymaxtimes"].ToString() != "")
                    transferscheme.dailymaxtimes = int.Parse(row["dailymaxtimes"].ToString());
                if (row["dailymaxamt"] != null && row["dailymaxamt"].ToString() != "")
                    transferscheme.dailymaxamt = Decimal.Parse(row["dailymaxamt"].ToString());
                if (row["monthmaxtimes"] != null && row["monthmaxtimes"].ToString() != "")
                    transferscheme.monthmaxtimes = int.Parse(row["monthmaxtimes"].ToString());
                if (row["monthmaxamt"] != null && row["monthmaxamt"].ToString() != "")
                    transferscheme.monthmaxamt = Decimal.Parse(row["monthmaxamt"].ToString());
                if (row["chargerate"] != null && row["chargerate"].ToString() != "")
                    transferscheme.chargerate = Decimal.Parse(row["chargerate"].ToString());
                if (row["chargeleastofeach"] != null && row["chargeleastofeach"].ToString() != "")
                    transferscheme.chargeleastofeach = Decimal.Parse(row["chargeleastofeach"].ToString());
                if (row["chargemostofeach"] != null && row["chargemostofeach"].ToString() != "")
                    transferscheme.chargemostofeach = Decimal.Parse(row["chargemostofeach"].ToString());
                if (row["isdefault"] != null && row["isdefault"].ToString() != "")
                    transferscheme.isdefault = int.Parse(row["isdefault"].ToString());
            }
            return transferscheme;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select id,schemename,minamtlimitofeach,maxamtlimitofeach,dailymaxtimes,dailymaxamt,monthmaxtimes,monthmaxamt,chargerate,chargeleastofeach,chargemostofeach,isdefault ");
            stringBuilder.Append(" FROM transferscheme ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            return DbHelperSQL.Query(stringBuilder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ");
            if (Top > 0)
                stringBuilder.Append(" top " + Top.ToString());
            stringBuilder.Append(" id,schemename,minamtlimitofeach,maxamtlimitofeach,dailymaxtimes,dailymaxamt,monthmaxtimes,monthmaxamt,chargerate,chargeleastofeach,chargemostofeach,isdefault ");
            stringBuilder.Append(" FROM transferscheme ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            stringBuilder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(stringBuilder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select count(1) FROM transferscheme ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            object single = DbHelperSQL.GetSingle(stringBuilder.ToString());
            if (single == null)
                return 0;
            return Convert.ToInt32(single);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT * FROM ( ");
            stringBuilder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
                stringBuilder.Append("order by T." + orderby);
            else
                stringBuilder.Append("order by T.id desc");
            stringBuilder.Append(")AS Row, T.*  from transferscheme T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
                stringBuilder.Append(" WHERE " + strWhere);
            stringBuilder.Append(" ) TT");
            stringBuilder.AppendFormat(" WHERE TT.Row between {0} and {1}", (object)startIndex, (object)endIndex);
            return DbHelperSQL.Query(stringBuilder.ToString());
        }
    }
}
