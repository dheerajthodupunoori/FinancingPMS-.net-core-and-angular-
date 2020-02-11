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
        public string GetConnectionStringFromAzureKeyVault(string keyVaultName , string secretName)
        {
            string connectionString = string.Empty;

            try
            {
                var keyVaultUri = "https://" + keyVaultName + ".vault.azure.net";
                var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
                KeyVaultSecret secret = client.GetSecret(secretName);
                connectionString = secret.Value;
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
            return connectionString;
        }
    }
}
