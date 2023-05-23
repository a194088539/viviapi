namespace viviapi.BLL.Payment
{
    using DBAccess;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using viviapi.BLL.Sys;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviapi.Model.Payment;
    using viviapi.Model.User;
    using viviLib.ExceptionHandling;
    public sealed class PayRateFactory
    {
        internal static string PAYRATEFACTORY_CACHEKEY = (Constant.Cache_Mark + "{{D2F59122-0613-4968-ABD1-9A8F9FFD7B13}}_{0}_{1}");
        internal static string SQL_TABLE = "payrate";
        internal static string SQL_TABLE_FIELD = "[id],[rateType],[userLevel],[levName],[p95],[p94],[p96],[p98],[p99],[p100],[p101],[p102],[p103],[p104],[p105],[p106],[p107],[p108],[p109],[p110],[p111],[p112],[p113],[p114],[p115],[p116],[p117],[p118],[p119],[p90],[p200]\r\n      ,[p201]\r\n      ,[p202]\r\n      ,[p203]\r\n      ,[p204]\r\n      ,[p205]\r\n\t  ,[p206]\r\n      ,[p207]\r\n      ,[p208]\r\n      ,[p209]\r\n      ,[p210]\r\n      ,[p91]\r\n      ,[p92]\r\n      ,[p93]\r\n      ,[p97]\r\n      ,[p89]\r\n      ,[p88]\r\n      ,[p87]\r\n      ,[p86]\r\n      ,[p206]";

        public static int Add(PayRate model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] {
                    new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@rateType", SqlDbType.TinyInt, 1), new SqlParameter("@userLevel", SqlDbType.Int, 4), new SqlParameter("@levName", SqlDbType.VarChar, 50), new SqlParameter("@p98", SqlDbType.Decimal, 9), new SqlParameter("@p99", SqlDbType.Decimal, 9), new SqlParameter("@p100", SqlDbType.Decimal, 9), new SqlParameter("@p101", SqlDbType.Decimal, 9), new SqlParameter("@p102", SqlDbType.Decimal, 9), new SqlParameter("@p103", SqlDbType.Decimal, 9), new SqlParameter("@p104", SqlDbType.Decimal, 9), new SqlParameter("@p105", SqlDbType.Decimal, 9), new SqlParameter("@p106", SqlDbType.Decimal, 9), new SqlParameter("@p107", SqlDbType.Decimal, 9), new SqlParameter("@p108", SqlDbType.Decimal, 9), new SqlParameter("@p109", SqlDbType.Decimal, 9),
                    new SqlParameter("@p110", SqlDbType.Decimal, 9), new SqlParameter("@p111", SqlDbType.Decimal, 9), new SqlParameter("@p112", SqlDbType.Decimal, 9), new SqlParameter("@p113", SqlDbType.Decimal, 9), new SqlParameter("@p114", SqlDbType.Decimal, 9), new SqlParameter("@p115", SqlDbType.Decimal, 9), new SqlParameter("@p116", SqlDbType.Decimal, 9), new SqlParameter("@p117", SqlDbType.Decimal, 9), new SqlParameter("@p118", SqlDbType.Decimal, 9), new SqlParameter("@p119", SqlDbType.Decimal, 9), new SqlParameter("@p90", SqlDbType.Decimal, 9), new SqlParameter("@p200", SqlDbType.Decimal, 9), new SqlParameter("@p201", SqlDbType.Decimal, 9), new SqlParameter("@p202", SqlDbType.Decimal, 9), new SqlParameter("@p203", SqlDbType.Decimal, 9), new SqlParameter("@p204", SqlDbType.Decimal, 9),
                    new SqlParameter("@p205", SqlDbType.Decimal, 9), new SqlParameter("@p207", SqlDbType.Decimal, 9), new SqlParameter("@p208", SqlDbType.Decimal, 9), new SqlParameter("@p209", SqlDbType.Decimal, 9), new SqlParameter("@p210", SqlDbType.Decimal, 9), new SqlParameter("@p206", SqlDbType.Decimal, 9), new SqlParameter("@p95", SqlDbType.Decimal, 9), new SqlParameter("@p94", SqlDbType.Decimal, 9), new SqlParameter("@p96", SqlDbType.Decimal, 9), new SqlParameter("@p91", SqlDbType.Decimal, 9), new SqlParameter("@p92", SqlDbType.Decimal, 9), new SqlParameter("@p93", SqlDbType.Decimal, 9), new SqlParameter("@p97", SqlDbType.Decimal, 9), new SqlParameter("@p89", SqlDbType.Decimal, 9), new SqlParameter("@p88", SqlDbType.Decimal, 9), new SqlParameter("@p87", SqlDbType.Decimal, 9), new SqlParameter("@p86", SqlDbType.Decimal, 9)
                 };
                commandParameters[0].Direction = ParameterDirection.Output;
                commandParameters[1].Value = model.rateType;
                commandParameters[2].Value = model.userLevel;
                commandParameters[3].Value = model.levName;
                commandParameters[4].Value = model.p98;
                commandParameters[5].Value = model.p99;
                commandParameters[6].Value = model.p100;
                commandParameters[7].Value = model.p101;
                commandParameters[8].Value = model.p102;
                commandParameters[9].Value = model.p103;
                commandParameters[10].Value = model.p104;
                commandParameters[11].Value = model.p105;
                commandParameters[12].Value = model.p106;
                commandParameters[13].Value = model.p107;
                commandParameters[14].Value = model.p108;
                commandParameters[15].Value = model.p109;
                commandParameters[16].Value = model.p110;
                commandParameters[17].Value = model.p111;
                commandParameters[18].Value = model.p112;
                commandParameters[19].Value = model.p113;
                commandParameters[20].Value = model.p114;
                commandParameters[21].Value = model.p115;
                commandParameters[22].Value = model.p116;
                commandParameters[23].Value = model.p117;
                commandParameters[24].Value = model.p118;
                commandParameters[25].Value = model.p119;
                commandParameters[26].Value = model.p90;
                commandParameters[27].Value = model.p200;
                commandParameters[28].Value = model.p201;
                commandParameters[29].Value = model.p202;
                commandParameters[30].Value = model.p203;
                commandParameters[31].Value = model.p204;
                commandParameters[32].Value = model.p205;
                commandParameters[33].Value = model.p207;
                commandParameters[34].Value = model.p208;
                commandParameters[35].Value = model.p209;
                commandParameters[36].Value = model.p210;
                commandParameters[37].Value = model.p206;
                commandParameters[38].Value = model.p95;
                commandParameters[39].Value = model.p94;
                commandParameters[40].Value = model.p96;
                commandParameters[41].Value = model.p91;
                commandParameters[42].Value = model.p92;
                commandParameters[43].Value = model.p93;
                commandParameters[44].Value = model.p97;
                commandParameters[45].Value = model.p89;
                commandParameters[46].Value = model.p88;
                commandParameters[47].Value = model.p87;
                commandParameters[48].Value = model.p86;
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_payrate_add", commandParameters) > 0)
                {
                    return (int)commandParameters[0].Value;
                }
                return 0;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public static DataTable GetLevName(RateTypeEnum rateType)
        {
            string commandText = "SELECT id,userLevel,levName FROM payrate WHERE rateType = @rateType";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rateType", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = (int)rateType;
            return DataBase.ExecuteDataset(CommandType.Text, commandText, commandParameters).Tables[0];
        }

        public static DataTable GetList(string where)
        {
            try
            {
                DataSet set = new DataSet();
                string commandText = "select [id]\r\n      ,[rateType]\r\n      ,[userLevel]\r\n      ,[levName]\r\n      ,isnull([p98],0) as p98\r\n      ,isnull([p99],0) as p99\r\n      ,isnull([p100],0) as p100\r\n      ,isnull([p101],0) as p101\r\n      ,isnull([p102],0) as p102\r\n      ,isnull([p103],0) as p103\r\n      ,isnull([p104],0) as p104\r\n      ,isnull([p105],0) as p105\r\n      ,isnull([p106],0) as p106\r\n      ,isnull([p107],0) as p107\r\n      ,isnull([p108],0) as p108\r\n      ,isnull([p109],0) as p109\r\n      ,isnull([p110],0) as p110\r\n      ,isnull([p111],0) as p111\r\n      ,isnull([p112],0) as p112\r\n      ,isnull([p113],0) as p113\r\n      ,isnull([p114],0) as p114\r\n      ,isnull([p115],0) as p115\r\n      ,isnull([p116],0) as p116\r\n      ,isnull([p117],0) as p117\r\n      ,isnull([p118],0) as p118\r\n      ,isnull([p119],0) as p119\r\n      ,isnull([p90],0) as p90\r\n      ,isnull([p200],0) as p200\r\n      ,isnull([p201],0) as p201\r\n      ,isnull([p202],0) as p202\r\n      ,isnull([p203],0) as p203\r\n      ,isnull([p204],0) as p204\r\n      ,isnull([p205],0) as p205\r\n      ,isnull([p206],0) as p206\r\n      ,isnull([p207],0) as p207\r\n      ,isnull([p208],0) as p208\r\n      ,isnull([p209],0) as p209\r\n      ,isnull([p210],0) as p210\r\n      ,isnull([p95],0) as p95\r\n      ,isnull([p94],0) as p94\r\n      ,isnull([p91],0) as p91\r\n      ,isnull([p92],0) as p92\r\n      ,isnull([p93],0) as p93\r\n      ,isnull([p97],0) as p97\r\n      ,isnull([p89],0) as p89\r\n      ,isnull([p88],0) as p88\r\n      ,isnull([p87],0) as p87\r\n      ,isnull([p86],0) as p86\r\n      ,isnull([p96],0) as p96 from [payrate] where ";
                commandText = commandText + where;
                return DataBase.ExecuteDataset(CommandType.Text, commandText).Tables[0];
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static DataTable GetList(RateTypeEnum rateType, int lev)
        {
            try
            {
                string objId = string.Format(PAYRATEFACTORY_CACHEKEY, (int)rateType, lev);
                DataSet o = new DataSet();
                o = (DataSet)WebCache.GetCacheService().RetrieveObject(objId);
                if (o == null)
                {
                    IDictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("rateType", (int)rateType);
                    parameters.Add("userLevel", lev);
                    SqlDependency dependency = DataBase.AddSqlDependency(objId, SQL_TABLE, SQL_TABLE_FIELD, "[rateType]=@rateType and userLevel=@userLevel", parameters);
                    string commandText = "select [id],[rateType],[userLevel],[levName],[p95],[p94],[p96],[p98],[p99],[p100]\r\n      ,[p101]\r\n      ,[p102]\r\n      ,[p103]\r\n      ,[p104]\r\n      ,[p105]\r\n      ,[p106]\r\n      ,[p107]\r\n      ,[p108]\r\n      ,[p109]\r\n      ,[p110]\r\n      ,[p111]\r\n      ,[p112]\r\n      ,[p113]\r\n      ,[p114]\r\n      ,[p115]\r\n      ,[p116]\r\n      ,[p117]\r\n      ,[p118]\r\n      ,[p119]\r\n      ,[p90]\r\n      ,[p200]\r\n      ,[p201]\r\n      ,[p202]\r\n      ,[p203]\r\n      ,[p204]\r\n      ,[p205]\r\n      ,[p206]\r\n      ,[p207]\r\n      ,[p208]\r\n      ,[p209]\r\n      ,[p91]\r\n      ,[p92]\r\n      ,[p93]\r\n      ,[p97]\r\n      ,[p89]\r\n      ,[p88]\r\n      ,[p87]\r\n      ,[p86]\r\n      ,[p210] from [payrate]";
                    o = DataBase.ExecuteDataset(CommandType.Text, commandText);
                    WebCache.GetCacheService().AddObject(objId, o);
                }
                return o.Tables[0];
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static PayRate GetModel(int id)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            commandParameters[0].Value = id;
            PayRate rate = new PayRate();
            DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_payrate_GetModel", commandParameters);
            if (set.Tables[0].Rows.Count > 0)
            {
                if ((set.Tables[0].Rows[0]["id"] != null) && (set.Tables[0].Rows[0]["id"].ToString() != ""))
                {
                    rate.id = int.Parse(set.Tables[0].Rows[0]["id"].ToString());
                }
                if ((set.Tables[0].Rows[0]["rateType"] != null) && (set.Tables[0].Rows[0]["rateType"].ToString() != ""))
                {
                    rate.rateType = (RateTypeEnum)int.Parse(set.Tables[0].Rows[0]["rateType"].ToString());
                }
                if ((set.Tables[0].Rows[0]["userLevel"] != null) && (set.Tables[0].Rows[0]["userLevel"].ToString() != ""))
                {
                    rate.userLevel = int.Parse(set.Tables[0].Rows[0]["userLevel"].ToString());
                }
                if ((set.Tables[0].Rows[0]["levName"] != null) && (set.Tables[0].Rows[0]["levName"].ToString() != ""))
                {
                    rate.levName = set.Tables[0].Rows[0]["levName"].ToString();
                }
                if ((set.Tables[0].Rows[0]["p95"] != null) && (set.Tables[0].Rows[0]["p95"].ToString() != ""))
                {
                    rate.p95 = decimal.Parse(set.Tables[0].Rows[0]["p95"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p94"] != null) && (set.Tables[0].Rows[0]["p94"].ToString() != ""))
                {
                    rate.p94 = decimal.Parse(set.Tables[0].Rows[0]["p94"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p96"] != null) && (set.Tables[0].Rows[0]["p96"].ToString() != ""))
                {
                    rate.p96 = decimal.Parse(set.Tables[0].Rows[0]["p96"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p98"] != null) && (set.Tables[0].Rows[0]["p98"].ToString() != ""))
                {
                    rate.p98 = decimal.Parse(set.Tables[0].Rows[0]["p98"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p99"] != null) && (set.Tables[0].Rows[0]["p99"].ToString() != ""))
                {
                    rate.p99 = decimal.Parse(set.Tables[0].Rows[0]["p99"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p100"] != null) && (set.Tables[0].Rows[0]["p100"].ToString() != ""))
                {
                    rate.p100 = decimal.Parse(set.Tables[0].Rows[0]["p100"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p101"] != null) && (set.Tables[0].Rows[0]["p101"].ToString() != ""))
                {
                    rate.p101 = decimal.Parse(set.Tables[0].Rows[0]["p101"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p102"] != null) && (set.Tables[0].Rows[0]["p102"].ToString() != ""))
                {
                    rate.p102 = decimal.Parse(set.Tables[0].Rows[0]["p102"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p103"] != null) && (set.Tables[0].Rows[0]["p103"].ToString() != ""))
                {
                    rate.p103 = decimal.Parse(set.Tables[0].Rows[0]["p103"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p104"] != null) && (set.Tables[0].Rows[0]["p104"].ToString() != ""))
                {
                    rate.p104 = decimal.Parse(set.Tables[0].Rows[0]["p104"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p105"] != null) && (set.Tables[0].Rows[0]["p105"].ToString() != ""))
                {
                    rate.p105 = decimal.Parse(set.Tables[0].Rows[0]["p105"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p106"] != null) && (set.Tables[0].Rows[0]["p106"].ToString() != ""))
                {
                    rate.p106 = decimal.Parse(set.Tables[0].Rows[0]["p106"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p107"] != null) && (set.Tables[0].Rows[0]["p107"].ToString() != ""))
                {
                    rate.p107 = decimal.Parse(set.Tables[0].Rows[0]["p107"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p108"] != null) && (set.Tables[0].Rows[0]["p108"].ToString() != ""))
                {
                    rate.p108 = decimal.Parse(set.Tables[0].Rows[0]["p108"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p109"] != null) && (set.Tables[0].Rows[0]["p109"].ToString() != ""))
                {
                    rate.p109 = decimal.Parse(set.Tables[0].Rows[0]["p109"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p110"] != null) && (set.Tables[0].Rows[0]["p110"].ToString() != ""))
                {
                    rate.p110 = decimal.Parse(set.Tables[0].Rows[0]["p110"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p111"] != null) && (set.Tables[0].Rows[0]["p111"].ToString() != ""))
                {
                    rate.p111 = decimal.Parse(set.Tables[0].Rows[0]["p111"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p112"] != null) && (set.Tables[0].Rows[0]["p112"].ToString() != ""))
                {
                    rate.p112 = decimal.Parse(set.Tables[0].Rows[0]["p112"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p113"] != null) && (set.Tables[0].Rows[0]["p113"].ToString() != ""))
                {
                    rate.p113 = decimal.Parse(set.Tables[0].Rows[0]["p113"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p114"] != null) && (set.Tables[0].Rows[0]["p114"].ToString() != ""))
                {
                    rate.p114 = decimal.Parse(set.Tables[0].Rows[0]["p114"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p115"] != null) && (set.Tables[0].Rows[0]["p115"].ToString() != ""))
                {
                    rate.p115 = decimal.Parse(set.Tables[0].Rows[0]["p115"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p116"] != null) && (set.Tables[0].Rows[0]["p116"].ToString() != ""))
                {
                    rate.p116 = decimal.Parse(set.Tables[0].Rows[0]["p116"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p117"] != null) && (set.Tables[0].Rows[0]["p117"].ToString() != ""))
                {
                    rate.p117 = decimal.Parse(set.Tables[0].Rows[0]["p117"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p118"] != null) && (set.Tables[0].Rows[0]["p118"].ToString() != ""))
                {
                    rate.p118 = decimal.Parse(set.Tables[0].Rows[0]["p118"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p119"] != null) && (set.Tables[0].Rows[0]["p119"].ToString() != ""))
                {
                    rate.p119 = decimal.Parse(set.Tables[0].Rows[0]["p119"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p90"] != null) && (set.Tables[0].Rows[0]["p90"].ToString() != ""))
                {
                    rate.p90 = decimal.Parse(set.Tables[0].Rows[0]["p90"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p200"] != null) && (set.Tables[0].Rows[0]["p200"].ToString() != ""))
                {
                    rate.p200 = decimal.Parse(set.Tables[0].Rows[0]["p200"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p201"] != null) && (set.Tables[0].Rows[0]["p201"].ToString() != ""))
                {
                    rate.p201 = decimal.Parse(set.Tables[0].Rows[0]["p201"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p202"] != null) && (set.Tables[0].Rows[0]["p202"].ToString() != ""))
                {
                    rate.p202 = decimal.Parse(set.Tables[0].Rows[0]["p202"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p203"] != null) && (set.Tables[0].Rows[0]["p203"].ToString() != ""))
                {
                    rate.p203 = decimal.Parse(set.Tables[0].Rows[0]["p203"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p204"] != null) && (set.Tables[0].Rows[0]["p204"].ToString() != ""))
                {
                    rate.p204 = decimal.Parse(set.Tables[0].Rows[0]["p204"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p205"] != null) && (set.Tables[0].Rows[0]["p205"].ToString() != ""))
                {
                    rate.p205 = decimal.Parse(set.Tables[0].Rows[0]["p205"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p206"] != null) && (set.Tables[0].Rows[0]["p206"].ToString() != ""))
                {
                    rate.p206 = decimal.Parse(set.Tables[0].Rows[0]["p206"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p207"] != null) && (set.Tables[0].Rows[0]["p207"].ToString() != ""))
                {
                    rate.p207 = decimal.Parse(set.Tables[0].Rows[0]["p207"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p208"] != null) && (set.Tables[0].Rows[0]["p208"].ToString() != ""))
                {
                    rate.p208 = decimal.Parse(set.Tables[0].Rows[0]["p208"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p209"] != null) && (set.Tables[0].Rows[0]["p209"].ToString() != ""))
                {
                    rate.p209 = decimal.Parse(set.Tables[0].Rows[0]["p209"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p210"] != null) && (set.Tables[0].Rows[0]["p210"].ToString() != ""))
                {
                    rate.p210 = decimal.Parse(set.Tables[0].Rows[0]["p210"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p91"] != null) && (set.Tables[0].Rows[0]["p91"].ToString() != ""))
                {
                    rate.p91 = decimal.Parse(set.Tables[0].Rows[0]["p91"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p92"] != null) && (set.Tables[0].Rows[0]["p92"].ToString() != ""))
                {
                    rate.p92 = decimal.Parse(set.Tables[0].Rows[0]["p92"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p93"] != null) && (set.Tables[0].Rows[0]["p93"].ToString() != ""))
                {
                    rate.p93 = decimal.Parse(set.Tables[0].Rows[0]["p93"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p97"] != null) && (set.Tables[0].Rows[0]["p97"].ToString() != ""))
                {
                    rate.p97 = decimal.Parse(set.Tables[0].Rows[0]["p97"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p89"] != null) && (set.Tables[0].Rows[0]["p89"].ToString() != ""))
                {
                    rate.p89 = decimal.Parse(set.Tables[0].Rows[0]["p89"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p88"] != null) && (set.Tables[0].Rows[0]["p88"].ToString() != ""))
                {
                    rate.p88 = decimal.Parse(set.Tables[0].Rows[0]["p88"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p87"] != null) && (set.Tables[0].Rows[0]["p87"].ToString() != ""))
                {
                    rate.p87 = decimal.Parse(set.Tables[0].Rows[0]["p87"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p86"] != null) && (set.Tables[0].Rows[0]["p86"].ToString() != ""))
                {
                    rate.p86 = decimal.Parse(set.Tables[0].Rows[0]["p86"].ToString());
                }
                return rate;
            }
            return null;
        }

        public static PayRate GetModelByUser(int userid)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userid", SqlDbType.Int, 4) };
            commandParameters[0].Value = userid;
            PayRate rate = new PayRate();
            DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_payrate_GetModelByUser", commandParameters);
            if (set.Tables[0].Rows.Count > 0)
            {
                if ((set.Tables[0].Rows[0]["id"] != null) && (set.Tables[0].Rows[0]["id"].ToString() != ""))
                {
                    rate.id = int.Parse(set.Tables[0].Rows[0]["id"].ToString());
                }
                if ((set.Tables[0].Rows[0]["rateType"] != null) && (set.Tables[0].Rows[0]["rateType"].ToString() != ""))
                {
                    rate.rateType = (RateTypeEnum)int.Parse(set.Tables[0].Rows[0]["rateType"].ToString());
                }
                if ((set.Tables[0].Rows[0]["userLevel"] != null) && (set.Tables[0].Rows[0]["userLevel"].ToString() != ""))
                {
                    rate.userLevel = int.Parse(set.Tables[0].Rows[0]["userLevel"].ToString());
                }
                if ((set.Tables[0].Rows[0]["levName"] != null) && (set.Tables[0].Rows[0]["levName"].ToString() != ""))
                {
                    rate.levName = set.Tables[0].Rows[0]["levName"].ToString();
                }
                if ((set.Tables[0].Rows[0]["p95"] != null) && (set.Tables[0].Rows[0]["p95"].ToString() != ""))
                {
                    rate.p95 = decimal.Parse(set.Tables[0].Rows[0]["p95"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p94"] != null) && (set.Tables[0].Rows[0]["p94"].ToString() != ""))
                {
                    rate.p94 = decimal.Parse(set.Tables[0].Rows[0]["p94"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p96"] != null) && (set.Tables[0].Rows[0]["p96"].ToString() != ""))
                {
                    rate.p96 = decimal.Parse(set.Tables[0].Rows[0]["p96"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p98"] != null) && (set.Tables[0].Rows[0]["p98"].ToString() != ""))
                {
                    rate.p98 = decimal.Parse(set.Tables[0].Rows[0]["p98"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p99"] != null) && (set.Tables[0].Rows[0]["p99"].ToString() != ""))
                {
                    rate.p99 = decimal.Parse(set.Tables[0].Rows[0]["p99"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p100"] != null) && (set.Tables[0].Rows[0]["p100"].ToString() != ""))
                {
                    rate.p100 = decimal.Parse(set.Tables[0].Rows[0]["p100"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p101"] != null) && (set.Tables[0].Rows[0]["p101"].ToString() != ""))
                {
                    rate.p101 = decimal.Parse(set.Tables[0].Rows[0]["p101"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p102"] != null) && (set.Tables[0].Rows[0]["p102"].ToString() != ""))
                {
                    rate.p102 = decimal.Parse(set.Tables[0].Rows[0]["p102"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p103"] != null) && (set.Tables[0].Rows[0]["p103"].ToString() != ""))
                {
                    rate.p103 = decimal.Parse(set.Tables[0].Rows[0]["p103"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p104"] != null) && (set.Tables[0].Rows[0]["p104"].ToString() != ""))
                {
                    rate.p104 = decimal.Parse(set.Tables[0].Rows[0]["p104"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p105"] != null) && (set.Tables[0].Rows[0]["p105"].ToString() != ""))
                {
                    rate.p105 = decimal.Parse(set.Tables[0].Rows[0]["p105"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p106"] != null) && (set.Tables[0].Rows[0]["p106"].ToString() != ""))
                {
                    rate.p106 = decimal.Parse(set.Tables[0].Rows[0]["p106"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p107"] != null) && (set.Tables[0].Rows[0]["p107"].ToString() != ""))
                {
                    rate.p107 = decimal.Parse(set.Tables[0].Rows[0]["p107"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p108"] != null) && (set.Tables[0].Rows[0]["p108"].ToString() != ""))
                {
                    rate.p108 = decimal.Parse(set.Tables[0].Rows[0]["p108"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p109"] != null) && (set.Tables[0].Rows[0]["p109"].ToString() != ""))
                {
                    rate.p109 = decimal.Parse(set.Tables[0].Rows[0]["p109"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p110"] != null) && (set.Tables[0].Rows[0]["p110"].ToString() != ""))
                {
                    rate.p110 = decimal.Parse(set.Tables[0].Rows[0]["p110"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p111"] != null) && (set.Tables[0].Rows[0]["p111"].ToString() != ""))
                {
                    rate.p111 = decimal.Parse(set.Tables[0].Rows[0]["p111"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p112"] != null) && (set.Tables[0].Rows[0]["p112"].ToString() != ""))
                {
                    rate.p112 = decimal.Parse(set.Tables[0].Rows[0]["p112"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p113"] != null) && (set.Tables[0].Rows[0]["p113"].ToString() != ""))
                {
                    rate.p113 = decimal.Parse(set.Tables[0].Rows[0]["p113"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p114"] != null) && (set.Tables[0].Rows[0]["p114"].ToString() != ""))
                {
                    rate.p114 = decimal.Parse(set.Tables[0].Rows[0]["p114"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p115"] != null) && (set.Tables[0].Rows[0]["p115"].ToString() != ""))
                {
                    rate.p115 = decimal.Parse(set.Tables[0].Rows[0]["p115"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p116"] != null) && (set.Tables[0].Rows[0]["p116"].ToString() != ""))
                {
                    rate.p116 = decimal.Parse(set.Tables[0].Rows[0]["p116"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p117"] != null) && (set.Tables[0].Rows[0]["p117"].ToString() != ""))
                {
                    rate.p117 = decimal.Parse(set.Tables[0].Rows[0]["p117"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p118"] != null) && (set.Tables[0].Rows[0]["p118"].ToString() != ""))
                {
                    rate.p118 = decimal.Parse(set.Tables[0].Rows[0]["p118"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p119"] != null) && (set.Tables[0].Rows[0]["p119"].ToString() != ""))
                {
                    rate.p119 = decimal.Parse(set.Tables[0].Rows[0]["p119"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p90"] != null) && (set.Tables[0].Rows[0]["p90"].ToString() != ""))
                {
                    rate.p90 = decimal.Parse(set.Tables[0].Rows[0]["p90"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p200"] != null) && (set.Tables[0].Rows[0]["p200"].ToString() != ""))
                {
                    rate.p200 = decimal.Parse(set.Tables[0].Rows[0]["p200"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p201"] != null) && (set.Tables[0].Rows[0]["p201"].ToString() != ""))
                {
                    rate.p201 = decimal.Parse(set.Tables[0].Rows[0]["p201"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p202"] != null) && (set.Tables[0].Rows[0]["p202"].ToString() != ""))
                {
                    rate.p202 = decimal.Parse(set.Tables[0].Rows[0]["p202"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p203"] != null) && (set.Tables[0].Rows[0]["p203"].ToString() != ""))
                {
                    rate.p203 = decimal.Parse(set.Tables[0].Rows[0]["p203"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p204"] != null) && (set.Tables[0].Rows[0]["p204"].ToString() != ""))
                {
                    rate.p204 = decimal.Parse(set.Tables[0].Rows[0]["p204"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p205"] != null) && (set.Tables[0].Rows[0]["p205"].ToString() != ""))
                {
                    rate.p205 = decimal.Parse(set.Tables[0].Rows[0]["p205"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p206"] != null) && (set.Tables[0].Rows[0]["p206"].ToString() != ""))
                {
                    rate.p206 = decimal.Parse(set.Tables[0].Rows[0]["p206"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p207"] != null) && (set.Tables[0].Rows[0]["p207"].ToString() != ""))
                {
                    rate.p207 = decimal.Parse(set.Tables[0].Rows[0]["p207"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p208"] != null) && (set.Tables[0].Rows[0]["p208"].ToString() != ""))
                {
                    rate.p208 = decimal.Parse(set.Tables[0].Rows[0]["p208"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p209"] != null) && (set.Tables[0].Rows[0]["p209"].ToString() != ""))
                {
                    rate.p209 = decimal.Parse(set.Tables[0].Rows[0]["p209"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p210"] != null) && (set.Tables[0].Rows[0]["p210"].ToString() != ""))
                {
                    rate.p210 = decimal.Parse(set.Tables[0].Rows[0]["p210"].ToString());
                }

                if ((set.Tables[0].Rows[0]["p91"] != null) && (set.Tables[0].Rows[0]["p91"].ToString() != ""))
                {
                    rate.p91 = decimal.Parse(set.Tables[0].Rows[0]["p91"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p92"] != null) && (set.Tables[0].Rows[0]["p92"].ToString() != ""))
                {
                    rate.p92 = decimal.Parse(set.Tables[0].Rows[0]["p92"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p93"] != null) && (set.Tables[0].Rows[0]["p93"].ToString() != ""))
                {
                    rate.p93 = decimal.Parse(set.Tables[0].Rows[0]["p93"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p97"] != null) && (set.Tables[0].Rows[0]["p97"].ToString() != ""))
                {
                    rate.p97 = decimal.Parse(set.Tables[0].Rows[0]["p97"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p89"] != null) && (set.Tables[0].Rows[0]["p89"].ToString() != ""))
                {
                    rate.p89 = decimal.Parse(set.Tables[0].Rows[0]["p89"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p88"] != null) && (set.Tables[0].Rows[0]["p88"].ToString() != ""))
                {
                    rate.p88 = decimal.Parse(set.Tables[0].Rows[0]["p88"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p87"] != null) && (set.Tables[0].Rows[0]["p87"].ToString() != ""))
                {
                    rate.p87 = decimal.Parse(set.Tables[0].Rows[0]["p87"].ToString());
                }
                if ((set.Tables[0].Rows[0]["p86"] != null) && (set.Tables[0].Rows[0]["p86"].ToString() != ""))
                {
                    rate.p86 = decimal.Parse(set.Tables[0].Rows[0]["p86"].ToString());
                }
                return rate;
            }
            return null;
        }

        public static decimal GetPayRate(RateTypeEnum rateType, int lev, int paytype)
        {
            try
            {
                decimal num = 0M;
                DataTable list = GetList(rateType, lev);
                if ((list == null) && (list.Rows.Count < 1))
                {
                    return num;
                }
                DataRow[] rowArray = list.Select("userLevel=" + lev.ToString() + " and rateType=" + ((int)rateType).ToString());
                if ((rowArray == null) || (rowArray.Length <= 0))
                {
                    return 0M;
                }
                return Convert.ToDecimal(rowArray[0]["p" + paytype.ToString()]);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0M;
            }
        }

        public static decimal GetPlatformRate(int paytype)
        {
            return GetPayRate(RateTypeEnum.Platform, -2, paytype);
        }

        public static DataTable GetUserlevData()
        {
            string commandText = "select \r\n      [userLevel]\r\n      ,[levName] from [payrate] where rateType = 3 or rateType = 4 ";
            return DataBase.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public static string GetUserLevName(int userid)
        {
            try
            {
                string commandText = "SELECT a.levName FROM payrate a,userbase b WHERE\r\na.userLevel = b.userLevel and b.ID =@userid ";
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userid", SqlDbType.Int, 4) };
                commandParameters[0].Value = userid;
                return Convert.ToString(DataBase.ExecuteScalar(CommandType.Text, commandText, commandParameters));
            }
            catch
            {
                return string.Empty;
            }
        }

        public static decimal GetUserPayRate(int userId, int typeId)
        {
            UserInfo cacheUserBaseInfo = UserFactory.GetCacheUserBaseInfo(userId);
            if (cacheUserBaseInfo == null)
            {
                return 0M;
            }
            usersettingInfo model = new usersetting().GetModel(userId);
            if ((model != null) && (model.special == 1))
            {
                string payrate = model.payrate;
                if (!string.IsNullOrEmpty(payrate))
                {
                    string[] strArray = payrate.Split(new char[] { '|' });
                    foreach (string str2 in strArray)
                    {
                        string[] strArray2 = str2.Split(new char[] { ':' });
                        if (strArray2[0] == typeId.ToString())
                        {
                            return Convert.ToDecimal(strArray2[1]);
                        }
                    }
                }
            }
            RateTypeEnum member = RateTypeEnum.Member;
            if (cacheUserBaseInfo.UserType == UserTypeEnum.代理)
            {
                member = RateTypeEnum.Agent;
            }
            return GetPayRate(member, (int)cacheUserBaseInfo.UserLevel, typeId);
        }

        public static decimal GetUserPayRate(UserInfo userInfo, int userId, int typeId)
        {
            usersettingInfo model = new usersetting().GetModel(userId);
            if ((model != null) && (model.special == 1))
            {
                string payrate = model.payrate;
                if (!string.IsNullOrEmpty(payrate))
                {
                    string[] strArray = payrate.Split(new char[] { '|' });
                    foreach (string str2 in strArray)
                    {
                        string[] strArray2 = str2.Split(new char[] { ':' });
                        if (strArray2[0] == typeId.ToString())
                        {
                            return Convert.ToDecimal(strArray2[1]);
                        }
                    }
                }
            }
            RateTypeEnum member = RateTypeEnum.Member;
            if (userInfo.UserType == UserTypeEnum.代理)
            {
                member = RateTypeEnum.Agent;
            }
            return GetPayRate(member, (int)userInfo.UserLevel, typeId);
        }

        public static bool Update(PayRate model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] {
                    new SqlParameter("@rateType", SqlDbType.TinyInt, 1), new SqlParameter("@userLevel", SqlDbType.Int, 4), new SqlParameter("@levName", SqlDbType.VarChar, 50), new SqlParameter("@p98", SqlDbType.Decimal, 9), new SqlParameter("@p99", SqlDbType.Decimal, 9), new SqlParameter("@p100", SqlDbType.Decimal, 9), new SqlParameter("@p101", SqlDbType.Decimal, 9), new SqlParameter("@p102", SqlDbType.Decimal, 9), new SqlParameter("@p103", SqlDbType.Decimal, 9), new SqlParameter("@p104", SqlDbType.Decimal, 9), new SqlParameter("@p105", SqlDbType.Decimal, 9), new SqlParameter("@p106", SqlDbType.Decimal, 9), new SqlParameter("@p107", SqlDbType.Decimal, 9), new SqlParameter("@p108", SqlDbType.Decimal, 9), new SqlParameter("@p109", SqlDbType.Decimal, 9), new SqlParameter("@p110", SqlDbType.Decimal, 9),
                    new SqlParameter("@p111", SqlDbType.Decimal, 9), new SqlParameter("@p112", SqlDbType.Decimal, 9), new SqlParameter("@p113", SqlDbType.Decimal, 9), new SqlParameter("@p114", SqlDbType.Decimal, 9), new SqlParameter("@p115", SqlDbType.Decimal, 9), new SqlParameter("@p116", SqlDbType.Decimal, 9), new SqlParameter("@p117", SqlDbType.Decimal, 9), new SqlParameter("@p118", SqlDbType.Decimal, 9), new SqlParameter("@p119", SqlDbType.Decimal, 9), new SqlParameter("@p90", SqlDbType.Decimal, 9), new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@p200", SqlDbType.Decimal, 9), new SqlParameter("@p201", SqlDbType.Decimal, 9), new SqlParameter("@p202", SqlDbType.Decimal, 9), new SqlParameter("@p203", SqlDbType.Decimal, 9), new SqlParameter("@p204", SqlDbType.Decimal, 9),
                    new SqlParameter("@p205", SqlDbType.Decimal, 9), new SqlParameter("@p207", SqlDbType.Decimal, 9), new SqlParameter("@p208", SqlDbType.Decimal, 9), new SqlParameter("@p209", SqlDbType.Decimal, 9), new SqlParameter("@p210", SqlDbType.Decimal, 9), new SqlParameter("@p206", SqlDbType.Decimal, 9), new SqlParameter("@p95", SqlDbType.Decimal, 9), new SqlParameter("@p94", SqlDbType.Decimal, 9), new SqlParameter("@p96", SqlDbType.Decimal, 9), new SqlParameter("@p91", SqlDbType.Decimal, 9), new SqlParameter("@p92", SqlDbType.Decimal, 9), new SqlParameter("@p93", SqlDbType.Decimal, 9), new SqlParameter("@p97", SqlDbType.Decimal, 9), new SqlParameter("@p89", SqlDbType.Decimal, 9), new SqlParameter("@p88", SqlDbType.Decimal, 9), new SqlParameter("@p87", SqlDbType.Decimal, 9), new SqlParameter("@p86", SqlDbType.Decimal, 9)
                 };
                commandParameters[0].Value = (int)model.rateType;
                commandParameters[1].Value = model.userLevel;
                commandParameters[2].Value = model.levName;
                commandParameters[3].Value = model.p98;
                commandParameters[4].Value = model.p99;
                commandParameters[5].Value = model.p100;
                commandParameters[6].Value = model.p101;
                commandParameters[7].Value = model.p102;
                commandParameters[8].Value = model.p103;
                commandParameters[9].Value = model.p104;
                commandParameters[10].Value = model.p105;
                commandParameters[11].Value = model.p106;
                commandParameters[12].Value = model.p107;
                commandParameters[13].Value = model.p108;
                commandParameters[14].Value = model.p109;
                commandParameters[15].Value = model.p110;
                commandParameters[16].Value = model.p111;
                commandParameters[17].Value = model.p112;
                commandParameters[18].Value = model.p113;
                commandParameters[19].Value = model.p114;
                commandParameters[20].Value = model.p115;
                commandParameters[21].Value = model.p116;
                commandParameters[22].Value = model.p117;
                commandParameters[23].Value = model.p118;
                commandParameters[24].Value = model.p119;
                commandParameters[25].Value = model.p90;
                commandParameters[26].Value = model.id;
                commandParameters[27].Value = model.p200;
                commandParameters[28].Value = model.p201;
                commandParameters[29].Value = model.p202;
                commandParameters[30].Value = model.p203;
                commandParameters[31].Value = model.p204;
                commandParameters[32].Value = model.p205;
                commandParameters[33].Value = model.p207;
                commandParameters[34].Value = model.p208;
                commandParameters[35].Value = model.p209;
                commandParameters[36].Value = model.p210;
                commandParameters[37].Value = model.p206;
                commandParameters[38].Value = model.p95;
                commandParameters[39].Value = model.p94;
                commandParameters[40].Value = model.p96;
                commandParameters[41].Value = model.p91;
                commandParameters[42].Value = model.p92;
                commandParameters[43].Value = model.p93;
                commandParameters[44].Value = model.p97;
                commandParameters[45].Value = model.p89;
                commandParameters[46].Value = model.p88;
                commandParameters[47].Value = model.p87;
                commandParameters[48].Value = model.p86;

                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_payrate_Update", commandParameters) > 0)
                {
                    string objId = string.Format(PAYRATEFACTORY_CACHEKEY, (int)model.rateType, model.userLevel);
                    WebCache.GetCacheService().RemoveObject(objId);
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

