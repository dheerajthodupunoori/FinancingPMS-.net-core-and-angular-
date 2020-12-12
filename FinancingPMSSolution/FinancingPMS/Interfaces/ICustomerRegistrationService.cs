using FinancingPMS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Interfaces
{
    public interface ICustomerRegistrationService
    {

        public string PerformCustomerRegistration(CustomerLoginDetails customerLoginDetails);

        public Task<string >SaveCustomerAdditionalDetails(CustomerAdditionalDetails customerAdditionalDetails);
    }
}
