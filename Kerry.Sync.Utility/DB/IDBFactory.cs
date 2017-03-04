using Kerry.Sync.Utility.DB;
using System.Data;
using System.Data.Common;

namespace Kerry.Sync.Utility
{
    public interface IDBFactory
    {
        void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value);
        void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size);
        void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection);
        void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType);
        DataSet ExecuteDataSet(DbCommand cmd);
        DataSet ExecuteDataSet(string sql);
        DataSet ExecuteDataSet(DbCommand cmd, Transaction t);
        DataTable ExecuteDataTable(string sql);
        DataTable ExecuteDataTable(DbCommand cmd);
        DataTable ExecuteDataTable(DbCommand cmd, Transaction t);
        int ExecuteNonQuery(DbCommand cmd);
        int ExecuteNonQuery(string sql);
        int ExecuteNonQuery(DbCommand cmd, Transaction t);
        DbDataReader ExecuteReader(DbCommand cmd);
        DbDataReader ExecuteReader(DbCommand cmd, Transaction t);
        object ExecuteScalar(DbCommand cmd);
        object ExecuteScalar(DbCommand cmd, Transaction t);
        DbParameter GetParameter(DbCommand cmd, string parameterName);
        DbCommand GetSqlStringCommond(string sqlQuery);
        DbCommand GetStoredProcCommond(string storedProcedure);
    }
}