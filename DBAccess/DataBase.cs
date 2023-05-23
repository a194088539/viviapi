using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.Xml;
using viviapi.Cache;
using viviapi.SysConfig;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace DBAccess
{
    public sealed class DataBase
    {
        public static string ConnectionString
        {
            get
            {
                return Cryptography.RijndaelDecrypt(RuntimeSetting.ConnectString);
            }
        }

        private DataBase()
        {
        }

        private static void AssignParameterValues(SqlParameter[] commandParameters, DataRow dataRow)
        {
            if (commandParameters == null || dataRow == null)
                return;
            int num = 0;
            foreach (SqlParameter sqlParameter in commandParameters)
            {
                if (sqlParameter.ParameterName == null || sqlParameter.ParameterName.Length <= 1)
                    throw new Exception(string.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.", (object)num, (object)sqlParameter.ParameterName));
                if (dataRow.Table.Columns.IndexOf(sqlParameter.ParameterName.Substring(1)) != -1)
                    sqlParameter.Value = dataRow[sqlParameter.ParameterName.Substring(1)];
                ++num;
            }
        }

        public static void AssignParameterValues(SqlParameter[] commandParameters, params object[] parameterValues)
        {
            if (commandParameters == null || parameterValues == null)
                return;
            if (commandParameters.Length != parameterValues.Length)
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            int index = 0;
            for (int length = commandParameters.Length; index < length; ++index)
            {
                if (parameterValues[index] is IDbDataParameter)
                {
                    IDbDataParameter dbDataParameter = (IDbDataParameter)parameterValues[index];
                    if (dbDataParameter.Value == null)
                        commandParameters[index].Value = (object)DBNull.Value;
                    else
                        commandParameters[index].Value = dbDataParameter.Value;
                }
                else if (parameterValues[index] == null)
                    commandParameters[index].Value = (object)DBNull.Value;
                else
                    commandParameters[index].Value = parameterValues[index];
            }
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null)
                throw new ArgumentNullException("command");
            if (commandParameters == null)
                return;
            foreach (SqlParameter sqlParameter in commandParameters)
            {
                if (sqlParameter != null)
                {
                    if ((sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Input) && sqlParameter.Value == null)
                        sqlParameter.Value = (object)DBNull.Value;
                    command.Parameters.Add(sqlParameter);
                }
            }
        }

        public static SqlCommand CreateCommand(SqlConnection connection, string spName, params string[] sourceColumns)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            SqlCommand command = new SqlCommand(spName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (sourceColumns != null && sourceColumns.Length > 0)
            {
                SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connection, spName);
                for (int index = 0; index < sourceColumns.Length; ++index)
                    spParameterSet[index].SourceColumn = sourceColumns[index];
                DataBase.AttachParameters(command, spParameterSet);
            }
            return command;
        }

        public static DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            return DataBase.ExecuteDataset(DataBase.ConnectionString, commandType, commandText, (SqlParameter[])null);
        }

        public static DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return DataBase.ExecuteDataset(DataBase.ConnectionString, commandType, commandText, commandParameters);
        }

        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteDataset(connection, commandType, commandText, (SqlParameter[])null);
        }

        public static DataSet ExecuteDataset(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connection, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteDataset(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteDataset(transaction, commandType, commandText, (SqlParameter[])null);
        }

        public static DataSet ExecuteDataset(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(transaction.Connection, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteDataset(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connectionString, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            SqlCommand sqlCommand = new SqlCommand();
            bool mustCloseConnection = false;
            DataBase.PrepareCommand(sqlCommand, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
            {
                DataSet dataSet = new DataSet();
                ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                sqlCommand.Parameters.Clear();
                if (mustCloseConnection)
                    connection.Close();
                return dataSet;
            }
        }

        public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            SqlCommand sqlCommand = new SqlCommand();
            bool mustCloseConnection = false;
            DataBase.PrepareCommand(sqlCommand, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
            {
                DataSet dataSet = new DataSet();
                ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                sqlCommand.Parameters.Clear();
                return dataSet;
            }
        }

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return DataBase.ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }

        public static DataSet ExecuteDatasetTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connection, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteDataset(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static DataSet ExecuteDatasetTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(transaction.Connection, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static DataSet ExecuteDatasetTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connectionString, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return DataBase.ExecuteNonQuery(DataBase.ConnectionString, commandType, commandText, (SqlParameter[])null);
        }

        public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return DataBase.ExecuteNonQuery(DataBase.ConnectionString, commandType, commandText, commandParameters);
        }

        public static int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            return DataBase.ExecuteNonQuery(CommandType.Text, commandText, commandParameters);
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteNonQuery(connection, commandType, commandText, (SqlParameter[])null);
        }

        public static int ExecuteNonQuery(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connection, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteNonQuery(transaction, commandType, commandText, (SqlParameter[])null);
        }

        public static int ExecuteNonQuery(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(transaction.Connection, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteNonQuery(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connectionString, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            DataBase.PrepareCommand(command, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
            int num = command.ExecuteNonQuery();
            command.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return num;
        }

        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            DataBase.PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            int num = command.ExecuteNonQuery();
            command.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return DataBase.ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        public static int ExecuteNonQueryTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connection, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static int ExecuteNonQueryTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(transaction.Connection, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static int ExecuteNonQueryTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connectionString, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return DataBase.ExecuteReader(DataBase.ConnectionString, commandType, commandText, (SqlParameter[])null);
        }

        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return DataBase.ExecuteReader(DataBase.ConnectionString, commandType, commandText, commandParameters);
        }

        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteReader(connection, commandType, commandText, (SqlParameter[])null);
        }

        public static SqlDataReader ExecuteReader(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteReader(connection, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connection, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteReader(transaction, commandType, commandText, (SqlParameter[])null);
        }

        public static SqlDataReader ExecuteReader(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(transaction.Connection, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteReader(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        public static SqlDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connectionString, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return DataBase.ExecuteReader(connection, (SqlTransaction)null, commandType, commandText, commandParameters, DataBase.SqlConnectionOwnership.External);
        }

        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            else
                return DataBase.ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, DataBase.SqlConnectionOwnership.External);
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            SqlConnection connection = (SqlConnection)null;
            SqlDataReader sqlDataReader;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                sqlDataReader = DataBase.ExecuteReader(connection, (SqlTransaction)null, commandType, commandText, commandParameters, DataBase.SqlConnectionOwnership.Internal);
            }
            catch
            {
                if (connection != null)
                    connection.Close();
                throw;
            }
            return sqlDataReader;
        }

        private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, DataBase.SqlConnectionOwnership connectionOwnership)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            bool mustCloseConnection = false;
            SqlCommand command = new SqlCommand();
            SqlDataReader sqlDataReader1;
            try
            {
                DataBase.PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
                SqlDataReader sqlDataReader2 = connectionOwnership != DataBase.SqlConnectionOwnership.External ? command.ExecuteReader(CommandBehavior.CloseConnection) : command.ExecuteReader(CommandBehavior.CloseConnection);
                bool flag = true;
                foreach (DbParameter dbParameter in (DbParameterCollection)command.Parameters)
                {
                    if (dbParameter.Direction != ParameterDirection.Input)
                        flag = false;
                }
                if (flag)
                    command.Parameters.Clear();
                sqlDataReader1 = sqlDataReader2;
            }
            catch
            {
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
            return sqlDataReader1;
        }

        public static SqlDataReader ExecuteReaderTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteReader(connection, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connection, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static SqlDataReader ExecuteReaderTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(transaction.Connection, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static SqlDataReader ExecuteReaderTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connectionString, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static object ExecuteScalar(string commandText, params SqlParameter[] parameterValues)
        {
            return DataBase.ExecuteScalar(CommandType.Text, commandText, parameterValues);
        }

        public static object ExecuteScalar(CommandType commandType, string commandText)
        {
            return DataBase.ExecuteScalar(DataBase.ConnectionString, commandType, commandText, (SqlParameter[])null);
        }

        public static object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return DataBase.ExecuteScalar(DataBase.ConnectionString, commandType, commandText, commandParameters);
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteScalar(connection, commandType, commandText, (SqlParameter[])null);
        }

        public static object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connection, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteScalar(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteScalar(transaction, commandType, commandText, (SqlParameter[])null);
        }

        public static object ExecuteScalar(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(transaction.Connection, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteScalar(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connectionString, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            DataBase.PrepareCommand(command, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
            object obj = command.ExecuteScalar();
            command.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return obj;
        }

        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            DataBase.PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            object obj = command.ExecuteScalar();
            command.Parameters.Clear();
            return obj;
        }

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return DataBase.ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        public static string ExecuteScalarToStr(CommandType commandType, string commandText)
        {
            object obj = DataBase.ExecuteScalar(DataBase.ConnectionString, commandType, commandText);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }

        public static string ExecuteScalarToStr(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            object obj = DataBase.ExecuteScalar(DataBase.ConnectionString, commandType, commandText, commandParameters);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }

        public static string ExecuteScalarToStr(string connectionString, CommandType commandType, string commandText)
        {
            object obj = DataBase.ExecuteScalar(DataBase.ConnectionString, commandType, commandText);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }

        public static string ExecuteScalarToStr(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            object obj = DataBase.ExecuteScalar(DataBase.ConnectionString, commandType, commandText, commandParameters);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }

        public static object ExecuteScalarTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connection, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteScalar(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static object ExecuteScalarTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(transaction.Connection, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static object ExecuteScalarTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connectionString, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteXmlReader(connection, commandType, commandText, (SqlParameter[])null);
        }

        public static XmlReader ExecuteXmlReader(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connection, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return DataBase.ExecuteXmlReader(transaction, commandType, commandText, (SqlParameter[])null);
        }

        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues == null || parameterValues.Length <= 0)
                return DataBase.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(transaction.Connection, spName);
            DataBase.AssignParameterValues(spParameterSet, parameterValues);
            return DataBase.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            bool mustCloseConnection = false;
            SqlCommand command = new SqlCommand();
            XmlReader xmlReader1;
            try
            {
                DataBase.PrepareCommand(command, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
                XmlReader xmlReader2 = command.ExecuteXmlReader();
                command.Parameters.Clear();
                xmlReader1 = xmlReader2;
            }
            catch
            {
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
            return xmlReader1;
        }

        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            DataBase.PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            XmlReader xmlReader = command.ExecuteXmlReader();
            command.Parameters.Clear();
            return xmlReader;
        }

        public static XmlReader ExecuteXmlReaderTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connection, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static XmlReader ExecuteXmlReaderTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (dataRow == null || dataRow.ItemArray.Length <= 0)
                return DataBase.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(transaction.Connection, spName);
            DataBase.AssignParameterValues(spParameterSet, dataRow);
            return DataBase.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static void FillDataset(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            DataBase.FillDataset(connection, commandType, commandText, dataSet, tableNames, (SqlParameter[])null);
        }

        public static void FillDataset(SqlConnection connection, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(connection, spName);
                DataBase.AssignParameterValues(spParameterSet, parameterValues);
                DataBase.FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, spParameterSet);
            }
            else
                DataBase.FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
        }

        public static void FillDataset(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            DataBase.FillDataset(transaction, commandType, commandText, dataSet, tableNames, (SqlParameter[])null);
        }

        public static void FillDataset(SqlTransaction transaction, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");
            if (spName == null || spName.Length == 0)
                throw new ArgumentNullException("spName");
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] spParameterSet = ParamsCache.GetSpParameterSet(transaction.Connection, spName);
                DataBase.AssignParameterValues(spParameterSet, parameterValues);
                DataBase.FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, spParameterSet);
            }
            else
                DataBase.FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
        }

        public static void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataBase.FillDataset(connection, commandType, commandText, dataSet, tableNames);
            }
        }

        public static void FillDataset(string connectionString, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataBase.FillDataset(connection, spName, dataSet, tableNames, parameterValues);
            }
        }

        public static void FillDataset(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            DataBase.FillDataset(connection, (SqlTransaction)null, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public static void FillDataset(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            DataBase.FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public static void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataBase.FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters);
            }
        }

        private static void FillDataset(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");
            SqlCommand sqlCommand = new SqlCommand();
            bool mustCloseConnection = false;
            DataBase.PrepareCommand(sqlCommand, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
            {
                if (tableNames != null && tableNames.Length > 0)
                {
                    string sourceTable = "Table";
                    for (int index = 0; index < tableNames.Length; ++index)
                    {
                        if (tableNames[index] == null || tableNames[index].Length == 0)
                            throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                        sqlDataAdapter.TableMappings.Add(sourceTable, tableNames[index]);
                        sourceTable = sourceTable + (index + 1).ToString();
                    }
                }
                ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                sqlCommand.Parameters.Clear();
            }
            if (!mustCloseConnection)
                return;
            connection.Close();
        }

        public static SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return DataBase.MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        public static SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return DataBase.MakeParam(ParamName, DbType, Size, ParameterDirection.Output, (object)null);
        }

        public static SqlParameter MakeParam(string ParamName, SqlDbType DbType, int Size, ParameterDirection Direction, object Value)
        {
            SqlParameter sqlParameter = Size <= 0 ? new SqlParameter(ParamName, DbType) : new SqlParameter(ParamName, DbType, Size);
            sqlParameter.Direction = Direction;
            if (Direction != ParameterDirection.Output || Value != null)
                sqlParameter.Value = Value;
            return sqlParameter;
        }

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null)
                throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
                mustCloseConnection = false;
            command.CommandTimeout = 180;
            command.Connection = connection;
            command.CommandText = commandText;
            if (transaction != null)
            {
                if (transaction.Connection == null)
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (commandParameters == null)
                return;
            DataBase.AttachParameters(command, commandParameters);
        }

        public static int RunProc(string procName)
        {
            return DataBase.ExecuteNonQuery(DataBase.ConnectionString, CommandType.StoredProcedure, procName, (SqlParameter[])null);
        }

        public static int RunProc(string procName, SqlParameter[] prams)
        {
            return DataBase.ExecuteNonQuery(DataBase.ConnectionString, CommandType.StoredProcedure, procName, prams);
        }

        public static void RunProc(string procName, out DataSet ds)
        {
            ds = DataBase.ExecuteDataset(DataBase.ConnectionString, CommandType.StoredProcedure, procName, (SqlParameter[])null);
        }

        public static void RunProc(string procName, out SqlDataReader reader)
        {
            reader = DataBase.ExecuteReader(DataBase.ConnectionString, CommandType.StoredProcedure, procName, (SqlParameter[])null);
        }

        public static void RunProc(string procName, out object obj)
        {
            obj = DataBase.ExecuteScalar(DataBase.ConnectionString, CommandType.StoredProcedure, procName, (SqlParameter[])null);
        }

        public static void RunProc(string procName, out XmlReader xmlReader)
        {
            xmlReader = DataBase.ExecuteXmlReader(new SqlConnection(DataBase.ConnectionString), CommandType.StoredProcedure, procName, (SqlParameter[])null);
        }

        public static void RunProc(string procName, SqlParameter[] prams, out DataSet ds)
        {
            ds = DataBase.ExecuteDataset(DataBase.ConnectionString, CommandType.StoredProcedure, procName, prams);
        }

        public static void RunProc(string procName, SqlParameter[] prams, out SqlDataReader reader)
        {
            reader = DataBase.ExecuteReader(DataBase.ConnectionString, CommandType.StoredProcedure, procName, prams);
        }

        public static void RunProc(string procName, SqlParameter[] prams, out object obj)
        {
            obj = DataBase.ExecuteScalar(DataBase.ConnectionString, CommandType.StoredProcedure, procName, prams);
        }

        public static void RunProc(string procName, SqlParameter[] prams, out XmlReader xmlReader)
        {
            xmlReader = DataBase.ExecuteXmlReader(new SqlConnection(DataBase.ConnectionString), CommandType.StoredProcedure, procName, prams);
        }

        public static bool TableExists(string tablename)
        {
            if (HttpContext.Current.Cache[tablename] != null)
                return true;
            bool flag = false;
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "SELECT * FROM SysObjects WHERE xtype='U' AND Name='" + tablename + "'");
            if (sqlDataReader.Read())
                flag = true;
            sqlDataReader.Close();
            HttpContext.Current.Cache[tablename] = (object)"";
            return flag;
        }

        public static void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet, string tableName)
        {
            if (insertCommand == null)
                throw new ArgumentNullException("insertCommand");
            if (deleteCommand == null)
                throw new ArgumentNullException("deleteCommand");
            if (updateCommand == null)
                throw new ArgumentNullException("updateCommand");
            if (tableName == null || tableName.Length == 0)
                throw new ArgumentNullException("tableName");
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            {
                sqlDataAdapter.UpdateCommand = updateCommand;
                sqlDataAdapter.InsertCommand = insertCommand;
                sqlDataAdapter.DeleteCommand = deleteCommand;
                sqlDataAdapter.Update(dataSet, tableName);
                dataSet.AcceptChanges();
            }
        }

        private static void AddParameters(SqlCommand command, IDictionary<string, object> parameters)
        {
            command.Parameters.Clear();
            foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>)parameters)
            {
                string parameterName = keyValuePair.Key;
                if (!keyValuePair.Key.StartsWith("@"))
                    parameterName = "@" + parameterName;
                command.Parameters.Add(new SqlParameter(parameterName, keyValuePair.Value));
            }
        }

        public static SqlDependency AddSqlDependency(string cacheKey, string strSql, IDictionary<string, object> parameters)
        {
            try
            {
                string connectionString = DataBase.ConnectionString;
                SqlDependency.Start(connectionString);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(strSql, connection);
                    if (parameters != null)
                        DataBase.AddParameters(command, parameters);
                    SqlDependency sqlDependency = new SqlDependency(command);
                    sqlDependency.OnChange += (OnChangeEventHandler)((sender, e) =>
                    {
                        if (e.Info != SqlNotificationInfo.Invalid)
                            ;
                        WebCache.GetCacheService().RemoveObject(cacheKey);
                    });
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    command.ExecuteNonQuery();
                    return sqlDependency;
                }
            }
            catch (SqlException ex)
            {
                ExceptionHandler.HandleException((Exception)ex);
                return (SqlDependency)null;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (SqlDependency)null;
            }
        }

        public static SqlDependency AddSqlDependency(string cacheKey, string dbOwner, string tableName, string column, string where, IDictionary<string, object> parameters)
        {
            string strSql = string.Format("select {0} from {1}.{2}", (object)column, (object)dbOwner, (object)tableName);
            if (!string.IsNullOrEmpty(where))
                strSql = strSql + " where " + where;
            return DataBase.AddSqlDependency(cacheKey, strSql, parameters);
        }

        public static SqlDependency AddSqlDependency(string cacheKey, string tableName, string column, string where, IDictionary<string, object> parameters)
        {
            string strSql = string.Format("select {0} from {1}.{2}", (object)column, (object)"dbo", (object)tableName);
            if (!string.IsNullOrEmpty(where))
                strSql = strSql + " where " + where;
            return DataBase.AddSqlDependency(cacheKey, strSql, parameters);
        }

        private enum SqlConnectionOwnership
        {
            Internal = 1,
            External = 2,
        }
    }
}
