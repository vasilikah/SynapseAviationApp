using Microsoft.AspNetCore.Mvc;
using SynapseAviationApp.Server.Models;
using SynapseAviationApp.Server.Services.Interfaces;

namespace SynapseAviationApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }
        
        [HttpGet("GetExcangeRates")]
        public async Task<ActionResult> GetExcangeRates()
        {
            return Ok(await _exchangeRateService.GetExcangeRates());
        }
    }
}
