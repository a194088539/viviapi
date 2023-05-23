namespace viviapi.BLL
{
    using DBAccess;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using viviapi.BLL.Sys;
    using viviapi.Cache;
    using viviapi.Model;
    using viviLib.ExceptionHandling;
    public sealed class SupplierFactory
    {
        public static string CACHE_KEY = (Constant.Cache_Mark + "SUPPLIER_{0}");
        internal const string SQL_TABLE = "supplier";
        internal const string SQL_TABLE_FIELD = "[id]\r\n      ,[code]\r\n      ,[name]\r\n      ,[name1]\r\n      ,[logourl]\r\n      ,[isbank]\r\n      ,[iscard]\r\n      ,[issms]\r\n      ,[issx],[isdistribution]\r\n      ,[puserid]\r\n      ,[puserkey]\r\n      ,[pusername]\r\n      ,[puserid1]\r\n      ,[puserkey1]\r\n      ,[puserid2]\r\n      ,[puserkey2]\r\n      ,[puserid3]\r\n      ,[puserkey3]\r\n      ,[puserid4]\r\n      ,[puserkey4]\r\n      ,[puserid5]\r\n      ,[puserkey5]\r\n      ,[purl]\r\n      ,[pbakurl]\r\n      ,[jumpUrl]\r\n      ,[pcardbakurl]\r\n      ,[postBankUrl]\r\n      ,[postCardUrl]\r\n      ,[postSMSUrl],[distributionUrl]\r\n      ,[desc]\r\n      ,[sort]\r\n      ,[release]\r\n      ,[issys],[iswap],[isali],[iswx]";

        public static int Add(SupplierInfo model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] {
                    new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@code", SqlDbType.Int, 4), new SqlParameter("@name", SqlDbType.VarChar, 50), new SqlParameter("@logourl", SqlDbType.VarChar, 50), new SqlParameter("@isbank", SqlDbType.Bit, 1), new SqlParameter("@iscard", SqlDbType.Bit, 1), new SqlParameter("@issms", SqlDbType.Bit, 1), new SqlParameter("@issx", SqlDbType.Bit, 1), new SqlParameter("@puserid", SqlDbType.VarChar, 100), new SqlParameter("@puserkey", SqlDbType.VarChar, 200), new SqlParameter("@pusername", SqlDbType.VarChar, 50), new SqlParameter("@puserid1", SqlDbType.VarChar, 100), new SqlParameter("@puserkey1", SqlDbType.VarChar, 200), new SqlParameter("@puserid2", SqlDbType.VarChar, 100), new SqlParameter("@puserkey2", SqlDbType.VarChar, 200), new SqlParameter("@puserid3", SqlDbType.VarChar, 100),
                    new SqlParameter("@puserkey3", SqlDbType.VarChar, 200), new SqlParameter("@puserid4", SqlDbType.VarChar, 100), new SqlParameter("@puserkey4", SqlDbType.VarChar, 200), new SqlParameter("@puserid5", SqlDbType.VarChar, 100), new SqlParameter("@puserkey5", SqlDbType.VarChar, 200), new SqlParameter("@purl", SqlDbType.VarChar, 50), new SqlParameter("@pbakurl", SqlDbType.VarChar, 50), new SqlParameter("@postBankUrl", SqlDbType.VarChar, 200), new SqlParameter("@postCardUrl", SqlDbType.VarChar, 200), new SqlParameter("@postSMSUrl", SqlDbType.VarChar, 200), new SqlParameter("@desc", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@sort", SqlDbType.Int, 4), new SqlParameter("@release", SqlDbType.Bit, 1), new SqlParameter("@issys", SqlDbType.Bit, 1), new SqlParameter("@pcardbakurl", SqlDbType.VarChar, 50), new SqlParameter("@name1", SqlDbType.VarChar, 100),
                    new SqlParameter("@jumpUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@isdistribution", SqlDbType.Bit, 1), new SqlParameter("@distributionUrl", SqlDbType.VarChar, 0xff), new SqlParameter("@queryCardUrl", SqlDbType.VarChar, 0xff), new SqlParameter("@iswap", SqlDbType.Bit, 1), new SqlParameter("@isali", SqlDbType.Bit, 1), new SqlParameter("@iswx", SqlDbType.Bit, 1)
                 };
                commandParameters[0].Direction = ParameterDirection.Output;
                commandParameters[1].Value = model.code;
                commandParameters[2].Value = model.name;
                commandParameters[3].Value = model.logourl;
                commandParameters[4].Value = model.isbank;
                commandParameters[5].Value = model.iscard;
                commandParameters[6].Value = model.issms;
                commandParameters[7].Value = model.issx;
                commandParameters[8].Value = model.puserid;
                commandParameters[9].Value = model.puserkey;
                commandParameters[10].Value = model.pusername;
                commandParameters[11].Value = model.puserid1;
                commandParameters[12].Value = model.puserkey1;
                commandParameters[13].Value = model.puserid2;
                commandParameters[14].Value = model.puserkey2;
                commandParameters[15].Value = model.puserid3;
                commandParameters[0x10].Value = model.puserkey3;
                commandParameters[0x11].Value = model.puserid4;
                commandParameters[0x12].Value = model.puserkey4;
                commandParameters[0x13].Value = model.puserid5;
                commandParameters[20].Value = model.puserkey5;
                commandParameters[0x15].Value = model.purl;
                commandParameters[0x16].Value = model.pbakurl;
                commandParameters[0x17].Value = model.postBankUrl;
                commandParameters[0x18].Value = model.postCardUrl;
                commandParameters[0x19].Value = model.postSMSUrl;
                commandParameters[0x1a].Value = model.desc;
                commandParameters[0x1b].Value = model.sort;
                commandParameters[0x1c].Value = model.release;
                commandParameters[0x1d].Value = model.issys;
                commandParameters[30].Value = model.pcardbakurl;
                commandParameters[0x1f].Value = model.name1;
                commandParameters[0x20].Value = model.jumpUrl;
                commandParameters[0x21].Value = model.isdistribution;
                commandParameters[0x22].Value = model.distributionUrl;
                commandParameters[0x23].Value = model.queryCardUrl;
                commandParameters[0x24].Value = model.iswap;
                commandParameters[0x25].Value = model.isali;
                commandParameters[0x26].Value = model.iswx;
                int num = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_supplier_add", commandParameters);
                ClearCache(model.code.Value);
                return (int)commandParameters[0].Value;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        private static void ClearCache(int code)
        {
            string objId = string.Format(CACHE_KEY, code);
            WebCache.GetCacheService().RemoveObject(objId);
        }

        public static SupplierInfo GetCacheModel(int code)
        {
            SupplierInfo o = new SupplierInfo();
            string objId = string.Format(CACHE_KEY, code);
            o = (SupplierInfo)WebCache.GetCacheService().RetrieveObject(objId);
            if (o == null)
            {
                IDictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("code", code);
                SqlDependency dependency = DataBase.AddSqlDependency(objId, "supplier", "[id]\r\n      ,[code]\r\n      ,[name]\r\n      ,[name1]\r\n      ,[logourl]\r\n      ,[isbank]\r\n      ,[iscard]\r\n      ,[issms]\r\n      ,[issx],[isdistribution]\r\n      ,[puserid]\r\n      ,[puserkey]\r\n      ,[pusername]\r\n      ,[puserid1]\r\n      ,[puserkey1]\r\n      ,[puserid2]\r\n      ,[puserkey2]\r\n      ,[puserid3]\r\n      ,[puserkey3]\r\n      ,[puserid4]\r\n      ,[puserkey4]\r\n      ,[puserid5]\r\n      ,[puserkey5]\r\n      ,[purl]\r\n      ,[pbakurl]\r\n      ,[jumpUrl]\r\n      ,[pcardbakurl]\r\n      ,[postBankUrl]\r\n      ,[postCardUrl]\r\n      ,[postSMSUrl],[distributionUrl]\r\n      ,[desc]\r\n      ,[sort]\r\n      ,[release]\r\n      ,[issys],[iswap],[isali],[iswx]", "[code]=@code", parameters);
                o = GetModelByCode(code);
                WebCache.GetCacheService().AddObject(objId, o);
            }
            return o;
        }

        public static DataSet GetList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,code,name,name1,logourl,isbank,iscard,issms,issx,puserid,puserkey,pusername,puserid1,puserkey1,puserid2,puserkey2,puserid3,puserkey3,puserid4,puserkey4,puserid5,puserkey5,purl,pbakurl,postBankUrl,postCardUrl,postSMSUrl,[desc],sort,release,issys,pcardbakurl,iswap,isali,iswx ");
            builder.Append(" FROM supplier ");
            builder.Append(" Order by sort ");
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public static DataSet GetList(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,code,name,name1,logourl,isbank,iscard,issms,issx,puserid,puserkey,pusername,puserid1,puserkey1,puserid2,puserkey2,puserid3,puserkey3,puserid4,puserkey4,puserid5,puserkey5,purl,pbakurl,postBankUrl,postCardUrl,postSMSUrl,[desc],sort,release,issys,pcardbakurl,iswap,isali,iswx ");
            builder.Append(" FROM supplier ");
            if (!string.IsNullOrEmpty(where))
            {
                builder.AppendFormat(" where {0}", where);
            }
            builder.Append(" Order by sort ");
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public static SupplierInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
                commandParameters[0].Value = id;
                return GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_supplier_GetModel", commandParameters));
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static SupplierInfo GetModelByCode(int code)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@code", SqlDbType.Int, 4) };
                commandParameters[0].Value = code;
                return GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_supplier_GetModelBycode", commandParameters));
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static SupplierInfo GetModelFromDs(DataSet ds)
        {
            SupplierInfo info = new SupplierInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    info.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["code"].ToString() != "")
                {
                    info.code = new int?(int.Parse(ds.Tables[0].Rows[0]["code"].ToString()));
                }
                info.name = ds.Tables[0].Rows[0]["name"].ToString();
                info.name1 = ds.Tables[0].Rows[0]["name1"].ToString();
                info.logourl = ds.Tables[0].Rows[0]["logourl"].ToString();
                info.pcardbakurl = ds.Tables[0].Rows[0]["pcardbakurl"].ToString();
                if (ds.Tables[0].Rows[0]["isbank"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isbank"].ToString() == "1") || (ds.Tables[0].Rows[0]["isbank"].ToString().ToLower() == "true"))
                    {
                        info.isbank = true;
                    }
                    else
                    {
                        info.isbank = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["iscard"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["iscard"].ToString() == "1") || (ds.Tables[0].Rows[0]["iscard"].ToString().ToLower() == "true"))
                    {
                        info.iscard = true;
                    }
                    else
                    {
                        info.iscard = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["issms"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["issms"].ToString() == "1") || (ds.Tables[0].Rows[0]["issms"].ToString().ToLower() == "true"))
                    {
                        info.issms = true;
                    }
                    else
                    {
                        info.issms = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["issx"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["issx"].ToString() == "1") || (ds.Tables[0].Rows[0]["issx"].ToString().ToLower() == "true"))
                    {
                        info.issx = true;
                    }
                    else
                    {
                        info.issx = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["isdistribution"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isdistribution"].ToString() == "1") || (ds.Tables[0].Rows[0]["isdistribution"].ToString().ToLower() == "true"))
                    {
                        info.isdistribution = true;
                    }
                    else
                    {
                        info.isdistribution = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["iswap"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["iswap"].ToString() == "1") || (ds.Tables[0].Rows[0]["iswap"].ToString().ToLower() == "true"))
                    {
                        info.iswap = true;
                    }
                    else
                    {
                        info.iswap = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["isali"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isali"].ToString() == "1") || (ds.Tables[0].Rows[0]["isali"].ToString().ToLower() == "true"))
                    {
                        info.isali = true;
                    }
                    else
                    {
                        info.isali = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["iswx"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["iswx"].ToString() == "1") || (ds.Tables[0].Rows[0]["iswx"].ToString().ToLower() == "true"))
                    {
                        info.iswx = true;
                    }
                    else
                    {
                        info.iswx = false;
                    }
                }
                info.puserid = ds.Tables[0].Rows[0]["puserid"].ToString();
                info.puserkey = ds.Tables[0].Rows[0]["puserkey"].ToString();
                info.pusername = ds.Tables[0].Rows[0]["pusername"].ToString();
                info.puserid1 = ds.Tables[0].Rows[0]["puserid1"].ToString();
                info.puserkey1 = ds.Tables[0].Rows[0]["puserkey1"].ToString();
                info.puserid2 = ds.Tables[0].Rows[0]["puserid2"].ToString();
                info.puserkey2 = ds.Tables[0].Rows[0]["puserkey2"].ToString();
                info.puserid3 = ds.Tables[0].Rows[0]["puserid3"].ToString();
                info.puserkey3 = ds.Tables[0].Rows[0]["puserkey3"].ToString();
                info.puserid4 = ds.Tables[0].Rows[0]["puserid4"].ToString();
                info.puserkey4 = ds.Tables[0].Rows[0]["puserkey4"].ToString();
                info.puserid5 = ds.Tables[0].Rows[0]["puserid5"].ToString();
                info.puserkey5 = ds.Tables[0].Rows[0]["puserkey5"].ToString();
                info.purl = ds.Tables[0].Rows[0]["purl"].ToString();
                info.pbakurl = ds.Tables[0].Rows[0]["pbakurl"].ToString();
                info.postBankUrl = ds.Tables[0].Rows[0]["postBankUrl"].ToString();
                info.jumpUrl = ds.Tables[0].Rows[0]["jumpUrl"].ToString();
                info.postCardUrl = ds.Tables[0].Rows[0]["postCardUrl"].ToString();
                info.postSMSUrl = ds.Tables[0].Rows[0]["postSMSUrl"].ToString();
                info.distributionUrl = ds.Tables[0].Rows[0]["distributionUrl"].ToString();
                info.queryCardUrl = ds.Tables[0].Rows[0]["queryCardUrl"].ToString();
                info.desc = ds.Tables[0].Rows[0]["desc"].ToString();
                if (ds.Tables[0].Rows[0]["sort"].ToString() != "")
                {
                    info.sort = new int?(int.Parse(ds.Tables[0].Rows[0]["sort"].ToString()));
                }
                if (ds.Tables[0].Rows[0]["release"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["release"].ToString() == "1") || (ds.Tables[0].Rows[0]["release"].ToString().ToLower() == "true"))
                    {
                        info.release = true;
                    }
                    else
                    {
                        info.release = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["issys"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["issys"].ToString() == "1") || (ds.Tables[0].Rows[0]["issys"].ToString().ToLower() == "true"))
                    {
                        info.issys = true;
                        return info;
                    }
                    info.issys = false;
                }
                return info;
            }
            return null;
        }

        public static bool Update(SupplierInfo model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] {
                    new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@code", SqlDbType.Int, 4), new SqlParameter("@name", SqlDbType.VarChar, 50), new SqlParameter("@logourl", SqlDbType.VarChar, 50), new SqlParameter("@isbank", SqlDbType.Bit, 1), new SqlParameter("@iscard", SqlDbType.Bit, 1), new SqlParameter("@issms", SqlDbType.Bit, 1), new SqlParameter("@issx", SqlDbType.Bit, 1), new SqlParameter("@puserid", SqlDbType.VarChar, 100), new SqlParameter("@puserkey", SqlDbType.VarChar, 200), new SqlParameter("@pusername", SqlDbType.VarChar, 50), new SqlParameter("@puserid1", SqlDbType.VarChar, 100), new SqlParameter("@puserkey1", SqlDbType.VarChar, 200), new SqlParameter("@puserid2", SqlDbType.VarChar, 100), new SqlParameter("@puserkey2", SqlDbType.VarChar, 200), new SqlParameter("@puserid3", SqlDbType.VarChar, 100),
                    new SqlParameter("@puserkey3", SqlDbType.VarChar, 200), new SqlParameter("@puserid4", SqlDbType.VarChar, 100), new SqlParameter("@puserkey4", SqlDbType.VarChar, 200), new SqlParameter("@puserid5", SqlDbType.VarChar, 100), new SqlParameter("@puserkey5", SqlDbType.VarChar, 200), new SqlParameter("@purl", SqlDbType.VarChar, 50), new SqlParameter("@pbakurl", SqlDbType.VarChar, 50), new SqlParameter("@postBankUrl", SqlDbType.VarChar, 200), new SqlParameter("@postCardUrl", SqlDbType.VarChar, 200), new SqlParameter("@postSMSUrl", SqlDbType.VarChar, 200), new SqlParameter("@desc", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@sort", SqlDbType.Int, 4), new SqlParameter("@release", SqlDbType.Bit, 1), new SqlParameter("@issys", SqlDbType.Bit, 1), new SqlParameter("@pcardbakurl", SqlDbType.VarChar, 50), new SqlParameter("@name1", SqlDbType.VarChar, 100),
                    new SqlParameter("@jumpUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@isdistribution", SqlDbType.Bit, 1), new SqlParameter("@distributionUrl", SqlDbType.VarChar, 0xff), new SqlParameter("@queryCardUrl", SqlDbType.VarChar, 0xff), new SqlParameter("@iswap", SqlDbType.Bit, 1), new SqlParameter("@isali", SqlDbType.Bit, 1), new SqlParameter("@iswx", SqlDbType.Bit, 1)
                 };
                commandParameters[0].Value = model.id;
                commandParameters[1].Value = model.code;
                commandParameters[2].Value = model.name;
                commandParameters[3].Value = model.logourl;
                commandParameters[4].Value = model.isbank;
                commandParameters[5].Value = model.iscard;
                commandParameters[6].Value = model.issms;
                commandParameters[7].Value = model.issx;
                commandParameters[8].Value = model.puserid;
                commandParameters[9].Value = model.puserkey;
                commandParameters[10].Value = model.pusername;
                commandParameters[11].Value = model.puserid1;
                commandParameters[12].Value = model.puserkey1;
                commandParameters[13].Value = model.puserid2;
                commandParameters[14].Value = model.puserkey2;
                commandParameters[15].Value = model.puserid3;
                commandParameters[0x10].Value = model.puserkey3;
                commandParameters[0x11].Value = model.puserid4;
                commandParameters[0x12].Value = model.puserkey4;
                commandParameters[0x13].Value = model.puserid5;
                commandParameters[20].Value = model.puserkey5;
                commandParameters[0x15].Value = model.purl;
                commandParameters[0x16].Value = model.pbakurl;
                commandParameters[0x17].Value = model.postBankUrl;
                commandParameters[0x18].Value = model.postCardUrl;
                commandParameters[0x19].Value = model.postSMSUrl;
                commandParameters[0x1a].Value = model.desc;
                commandParameters[0x1b].Value = model.sort;
                commandParameters[0x1c].Value = model.release;
                commandParameters[0x1d].Value = model.issys;
                commandParameters[30].Value = model.pcardbakurl;
                commandParameters[0x1f].Value = model.name1;
                commandParameters[0x20].Value = model.jumpUrl;
                commandParameters[0x21].Value = model.isdistribution;
                commandParameters[0x22].Value = model.distributionUrl;
                commandParameters[0x23].Value = model.queryCardUrl;
                commandParameters[0x24].Value = model.iswap;
                commandParameters[0x25].Value = model.isali;
                commandParameters[0x26].Value = model.iswx;
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_supplier_Update", commandParameters) > 0)
                {
                    ClearCache(model.code.Value);
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }
    }
}

