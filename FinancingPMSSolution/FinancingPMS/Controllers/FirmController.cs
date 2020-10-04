using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancingPMS.Models;
using Microsoft.AspNetCore.Mvc;
using FinancingPMS.Interfaces;
using Microsoft.Extensions.Logging;
using FinancingPMS.Logger;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancingPMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirmController : ControllerBase
    {

        private readonly IFirmService _firmService;

        private readonly ILogger _logger;
        public FirmController(IFirmService firmService , ILogger<FirmController> logger)
        {
            _firmService = firmService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllFirms()
        {
            Guid transactionID = Guid.NewGuid();
            FinancingPMSLogger logMessage = new FinancingPMSLogger("GetAllFirms execution started", transactionID.ToString());
            _logger.LogInformation(message : logMessage.ToString());
            List<Firm> firmsList = new List<Firm>();
            try
            {
                firmsList = _firmService.GetAllFirms(transactionID.ToString());
                if (firmsList.Count == 0)
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            logMessage.message = "GetAllFirms execution ended";
            _logger.LogInformation(message:logMessage.ToString());
            return Ok(firmsList);
        }
    }
}
