using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model;

namespace viviapi.BLL
{
    public class PTypeConfFactory
    {
        public bool Exists(int ID)
        {
            SqlParameter[] prams = new SqlParameter[1]
            {
        new SqlParameter("@ID", SqlDbType.Int, 4)
            };
            prams[0].Value = (object)ID;
            return DataBase.RunProc("PTypeConf_Exists", prams) == 1;
        }

        public int Add(PTypeConf model)
        {
            SqlParameter[] prams = new SqlParameter[5]
            {
        new SqlParameter("@ID", SqlDbType.Int, 4),
        new SqlParameter("@GoodType", SqlDbType.TinyInt, 1),
        new SqlParameter("@GM_ID", SqlDbType.Int, 4),
        new SqlParameter("@PayType", SqlDbType.Int, 4),
        new SqlParameter("@IsUse", SqlDbType.Bit, 1)
            };
            prams[0].Direction = ParameterDirection.Output;
            prams[1].Value = (object)model.GoodType;
            prams[2].Value = (object)model.GM_ID;
            prams[3].Value = (object)model.PayType;
            prams[4].Value = (object)model.IsUse;
            DataBase.RunProc("PTypeConf_ADD", prams);
            return (int)prams[0].Value;
        }

        public bool Update(PTypeConf model)
        {
            object obj = (object)null;
            SqlParameter[] prams = new SqlParameter[4]
            {
        new SqlParameter("@GoodType", SqlDbType.TinyInt, 1),
        new SqlParameter("@GM_ID", SqlDbType.Int, 4),
        new SqlParameter("@PayType", SqlDbType.Int, 4),
        new SqlParameter("@IsUse", SqlDbType.TinyInt, 1)
            };
            prams[0].Value = (object)model.GoodType;
            prams[1].Value = (object)model.GM_ID;
            prams[2].Value = (object)model.PayType;
            prams[3].Value = (object)model.IsUse;
            DataBase.RunProc("PTypeConf_Update", prams, out obj);
            return obj != null && obj != DBNull.Value;
        }

        public bool Delete(int ID)
        {
            SqlParameter[] prams = new SqlParameter[1]
            {
        new SqlParameter("@ID", SqlDbType.Int, 4)
            };
            prams[0].Value = (object)ID;
            return DataBase.RunProc("PTypeConf_Delete", prams) > 0;
        }

        public bool DeleteList(string IDlist)
        {
            string str1 = string.Empty;
            string str2 = IDlist;
            char[] chArray = new char[1]
            {
        ','
            };
            foreach (string s in str2.Split(chArray))
            {
                int result = 0;
                if (int.TryParse(s, out result))
                    str1 = str1 + s + ",";
            }
            if (!string.IsNullOrEmpty(str1))
                str1 = str1.Substring(0, str1.Length - 1);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("delete from PTypeConf ");
            stringBuilder.Append(" where ID in (" + str1 + ")  ");
            return DataBase.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString()) > 0;
        }

        public PTypeConf GetModel(int ID)
        {
            SqlParameter[] prams = new SqlParameter[1]
            {
        new SqlParameter("@ID", SqlDbType.Int, 4)
            };
            prams[0].Value = (object)ID;
            PTypeConf ptypeConf = new PTypeConf();
            DataSet ds = (DataSet)null;
            DataBase.RunProc("PTypeConf_GetModel", prams, out ds);
            if (ds.Tables[0].Rows.Count <= 0)
                return (PTypeConf)null;
            if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                ptypeConf.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
            if (ds.Tables[0].Rows[0]["GoodType"].ToString() != "")
                ptypeConf.GoodType = int.Parse(ds.Tables[0].Rows[0]["GoodType"].ToString());
            if (ds.Tables[0].Rows[0]["GM_ID"].ToString() != "")
                ptypeConf.GM_ID = int.Parse(ds.Tables[0].Rows[0]["GM_ID"].ToString());
            if (ds.Tables[0].Rows[0]["PayType"].ToString() != "")
                ptypeConf.PayType = int.Parse(ds.Tables[0].Rows[0]["PayType"].ToString());
            if (ds.Tables[0].Rows[0]["IsUse"].ToString() != "")
                ptypeConf.IsUse = Convert.ToInt32(ds.Tables[0].Rows[0]["IsUse"].ToString());
            return ptypeConf;
        }

