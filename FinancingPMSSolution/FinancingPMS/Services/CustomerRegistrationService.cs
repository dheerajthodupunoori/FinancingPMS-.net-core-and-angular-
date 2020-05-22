using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Services
{
    public class CustomerRegistrationService : ICustomerRegistrationService
    {
        public void PerformCustomerRegistration(CustomerLoginDetails customerLoginDetails)
        {



        }



        private string GenerateCustomerID(CustomerLoginDetails customerLoginDetails)
        {
            string customerID = "FRM";

            if(!string.IsNullOrEmpty(customerLoginDetails.FirmID))
            {
                customerID += customerLoginDetails.FirmID;
            }

            if(!string.IsNullOrEmpty(customerLoginDetails.AadhaarNumber))
            {
                customerID += "UIDAI" + customerLoginDetails.AadhaarNumber.Substring(0, 3);
            }

            return !string.IsNullOrEmpty(customerID) ? customerID.Trim() : customerID;
        }
    }
}
