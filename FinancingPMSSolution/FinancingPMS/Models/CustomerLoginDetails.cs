using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Models
{
    public class CustomerLoginDetails
    {
        public string CustomerID { get; set; }

        public string FirmID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FatherName { get; set; }

        public DateTime DOB { get; set; }

        public string Password { get; set; }

        //[MaxLength(12)]
        //[MinLength(12)]
        public string AadhaarNumber { get; set; }

        //public string AadhaarFilePath { get; set; }

        public int CustomerRegistrationValidationStatus { get; set; } 

        public string EmailID { get; set; }
    }
}
