using System.Data;
using System.Data.Common;
using Kerry.Sync.Utility.DB;

namespace Kerry.Sync.Utility
{
    public class DBFactory : IDBFactory
    {

        public static string dbProviderName { get; set; }
        public static string dbConnectionString { get; set; }


        protected DbConnection connection;
        public DBFactory()
        {
        }
        public DBFactory(string connectionString)
        {
            this.connection = CreateConnection(connectionString);
        }
        public DBFactory(string provider, string connectionString)
        {
            this.connection = CreateConnection(connectionString);
        }
        public DbConnection CreateConnection()
        {
            DbProviderFactory dbfactory = GetDataProvider();
            DbConnection dbconn = dbfactory.CreateConnection();
            dbconn.ConnectionString = dbConnectionString;
            return dbconn;
        }

        private DbProviderFactory GetDataProvider()
        {
            return DbProviderFactories.GetFactory(dbProviderName);
        }

        public DbConnection CreateConnection(string connectionString)
        {
            DbProviderFactory dbfactory = GetDataProvider();
            DbConnection dbconn = dbfactory.CreateConnection();
            dbconn.ConnectionString = connectionString;
            return dbconn;
        }
        public DbConnection CreateConnection(string dbProvider, string connectionString)
        {
            DbProviderFactory dbfactory = GetDataProvider();
            DbConnection dbconn = dbfactory.CreateConnection();
            dbconn.ConnectionString = connectionString;
            return dbconn;
        }
        public DbCommand GetStoredProcCommond(string storedProcedure)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = storedProcedure;
            dbCommand.CommandType = CommandType.StoredProcedure;
            return dbCommand;
        }
        public DbCommand GetSqlStringCommond(string sqlQuery)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.CommandType = CommandType.Text;
            return dbCommand;
        }

        #region Add Parameter
        public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }
        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }
        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }
        public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(dbParameter);
        }
        public DbParameter GetParameter(DbCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }

        #endregion

        #region Execute
        public DataSet ExecuteDataSet(DbCommand cmd)
        {
            //temporary set connection time out 6000 seconds
            cmd.CommandTimeout = 6000;
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DBFactory.dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }
        public DataSet ExecuteDataSet(string sql)
        {
            //temporary set connection time out 6000 seconds
            this.GetSqlStringCommond(sql).CommandTimeout = 6000;
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DBFactory.dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = this.GetSqlStringCommond(sql);
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }
        public DataTable ExecuteDataTable(DbCommand cmd)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DBFactory.dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable ExecuteDataTable(string sql)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DBFactory.dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = this.GetSqlStringCommond(sql);
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            cmd.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        public DbDataReader ExecuteReader(string sql)
        {
            this.GetSqlStringCommond(sql).Connection.Open();
            DbDataReader reader = this.GetSqlStringCommond(sql).ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        public int ExecuteNonQuery(DbCommand cmd)
        {
            cmd.Connection.Open();
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;
        }
        public int ExecuteNonQuery(string sql)
        {
            this.GetSqlStringCommond(sql).Connection.Open();
            int ret = this.GetSqlStringCommond(sql).ExecuteNonQuery();
            this.GetSqlStringCommond(sql).Connection.Close();
            return ret;
        }
        public object ExecuteScalar(DbCommand cmd)
        {
            cmd.Connection.Open();
            object ret = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return ret;
        }
        #endregion

        #region Execute transaction
        public DataSet ExecuteDataSet(DbCommand cmd, Transaction t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }

        public DataTable ExecuteDataTable(DbCommand cmd, Transaction t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DbDataReader ExecuteReader(DbCommand cmd, Transaction t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            return reader;
        }
        public int ExecuteNonQuery(DbCommand cmd, Transaction t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            int ret = cmd.ExecuteNonQuery();
            return ret;
        }

        public object ExecuteScalar(DbCommand cmd, Transaction t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            object ret = cmd.ExecuteScalar();
            return ret;
        }
        #endregion
    }


}
