using FinancingPMS.Config;
using FinancingPMS.Enums;
using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Services
{
    public class RegistrationService : IRegistration
    {

        public IConfiguration _configuration;

        private string connectionString = string.Empty;

        private SqlConnection _connection ;

        private readonly AzureConfig azureConfigOptions;

        private readonly IAzureOperations _azureOperations;

        private INotificationService _notificationService;

        public RegistrationService(IConfiguration configuration , IOptions<AzureConfig> azureConfig,IAzureOperations azureOperations , INotificationService notificationService)
        {
            _configuration = configuration;

            //connectionString = _configuration.GetConnectionString("DefaultConnection");

            azureConfigOptions = azureConfig.Value;

            _azureOperations = azureOperations;

            //connectionString = _azureOperations.GetConnectionStringFromAzureKeyVault(azureConfigOptions.KeyVaultName, azureConfigOptions.AzureSQLDatabaseSecretName);

            _connection = new SqlConnection(connectionString);

            _notificationService = notificationService;
        }

        public FirmRegistrationResponse RegisterFirmOwner(Firm firm)
        {
            FirmRegistrationResponse firmRegistrationResponse = new FirmRegistrationResponse();
            string failureMessage = string.Empty;
            var errorList = new List<String>();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                        sqlCommand.Connection = _connection;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandText = "spInsertIntoFirm";
                        sqlCommand.Parameters.AddWithValue("Id", firm.Id);
                        sqlCommand.Parameters.AddWithValue("Name", firm.Name);
                        sqlCommand.Parameters.AddWithValue("Email", firm.Email);
                        sqlCommand.Parameters.AddWithValue("PhoneNumber", firm.PhoneNumber);
                        sqlCommand.Parameters.AddWithValue("Password", firm.Password);


                    if (_connection.State == System.Data.ConnectionState.Closed)
                        {
                            _connection.Open();
                        }

                        int rowsAffected = sqlCommand.ExecuteNonQuery();

                    if(rowsAffected > 0)
                    {
                        firmRegistrationResponse.RegistrationStatus = true;
                        firmRegistrationResponse.SuccessMessage = "Your Firm Registered successfully";

                        //Sending notification after successfull registration

                        NotificationDetails notificationDetails = new NotificationDetails()
                        {
                            NotificationType = NotificationType.FirmOwnerRegistration.ToString(),
                            Body = "Your Firm is successfully registered and please find your registration details below." +
                                               Environment.NewLine + "Firm Name : " + firm.Name +
                                               Environment.NewLine + "Firm ID : " + firm.Id +
                                               Environment.NewLine + "Phone Number : " + firm.PhoneNumber +
                                               Environment.NewLine + "Email ID : " + firm.Email,
                            Subject = "FinancingPMS Firm Owner Registration Update",
                            ReceiverMailID = firm.Email
                        };

                        _notificationService.SendNotification(notificationDetails);
                    }
                    else
                    {
                        firmRegistrationResponse.RegistrationStatus = false;
                        failureMessage = "Firm Registration failed";
                        errorList.Add(failureMessage);
                        firmRegistrationResponse.ErrorDetails = errorList;
                    }
                }
            }
            catch (Exception ex)
            {
                firmRegistrationResponse.RegistrationStatus = false;
                errorList.Add(ex.Message);
                firmRegistrationResponse.ErrorDetails = errorList;
            }
            finally
            {
                _connection.Close();
            }
            return firmRegistrationResponse;
        }

        private bool DoesFirmExists(int firmId)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {

                    sqlCommand.Connection = _connection;

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.CommandText = "spDoesFirmExists";

                    sqlCommand.Parameters.AddWithValue("Id", firmId);

                    if (_connection.State == System.Data.ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        return true;
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
            return false;
        }


        public void SaveFirmDetails(FirmAddress firmAddress)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = _connection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "spInsertIntoFirmDetails";
                    sqlCommand.Parameters.AddWithValue("Address1", firmAddress.Address1);
                    sqlCommand.Parameters.AddWithValue("Address2", firmAddress.Address2);
                    sqlCommand.Parameters.AddWithValue("City", firmAddress.City);
                    sqlCommand.Parameters.AddWithValue("State", firmAddress.State);
                    sqlCommand.Parameters.AddWithValue("Zip", firmAddress.Zip);
                    sqlCommand.Parameters.AddWithValue("FirmId", firmAddress.FirmId);

                    if (_connection.State == System.Data.ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    //Sending notification after saving the firm additional details.

                    NotificationDetails notificationDetails = new NotificationDetails()
                    {
                        NotificationType = NotificationType.FirmAdditionalDetailsSave.ToString(),
                        Body = "Your firm details with firmID "+ firmAddress.FirmId +" are saved . Below are the details provided ." +
                                                 Environment.NewLine + Environment.NewLine + 
                                                 "Address1 : " + firmAddress.Address1 +
                                                 Environment.NewLine + "Address2 : " + firmAddress.Address2 +
                                                 Environment.NewLine + "City : " + firmAddress.City+
                                                 Environment.NewLine + "State : " + firmAddress.State+
                                                 Environment.NewLine + "Zip : " + firmAddress.Zip,
                        Subject = "FinancingPMS Firm additional details update."
                    };

                    _notificationService.SendNotification(notificationDetails);
                }
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
