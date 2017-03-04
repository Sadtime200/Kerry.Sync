using System.Configuration;

namespace Kerry.Sync.Utility.DB
{
    public class K35DESTDB : DBFactory
    {
        public K35DESTDB():base()
        {
            dbProviderName = ConfigurationManager.AppSettings["K35DataProvider"];
            dbConnectionString = ConfigurationManager.ConnectionStrings["K35DESTEntitiesADO"].ToString();

            this.connection = CreateConnection(dbProviderName, dbConnectionString);

        }
    }
}
