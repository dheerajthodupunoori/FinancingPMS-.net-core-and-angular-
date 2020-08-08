using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace FinancingPMS.Models
{
    public class CustomerAdditionalDetails
    {
        public IFormFile signature { get; set; }
        
        public IFormFile passport { get; set; }

        //public CustomerDetails additionalDetails { get; set; }

        public string customerID { get; set; }

        public string occupation { get; set; }

        public string country { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public string street { get; set; }

        public string phone { get; set; }

        public string flatNumber { get; set; }

        public string income { get; set; }

        public string zip { get; set; }

    }
}
