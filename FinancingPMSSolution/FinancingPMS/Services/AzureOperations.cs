using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using FinancingPMS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Services
{
    public  class AzureOperations : IAzureOperations
    {

        public AzureOperations()
        {

        }

        public string GetConnectionStringFromAzureKeyVault(string keyVaultName , string secretName , string version = "")
        {
            string connectionString;
            try
            {
                var keyVaultUri = "https://" + keyVaultName + ".vault.azure.net";
                var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
                KeyVaultSecret secret = client.GetSecret(secretName , version);
                connectionString = secret.Value;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return connectionString;
        }
    }
}
