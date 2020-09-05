using FinancingPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FinancingPMS.Interfaces
{
    public interface IFirmService
    {

        public List<Firm> GetAllFirms();

        public Firm GetFirmDetails(string ID);
    }
}
