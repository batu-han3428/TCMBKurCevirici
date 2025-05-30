using TCMBKurCevirici.Models;

namespace TCMBKurCevirici.Application.Interfaces
{
    public interface ICurrencyProvider
    {
        Task<List<CurrencyRate>> GetRatesAsync();
    }
}
