using TCMBKurCevirici.Application.Interfaces;
using TCMBKurCevirici.Models;

namespace TCMBKurCevirici.Services
{
    public class CurrencyService
    {
        private readonly ICurrencyProvider _provider;

        public CurrencyService(ICurrencyProvider provider)
        {
            _provider = provider;
        }

        public Task<List<CurrencyRate>> GetCurrencyRatesAsync()
        {
            return _provider.GetRatesAsync();
        }
    }
}
