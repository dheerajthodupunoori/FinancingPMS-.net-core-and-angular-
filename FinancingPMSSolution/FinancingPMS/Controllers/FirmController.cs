using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancingPMS.Models;
using Microsoft.AspNetCore.Mvc;
using FinancingPMS.Interfaces;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancingPMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirmController : ControllerBase
    {

        private IFirmService _firmService;

        private ILogger _logger;
        // GET: /<controller>/
        public FirmController(IFirmService firmService , ILogger<FirmController> logger)
        {
            _firmService = firmService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllFirms()
        {
            _logger.LogInformation("GetAllFirms execution started", null);
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
