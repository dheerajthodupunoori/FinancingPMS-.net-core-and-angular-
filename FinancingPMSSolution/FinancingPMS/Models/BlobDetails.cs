using FinancingPMS.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Models
{
    public class BlobDetails
    {
        public IFormFile BlobFile { get; set; }

        public string CustomerID { get; set; }

        public BlobType Type { get; set; }
    }
}
