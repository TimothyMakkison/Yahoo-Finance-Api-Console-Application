using System.Text.Json.Serialization;

namespace Yahoo_Finance_Api.Models.Responses.StockHistory;

public record TimeZone
{
    [JsonPropertyName("gmtOffset")]
    public long GmtOffset { get; set; }
}
