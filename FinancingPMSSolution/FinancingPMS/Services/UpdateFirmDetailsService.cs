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
    public class UpdateFirmDetailsService : IUpdateFirmDetails
    {

        private const string GETFIRMPASSWORDQUERY = @"SELECT * from Firm where Id=@Id";
        private readonly string connectionString = string.Empty;
        public IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public UpdateFirmDetailsService(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(connectionString);
        }

        public bool ValidateFirmOldPassword(string firmId , string password)
        {
            Firm firmDetails = new Firm();
            try
            {
                //using (SqlCommand command = new SqlCommand())
                //{
                //    command.CommandText = GETFIRMPASSWORDQUERY;
                //    command.Connection = _connection;
                //    command.CommandType = CommandType.Text;

                //    command.Parameters.AddWithValue("Id", firmId);


                //    if (_connection.State == ConnectionState.Closed)
                //    {
                //        _connection.Open();
                //    }

                //    using (var reader = command.ExecuteReader())
                //    {
                //        if (reader.HasRows)
                //        {
                //            while (reader.Read())
                //            {
                //                firmDetails = new Firm()
                //                {
                //                    Id = reader["Id"].ToString(),
                //                    Name = reader["Name"].ToString(),
                //                    PhoneNumber = reader["PhoneNumber"].ToString(),
                //                    Email = reader["Email"].ToString(),
                //                    Password = reader["Password"].ToString()
                //                };
                //            }
                //        }
                //    }

                //}

                if(password.Equals("dheeraj"))
                {
                    return true;
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

        public bool UpdateFirmPassword(string firmId, string newPassword)
        {
            return true;
        }
    }
}
