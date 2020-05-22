using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Enums
{
    public enum CustomerRegistrationStatusEnum
    {
        NotValidated,

        IsAutoValidatedAndIsNotManualValidated,

        IsManualValidated
    }
}
