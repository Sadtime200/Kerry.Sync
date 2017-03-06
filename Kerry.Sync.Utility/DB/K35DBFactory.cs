using System.Configuration;

namespace Kerry.Sync.Utility.DB
{
    public  class K35DBFactory:DBFactory
    {
        public K35DBFactory():base()
        {
            dbProviderName = ConfigurationManager.AppSettings["K35DataProvider"];
            dbConnectionString = ConfigurationManager.ConnectionStrings["K35EntitiesADO"].ToString();

            this.connection = CreateConnection(dbProviderName, dbConnectionString);

        }
    }
}
