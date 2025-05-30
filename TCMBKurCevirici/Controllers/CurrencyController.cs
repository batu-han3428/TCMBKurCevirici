using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TCMBKurCevirici.Hubs;
using TCMBKurCevirici.Services;

namespace TCMBKurCevirici.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyService _currencyService;

        public CurrencyController(CurrencyService currencyService, IHubContext<CurrencyHub> hubContext)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rates = await _currencyService.GetCurrencyRatesAsync();
            return Ok(rates);
        }
    }
}
