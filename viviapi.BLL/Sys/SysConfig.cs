namespace viviapi.BLL
{
    using DBAccess;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using viviapi.BLL.Sys;
    using viviapi.Cache;
    using viviLib.ExceptionHandling;
    public class SysConfig
    {
        internal static string SQL_TABLE = "SysConfig";
        internal static string SQL_TABLE_FIELD = "id,value";
        public static string SYSCONFIG_CACHEKEY = (Constant.Cache_Mark + "SYSCONFIG");

        internal static void ClearCache()
        {
            string objId = SYSCONFIG_CACHEKEY;
            WebCache.GetCacheService().RemoveObject(objId);
        }

        public static DataSet GetCacheList()
        {
            try
            {
                string objId = SYSCONFIG_CACHEKEY;
                DataSet o = new DataSet();
                o = (DataSet)WebCache.GetCacheService().RetrieveObject(objId);
                if (o == null)
                {
                    SqlDependency dependency = DataBase.AddSqlDependency(objId, SQL_TABLE, SQL_TABLE_FIELD, string.Empty, null);
                    StringBuilder builder = new StringBuilder();
                    builder.Append(" select id,type,value ");
                    builder.Append(" FROM SysConfig ");
                    o = DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
                    WebCache.GetCacheService().AddObject(objId, o);
                }
                return o;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        private static string getData(int id)
        {
            try
            {
                DataSet cacheList = GetCacheList();
                if (cacheList == null)
                {
                    return string.Empty;
                }
                DataRow[] rowArray = cacheList.Tables[0].Select("id=" + id.ToString());
                if ((rowArray == null) || (rowArray.Length < 1))
                {
                    return string.Empty;
                }
                return rowArray[0]["value"].ToString();
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return string.Empty;
            }
        }

        public static bool Update(int id, string value)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update SysConfig set ");
                builder.Append("value=@value");
                builder.Append(" where id=@id");
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@value", SqlDbType.VarChar, 100), new SqlParameter("@id", SqlDbType.Int, 4) };
                commandParameters[0].Value = value;
                commandParameters[1].Value = id;
                ClearCache();
                return (DataBase.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static decimal alilimit
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x4f)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x4f));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static DateTime ApiDocsUpdateTime
        {
            get
            {
                try
                {
                    return Convert.ToDateTime(getData(0x30));
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
        }

        public static decimal banklimit
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x4d)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x4d));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static int BankPaySupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(5)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(5));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int CashTimesEveryDay
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x1b)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(0x1b));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int CashTimesEveryDay1
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x1f)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(0x1f));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int CashTimesEveryDay2
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x23)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(0x23));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static decimal Charges1
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x20)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x20));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static decimal Charges2
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x24)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x24));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static decimal ChargesRate
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x1c)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x1c));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static string closecashReason
        {
            get
            {
                return getData(0x2d);
            }
        }

        public static decimal DayMaxGetMoney
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x33)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x33));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static bool debuglog
        {
            get
            {
                try
                {
                    return (Convert.ToInt32(getData(0x34)) == 1);
                }
                catch
                {
                    return true;
                }
            }
        }

        public static int DefaultCardPaySupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(6)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(6));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int DefaultCPSDrate
        {
            get
            {
                try
                {
                    return Convert.ToInt32(getData(0x2b));
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static int DefaultSettledMode
        {
            get
            {
                try
                {
                    return Convert.ToInt32(getData(0x2e));
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static bool IsAudit
        {
            get
            {
                return (getData(1) == "1");
            }
        }

        public static bool isopenCash
        {
            get
            {
                return (getData(0x2c) == "1");
            }
        }

        public static bool isOpenDeduct
        {
            get
            {
                return (getData(0x2a) == "1");
            }
        }

        public static bool IsOpenNoLaiLu
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x26)))
                    {
                        return false;
                    }
                    return (Convert.ToInt32(getData(0x26)) == 1);
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return false;
                }
            }
        }

        public static bool IsOpenRegistration
        {
            get
            {
                return (getData(2) == "1");
            }
        }

        public static bool IsPhoneVerification
        {
            get
            {
                return (getData(3) == "1");
            }
        }

        public static string isUserloginByEmail
        {
            get
            {
                return getData(0x45);
            }
        }

        public static int LaiLuCount
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x25)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(0x25));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static string MailDisplayName
        {
            get
            {
                return getData(0x44);
            }
        }

        public static string MailDomain
        {
            get
            {
                return getData(0x3d);
            }
        }

        public static int MailDomainPort
        {
            get
            {
                try
                {
                    return Convert.ToInt32(getData(0x3e));
                }
                catch
                {
                    return 0x19;
                }
            }
        }

        public static string MailFrom
        {
            get
            {
                return getData(0x42);
            }
        }

        public static string MailIsSsl
        {
            get
            {
                return getData(0x43);
            }
        }

        public static string MailServerDisplayName
        {
            get
            {
                return getData(0x41);
            }
        }

        public static string MailServerPassWord
        {
            get
            {
                return getData(0x40);
            }
        }

        public static string MailServerUserName
        {
            get
            {
                return getData(0x3f);
            }
        }

        public static decimal MaxCharges
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(50)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(50));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static decimal MaxGetMoney
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x1a)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x1a));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static decimal MaxGetMoney1
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(30)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(30));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static decimal MaxGetMoney2
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x22)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x22));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static int MaxInformationNumber
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(4)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(4));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static decimal MinCharges
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x31)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x31));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static decimal MinGetMoney
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x19)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x19));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static decimal MinGetMoney1
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x1d)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x1d));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static decimal MinGetMoney2
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x21)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x21));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public static int PayDianXinSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x11)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(0x11));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int PayJiuYouSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(13)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(13));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int PayJuWangSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(10)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(10));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int PayLianTongSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(12)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(12));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int PayQQSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(11)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(11));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int PayShengDaSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(8)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(8));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int PayShenZhouXingSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(7)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(7));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int PayShuHuSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x10)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(0x10));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int PayWangYiSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(14)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(14));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int PayWanMeiSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(15)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(15));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static int PayZhengTuSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(9)))
                    {
                        return 0;
                    }
                    return Convert.ToInt32(getData(9));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
            }
        }

        public static bool radioButtonemail
        {
            get
            {
                return (getData(0x4c) == "1");
            }
        }

        public static bool radioButtonPhone
        {
            get
            {
                return (getData(0x47) == "1");
            }
        }

        public static bool radioButtonshouji
        {
            get
            {
                return (getData(0x4b) == "1");
            }
        }

        public static bool RegistrationActivationByEmail
        {
            get
            {
                try
                {
                    return (Convert.ToInt32(getData(0x2f)) == 1);
                }
                catch
                {
                    return false;
                }
            }
        }

        public static string sms_caiwu_tocash
        {
            get
            {
                return Convert.ToString(getData(0x4a));
            }
        }

        public static string sms_caiwu_tocash2
        {
            get
            {
                return Convert.ToString(getData(0x49));
            }
        }

        public static string sms_temp_Authenticate
        {
            get
            {
                return Convert.ToString(getData(0x36));
            }
        }

        public static string sms_temp_FindPwd
        {
            get
            {
                return Convert.ToString(getData(0x38));
            }
        }

        public static string sms_temp_Modify
        {
            get
            {
                return Convert.ToString(getData(0x37));
            }
        }

        public static string sms_temp_Register
        {
            get
            {
                return Convert.ToString(getData(0x35));
            }
        }

        public static string sms_temp_tocash
        {
            get
            {
                return Convert.ToString(getData(60));
            }
        }

        public static string textPhone
        {
            get
            {
                return getData(0x48);
            }
        }

        public static string UserloginMsgForCheckfail
        {
            get
            {
                return getData(0x29);
            }
        }

        public static string UserloginMsgForlock
        {
            get
            {
                return getData(0x27);
            }
        }

        public static string UserloginMsgForUnCheck
        {
            get
            {
                return getData(40);
            }
        }

        public static string WebSitedescription
        {
            get
            {
                return getData(0x3b);
            }
        }

        public static string WebSiteKey
        {
            get
            {
                return getData(0x3a);
            }
        }

        public static string WebSiteTitleSuffix
        {
            get
            {
                return getData(0x39);
            }
        }

        public static decimal weixinlimit
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(0x4e)))
                    {
                        return 0M;
                    }
                    return Convert.ToDecimal(getData(0x4e));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }
    }
}

