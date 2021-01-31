using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancingPMS.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancingPMS.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UpdateFirmDetailsController : Controller
    {

        public IConfiguration _configuration;

        public IUpdateFirmDetails _updatePassword;

        public UpdateFirmDetailsController(IConfiguration configuration,IUpdateFirmDetails updatePassword)
        {
            _configuration = configuration;
            _updatePassword = updatePassword;
        }

        [HttpGet]
        [MapToApiVersion("1")]
        [Route("getOldPassword")]

        public IActionResult ValidateFirmOldPassword(string firmId , string password)
        {
            bool response = false;
            try
            {
              response =  _updatePassword.ValidateFirmOldPassword(firmId, password);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            if(!response)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        [HttpPost]
        [MapToApiVersion("1")]
        [Route("updateFirmPassword")]

        public IActionResult UpdateFirmPassword(string firmId , string newPassword)
        {
            return Ok(true);
        }



        [HttpGet]
        [MapToApiVersion("2")]
        [Route("getOldPassword")]

        public IActionResult UpdatePasswordV2(string firmId, string password)
        {
            bool response = false;
            try
            {
                response = _updatePassword.ValidateFirmOldPassword(firmId, password);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            if (!response)
            {
                return Ok("false from V2");
            }

            return Ok("true from V2");
        }

    }
}
