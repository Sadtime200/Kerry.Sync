using System.Configuration;

namespace Kerry.Sync.Utility.DB
{
    public class K3DBFactory : DBFactory
    {
        public K3DBFactory()
        {
            DBFactory.dbProviderName = ConfigurationManager.AppSettings["K3DataProvider"];
            DBFactory.dbConnectionString = ConfigurationManager.ConnectionStrings["K3EntitiesADO"].ToString();
            this.connection = CreateConnection(dbProviderName, dbConnectionString);

        }
    }
}
