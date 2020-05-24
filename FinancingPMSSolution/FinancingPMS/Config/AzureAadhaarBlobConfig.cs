using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Config
{
    public class AzureAadhaarBlobConfig
    {
        public string Key { get; set; }

        public string ConnectionString { get; set; }

        public string FinancingAadhaarContainerName { get; set; }
    }
}
