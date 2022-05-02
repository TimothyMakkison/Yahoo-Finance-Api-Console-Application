using AutoMapper;
using CommandLine;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yahoo_Finance_Api.Apis;
using Yahoo_Finance_Api.Errors;
using Yahoo_Finance_Api.Helpers;
using Yahoo_Finance_Api.Models.Results;

namespace Yahoo_Finance_Api.Handlers;

[Verb("summary", HelpText = "Retrieve summary of stock.")]
public record StockSummaryRequest : IRequest<Unit>
{
    [Value(0, HelpText = "Stock symbol.")]
    public string Symbol { get; init; }
    [Value(1, Required = false, HelpText = "Return type language.")]
    public string Region { get; init; }
}

public class StockSummaryHandler : IRequestHandler<StockSummaryRequest, Unit>
{
    private readonly IStockApi _stockApi;
    private readonly IMapper _mapper;
    private readonly IConsoleWriter _consoleWriter;

    public StockSummaryHandler(IStockApi stockApi, IMapper mapper, IConsoleWriter consoleWriter)
    {
        _stockApi = stockApi;
        _mapper = mapper;
        _consoleWriter=consoleWriter;
    }

    public async Task<Unit> Handle(StockSummaryRequest request, CancellationToken cancellationToken)
    {
        var response = await (string.IsNullOrEmpty(request.Region)
            ? _stockApi.GetStockSummaryAsync(request.Symbol)
            : _stockApi.GetStockSummaryAsync(request.Symbol, request.Region));


        if (!response.IsSuccessStatusCode)
        {
            throw new NotSuccessException();
        }

        var mapped = _mapper.Map<StockSummaryResult>(response.Content);
        _consoleWriter.WriteLine(mapped);

        return Unit.Value;
    }
}
