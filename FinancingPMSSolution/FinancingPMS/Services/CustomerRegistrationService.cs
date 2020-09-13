using Azure.Core;
using FinancingPMS.Config;
using FinancingPMS.Enums;
using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using FinancingPMS.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BlobType = FinancingPMS.Enums.BlobType;

namespace FinancingPMS.Services
{
    public class CustomerRegistrationService : ICustomerRegistrationService
    {


        public IConfiguration _configuration;

        private string connectionString = string.Empty;

        private SqlConnection _connection;

        private IOptions<AzureAadhaarBlobConfig> _azureBlobConfig;

        private const string CUSTOMERIDPREFIX = "FRM";

        private IFirmService _firmService;

        private IHttpClientFactory _httpClientFactory;



        public CustomerRegistrationService(IConfiguration configuration, IOptions<AzureAadhaarBlobConfig> azureBlobConfig , IFirmService firmService, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;

            connectionString = _configuration.GetConnectionString("DefaultConnection");

            _connection = new SqlConnection(connectionString);

            _azureBlobConfig = azureBlobConfig;

            _firmService = firmService;

            _httpClientFactory = httpClientFactory;
        }



        public string PerformCustomerRegistration(CustomerLoginDetails customerLoginDetails)
        {
            string customerID = GenerateCustomerID(customerLoginDetails);

            try
            {
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.CommandText = "spInsertIntoCustomerLoginDetails";
                    sqlcommand.Connection = _connection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;


                    sqlcommand.Parameters.AddWithValue("CustomerID", customerID);
                    sqlcommand.Parameters.AddWithValue("FirmID", customerLoginDetails.FirmID);
                    sqlcommand.Parameters.AddWithValue("CustomerStatus", customerLoginDetails.CustomerRegistrationValidationStatus);
                    sqlcommand.Parameters.AddWithValue("FirstName", customerLoginDetails.FirstName);
                    sqlcommand.Parameters.AddWithValue("LastName", customerLoginDetails.LastName);
                    sqlcommand.Parameters.AddWithValue("FatherName", customerLoginDetails.FatherName);
                    sqlcommand.Parameters.AddWithValue("DOB", customerLoginDetails.DOB);
                    sqlcommand.Parameters.AddWithValue("AadhaarNumber", customerLoginDetails.AadhaarNumber);
                    sqlcommand.Parameters.AddWithValue("Password", customerLoginDetails.Password);
                    sqlcommand.Parameters.AddWithValue("EmailID", customerLoginDetails.EmailID);


                    if (_connection.State == System.Data.ConnectionState.Closed)
                    {
                        _connection.Open();
                    }


                    if (IsCustomerAlreadyRegistered(customerID))
                    {
                        throw new Exception($"You are already registered to firm {customerLoginDetails.FirmID} and your CustomerID id is {customerID}");
                    }
                    else
                    {
                        int rowsAffected = sqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
            return customerID;
        }



        private string GenerateCustomerID(CustomerLoginDetails customerLoginDetails)
        {
            string customerID = string.Empty;
            try
            {
                using (SqlCommand command = new SqlCommand("spGetNextCustomerID", _connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("FirmID", customerLoginDetails.FirmID);

                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            customerID = reader.GetString(0);
                        }
                    }
                }
            }
            catch (SqlNullValueException ex)
            {
                var firm = _firmService.GetFirmDetails(customerLoginDetails.FirmID);
                throw new SqlNullValueException($"Number of customers to the Firm {customerLoginDetails.FirmID} reached maximum . Please contact Firm Owner. Phone Number : { firm.PhoneNumber } . EmailID : {firm.Email}" , ex);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
            return customerID.Trim();
        }


        private bool IsCustomerAlreadyRegistered(string customerID)
        {
            bool isCustomerAlreadyRegistered = false;
            SqlDataReader reader = null;

            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = _connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"SELECT * from CustomerLoginDetails  WHERE CustomerID='{customerID}'";

                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        isCustomerAlreadyRegistered = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //_connection.Close();
                reader.Close();
            }
            return isCustomerAlreadyRegistered;
        }

        public async void SaveCustomerAdditionalDetails(CustomerAdditionalDetails customerAdditionalDetails)
        {
            SqlTransaction transaction = null;
            try
            {
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.CommandText = "spInsertIntoCustomerDetails";
                    sqlcommand.Connection = _connection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;

                    sqlcommand.Parameters.AddWithValue("CustomerID", customerAdditionalDetails.customerID);
                    sqlcommand.Parameters.AddWithValue("Occupation", customerAdditionalDetails.occupation);
                    sqlcommand.Parameters.AddWithValue("Phone", customerAdditionalDetails.phone);
                    sqlcommand.Parameters.AddWithValue("State", customerAdditionalDetails.state);
                    sqlcommand.Parameters.AddWithValue("City", customerAdditionalDetails.city);
                    sqlcommand.Parameters.AddWithValue("Zip", customerAdditionalDetails.zip);
                    sqlcommand.Parameters.AddWithValue("FlatNumber", customerAdditionalDetails.flatNumber);
                    sqlcommand.Parameters.AddWithValue("Street", customerAdditionalDetails.street);
                    sqlcommand.Parameters.AddWithValue("Country", customerAdditionalDetails.country);
                    sqlcommand.Parameters.AddWithValue("Income", customerAdditionalDetails.income);


                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    transaction = _connection.BeginTransaction();
                    sqlcommand.Transaction = transaction;

                    int rowsaffected = sqlcommand.ExecuteNonQuery();

                    if (rowsaffected > 0)
                    {
                        //var uri = "http://localhost:5000/api/AzureBlobOperations​/" + "UploadAadhaarImageToAzureBlobContainer";
                        var client = _httpClientFactory.CreateClient();

                        if (customerAdditionalDetails.signature.Length>0)
                        {
                            using(MemoryStream fs = new MemoryStream())
                            {
                               await customerAdditionalDetails.signature.CopyToAsync(fs);
                                var byteData = fs.ToArray();
                                var byteContent = new ByteArrayContent(byteData);
                                var content = new MultipartFormDataContent();
                                content.Add(byteContent, "signature");
                                content.Add(new StringContent(customerAdditionalDetails.customerID), "customerID");
                                content.Add(new StringContent(BlobType.Signature.ToString()), "type");
                                var response = await client.PostAsync("http://localhost:5000/api/AzureBlobOperations/UploadAadhaarImageToAzureBlobContainer", content);
                            }
                        }

                        //if (customerAdditionalDetails.signature != null)
                        //{
                        //    var result = await SaveToAzureBlob(new BlobDetails()
                        //    {
                        //        BlobFile = customerAdditionalDetails.signature,
                        //        CustomerID = customerAdditionalDetails.customerID,
                        //        Type = BlobType.Signature
                        //    });

                        //    if (result && customerAdditionalDetails.passport != null)
                        //    {
                        //        var result1 = await SaveToAzureBlob(new BlobDetails()
                        //        {
                        //            BlobFile = customerAdditionalDetails.passport,
                        //            CustomerID = customerAdditionalDetails.customerID,
                        //            Type = BlobType.PASSPORTPHOTO
                        //        });

                        //        if (!result1)
                        //        {
                        //            throw new Exception("SaveToBlob failed");
                        //        }
                        //    }
                        //    else
                        //    {
                        //        throw new Exception("SaveToBlob failed");
                        //    }
                        //}
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
        }


        private async Task<bool> SaveToAzureBlob(BlobDetails blobDetails)
        {
            MemoryStream ms = null;
            IFormFile file = null;
            try
            {
                if (CloudStorageAccount.TryParse(_azureBlobConfig.Value.ConnectionString, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer container = BlobUtilities.GetBlobContainer(blobClient, blobDetails.Type, _azureBlobConfig);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(BlobUtilities.GenerateUniqueAadhaarImageName(blobDetails.CustomerID, blobDetails.Type));

                    using (ms = new MemoryStream())
                    {
                        file = blobDetails.BlobFile;
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        await blockBlob.UploadFromByteArrayAsync(fileBytes, 0, fileBytes.Length);
                    }
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
            //finally
            //{
            //    await ms.DisposeAsync();
            //}
        }
    }
}
