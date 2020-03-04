using FinancingPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Interfaces
{
    public interface ILoginService
    {

        public (bool , string) IsFirmRegistered(string FirmId);


        public LoginResponse ValidateFirmLogin(LoginDetails loginDetails);
    }
}
