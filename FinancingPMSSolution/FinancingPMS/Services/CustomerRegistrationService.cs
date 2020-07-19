using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinancingPMS.Services
{
    public class CustomerRegistrationService : ICustomerRegistrationService
    {


        public IConfiguration _configuration;

        private string connectionString = string.Empty;

        private SqlConnection _connection;



        public CustomerRegistrationService(IConfiguration configuration)
        {
            _configuration = configuration;

            connectionString = _configuration.GetConnectionString("DefaultConnection");

            _connection = new SqlConnection(connectionString);
        }



        public string PerformCustomerRegistration(CustomerLoginDetails customerLoginDetails)
        {
            string customerID = GenerateCustomerID(customerLoginDetails);

            try
            {
                using(SqlCommand sqlcommand = new SqlCommand())
                {
                    Thread.Sleep(2000);
                    sqlcommand.CommandText = "spInsertIntoCustomerLoginDetails";
                    sqlcommand.Connection = _connection;
                    sqlcommand.CommandType = System.Data.CommandType.StoredProcedure;


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
            catch(Exception ex)
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
            string customerID = "FRM";

            if(!string.IsNullOrEmpty(customerLoginDetails.FirmID))
            {
                customerID += customerLoginDetails.FirmID;
            }

            if(!string.IsNullOrEmpty(customerLoginDetails.AadhaarNumber))
            {
                customerID += "UIDAI" + customerLoginDetails.AadhaarNumber.Substring(0, 3);
            }

            return !string.IsNullOrEmpty(customerID) ? customerID.Trim() : customerID;
        }


        private bool  IsCustomerAlreadyRegistered(string customerID)
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


    }
}
