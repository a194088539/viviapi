namespace viviapi.BLL.User
{
    using DBAccess;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using viviapi.Cache;
    using viviapi.Model.User;
    using viviLib.ExceptionHandling;

    public sealed class Question
    {
        public const string CACHE_KEY = "Question";
        internal const string SQL_FIELDS = "[id]\r\n      ,[question]\r\n      ,[release]\r\n      ,[sort]";
        internal const string SQL_TABLE = "question";

        public int Add(QuestionInfo model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@question", SqlDbType.NVarChar, 150), new SqlParameter("@release", SqlDbType.Bit, 1), new SqlParameter("@sort", SqlDbType.Int, 4) };
                commandParameters[0].Direction = ParameterDirection.Output;
                commandParameters[1].Value = model.question;
                commandParameters[2].Value = model.release;
                commandParameters[3].Value = model.sort;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_question_ADD", commandParameters);
                ClearCache();
                return (int)commandParameters[0].Value;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        internal static void ClearCache()
        {
            string objId = "Question";
            WebCache.GetCacheService().RemoveObject(objId);
        }

        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
                commandParameters[0].Value = id;
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_question_Delete", commandParameters) > 0)
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

        public DataTable GetCacheList()
        {
            try
            {
                string objId = "Question";
                DataSet o = new DataSet();
                o = (DataSet)WebCache.GetCacheService().RetrieveObject(objId);
                if (o == null)
                {
                    SqlDependency dependency = DataBase.AddSqlDependency(objId, "question", "[id]\r\n      ,[question]\r\n      ,[release]\r\n      ,[sort]", "", null);
                    o = this.GetList("release=1");
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

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,question,release,sort ");
            builder.Append(" FROM question ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public QuestionInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
                commandParameters[0].Value = id;
                QuestionInfo info = new QuestionInfo();
                DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_question_GetModel", commandParameters);
                if (set.Tables[0].Rows.Count > 0)
                {
                    if (set.Tables[0].Rows[0]["id"].ToString() != "")
                    {
                        info.id = int.Parse(set.Tables[0].Rows[0]["id"].ToString());
                    }
                    info.question = set.Tables[0].Rows[0]["question"].ToString();
                    if (set.Tables[0].Rows[0]["release"].ToString() != "")
                    {
                        if ((set.Tables[0].Rows[0]["release"].ToString() == "1") || (set.Tables[0].Rows[0]["release"].ToString().ToLower() == "true"))
                        {
                            info.release = true;
                        }
                        else
                        {
                            info.release = false;
                        }
                    }
                    if (set.Tables[0].Rows[0]["sort"].ToString() != "")
                    {
                        info.sort = int.Parse(set.Tables[0].Rows[0]["sort"].ToString());
                    }
                    return info;
                }
                return null;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public bool Update(QuestionInfo model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@question", SqlDbType.NVarChar, 150), new SqlParameter("@release", SqlDbType.Bit, 1), new SqlParameter("@sort", SqlDbType.Int, 4) };
                commandParameters[0].Value = model.id;
                commandParameters[1].Value = model.question;
                commandParameters[2].Value = model.release;
                commandParameters[3].Value = model.sort;
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_question_Update", commandParameters) > 0)
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

