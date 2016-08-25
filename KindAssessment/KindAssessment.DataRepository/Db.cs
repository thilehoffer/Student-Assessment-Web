using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.IO;
 


namespace AssessmentApp.Data
{
    internal class Db : IDisposable
    {
        //Implement IDisposable.
        public void Dispose()
        {
            _connString = string.Empty;
            GC.SuppressFinalize(this);
        }

        public int CommandTimeout { get; set; }

        #region private methods and properties

        private string _connString = string.Empty;

        #endregion

        #region Constructor

        public Db(string sqlConnectionString)
        {
            _connString = sqlConnectionString;
            CommandTimeout = 60;
        }

        #endregion

        #region Call Stored Procs

        public int CallProcWithReturnValue(string procName)
        {
            int i;
            using (var cmd = new SqlCommand(procName))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var conn = new SqlConnection(_connString))
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = CommandTimeout;
                    var prmReturn = new SqlParameter("@ReturnValue", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                    cmd.Parameters.Add(prmReturn);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    i = (int)prmReturn.Value;
                    cmd.Parameters.Clear();
                }
            }
            return i;
        }

        public int CallProcWithReturnValue(string procName, SqlParameter[] parameters)
        {
            int i;
            using (var cmd = new SqlCommand(procName))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var conn = new SqlConnection(_connString))
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = CommandTimeout;
                    foreach (SqlParameter prm in parameters)
                    {
                        cmd.Parameters.Add(prm);
                    }
                    var prmReturn = new SqlParameter("@ReturnValue", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                    cmd.Parameters.Add(prmReturn);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    i = (int)prmReturn.Value;
                    cmd.Parameters.Clear();
                }
            }
            return i;
        }

        public object CallProcWithReturnScalar(string procName)
        {
            object obj;
            using (var cmd = new SqlCommand(procName))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                using (var conn = new SqlConnection(_connString))
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = CommandTimeout;
                    var prmReturn = new SqlParameter("@ReturnValue", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                    cmd.Parameters.Add(prmReturn);
                    cmd.Connection.Open();
                    obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
            }
            return obj;
        }

        public object CallProcWithReturnScalar(string procName, SqlParameter[] parameters)
        {
            object obj;
            using (var cmd = new SqlCommand(procName))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = CommandTimeout;
                using (var conn = new SqlConnection(_connString))
                {
                    cmd.Connection = conn;
                    foreach (var prm in parameters)
                    {
                        cmd.Parameters.Add(prm);
                    }
                    var prmReturn = new SqlParameter("@ReturnValue", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                    cmd.Parameters.Add(prmReturn);
                    cmd.Connection.Open();
                    obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
            }
            return obj;
        }

        public void CallProc(string procName, SqlParameter[] parameters)
        {
            using (var cmd = new SqlCommand(procName))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var conn = new SqlConnection(_connString))
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = CommandTimeout;
                    foreach (var prm in parameters)
                    {
                        cmd.Parameters.Add(prm);
                    }
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
        }

        public void CallProc(string procName)
        {
            using (var cmd = new SqlCommand(procName))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var conn = new SqlConnection(_connString))
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = CommandTimeout;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallProcsWithTransaction(IEnumerable<SqlTransactionItem> sqlTransactionItems, Action<SqlCommand> commandStart = null, Action<SqlCommand> commandEnd = null)
        {
            using (var conn = new SqlConnection(_connString))
            {
                var commands = new List<SqlCommand>();
                foreach (var item in sqlTransactionItems)
                {
                    var cmd = new SqlCommand
                    {
                        CommandText = item.StoredProcName,
                        CommandTimeout = CommandTimeout,
                        CommandType = CommandType.StoredProcedure,
                        Connection = conn
                    };
                    foreach (var prm in item.SqlParameters)
                    {
                        cmd.Parameters.Add(prm);
                    }
                    commands.Add(cmd);
                }
                conn.Open();
                var trans = conn.BeginTransaction();
                try
                {
                    foreach (var cmd in commands)
                    {
                        if (commandStart != null)
                            commandStart(cmd);

                        cmd.Transaction = trans;
                        cmd.ExecuteNonQuery();

                        if (commandEnd != null)
                            commandEnd(cmd);
                    }
                    trans.Commit();
                }
                catch (SqlException sqlError)
                {
                    trans.Rollback();
                    throw sqlError;
                }

            }
        }

        public void CallProcsWithTransactionWithReturn(IEnumerable<SqlTransactionItem> sqlTransactionItems, Action<SqlCommand> commandStart = null, Func<SqlCommand, Boolean> commandEndReturnTrueToContinue = null)
        {
            using (var conn = new SqlConnection(_connString))
            {
                var commands = new List<SqlCommand>();
                foreach (var item in sqlTransactionItems)
                {
                    var cmd = new SqlCommand
                    {
                        CommandText = item.StoredProcName,
                        CommandTimeout = CommandTimeout,
                        CommandType = CommandType.StoredProcedure,
                        Connection = conn
                    };
                    foreach (var prm in item.SqlParameters)
                    {
                        cmd.Parameters.Add(prm);
                    }
                    commands.Add(cmd);
                }
                conn.Open();
                var trans = conn.BeginTransaction();
                try
                {
                    foreach (var cmd in commands)
                    {
                        if (commandStart != null)
                            commandStart(cmd);

                        cmd.Transaction = trans;
                        cmd.ExecuteNonQuery();

                        if (commandEndReturnTrueToContinue != null)
                            if (!commandEndReturnTrueToContinue(cmd))//true to continue the following commands, false to break
                            {
                                break;
                            }
                    }
                    trans.Commit();
                }
                catch (SqlException sqlError)
                {
                    trans.Rollback();
                    throw sqlError;
                }

            }
        }
        #endregion

        #region DataReader

        public void UseDataReader(string sqlProcName, Action<IDataReader> handler)
        {
            using (var conn = new SqlConnection(_connString))
            {
                using (var cmd = new SqlCommand(sqlProcName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = CommandTimeout;
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        handler(dr);
                    }
                }
            }
        }

        public void UseDataReader(string sqlProcName, SqlParameter[] parameters, Action<IDataReader> handler)
        {
            using (var conn = new SqlConnection(_connString))
            {
                using (var cmd = new SqlCommand(sqlProcName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = CommandTimeout;
                    foreach (SqlParameter prm in parameters)
                    {
                        cmd.Parameters.Add(prm);
                    }
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        handler(dr);
                    }
                    cmd.Parameters.Clear();
                }
            }
        }

        public SqlDataReader GetOpenReader(string sqlProcName, out SqlConnection conn)
        {
            conn = new SqlConnection(_connString);
            var cmd = new SqlCommand(sqlProcName, conn) { CommandType = CommandType.StoredProcedure, CommandTimeout = CommandTimeout };
            conn.Open();

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader GetOpenReader(string sqlProcName, SqlParameter[] parameters, out SqlConnection conn)
        {
            conn = new SqlConnection(_connString);
            var cmd = new SqlCommand(sqlProcName, conn) { CommandType = CommandType.StoredProcedure, CommandTimeout = CommandTimeout };
            foreach (SqlParameter prm in parameters)
            {
                cmd.Parameters.Add(prm);
            }
            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        #endregion

        #region DataSets and DataTables

        public DataTable GetDataTable(string procName, SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using (var cmd = new SqlCommand(procName))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var conn = new SqlConnection(_connString))
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = CommandTimeout;
                    foreach (var prm in parameters)
                    {
                        cmd.Parameters.Add(prm);
                    }
                    var da = new SqlDataAdapter(cmd);
                    cmd.Connection.Open();
                    da.Fill(dt);
                    cmd.Parameters.Clear();
                }
            }
            return dt;
        }

        public DataTable GetDataTable(string procName)
        {
            var dt = new DataTable();
            using (var cmd = new SqlCommand(procName))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var conn = new SqlConnection(_connString))
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = CommandTimeout;
                    var da = new SqlDataAdapter(cmd);
                    cmd.Connection.Open();
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataSet GetDataSet(string procName, SqlParameter[] parameters)
        {
            var dst = new DataSet();
            using (var cmd = new SqlCommand(procName))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var conn = new SqlConnection(_connString))
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = CommandTimeout;
                    foreach (var prm in parameters)
                    {
                        cmd.Parameters.Add(prm);
                    }
                    var da = new SqlDataAdapter(cmd);
                    cmd.Connection.Open();
                    da.Fill(dst);
                    cmd.Parameters.Clear();
                }
            }
            return dst;
        }

        public DataSet GetDataSet(string procName)
        {
            var dst = new DataSet();
            using (var cmd = new SqlCommand(procName))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var conn = new SqlConnection(_connString))
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = CommandTimeout;
                    var da = new SqlDataAdapter(cmd);
                    cmd.Connection.Open();
                    da.Fill(dst);
                }
            }
            return dst;
        }

        public void BulkInsert(DataTable dt, string tableName)
        {
            using (var conn = new SqlConnection(_connString))
            {
                using (var bulkCopy = new SqlBulkCopy(conn))
                {
                    conn.Open();
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BulkCopyTimeout = 1200;
                    bulkCopy.WriteToServer(dt);
                }
            }
        }

        #endregion

    }

    internal class SqlTransactionItem
    {
        public string StoredProcName { get; set; }
        public SqlParameter[] SqlParameters { get; set; }

    }

    internal static class DataReaderExtensions
    {
        internal static string GetString(this IDataReader reader, string column) => reader[column] == DBNull.Value ? string.Empty : reader[column] as string;
        internal static int GetInt(this IDataReader reader, string column) => (int)reader[column];
        internal static decimal GetDecimal(this IDataReader reader, string column) => (decimal)reader[column];
        internal static DateTime GetDateTime(this IDataReader reader, string column) => (DateTime)reader[column];
        internal static Guid GetGuid(this IDataReader reader, string column) => (Guid)reader[column];
        internal static bool GetBool(this IDataReader reader, string column) => (bool)reader[column];
        internal static int? GetNullableInt(this IDataReader reader, string column) => (reader[column] == DBNull.Value) ? null : reader[column] as int?;

        internal static decimal? GetNullableDecimal(this IDataReader reader, string column) => (reader[column] == DBNull.Value) ? null :reader[column] as decimal?;

        internal static Guid? GetNullableGuid(this IDataReader reader, string column) =>  (reader[column] == DBNull.Value) ? null : reader[column] as Guid?;

        internal static DateTime? GetNullableDateTime(this IDataReader reader, string column) =>(reader[column] == DBNull.Value) ? null : reader[column] as DateTime?;

        internal static bool? GetNullableBool(this IDataReader reader, string column)=> (reader[column] == DBNull.Value) ? null : reader[column] as bool?;
       
    }
}
