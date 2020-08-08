using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using IronOcr;
using Microsoft.AspNetCore.Http.Extensions;
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
            string customerID = string.Empty;
            try
            {
                if (customerLoginDetails != null)
                {
                    Thread.Sleep(2000);
                    customerID =   _customerRegistrationService.PerformCustomerRegistration(customerLoginDetails);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(new { CustomerID = customerID }); ;
        }


        [HttpPost]
        [Route("GetTextFromImage")]
        public async Task<IActionResult> GetTextFromImageAsync()
        {
            var file = Request.Form.Files[0];
            Image img = null;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                img = Image.FromStream(memoryStream);
            }

            var Ocr = new AdvancedOcr()
            {
                ReadBarCodes = false
            };
          
            var Results = Ocr.Read(img);


            return Ok();
        }


        [HttpPost]
        [Route("saveCustomerAdditionalDetails")]
        public async Task<IActionResult> SaveCustomerAdditionalDetails([FromForm] CustomerAdditionalDetails customerAdditionalDetails)
        {
          
            return Ok();
        }
    }
}
