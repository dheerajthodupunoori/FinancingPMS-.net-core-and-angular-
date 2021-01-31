using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancingPMS.Controllers
{


    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LoginController : ControllerBase
    {


        private ILoginService _loginService;

        public LoginController(ILoginService loginService) => _loginService = loginService;
        [HttpPost]
        public LoginResponse LoginToFirm(LoginDetails loginDetails)
        {
            LoginResponse loginResponse = new LoginResponse();

            if(loginDetails!=null)
            {
                var response = _loginService.IsFirmRegistered(loginDetails.FirmId);

                if (response.Item1)
                {
                    loginResponse = _loginService.ValidateFirmLogin(loginDetails);
                }
                else
                {
                    loginResponse.ErrorMessage = !string.IsNullOrEmpty(response.Item2) ? response.Item2 : "Firm is not registered yet . Please register your Firm to login.";
                    loginResponse.LoginStatus = false;
                }
            }
            else
            {
                loginResponse.LoginStatus = false;
                loginResponse.ErrorMessage = "Login details are null";
                loginResponse.SuccessMessage = string.Empty;
            }
            return loginResponse;
        }

        [Route("customerLogin")]
        [HttpPost]
        public IActionResult LoginCustomer(CustomerLoginInfo customerLoginInfo)
        {
            if(ModelState.IsValid)
            {
                return Ok(_loginService.ValidateCustomerLogin(customerLoginInfo));
            }
            return BadRequest();
        }

       
    }
}
