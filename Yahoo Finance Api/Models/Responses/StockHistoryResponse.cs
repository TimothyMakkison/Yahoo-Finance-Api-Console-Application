using System.Collections.Generic;
using System.Text.Json.Serialization;
using Yahoo_Finance_Api.Models.Responses.StockHistory;

namespace Yahoo_Finance_Api.Models.Responses;

public record StockHistoryResponse
{
    [JsonPropertyName("prices")]
    public List<Price> Prices { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("timeZone")]
    public TimeZone TimeZone { get; init; }
}
