using FinancingPMS.Config;
using FinancingPMS.Enums;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlobType = FinancingPMS.Enums.BlobType;

namespace FinancingPMS.Utilities
{
    public static class BlobUtilities
    {
        public static CloudBlobContainer GetBlobContainer(CloudBlobClient cloudBlobClient, BlobType type , IOptions<AzureAadhaarBlobConfig> _azureBlobConfig)
        {
            return type switch
            {
                BlobType.Aadhaar => cloudBlobClient.GetContainerReference(_azureBlobConfig.Value.FinancingAadhaarContainerName),
                BlobType.PAN => cloudBlobClient.GetContainerReference(_azureBlobConfig.Value.FinancingPANContainerName),
                BlobType.Signature => cloudBlobClient.GetContainerReference(_azureBlobConfig.Value.FinancingSignatureContainerName),
                _ => throw new Exception("Invalid BlobType"),
            };
        }

        public static string GenerateUniqueAadhaarImageName(string customerID, BlobType type)
        {
            return type switch
            {
                BlobType.Aadhaar => !string.IsNullOrEmpty(customerID) ? $"{customerID}_aadhaar.png" : string.Empty,
                BlobType.PAN => !string.IsNullOrEmpty(customerID) ? $"{customerID}_pan.png" : string.Empty,
                BlobType.Signature => !string.IsNullOrEmpty(customerID) ? $"{customerID}_signature.png" : string.Empty,
                _ => throw new Exception("Invalid BlobType"),
            };
        }
    }
}
