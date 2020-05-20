using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
        public FirmService(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(connectionString);
        }


        public List<Firm> GetAllFirms()
        {
            List<Firm> firmsList = new List<Firm>();



            return firmsList;
        }
    }
}
