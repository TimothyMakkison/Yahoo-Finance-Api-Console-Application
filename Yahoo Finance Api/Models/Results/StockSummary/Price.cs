namespace Yahoo_Finance_Api.Models.Results.StockSummary
{
    public record Price
    {
        public string LongName { get; init; }
        public double PreMarketPrice { get; init; }
        public double PostMarketPrice { get; init; }
        public long MarketCap { get; init; }
        public string Currency { get; init; }
    }
}