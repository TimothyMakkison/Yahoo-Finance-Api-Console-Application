using Yahoo_Finance_Api.Models.Results.StockSummary;

namespace Yahoo_Finance_Api.Models.Results;

public record StockSummaryResult
{
    public string Symbol { get; init; }
    public SummaryProfile SummaryProfile { get; init; }
    public Price Price { get; init; }
}
