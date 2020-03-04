using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using Microsoft.Extensions.Configuration;
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
        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(connectionString);
        }



        public (bool , string) IsFirmRegistered(string FirmId)
        {
            bool isFirmRegistered = false;
            string message = string.Empty;
            SqlDataReader reader = null;
            try
            {
                using(SqlCommand command = new SqlCommand())
                {
                    command.Connection = _connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"SELECT * FROM firm WHERE Id='{FirmId}'";

                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }
                    reader = command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        isFirmRegistered = true;
                    }
                }

            }
            catch(Exception ex)
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
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                             password = reader["Password"].ToString();
                             firmId = reader["Id"].ToString();
                        }
                        if(loginDetails.Password.Equals(password) && loginDetails.FirmId.Equals(firmId))
                        {
                            loginResponse.LoginStatus = true;



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
                        }
                        else
                        {
                            loginResponse.ErrorMessage = "Incorrect Passowrd.Please enter valid Password.";
                            loginResponse.LoginStatus = false;
                        }
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
                reader.Close();
            }

            return loginResponse;
        }
    }
}
