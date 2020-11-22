using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Config
{
    public   class AzureConfig
    {
        public string KeyVaultName { get; set; }

        public string AzureSQLDatabaseSecretName { get; set; }

        public string AzureSQLDataBaseConnectionStringSecretVersion { get; set; }
    }
}
