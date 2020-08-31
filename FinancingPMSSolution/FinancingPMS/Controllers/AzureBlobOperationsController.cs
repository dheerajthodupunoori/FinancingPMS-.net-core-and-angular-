using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancingPMS.Config;
using FinancingPMS.Enums;
using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using FinancingPMS.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BlobType = FinancingPMS.Enums.BlobType;

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
        public async Task<bool> InsertFile(string customerID, BlobType type = BlobType.Aadhaar)
        {
            var file = Request.Form.Files[0];
            try
            {
                if (CloudStorageAccount.TryParse(_azureBlobConfig.Value.ConnectionString, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer container = BlobUtilities.GetBlobContainer(blobClient, type , _azureBlobConfig);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(BlobUtilities.GenerateUniqueAadhaarImageName(customerID, type));

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
    }
}
