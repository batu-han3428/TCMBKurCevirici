using TCMBKurCevirici.Application.Interfaces;
using TCMBKurCevirici.Application.Providers;
using TCMBKurCevirici.Hubs;
using TCMBKurCevirici.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();
builder.Services.AddHttpClient<ICurrencyProvider, TcmbCurrencyProvider>();
builder.Services.AddScoped<CurrencyService>();
builder.Services.AddHostedService<CurrencyBroadcastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapHub<CurrencyHub>("/currencyHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
