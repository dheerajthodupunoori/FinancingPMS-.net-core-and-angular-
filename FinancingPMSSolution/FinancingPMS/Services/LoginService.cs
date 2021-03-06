﻿using FinancingPMS.Config;
using FinancingPMS.Enums;
using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinancingPMS.Services
{
    public class LoginService : ILoginService
    {

        private IConfiguration _configuration;

        private string connectionString = string.Empty;

        private SqlConnection _connection;

        private AzureConfig azureConfigOptions;

        private IAzureOperations _azureOperations;

        private const string FIRMDETAILSQUERY = @"SELECT * FROM FirmDetails WHERE FirmId=@Id";

        private const string CUSTOMERDETAILSQUERY = @"SELECT * FROM CustomerDetails WHERE CustomerID=@Id";

        public LoginService(IConfiguration configuration, IOptions<AzureConfig> azureConfig, IAzureOperations azureOperations)
        {
            _configuration = configuration;
            //connectionString = _configuration.GetConnectionString("DefaultConnection");
            azureConfigOptions = azureConfig.Value;

            _azureOperations = azureOperations;

            connectionString = _azureOperations.GetConnectionStringFromAzureKeyVault(azureConfigOptions.KeyVaultName, azureConfigOptions.AzureSQLDatabaseSecretName);

            _connection = new SqlConnection(connectionString);
        }



        public (bool, string) IsFirmRegistered(string FirmId)
        {
            bool isFirmRegistered = false;
            string message = string.Empty;
            SqlDataReader reader = null;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = _connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"SELECT * FROM firm WHERE Id='{FirmId}'";

                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        isFirmRegistered = true;
                    }
                }

            }
            catch (Exception ex)
            {
                isFirmRegistered = false;
                message = ex.Message;

            }
            finally
            {
                _connection.Close();
                reader.Close();
            }

            return (isFirmRegistered, message);
        }

        public LoginResponse ValidateFirmLogin(LoginDetails loginDetails)
        {
            LoginResponse loginResponse = new LoginResponse();
            SqlDataReader reader = null;
            string password = string.Empty;
            string firmId = string.Empty;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = _connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"SELECT * FROM firm WHERE Id=@Id";
                    command.Parameters.AddWithValue("@Id", loginDetails.FirmId);

                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }
                    using (reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                password = reader["Password"].ToString();
                                firmId = reader["Id"].ToString();
                            }
                        }
                    }

                    if (loginDetails.Password.Equals(password) && loginDetails.FirmId.Equals(firmId))
                    {
                        loginResponse.LoginStatus = true;


                        //creating JWT token

                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                        var tokeOptions = new JwtSecurityToken(
                            issuer: "http://localhost:49366",
                            audience: "http://localhost:49366",
                            claims: new List<Claim>(),
                            expires: DateTime.Now.AddMinutes(1),
                            signingCredentials: signinCredentials
                        );

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                        loginResponse.jsonToken = tokenString;

                        //just checking whether firm details are saved or not.

                        if (AreDetailsSaved(loginDetails.FirmId , UserType.FirmOwner))
                        {
                            loginResponse.AreFirmDetailsSaved = true;
                        }

                        else
                        {
                            loginResponse.AreFirmDetailsSaved = false;
                        }



                    }
                    else
                    {
                        //loginResponse.ErrorMessage = "Incorrect Passowrd.Please enter valid credentials.";
                        loginResponse.LoginStatus = false;
                        throw new Exception("Incorrect Passowrd.Please enter valid credentials.");
                    }
                }

            }
            catch (Exception ex)
            {
                loginResponse.ErrorMessage = ex.Message;
                loginResponse.LoginStatus = false;

            }
            finally
            {
                _connection.Close();
            }

            return loginResponse;
        }



        private bool AreDetailsSaved(string id , UserType user)
        {
            bool areDetailsSaved = false;
            SqlDataReader reader = null;

            try
            {

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = _connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = string.Empty;


                    switch(user)
                    {
                        case UserType.FirmOwner:
                            command.CommandText = FIRMDETAILSQUERY;
                            break;
                        case UserType.Customer:
                            command.CommandText = CUSTOMERDETAILSQUERY;
                            break;
                    }


                    command.Parameters.AddWithValue("@Id", id);

                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }
                    using (reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            areDetailsSaved = true;
                        }
                    }
                }
            }
            finally
            {
                _connection.Close();
                reader.Close();
            }

            return areDetailsSaved;
        }


        public CustomerLoginResponse ValidateCustomerLogin(CustomerLoginInfo customerLoginInfo)
        {
            CustomerLoginResponse customerLoginResponse = new CustomerLoginResponse();
            string customerID = string.Empty;
            string password = string.Empty;
            bool isCustomerRegistered = false;
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = _connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"SELECT * FROM CustomerLoginDetails WHERE CustomerID=@Id";
                    command.Parameters.AddWithValue("@Id", customerLoginInfo.CustomerID);

                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                password = reader["Password"].ToString();
                                customerID = reader["CustomerID"].ToString();
                            }
                            isCustomerRegistered = true;
                        }
                    }

                    if (isCustomerRegistered && customerLoginInfo.Password.Equals(password) && customerLoginInfo.CustomerID.Equals(customerID))
                    {
                        customerLoginResponse.CustomerLoginStatus = true;
                        //creating JWT token

                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                        var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:49366",
                        audience: "http://localhost:49366",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(1),
                        signingCredentials: signinCredentials
                                     );

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                        customerLoginResponse.jsonToken = tokenString;


                        if (AreDetailsSaved(customerLoginInfo.CustomerID , UserType.Customer))
                        {
                            customerLoginResponse.AreCustomerAdditionalDetailsSaved = true;
                        }

                        else
                        {
                            customerLoginResponse.AreCustomerAdditionalDetailsSaved = false;
                        }
                    }
                    else
                    {
                        customerLoginResponse.CustomerLoginStatus = false;
                        throw new Exception("Incorrect Passowrd.Please enter valid credentials/your are not registered");
                    }
                }

            }
            catch (Exception ex)
            {
                customerLoginResponse.ErrorMessage = ex.Message;
            }
            finally
            {
                _connection.Close();
            }

            return customerLoginResponse;
        }

    }
}
