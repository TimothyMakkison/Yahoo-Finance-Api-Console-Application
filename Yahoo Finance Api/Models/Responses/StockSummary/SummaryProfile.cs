using System.Text.Json.Serialization;

namespace Yahoo_Finance_Api.Models.Responses.StockSummary
{
    public record SummaryProfile
    {
        [JsonPropertyName("sector")]
        public string Sector { get; init; }

        [JsonPropertyName("longBusinessSummary")]
        public string LongBusinessSummary { get; init; }

        [JsonPropertyName("country")]
        public string Country { get; init; }

        [JsonPropertyName("industry")]
        public string Industry { get; init; }
    }
}