using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancingPMS.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancingPMS.Controllers
{
    [ApiController]
    public class FirmController : ControllerBase
    {
        // GET: /<controller>/
        public FirmController()
        {

        }

        public List<Firm> GetAllFirms()
        {
            List<Firm> firmsList = new List<Firm>();



            return firmsList;
        }
    }
}
