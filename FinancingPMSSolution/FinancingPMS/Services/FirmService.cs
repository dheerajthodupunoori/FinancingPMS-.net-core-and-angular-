using FinancingPMS.Interfaces;
using FinancingPMS.Logger;
using FinancingPMS.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FinancingPMS.Services
{
    public class FirmService : IFirmService
    {

        private const string GETALLFIRMS_SP = "spGetAllFirmDetails";

        private readonly IConfiguration _configuration;

        private readonly string connectionString = string.Empty;

        private readonly SqlConnection _connection;

        private readonly ILogger<FirmService> _logger;

        private const string FIRMDETAILSQUERY = @"SELECT * FROM Firm WHERE Id=@Id";

        public FirmService(IConfiguration configuration , ILogger<FirmService> logger)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(connectionString);
            _logger = logger;
        }


        public List<Firm> GetAllFirms(string transactionID)
        {
            List<Firm> firmsList = new List<Firm>();
            FinancingPMSLogger financingPMSLogger = new FinancingPMSLogger("GetAllFirms service (DB call) has started", transactionID);
            SqlDataReader reader = null;
            _logger.LogInformation(message: financingPMSLogger.ToString());
            try
            {
                using(_logger.BeginScope("GetAllFirms"))
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = _connection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = GETALLFIRMS_SP;

                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    using (reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Firm firm = new Firm()
                                {
                                    Id = reader["Id"].ToString(),
                                    Name = reader["Name"].ToString()
                                };
                                firmsList.Add(firm);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                financingPMSLogger.message = "Error occured while getting Firms list from database";
                _logger.LogError(exception: ex, message: financingPMSLogger.ToString());
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
            financingPMSLogger.message = "GetAllFirms service (DB call) has ended";
            _logger.LogInformation(message: financingPMSLogger.ToString());
            return firmsList;
        }



        public Firm GetFirmDetails(string FirmID)
        {
            Firm firmDetails = null;

            try
            {
                using(SqlCommand command = new SqlCommand())
                {
                    command.CommandText = FIRMDETAILSQUERY;
                    command.Connection = _connection;
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("Id", FirmID);


                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                firmDetails = new Firm()
                                {
                                    Id = reader["Id"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Password = string.Empty
                                };
                            }
                        }
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
            return firmDetails;
        }




    }
}
