using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Services
{
    public class FirmService : IFirmService
    {

        private IConfiguration _configuration;

        private string connectionString = string.Empty;

        private SqlConnection _connection;

        private const string FIRMDETAILSQUERY = @"SELECT * FROM Firm WHERE Id=@Id";

        public FirmService(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(connectionString);
        }


        public List<Firm> GetAllFirms()
        {
            List<Firm> firmsList = new List<Firm>();
            SqlDataReader reader = null;

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = _connection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "spGetAllFirmDetails";

                    if (_connection.State == System.Data.ConnectionState.Closed)
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
                throw ex;
            }
            finally
            {
                _connection.Close();
            }

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
