using FinancingPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Interfaces
{
    public interface IRegistration
    {
        void RegisterFirmOwner(Firm firm);

        //void SaveFirmDetails(FirmAddress firmAddress);
    }
}
