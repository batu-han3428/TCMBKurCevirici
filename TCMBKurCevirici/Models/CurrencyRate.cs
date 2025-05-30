namespace TCMBKurCevirici.Models
{
    public class CurrencyRate
    {
        public string Code { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public decimal BuyingRate { get; init; }
        public decimal SellingRate { get; init; }
    }
}
