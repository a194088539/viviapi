using DBAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model.Settled;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Settled
{
    public class TocashScheme
    {
        public static int Add(TocashSchemeInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[16]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@schemename", SqlDbType.NVarChar, 50),
          new SqlParameter("@minamtlimitofeach", SqlDbType.Decimal, 9),
          new SqlParameter("@maxamtlimitofeach", SqlDbType.Decimal, 9),
          new SqlParameter("@dailymaxtimes", SqlDbType.Int, 4),
          new SqlParameter("@dailymaxamt", SqlDbType.Decimal, 9),
          new SqlParameter("@chargerate", SqlDbType.Decimal, 9),
          new SqlParameter("@chargeleastofeach", SqlDbType.Decimal, 9),
          new SqlParameter("@chargemostofeach", SqlDbType.Decimal, 9),
          new SqlParameter("@isdefault", SqlDbType.TinyInt, 1),
          new SqlParameter("@tranapi", SqlDbType.Int, 4),
          new SqlParameter("@bankdetentiondays", SqlDbType.Int, 4),
          new SqlParameter("@otherdetentiondays", SqlDbType.Int, 4),
          new SqlParameter("@carddetentiondays", SqlDbType.Int, 4),
          new SqlParameter("@tranRequiredAudit", SqlDbType.TinyInt, 1),
          new SqlParameter("@type", SqlDbType.TinyInt, 1)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)model.schemename;
                sqlParameterArray[2].Value = (object)model.minamtlimitofeach;
                sqlParameterArray[3].Value = (object)model.maxamtlimitofeach;
                sqlParameterArray[4].Value = (object)model.dailymaxtimes;
                sqlParameterArray[5].Value = (object)model.dailymaxamt;
                sqlParameterArray[6].Value = (object)model.chargerate;
                sqlParameterArray[7].Value = (object)model.chargeleastofeach;
                sqlParameterArray[8].Value = (object)model.chargemostofeach;
                sqlParameterArray[9].Value = (object)model.isdefault;
                sqlParameterArray[10].Value = (object)model.vaiInterface;
                sqlParameterArray[11].Value = (object)model.bankdetentiondays;
                sqlParameterArray[12].Value = (object)model.otherdetentiondays;
                sqlParameterArray[13].Value = (object)model.carddetentiondays;
                sqlParameterArray[14].Value = (object)model.tranRequiredAudit;
                sqlParameterArray[15].Value = (object)model.type;
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_tocashscheme_Add", sqlParameterArray) > 0)
                    return (int)sqlParameterArray[0].Value;
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static bool Update(TocashSchemeInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[16]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@schemename", SqlDbType.NVarChar, 50),
          new SqlParameter("@minamtlimitofeach", SqlDbType.Decimal, 9),
          new SqlParameter("@maxamtlimitofeach", SqlDbType.Decimal, 9),
          new SqlParameter("@dailymaxtimes", SqlDbType.Int, 4),
          new SqlParameter("@dailymaxamt", SqlDbType.Decimal, 9),
          new SqlParameter("@chargerate", SqlDbType.Decimal, 9),
          new SqlParameter("@chargeleastofeach", SqlDbType.Decimal, 9),
          new SqlParameter("@chargemostofeach", SqlDbType.Decimal, 9),
          new SqlParameter("@isdefault", SqlDbType.TinyInt, 1),
          new SqlParameter("@tranapi", SqlDbType.Int, 4),
          new SqlParameter("@bankdetentiondays", SqlDbType.Int, 4),
          new SqlParameter("@otherdetentiondays", SqlDbType.Int, 4),
          new SqlParameter("@carddetentiondays", SqlDbType.Int, 4),
          new SqlParameter("@tranRequiredAudit", SqlDbType.TinyInt, 1),
          new SqlParameter("@type", SqlDbType.TinyInt, 1)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.schemename;
                sqlParameterArray[2].Value = (object)model.minamtlimitofeach;
                sqlParameterArray[3].Value = (object)model.maxamtlimitofeach;
                sqlParameterArray[4].Value = (object)model.dailymaxtimes;
                sqlParameterArray[5].Value = (object)model.dailymaxamt;
                sqlParameterArray[6].Value = (object)model.chargerate;
                sqlParameterArray[7].Value = (object)model.chargeleastofeach;
                sqlParameterArray[8].Value = (object)model.chargemostofeach;
                sqlParameterArray[9].Value = (object)model.isdefault;
                sqlParameterArray[10].Value = (object)model.vaiInterface;
                sqlParameterArray[11].Value = (object)model.bankdetentiondays;
                sqlParameterArray[12].Value = (object)model.otherdetentiondays;
                sqlParameterArray[13].Value = (object)model.carddetentiondays;
                sqlParameterArray[14].Value = (object)model.tranRequiredAudit;
                sqlParameterArray[15].Value = (object)model.type;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_tocashscheme_Update", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_tocashscheme_Delete", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static TocashSchemeInfo GetModelByUser(int type, int userId)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
                {
          new SqlParameter("@type", SqlDbType.TinyInt, 1),
          new SqlParameter("@userId", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)type;
                sqlParameterArray[1].Value = (object)userId;
                return TocashScheme.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_tocashscheme_GetModelByUser", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (TocashSchemeInfo)null;
            }
        }

        public static TocashSchemeInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return TocashScheme.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_tocashscheme_GetModel", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (TocashSchemeInfo)null;
            }
        }

        public static TocashSchemeInfo GetModelFromDs(DataSet ds)
        {
            TocashSchemeInfo tocashSchemeInfo = new TocashSchemeInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return new TocashSchemeInfo();
            DataRow dataRow = ds.Tables[0].Rows[0];
            if (dataRow["id"].ToString() != "")
                tocashSchemeInfo.id = int.Parse(dataRow["id"].ToString());
            tocashSchemeInfo.schemename = dataRow["schemename"].ToString();
            if (dataRow["minamtlimitofeach"].ToString() != "")
                tocashSchemeInfo.minamtlimitofeach = Decimal.Parse(dataRow["minamtlimitofeach"].ToString());
            if (dataRow["maxamtlimitofeach"].ToString() != "")
                tocashSchemeInfo.maxamtlimitofeach = Decimal.Parse(dataRow["maxamtlimitofeach"].ToString());
            if (dataRow["dailymaxtimes"].ToString() != "")
                tocashSchemeInfo.dailymaxtimes = int.Parse(dataRow["dailymaxtimes"].ToString());
            if (dataRow["dailymaxamt"].ToString() != "")
                tocashSchemeInfo.dailymaxamt = Decimal.Parse(dataRow["dailymaxamt"].ToString());
            if (dataRow["chargerate"].ToString() != "")
                tocashSchemeInfo.chargerate = Decimal.Parse(dataRow["chargerate"].ToString());
            if (dataRow["chargeleastofeach"].ToString() != "")
                tocashSchemeInfo.chargeleastofeach = Decimal.Parse(dataRow["chargeleastofeach"].ToString());
            if (dataRow["chargemostofeach"].ToString() != "")
                tocashSchemeInfo.chargemostofeach = Decimal.Parse(dataRow["chargemostofeach"].ToString());
            if (dataRow["isdefault"].ToString() != "")
                tocashSchemeInfo.isdefault = int.Parse(dataRow["isdefault"].ToString());
            if (dataRow["tranapi"].ToString() != "")
                tocashSchemeInfo.vaiInterface = int.Parse(dataRow["tranapi"].ToString());
            if (dataRow["bankdetentiondays"] != null && dataRow["bankdetentiondays"].ToString() != "")
                tocashSchemeInfo.bankdetentiondays = int.Parse(dataRow["bankdetentiondays"].ToString());
            if (dataRow["otherdetentiondays"] != null && dataRow["otherdetentiondays"].ToString() != "")
                tocashSchemeInfo.otherdetentiondays = int.Parse(dataRow["otherdetentiondays"].ToString());
            if (dataRow["carddetentiondays"] != null && dataRow["carddetentiondays"].ToString() != "")
                tocashSchemeInfo.carddetentiondays = int.Parse(dataRow["carddetentiondays"].ToString());
            if (dataRow["tranRequiredAudit"] != null && dataRow["tranRequiredAudit"].ToString() != "")
                tocashSchemeInfo.tranRequiredAudit = byte.Parse(dataRow["tranRequiredAudit"].ToString());
            if (dataRow["type"].ToString() != "")
                tocashSchemeInfo.type = int.Parse(dataRow["type"].ToString());
            return tocashSchemeInfo;
        }

        public static DataSet GetList(string strWhere)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("select id,tranRequiredAudit,schemename,minamtlimitofeach,maxamtlimitofeach,dailymaxtimes,dailymaxamt,chargerate,chargeleastofeach,chargemostofeach,isdefault,bankdetentiondays,otherdetentiondays,carddetentiondays ");
                stringBuilder.Append(" FROM tocashscheme ");
                if (strWhere.Trim() != "")
                    stringBuilder.Append(" where " + strWhere);
                return DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), (SqlParameter[])null);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }
    }
}
