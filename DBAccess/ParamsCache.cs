namespace DBAccess
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ParamsCache
    {
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        private ParamsCache()
        {
        }

        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((commandText == null) || (commandText.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            string str = connectionString + ":" + commandText;
            paramCache[str] = commandParameters;
        }

        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            SqlParameter[] parameterArray = new SqlParameter[originalParameters.Length];
            int index = 0;
            int length = originalParameters.Length;
            while (index < length)
            {
                parameterArray[index] = (SqlParameter)((ICloneable)originalParameters[index]).Clone();
                index++;
            }
            return parameterArray;
        }

        private static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            SqlCommand command = new SqlCommand(spName, connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlCommandBuilder.DeriveParameters(command);
            connection.Close();
            if (!includeReturnValueParameter)
            {
                command.Parameters.RemoveAt(0);
            }
            SqlParameter[] array = new SqlParameter[command.Parameters.Count];
            command.Parameters.CopyTo(array, 0);
            foreach (SqlParameter parameter in array)
            {
                parameter.Value = DBNull.Value;
            }
            return array;
        }

        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((commandText == null) || (commandText.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            string str = connectionString + ":" + commandText;
            SqlParameter[] originalParameters = paramCache[str] as SqlParameter[];
            if (originalParameters == null)
            {
                return null;
            }
            return CloneParameters(originalParameters);
        }

        public static bool GetCachedParameterSet(string connectionString, string spName, out SqlParameter[] parms)
        {
            bool flag = false;
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            string str = connectionString + ":" + spName;
            SqlParameter[] originalParameters = paramCache[str] as SqlParameter[];
            if (originalParameters == null)
            {
                parms = null;
                return flag;
            }
            parms = CloneParameters(originalParameters);
            return true;
        }

        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName)
        {
            return GetSpParameterSet(connection, spName, false);
        }

        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            using (SqlConnection connection2 = (SqlConnection)((ICloneable)connection).Clone())
            {
                return GetSpParameterSetInternal(connection2, spName, includeReturnValueParameter);
            }
        }

        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
        }

        private static SqlParameter[] GetSpParameterSetInternal(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            string str = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            SqlParameter[] originalParameters = paramCache[str] as SqlParameter[];
            if (originalParameters == null)
            {
                SqlParameter[] parameterArray2 = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                paramCache[str] = parameterArray2;
                originalParameters = parameterArray2;
            }
            return CloneParameters(originalParameters);
        }
    }
}

