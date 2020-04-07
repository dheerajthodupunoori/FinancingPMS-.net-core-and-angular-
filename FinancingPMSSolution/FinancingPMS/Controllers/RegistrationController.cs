﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancingPMS.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {

        public IConfiguration _configuration;

        private string connectionString = string.Empty;

        private IRegistration _registrationService;


        public RegistrationController(IConfiguration configuration, IRegistration registrationService)
        {
            _configuration = configuration;

            connectionString = _configuration.GetConnectionString("DefaultConnection");

            _registrationService = registrationService;
        }

        [ActionName("RegisterFirmOwner")]
        [HttpPost]
        public FirmRegistrationResponse RegisterFirmOwner([FromBody] Firm firm)
        {
            FirmRegistrationResponse firmRegistrationResponse = null;

            if (firm != null)
            {
                firmRegistrationResponse = _registrationService.RegisterFirmOwner(firm);
            }
            else
            {
                firmRegistrationResponse.RegistrationStatus = false;
            }
            return firmRegistrationResponse;
        }

        [HttpPost("saveFirmDetails")]
        public IActionResult SaveFirmDetails([FromBody] FirmAddress firmAddress)
        {
            if (firmAddress == null)
            {
                return BadRequest();
            }
            try
            {
                _registrationService.SaveFirmDetails(firmAddress);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.StackTrace, title: ex.Message, statusCode: 500);
            }

            return Ok();
        }

    }
}
