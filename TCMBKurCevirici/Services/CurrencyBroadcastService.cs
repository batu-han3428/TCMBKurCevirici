using Microsoft.AspNetCore.SignalR;
using TCMBKurCevirici.Hubs;

namespace TCMBKurCevirici.Services
{
    public class CurrencyBroadcastService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<CurrencyHub> _hubContext;

        public CurrencyBroadcastService(IServiceScopeFactory scopeFactory, IHubContext<CurrencyHub> hubContext)
        {
            _scopeFactory = scopeFactory;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<CurrencyService>();
                var data = await service.GetCurrencyRatesAsync();

                await _hubContext.Clients.All.SendAsync("ReceiveRates", data);

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
