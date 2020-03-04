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
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        private ILoginService _loginService;

        // GET: /<controller>/
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [ActionName("LoginFirm")]
        //[Route("api/[controller]/FirmId/{Id}/Email/{Email}/PhoneNumber/{PhoneNumber}")]
        //[HttpPost("Login")]
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
                    loginResponse.ErrorMessage = response.Item2;
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


    }
}
