using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Models
{
    public class LoginResponse
    {

        public bool LoginStatus { get; set; }

        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }

        public string jsonToken { get; set; }

    }
}
