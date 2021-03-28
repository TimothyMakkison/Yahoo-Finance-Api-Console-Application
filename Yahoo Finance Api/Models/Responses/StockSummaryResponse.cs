using System.Text.Json.Serialization;
using Yahoo_Finance_Api.Models.Responses.StockSummary;

namespace Yahoo_Finance_Api.Models.Responses
{
    public record StockSummaryResponse
    {
        [JsonPropertyName("summaryProfile")]
        public SummaryProfile SummaryProfile { get; init; }

        [JsonPropertyName("price")]
        public Price Price { get; init; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; init; }
    }
}