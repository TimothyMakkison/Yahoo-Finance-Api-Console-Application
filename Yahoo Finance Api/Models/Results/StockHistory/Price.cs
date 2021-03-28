namespace Yahoo_Finance_Api.Models.Results.StockHistory
{
    public record Price
    {
        public long Date { get; init; }

        public double Open { get; init; }

        public double High { get; init; }

        public double Low { get; init; }

        public double Close { get; init; }

        public long Volume { get; init; }
    }
}