using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Sync.Utility.DB
{
   public class K5UATDBFactory : DBFactory
    {
        public K5UATDBFactory():base()
        {
            dbProviderName = ConfigurationManager.AppSettings["K3DataProvider"];
            dbConnectionString = ConfigurationManager.ConnectionStrings["K5UATEntitiesADO"].ToString();

            this.connection = CreateConnection(dbProviderName, dbConnectionString);

        }
    }
}
