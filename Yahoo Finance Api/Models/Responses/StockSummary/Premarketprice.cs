using System.Text.Json.Serialization;

namespace Yahoo_Finance_Api.Models.Responses.StockSummary;

public record Premarketprice
{
    [JsonPropertyName("raw")]
    public float Raw { get; init; }
}
