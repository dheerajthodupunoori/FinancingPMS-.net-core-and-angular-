using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Interfaces
{
    public  interface IAzureOperations
    {
       public  string GetConnectionStringFromAzureKeyVault(string keyVaultName , string secretName , string version = "");
    }
}
