﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Models
{
    public class FirmRegistrationResponse
    {
        public bool RegistrationStatus { get; set; }

        //public ErrorDetails ErrorDetails { get; set; }

        public List<string> ErrorDetails { get; set; }

        public string SuccessMessage { get; set; }

    }
}
