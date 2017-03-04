using System.Configuration;

namespace Kerry.Sync.Utility.DB
{
    public  class K35DBFactory:DBFactory
    {
        //private static string dbProviderName = ConfigurationManager.AppSettings["K35DataProvider"];
        //private static string dbConnectionString = ConfigurationManager.AppSettings["K35EntitiesADO"];

        public K35DBFactory():base()
        {
            dbProviderName = ConfigurationManager.AppSettings["K35DataProvider"];
            dbConnectionString = ConfigurationManager.ConnectionStrings["K35EntitiesADO"].ToString();

            this.connection = CreateConnection(dbProviderName, dbConnectionString);

        }
    }
}
