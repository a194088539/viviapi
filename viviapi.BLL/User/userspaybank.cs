using DBAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace viviapi.BLL.User
{
    public class userspaybank
    {
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "userspaybank");
        }

        public bool Exists(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select count(1) from userspaybank");
            stringBuilder.Append(" where id=@id");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            return DbHelperSQL.Exists(stringBuilder.ToString(), sqlParameterArray);
        }

        public int Add(viviapi.Model.User.userspaybank model)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("insert into userspaybank(");
            stringBuilder.Append("userid,accoutType,pmode,account,payeeName,BankCode,payeeBank,provinceCode,bankProvince,cityCode,bankCity,bankAddress,status,AddTime,updateTime)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@userid,@accoutType,@pmode,@account,@payeeName,@BankCode,@payeeBank,@provinceCode,@bankProvince,@cityCode,@bankCity,@bankAddress,@status,@AddTime,@updateTime)");
            stringBuilder.Append(";select @@IDENTITY");
            SqlParameter[] sqlParameterArray = new SqlParameter[15]
            {
        new SqlParameter("@userid", SqlDbType.Int, 4),
        new SqlParameter("@accoutType", SqlDbType.TinyInt, 1),
        new SqlParameter("@pmode", SqlDbType.TinyInt, 1),
        new SqlParameter("@account", SqlDbType.VarChar, 50),
        new SqlParameter("@payeeName", SqlDbType.VarChar, 50),
        new SqlParameter("@BankCode", SqlDbType.VarChar, 50),
        new SqlParameter("@payeeBank", SqlDbType.VarChar, 50),
        new SqlParameter("@provinceCode", SqlDbType.VarChar, 50),
        new SqlParameter("@bankProvince", SqlDbType.VarChar, 50),
        new SqlParameter("@cityCode", SqlDbType.VarChar, 50),
        new SqlParameter("@bankCity", SqlDbType.VarChar, 50),
        new SqlParameter("@bankAddress", SqlDbType.VarChar, 100),
        new SqlParameter("@status", SqlDbType.TinyInt, 1),
        new SqlParameter("@AddTime", SqlDbType.DateTime),
        new SqlParameter("@updateTime", SqlDbType.DateTime)
            };
            sqlParameterArray[0].Value = (object)model.userid;
            sqlParameterArray[1].Value = (object)model.accoutType;
            sqlParameterArray[2].Value = (object)model.pmode;
            sqlParameterArray[3].Value = (object)model.account;
            sqlParameterArray[4].Value = (object)model.payeeName;
            sqlParameterArray[5].Value = (object)model.BankCode;
            sqlParameterArray[6].Value = (object)model.payeeBank;
            sqlParameterArray[7].Value = (object)model.provinceCode;
            sqlParameterArray[8].Value = (object)model.bankProvince;
            sqlParameterArray[9].Value = (object)model.cityCode;
            sqlParameterArray[10].Value = (object)model.bankCity;
            sqlParameterArray[11].Value = (object)model.bankAddress;
            sqlParameterArray[12].Value = (object)model.status;
            sqlParameterArray[13].Value = (object)model.AddTime;
            sqlParameterArray[14].Value = (object)model.updateTime;
            object single = DbHelperSQL.GetSingle(stringBuilder.ToString(), sqlParameterArray);
            if (single == null)
                return 0;
            return Convert.ToInt32(single);
        }

        public bool Update(viviapi.Model.User.userspaybank model)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("update userspaybank set ");
            stringBuilder.Append("userid=@userid,");
            stringBuilder.Append("accoutType=@accoutType,");
            stringBuilder.Append("pmode=@pmode,");
            stringBuilder.Append("account=@account,");
            stringBuilder.Append("payeeName=@payeeName,");
            stringBuilder.Append("BankCode=@BankCode,");
            stringBuilder.Append("payeeBank=@payeeBank,");
            stringBuilder.Append("provinceCode=@provinceCode,");
            stringBuilder.Append("bankProvince=@bankProvince,");
            stringBuilder.Append("cityCode=@cityCode,");
            stringBuilder.Append("bankCity=@bankCity,");
            stringBuilder.Append("bankAddress=@bankAddress,");
            stringBuilder.Append("status=@status,");
            stringBuilder.Append("AddTime=@AddTime,");
            stringBuilder.Append("updateTime=@updateTime");
            stringBuilder.Append(" where id=@id");
            SqlParameter[] sqlParameterArray = new SqlParameter[16]
            {
        new SqlParameter("@userid", SqlDbType.Int, 4),
        new SqlParameter("@accoutType", SqlDbType.TinyInt, 1),
        new SqlParameter("@pmode", SqlDbType.TinyInt, 1),
        new SqlParameter("@account", SqlDbType.VarChar, 50),
        new SqlParameter("@payeeName", SqlDbType.VarChar, 50),
        new SqlParameter("@BankCode", SqlDbType.VarChar, 50),
        new SqlParameter("@payeeBank", SqlDbType.VarChar, 50),
        new SqlParameter("@provinceCode", SqlDbType.VarChar, 50),
        new SqlParameter("@bankProvince", SqlDbType.VarChar, 50),
        new SqlParameter("@cityCode", SqlDbType.VarChar, 50),
        new SqlParameter("@bankCity", SqlDbType.VarChar, 50),
        new SqlParameter("@bankAddress", SqlDbType.VarChar, 100),
        new SqlParameter("@status", SqlDbType.TinyInt, 1),
        new SqlParameter("@AddTime", SqlDbType.DateTime),
        new SqlParameter("@updateTime", SqlDbType.DateTime),
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)model.userid;
            sqlParameterArray[1].Value = (object)model.accoutType;
            sqlParameterArray[2].Value = (object)model.pmode;
            sqlParameterArray[3].Value = (object)model.account;
            sqlParameterArray[4].Value = (object)model.payeeName;
            sqlParameterArray[5].Value = (object)model.BankCode;
            sqlParameterArray[6].Value = (object)model.payeeBank;
            sqlParameterArray[7].Value = (object)model.provinceCode;
            sqlParameterArray[8].Value = (object)model.bankProvince;
            sqlParameterArray[9].Value = (object)model.cityCode;
            sqlParameterArray[10].Value = (object)model.bankCity;
            sqlParameterArray[11].Value = (object)model.bankAddress;
            sqlParameterArray[12].Value = (object)model.status;
            sqlParameterArray[13].Value = (object)model.AddTime;
            sqlParameterArray[14].Value = (object)model.updateTime;
            sqlParameterArray[15].Value = (object)model.id;
            return DbHelperSQL.ExecuteSql(stringBuilder.ToString(), sqlParameterArray) > 0;
        }

        public bool Delete(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("delete from userspaybank ");
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
            stringBuilder.Append("delete from userspaybank ");
            stringBuilder.Append(" where id in (" + idlist + ")  ");
            return DbHelperSQL.ExecuteSql(stringBuilder.ToString()) > 0;
        }

        public viviapi.Model.User.userspaybank GetModel(int userid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select  top 1 id,userid,accoutType,pmode,account,payeeName,BankCode,payeeBank,provinceCode,bankProvince,cityCode,bankCity,bankAddress,status,AddTime,updateTime from userspaybank ");
            stringBuilder.Append(" where userid=@userid");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@userid", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)userid;
            viviapi.Model.User.userspaybank userspaybank = new viviapi.Model.User.userspaybank();
            DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count > 0)
                return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
            return (viviapi.Model.User.userspaybank)null;
        }

        public viviapi.Model.User.userspaybank DataRowToModel(DataRow row)
        {
            viviapi.Model.User.userspaybank userspaybank = new viviapi.Model.User.userspaybank();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                    userspaybank.id = int.Parse(row["id"].ToString());
                if (row["userid"] != null && row["userid"].ToString() != "")
                    userspaybank.userid = int.Parse(row["userid"].ToString());
                if (row["accoutType"] != null && row["accoutType"].ToString() != "")
                    userspaybank.accoutType = int.Parse(row["accoutType"].ToString());
                if (row["pmode"] != null && row["pmode"].ToString() != "")
                    userspaybank.pmode = int.Parse(row["pmode"].ToString());
                if (row["account"] != null)
                    userspaybank.account = row["account"].ToString();
                if (row["payeeName"] != null)
                    userspaybank.payeeName = row["payeeName"].ToString();
                if (row["BankCode"] != null)
                    userspaybank.BankCode = row["BankCode"].ToString();
                if (row["payeeBank"] != null)
                    userspaybank.payeeBank = row["payeeBank"].ToString();
                if (row["provinceCode"] != null)
                    userspaybank.provinceCode = row["provinceCode"].ToString();
                if (row["bankProvince"] != null)
                    userspaybank.bankProvince = row["bankProvince"].ToString();
                if (row["cityCode"] != null)
                    userspaybank.cityCode = row["cityCode"].ToString();
                if (row["bankCity"] != null)
                    userspaybank.bankCity = row["bankCity"].ToString();
                if (row["bankAddress"] != null)
                    userspaybank.bankAddress = row["bankAddress"].ToString();
                if (row["status"] != null && row["status"].ToString() != "")
                    userspaybank.status = new int?(int.Parse(row["status"].ToString()));
                if (row["AddTime"] != null && row["AddTime"].ToString() != "")
                    userspaybank.AddTime = DateTime.Parse(row["AddTime"].ToString());
                if (row["updateTime"] != null && row["updateTime"].ToString() != "")
                    userspaybank.updateTime = new DateTime?(DateTime.Parse(row["updateTime"].ToString()));
            }
            return userspaybank;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select id,userid,accoutType,pmode,account,payeeName,BankCode,payeeBank,provinceCode,bankProvince,cityCode,bankCity,bankAddress,status,AddTime,updateTime ");
            stringBuilder.Append(" FROM userspaybank ");
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
            stringBuilder.Append(" id,userid,accoutType,pmode,account,payeeName,BankCode,payeeBank,provinceCode,bankProvince,cityCode,bankCity,bankAddress,status,AddTime,updateTime ");
            stringBuilder.Append(" FROM userspaybank ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            stringBuilder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(stringBuilder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select count(1) FROM userspaybank ");
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
            stringBuilder.Append(")AS Row, T.*  from userspaybank T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
                stringBuilder.Append(" WHERE " + strWhere);
            stringBuilder.Append(" ) TT");
            stringBuilder.AppendFormat(" WHERE TT.Row between {0} and {1}", (object)startIndex, (object)endIndex);
            return DbHelperSQL.Query(stringBuilder.ToString());
        }

        public static string GetSettleModeName(object obj)
        {
            if (obj == DBNull.Value)
                return string.Empty;
            string str = string.Empty;
            switch (Convert.ToInt32(obj))
            {
                case 1:
                    str = "银行帐户";
                    break;
                case 2:
                    str = "支付宝";
                    break;
                case 3:
                    str = "财付通";
                    break;
            }
            return str;
        }

        public static string GetAccoutTypeName(object obj)
        {
            if (obj == DBNull.Value)
                return string.Empty;
            string str = string.Empty;
            switch (Convert.ToInt32(obj))
            {
                case 0:
                    str = "私";
                    break;
                case 1:
                    str = "公";
                    break;
            }
            return str;
        }
    }
}
