using System.Configuration;

namespace Kerry.Sync.Utility.DB
{
    public class K35DEVDBFactory : DBFactory
    {
        public K35DEVDBFactory():base()
        {
            dbProviderName = ConfigurationManager.AppSettings["K35DataProvider"];
            dbConnectionString = ConfigurationManager.ConnectionStrings["K35DEVEntitiesADO"].ToString();
            this.connection = CreateConnection(dbProviderName, dbConnectionString);
        }
    }
}
