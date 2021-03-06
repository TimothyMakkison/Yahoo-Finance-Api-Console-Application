using CommandLine;
using MediatR;
using Yahoo_Finance_Api.Models.Results;

namespace Yahoo_Finance_Api.Models.Requests
{
    [Verb("summary", HelpText = "Retrieve summary of stock.")]
    public record StockSummaryRequest : IRequest<StockSummaryResult>
    {
        [Value(0, HelpText = "Stock symbol.")]
        public string Symbol { get; init; }
        [Value(1, Required = false, HelpText = "Return type language.")]
        public string Region { get; init; }
    }
}