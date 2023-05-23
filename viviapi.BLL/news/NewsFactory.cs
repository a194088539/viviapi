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
    using viviLib.Data;
    using viviLib.ExceptionHandling;
    public class NewsFactory
    {
        internal const string FIELD_NEWS = "[newsid],[newstype],[newstitle],[addTime],[newscontent],[IsRed],[IsTop],[IsPop],[Isbold],[Color],[release]";
        internal const string FIELD_NEWS1 = "[newsid],[newstype],[newstitle],[release]";
        public static string NEWS_CACHE_KEY = (Constant.Cache_Mark + "NEWS");
        internal const string SQL_TABLE = "news";

        public static int Add(NewsInfo model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@newsid", SqlDbType.Int, 4), new SqlParameter("@newstype", SqlDbType.TinyInt, 1), new SqlParameter("@newstitle", SqlDbType.NVarChar, 50), new SqlParameter("@addTime", SqlDbType.DateTime), new SqlParameter("@newscontent", SqlDbType.Text), new SqlParameter("@IsRed", SqlDbType.TinyInt, 1), new SqlParameter("@IsTop", SqlDbType.TinyInt, 1), new SqlParameter("@IsPop", SqlDbType.TinyInt, 1), new SqlParameter("@Isbold", SqlDbType.TinyInt, 1), new SqlParameter("@Color", SqlDbType.VarChar, 20), new SqlParameter("@release", SqlDbType.Bit) };
                commandParameters[0].Direction = ParameterDirection.Output;
                commandParameters[1].Value = model.newstype;
                commandParameters[2].Value = model.newstitle;
                commandParameters[3].Value = model.addTime;
                commandParameters[4].Value = model.newscontent;
                commandParameters[5].Value = model.IsRed;
                commandParameters[6].Value = model.IsTop;
                commandParameters[7].Value = model.IsPop;
                commandParameters[8].Value = model.Isbold;
                commandParameters[9].Value = model.Color;
                commandParameters[10].Value = model.release;
                int num = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_news_add", commandParameters);
                int num2 = (int)commandParameters[0].Value;
                if (num2 > 0)
                {
                    ClearCache();
                }
                return num2;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            StringBuilder builder = new StringBuilder(" 1 = 1");
            if ((param != null) && (param.Count > 0))
            {
                for (int i = 0; i < param.Count; i++)
                {
                    SqlParameter parameter;
                    SearchParam param2 = param[i];
                    string str2 = param2.ParamKey.Trim().ToLower();
                    if (str2 != null)
                    {
                        if (!(str2 == "newstype"))
                        {
                            if (str2 == "newstitle")
                            {
                                goto Label_00BF;
                            }
                            if (str2 == "release")
                            {
                                goto Label_010C;
                            }
                        }
                        else
                        {
                            builder.Append(" AND [newstype] = @newstype");
                            parameter = new SqlParameter("@newstype", SqlDbType.Int);
                            parameter.Value = (int)param2.ParamValue;
                            paramList.Add(parameter);
                        }
                    }
                    continue;
                Label_00BF:
                    builder.Append(" AND [newstitle] like @newstitle");
                    parameter = new SqlParameter("@newstitle", SqlDbType.VarChar, 100);
                    parameter.Value = "%" + SqlHelper.CleanString((string)param2.ParamValue, 100) + "%";
                    paramList.Add(parameter);
                    continue;
                Label_010C:
                    builder.Append(" AND [release] = @release");
                    parameter = new SqlParameter("@release", SqlDbType.Int);
                    parameter.Value = (int)param2.ParamValue;
                    paramList.Add(parameter);
                }
            }
            return builder.ToString();
        }

        private static void ClearCache()
        {
            string objId = NEWS_CACHE_KEY;
            WebCache.GetCacheService().RemoveObject(objId);
        }

        public static bool Delete(int newsid)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@newsid", SqlDbType.Int, 4) };
                commandParameters[0].Value = newsid;
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_news_del", commandParameters) > 0)
                {
                    ClearCache();
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

        public static List<NewsInfo> GetCacheList(int newstype, int startIndex, int maxIndex)
        {
            if ((maxIndex >= 0) && (maxIndex >= startIndex))
            {
                DataTable releaseNews = GetReleaseNews();
                if (releaseNews != null)
                {
                    List<NewsInfo> list = new List<NewsInfo>();
                    int num = 0;
                    for (int i = 0; i < releaseNews.Rows.Count; i++)
                    {
                        if (int.Parse(releaseNews.Rows[i]["newstype"].ToString()) == newstype)
                        {
                            num++;
                            if ((num >= startIndex) && (num <= maxIndex))
                            {
                                list.Add(GetModelFromDR(releaseNews.Rows[i]));
                            }
                            if (num > maxIndex)
                            {
                                return list;
                            }
                        }
                    }
                    return list;
                }
            }
            return null;
        }

        public static NewsInfo GetCacheModel(int newsid)
        {
            DataTable releaseNews = GetReleaseNews();
            if (releaseNews == null)
            {
                return null;
            }
            DataRow[] rowArray = releaseNews.Select("newsid=" + newsid.ToString());
            if ((rowArray == null) || (rowArray.Length <= 0))
            {
                return null;
            }
            return GetModelFromDR(rowArray[0]);
        }

        public static List<int> GetCacheTipsNews()
        {
            DataTable releaseNews = GetReleaseNews();
            if (releaseNews == null)
            {
                return null;
            }
            List<int> list = new List<int>();
            for (int i = 0; i < releaseNews.Rows.Count; i++)
            {
                if ((int.Parse(releaseNews.Rows[i]["newstype"].ToString()) == 2) && (int.Parse(releaseNews.Rows[i]["IsPop"].ToString()) == 1))
                {
                    list.Add(int.Parse(releaseNews.Rows[i]["newsid"].ToString()));
                }
            }
            return list;
        }

        public static DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select [newsid],[newstype],[newstitle],[addTime],[newscontent],[IsRed],[IsTop],[IsPop],[Isbold],[Color],[release]");
            builder.Append(" FROM news ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public static DataSet GetList(int newstype, int startIndex, int maxIndex)
        {
            string commandText = string.Format("with t as(\r\nselect row_number() over(order by newsid Desc) as rowNum,*\r\nfrom news where newstype={0})\r\nselect * from t where rowNum between {1} and {2}", newstype, startIndex, maxIndex);
            return DataBase.ExecuteDataset(CommandType.Text, commandText);
        }

        public static NewsInfo GetModel(int newsid)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@newsid", SqlDbType.Int, 4) };
                commandParameters[0].Value = newsid;
                NewsInfo info = new NewsInfo();
                DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_news_GetModel", commandParameters);
                if (set.Tables[0].Rows.Count > 0)
                {
                    return GetModelFromDR(set.Tables[0].Rows[0]);
                }
                return null;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static NewsInfo GetModelFromDR(DataRow DR)
        {
            if (DR == null)
            {
                return null;
            }
            NewsInfo info = new NewsInfo();
            if (DR["newsid"].ToString() != "")
            {
                info.newsid = int.Parse(DR["newsid"].ToString());
            }
            if (DR["newstype"].ToString() != "")
            {
                info.newstype = int.Parse(DR["newstype"].ToString());
            }
            info.newstitle = DR["newstitle"].ToString();
            if (DR["addTime"].ToString() != "")
            {
                info.addTime = DateTime.Parse(DR["addTime"].ToString());
            }
            info.newscontent = DR["newscontent"].ToString();
            if (DR["IsRed"].ToString() != "")
            {
                info.IsRed = int.Parse(DR["IsRed"].ToString());
            }
            if (DR["IsTop"].ToString() != "")
            {
                info.IsTop = int.Parse(DR["IsTop"].ToString());
            }
            if (DR["IsPop"].ToString() != "")
            {
                info.IsPop = int.Parse(DR["IsPop"].ToString());
            }
            if (DR["Isbold"].ToString() != "")
            {
                info.Isbold = int.Parse(DR["Isbold"].ToString());
            }
            if (DR["release"].ToString() != "")
            {
                info.release = bool.Parse(DR["release"].ToString());
            }
            info.Color = DR["Color"].ToString();
            return info;
        }

        public static DataTable GetNewsList(int typeid, int pagesize, int page)
        {
            int num = (page * pagesize) + 1;
            int num2 = (page * pagesize) + pagesize;
            string str = string.Empty;
            if (typeid > 0)
            {
                str = "and newstype=" + typeid;
            }
            string commandText = string.Concat(new object[] { "SELECT * FROM \r\n\t(SELECT *,ROW_NUMBER() OVER(ORDER BY News.NewsId DESC) AS RW  FROM [News] WHERE 1=1 ", str, " )TG WHERE TG.RW between ", num, " AND ", num2, " ORDER BY istop desc,newsid desc" });
            return DataBase.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public static DataTable GetReleaseNews()
        {
            string objId = NEWS_CACHE_KEY;
            DataTable o = (DataTable)WebCache.GetCacheService().RetrieveObject(objId);
            if (o == null)
            {
                IDictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@release", 1);
                SqlDependency dependency = DataBase.AddSqlDependency(objId, "news", "[newsid],[newstype],[newstitle],[addTime],[newscontent],[IsRed],[IsTop],[IsPop],[Isbold],[Color],[release]", "[release]=@release", parameters);
                o = GetList("[release]=1 order by isTop desc,addtime desc").Tables[0];
                WebCache.GetCacheService().AddObject(objId, o);
            }
            return o;
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet set = new DataSet();
            try
            {
                string tables = "news";
                string key = "[newsid]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "addTime desc";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = BuilderWhere(searchParams, paramList);
                string commandText = SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[newsid],[newstype],[newstitle],[addTime],[newscontent],[IsRed],[IsTop],[IsPop],[Isbold],[Color],[release]", tables, wheres, orderby, key, pageSize, page, false);
                return DataBase.ExecuteDataset(CommandType.Text, commandText, paramList.ToArray());
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return set;
            }
        }

        public static bool Update(NewsInfo model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@newsid", SqlDbType.Int, 4), new SqlParameter("@newstype", SqlDbType.TinyInt, 1), new SqlParameter("@newstitle", SqlDbType.NVarChar, 50), new SqlParameter("@addTime", SqlDbType.DateTime), new SqlParameter("@newscontent", SqlDbType.Text), new SqlParameter("@IsRed", SqlDbType.TinyInt, 1), new SqlParameter("@IsTop", SqlDbType.TinyInt, 1), new SqlParameter("@IsPop", SqlDbType.TinyInt, 1), new SqlParameter("@Isbold", SqlDbType.TinyInt, 1), new SqlParameter("@Color", SqlDbType.VarChar, 20), new SqlParameter("@release", SqlDbType.Bit) };
                commandParameters[0].Value = model.newsid;
                commandParameters[1].Value = model.newstype;
                commandParameters[2].Value = model.newstitle;
                commandParameters[3].Value = model.addTime;
                commandParameters[4].Value = model.newscontent;
                commandParameters[5].Value = model.IsRed;
                commandParameters[6].Value = model.IsTop;
                commandParameters[7].Value = model.IsPop;
                commandParameters[8].Value = model.Isbold;
                commandParameters[9].Value = model.Color;
                commandParameters[10].Value = model.release;
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_news_Update", commandParameters) > 0)
                {
                    ClearCache();
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

