using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace FinancingPMS.Logger
{
    public class FinancingPMSLogger
    {
        public string message { get; set; }
        public string transactionID { get; set; }
        public FinancingPMSLogger()
        {

        }

        public FinancingPMSLogger(string message, string transactionID)
        {
            this.message = message;
            this.transactionID = transactionID;
        }

        public override string ToString()
        {
            return $"message : {(string.IsNullOrEmpty(this.message) ? string.Empty : this.message)} , " +
                 $"transactionID :{(string.IsNullOrEmpty(this.transactionID) ? string.Empty : this.transactionID)}";
        }
    }
}
