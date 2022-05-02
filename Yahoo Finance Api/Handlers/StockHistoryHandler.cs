using AutoMapper;
using CommandLine;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yahoo_Finance_Api.Apis;
using Yahoo_Finance_Api.Errors;
using Yahoo_Finance_Api.Helpers;
using Yahoo_Finance_Api.Models.Results;

namespace Yahoo_Finance_Api.Handlers;

[Verb("history", HelpText = "Retrieve summary of stock.")]
public record StockHistoryRequest : IRequest<Unit>
{
    [Value(0, Required = true, HelpText = "Stock symbol")]
    public string Symbol { get; init; }
    [Value(1, Required = false, HelpText = "Return type language.")]
    public string Region { get; init; }
}

public class StockHistoryHandler : IRequestHandler<StockHistoryRequest, Unit>
{
    private readonly IStockApi _stockApi;
    private readonly IMapper _mapper;
    private readonly IConsoleWriter _consoleWriter;

    public StockHistoryHandler(IStockApi stockApi, IMapper mapper, IConsoleWriter consoleWriter)
    {
        _stockApi = stockApi;
        _mapper = mapper;
        _consoleWriter=consoleWriter;
    }

    public async Task<Unit> Handle(StockHistoryRequest request, CancellationToken cancellationToken)
    {
        var response = await (string.IsNullOrEmpty(request.Region)
           ? _stockApi.GetStockHistoryAsync(request.Symbol)
           : _stockApi.GetStockHistoryAsync(request.Symbol, request.Region));

        if (!response.IsSuccessStatusCode)
        {
            throw new NotSuccessException();
        }

        var mapped = _mapper.Map<StockHistoryResult>(response.Content);
        var stringForm = string.Join("\n", mapped.Prices);

        _consoleWriter.WriteLine(stringForm);
        return Unit.Value;
    }
}