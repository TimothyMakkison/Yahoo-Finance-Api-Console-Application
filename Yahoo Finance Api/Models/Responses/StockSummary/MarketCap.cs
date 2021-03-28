using System.Text.Json.Serialization;

namespace Yahoo_Finance_Api.Models.Responses.StockSummary
{
    public record Marketcap
    {
        [JsonPropertyName("raw")]
        public long Raw { get; init; }
    }
}