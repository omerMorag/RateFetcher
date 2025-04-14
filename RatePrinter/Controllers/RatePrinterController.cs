using Microsoft.AspNetCore.Mvc;
using RatePrinter.Interfaces;
using RatePrinter.Models;

namespace RatePrinter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatePrinterController : ControllerBase
    {
        private readonly IRatePrinterService _service;

        public RatePrinterController(IRatePrinterService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public IActionResult GetAllRates()
        {
            var rates = _service.GetAllRates();
            return Ok(rates);
        }

        [HttpGet("{fromCurrency}/{toCurrency}")]
        public IActionResult GetRateByPair(Currency fromCurrency, Currency toCurrency)
        {
            var rate = _service.GetRateByPair(fromCurrency, toCurrency);
            if (rate == null)
                return NotFound($"No rate found for pair {fromCurrency},{toCurrency}");

            return Ok(new
            {
                Rate = rate,
                Message = $"The exchange rate for {fromCurrency} to {toCurrency} is available.",
                LastUpdated = rate.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss") 
            });
        }
    }
}
