using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Interfaces
{
    public interface IUpdateFirmDetails
    {
        public bool ValidateFirmOldPassword(string firmID , string password);

        public bool UpdateFirmPassword(string firmId, string newPassword);
    }
}
