using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancingPMS.Models;
using Microsoft.AspNetCore.Mvc;
using FinancingPMS.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancingPMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirmController : ControllerBase
    {

        private IFirmService _firmService;
        // GET: /<controller>/
        public FirmController(IFirmService firmService)
        {
            _firmService = firmService;
        }

        [HttpGet]
        public IActionResult GetAllFirms()
        {
            List<Firm> firmsList = new List<Firm>();
            try
            {
                firmsList = _firmService.GetAllFirms();
                if (firmsList.Count == 0)
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            return Ok(firmsList);
        }
    }
}
