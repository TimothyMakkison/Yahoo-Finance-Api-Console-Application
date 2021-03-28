using System.Collections.Generic;
using Yahoo_Finance_Api.Models.Results.StockHistory;

namespace Yahoo_Finance_Api.Models.Results
{
    public record StockHistoryResult
    {
        public IReadOnlyList<Price> Prices { get; init; }

        public string Id { get; init; }

        public long TimeZoneGmtOffset { get; init; }
    }
}