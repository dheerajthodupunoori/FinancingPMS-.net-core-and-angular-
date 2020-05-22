using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancingPMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerRegistrationController : Controller
    {


        public IConfiguration _configuration;

        private string connectionString = string.Empty;

        private ICustomerRegistrationService _customerRegistrationService;

        public CustomerRegistrationController(IConfiguration configuration, ICustomerRegistrationService customerRegistrationService)
        {
            _configuration = configuration;

            connectionString = _configuration.GetConnectionString("DefaultConnection");

            _customerRegistrationService = customerRegistrationService;
        }




       [HttpPost]
       [Route("PerformCustomerRegistration")]
       public IActionResult PerformCustomerRegistration(CustomerLoginDetails customerLoginDetails)
        {
            return Ok();
        }
    }
}