        public DataSet GetList(int userid, int type)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ID,GoodType,GM_ID,PayType,IsUse,isDisable ");
            stringBuilder.Append(" FROM PTypeConf where GM_ID=@GM_ID AND GoodType=@type");
            return DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), new List<SqlParameter>()
      {
        DataBase.MakeInParam("@GM_ID", SqlDbType.Int, 4, (object) userid),
        DataBase.MakeInParam("@type", SqlDbType.Int, 4, (object) type)
      }.ToArray());
        }

        public DataSet GetBankList(int userid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ID,GoodType,GM_ID,PayType,IsUse,isDisable ");
            stringBuilder.Append(" FROM PTypeConf where GM_ID=@GM_ID AND (PayType=100 or PayType=101 or PayType =102)");
            return DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), new List<SqlParameter>()
      {
        DataBase.MakeInParam("@GM_ID", SqlDbType.Int, 4, (object) userid)
      }.ToArray());
        }

        public DataSet GetGMCloseList(int userid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ID,GoodType,GM_ID,PayType,IsUse,isDisable ");
            stringBuilder.Append(" FROM PTypeConf where GM_ID=@GM_ID and GoodType=1 and PayType <> 300 and (IsUse=0 or isDisable = 1)");
            return DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), new List<SqlParameter>()
      {
        DataBase.MakeInParam("@GM_ID", SqlDbType.Int, 4, (object) userid)
      }.ToArray());
        }

        public DataSet GetCardCloseList(int userid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ID,GoodType,GM_ID,PayType,IsUse,isDisable ");
            stringBuilder.Append(" FROM PTypeConf where GM_ID=@GM_ID and GoodType=2 and PayType <> 300 and (IsUse=0 or isDisable = 1)");
            return DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), new List<SqlParameter>()
      {
        DataBase.MakeInParam("@GM_ID", SqlDbType.Int, 4, (object) userid)
      }.ToArray());
        }

        public PTypeConf GetModelByUser(int UserID, int gdtype)
        {
            SqlParameter[] prams = new SqlParameter[2]
            {
        new SqlParameter("@userID", SqlDbType.Int, 4),
        new SqlParameter("@gdType", SqlDbType.Int, 4)
            };
            prams[0].Value = (object)UserID;
            prams[1].Value = (object)gdtype;
            PTypeConf ptypeConf = new PTypeConf();
            DataSet ds = (DataSet)null;
            DataBase.RunProc("PTypeConf_GetModelByUserID", prams, out ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                int result1 = 0;
                int result2 = 0;
                int result3 = 0;
                foreach (DataRow dataRow in (InternalDataCollectionBase)ds.Tables[0].Rows)
                {
                    int.TryParse(dataRow["PayType"].ToString(), out result1);
                    int.TryParse(dataRow["IsUse"].ToString(), out result2);
                    int.TryParse(dataRow["isDisable"].ToString(), out result3);
                    if (result2 == 0 || result3 == 1)
                    {
                        if (result1 == 100)
                            ptypeConf.PayAlipay = false;
                        else if (result1 == 101)
                            ptypeConf.PayTanPay = false;
                        else if (result1 == 102)
                            ptypeConf.PayBank = false;
                        else if (result1 == 103)
                            ptypeConf.Pay103 = false;
                        else if (result1 == 104)
                            ptypeConf.Pay104 = false;
                        else if (result1 == 105)
                            ptypeConf.Pay105 = false;
                        else if (result1 == 106)
                            ptypeConf.Pay106 = false;
                        else if (result1 == 107)
                            ptypeConf.Pay107 = false;
                        else if (result1 == 108)
                            ptypeConf.Pay108 = false;
                        else if (result1 == 109)
                            ptypeConf.Pay109 = false;
                        else if (result1 == 110)
                            ptypeConf.Pay110 = false;
                        else if (result1 == 111)
                            ptypeConf.Pay111 = false;
                        else if (result1 == 112)
                            ptypeConf.Pay112 = false;
                    }
                }
            }
            return ptypeConf;
        }
    }
}
