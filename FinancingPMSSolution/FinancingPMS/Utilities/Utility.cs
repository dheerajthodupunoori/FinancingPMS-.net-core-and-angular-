using FinancingPMS.Config;
using FinancingPMS.Enums;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;
using System;
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
                BlobType.PASSPORTPHOTO => cloudBlobClient.GetContainerReference(_azureBlobConfig.Value.FinancingPANContainerName),
                BlobType.Signature => cloudBlobClient.GetContainerReference(_azureBlobConfig.Value.FinancingSignatureContainerName),
                _ => throw new Exception("Invalid BlobType"),
            };
        }

        public static string GenerateUniqueAadhaarImageName(string customerID, BlobType type)
        {
            return type switch
            {
                BlobType.Aadhaar => !string.IsNullOrEmpty(customerID) ? $"{customerID}_aadhaar.png" : string.Empty,
                BlobType.PASSPORTPHOTO => !string.IsNullOrEmpty(customerID) ? $"{customerID}_pan.png" : string.Empty,
                BlobType.Signature => !string.IsNullOrEmpty(customerID) ? $"{customerID}_signature.png" : string.Empty,
                _ => throw new Exception("Invalid BlobType"),
            };
        }

        public static BlobType GetBlobType(string type)
        {
            return type switch
            {
                "Signature" => BlobType.Signature,
                "Aadhaar" => BlobType.Aadhaar,
                "PASSPORTPHOTO" => BlobType.PASSPORTPHOTO,
                _ => throw new Exception("Invalid BlobType"),
            };
        }
    }
}
