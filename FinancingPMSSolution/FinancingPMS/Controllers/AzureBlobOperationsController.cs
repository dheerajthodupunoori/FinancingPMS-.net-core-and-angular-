using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancingPMS.Config;
using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancingPMS.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AzureBlobOperationsController : Controller
    {

        private IConfiguration _configuration;

        private IAzureOperations _azureOperations;

        private IOptions<AzureAadhaarBlobConfig> _azureBlobConfig;



        public AzureBlobOperationsController(IConfiguration configuration,IAzureOperations azureOperations ,IOptions<AzureAadhaarBlobConfig> azureBlobConfig)
        {
            _configuration = configuration;
            _azureOperations = azureOperations;
            _azureBlobConfig = azureBlobConfig;
        }

        [Route("UploadAadhaarImageToAzureBlobContainer")]
        [HttpPost]
        public async Task<bool> InsertFile(string customerID)
        {
            var file = Request.Form.Files[0];
            try
            {
                if (CloudStorageAccount.TryParse(_azureBlobConfig.Value.ConnectionString, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer container = blobClient.GetContainerReference(_azureBlobConfig.Value.FinancingAadhaarContainerName);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(GenerateUniqueAadhaarImageName(customerID));


                    await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        private string GenerateUniqueAadhaarImageName(string customerID)
        {
            return !string.IsNullOrEmpty(customerID) ? $"{customerID}_aadhaar.png" : string.Empty;
        }
    }
}
