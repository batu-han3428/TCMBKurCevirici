using System.Text.Json;
using TCMBKurCevirici.Application.Interfaces;
using TCMBKurCevirici.Models;

namespace TCMBKurCevirici.Application.Providers
{
    public class TcmbCurrencyProvider : ICurrencyProvider
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public TcmbCurrencyProvider(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _url = configuration["CurrencySources:TcmbUrl"]
                   ?? throw new ArgumentNullException("TCMB URL configuration missing.");
        }

        public async Task<List<CurrencyRate>> GetRatesAsync()
        {
            using var response = await _httpClient.GetAsync(_url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var root = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json)!;
            var result = new List<CurrencyRate>();

            foreach (var kvp in root)
            {
                if (kvp.Key == "Update_Date")
                    continue;

                var item = kvp.Value;
                if (item.TryGetProperty("Type", out var type) && type.GetString() == "Currency")
                {
                    result.Add(new CurrencyRate
                    {
                        Code = kvp.Key,
                        Name = kvp.Key,
                        BuyingRate = decimal.TryParse(item.GetProperty("Buying").GetString()?.Replace(",", "."), out var buy) ? buy / 10000 : 0,
                        SellingRate = decimal.TryParse(item.GetProperty("Selling").GetString()?.Replace(",", "."), out var sell) ? sell / 10000 : 0
                    });
                }
            }
            return result;
        }
    }
}
