using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Models
{
    public class CustomerLoginResponse
    {
        public bool CustomerLoginStatus { get; set; }

        public string jsonToken { get; set; }

        public bool AreCustomerAdditionalDetailsSaved { get; set; }

        public string ErrorMessage { get; set; }
    }
}
