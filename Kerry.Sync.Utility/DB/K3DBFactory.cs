using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Sync.Utility.DB
{
    public class K3DBFactory : DBFactory
    {
        public K3DBFactory()
        {
            DBFactory.dbProviderName = ConfigurationManager.AppSettings["K3DataProvider"];
            DBFactory.dbConnectionString = ConfigurationManager.ConnectionStrings["K3EntitiesADO"].ToString();
            //DBFactory.dbConnectionString = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST = 172.18.8.168)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = k3prod_srv))); User Id = KEWILLFWD; Password = fu6rufra";
            this.connection = CreateConnection(dbProviderName, dbConnectionString);

        }
    }
}
