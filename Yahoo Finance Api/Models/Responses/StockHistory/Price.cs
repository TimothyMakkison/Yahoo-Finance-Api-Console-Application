using System.Text.Json.Serialization;

namespace Yahoo_Finance_Api.Models.Responses.StockHistory;

public record Price
{
    [JsonPropertyName("date")]
    public long Date { get; init; }

    [JsonPropertyName("open")]
    public double Open { get; init; }

    [JsonPropertyName("high")]
    public double High { get; init; }

    [JsonPropertyName("low")]
    public double Low { get; init; }

    [JsonPropertyName("close")]
    public double Close { get; init; }

    [JsonPropertyName("volume")]
    public long Volume { get; init; }
}
