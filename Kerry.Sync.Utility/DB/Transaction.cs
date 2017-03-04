using System.Data.Common;

namespace Kerry.Sync.Utility.DB
{
    public class Transaction
    {
        private DbConnection conn;
        private DbTransaction dbTrans;
        public DbConnection DbConnection
        {
            get { return this.conn; }
        }
        public DbTransaction DbTrans
        {
            get { return this.dbTrans; }
        }

        public Transaction()
        {
            var dbFactory = new DBFactory();
            conn = dbFactory.CreateConnection();
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        public Transaction(string connectionString)
        {
            var dbFactory = new DBFactory();
            conn = dbFactory.CreateConnection(connectionString);
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        public void Commit()
        {
            dbTrans.Commit();
            this.Colse();
        }

        public void RollBack()
        {
            dbTrans.Rollback();
            this.Colse();
        }

        public void Dispose()
        {
            this.Colse();
        }

        public void Colse()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
