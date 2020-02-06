using System;
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


        public RegistrationController(IConfiguration configuration , IRegistration registrationService )
        {
            _configuration = configuration;

            connectionString = _configuration.GetConnectionString("DefaultConnection");

            _registrationService = registrationService;
        }

        [ActionName("RegisterFirmOwner")]
        [HttpPost]
        public void  RegisterFirmOwner(Firm firm)
        {
            if(firm!=null)
            {
                _registrationService.RegisterFirmOwner(firm);
            }
            else
            {

            }
        }

        //[ActionName("SaveFirmDetails")]
        //[HttpPost]
        //public void SaveFirmDetails(FirmAddress firmAddress)
        //{
        //    if (firmAddress != null)
        //    {
        //        _registrationService.SaveFirmDetails(firmAddress);
        //    }
        //    else
        //    {

        //    }
        //}

    }
}
