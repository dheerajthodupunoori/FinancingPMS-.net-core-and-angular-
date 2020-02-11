using FinancingPMS.Config;
using FinancingPMS.Interfaces;
using FinancingPMS.Models;
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

        private AzureConfig azureConfigOptions;

        private IAzureOperations _azureOperations;



        public RegistrationService(IConfiguration configuration , IOptions<AzureConfig> azureConfig,IAzureOperations azureOperations)
        {
            _configuration = configuration;

            //connectionString = _configuration.GetConnectionString("DefaultConnection");

            azureConfigOptions = azureConfig.Value;

            _azureOperations = azureOperations;

            connectionString = _azureOperations.GetConnectionStringFromAzureKeyVault(azureConfigOptions.KeyVaultName , azureConfigOptions.AzureSQLDatabaseSecretName);

            _connection = new SqlConnection(connectionString);
        }

        public void RegisterFirmOwner(Firm firm)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    //if (!DoesFirmExists(firm.Id))
                    //{
                        sqlCommand.Connection = _connection;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandText = "spInsertIntoFirm";
                        sqlCommand.Parameters.AddWithValue("Id", firm.Id);
                        sqlCommand.Parameters.AddWithValue("Name", firm.Name);
                        sqlCommand.Parameters.AddWithValue("Email", firm.Email);
                        sqlCommand.Parameters.AddWithValue("PhoneNumber", firm.PhoneNumber);

                        if (_connection.State == System.Data.ConnectionState.Closed)
                        {
                            _connection.Open();
                        }

                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                    //}
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _connection.Close();
            }
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


        //public void SaveFirmDetails(FirmAddress firmAddress)
        //{
        //    try
        //    {
        //        using (SqlCommand sqlCommand = new SqlCommand())
        //        {
        //            sqlCommand.Connection = _connection;
        //            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //            sqlCommand.CommandText = "spInsertIntoFirmDetails";
        //            sqlCommand.Parameters.AddWithValue("Address1", firmAddress.Address1);
        //            sqlCommand.Parameters.AddWithValue("Address2", firmAddress.Address2);
        //            sqlCommand.Parameters.AddWithValue("City", firmAddress.City);
        //            sqlCommand.Parameters.AddWithValue("State", firmAddress.State);
        //            sqlCommand.Parameters.AddWithValue("Zip", firmAddress.Zip);
        //            sqlCommand.Parameters.AddWithValue("FirmId", firmAddress.FirmId);

        //            if (_connection.State == System.Data.ConnectionState.Closed)
        //            {
        //                _connection.Open();
        //            }

        //            int rowsAffected = sqlCommand.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        _connection.Close();
        //    }
        //}
    }
}
