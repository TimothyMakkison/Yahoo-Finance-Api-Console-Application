namespace Yahoo_Finance_Api.Models.Results;

public record SummaryProfile
{
    public string Sector { get; init; }
    public string LongBusinessSummary { get; init; }
    public string Country { get; init; }
    public string Industry { get; init; }
}
