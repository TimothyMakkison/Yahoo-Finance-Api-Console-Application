using System.Text.Json.Serialization;

namespace Yahoo_Finance_Api.Models.Responses.StockSummary;

public record Price
{
    [JsonPropertyName("longName")]
    public string LongName { get; init; }
    [JsonPropertyName("preMarketPrice")]
    public Premarketprice PreMarketPrice { get; init; }
    [JsonPropertyName("postMarketPrice")]
    public Postmarketprice PostMarketPrice { get; init; }
    [JsonPropertyName("marketCap")]
    public Marketcap MarketCap { get; init; }

    [JsonPropertyName("currency")]
    public string Currency { get; init; }
}
