using System.Text.Json.Serialization;

namespace Yahoo_Finance_Api.Models.Responses.StockSummary;

public record Postmarketprice
{
    [JsonPropertyName("raw")]
    public float Raw { get; init; }
}
