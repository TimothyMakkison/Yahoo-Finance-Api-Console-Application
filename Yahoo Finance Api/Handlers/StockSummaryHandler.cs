using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yahoo_Finance_Api.Apis;
using Yahoo_Finance_Api.Models.Requests;
using Yahoo_Finance_Api.Models.Results;

namespace Yahoo_Finance_Api.Handlers
{
    public class StockSummaryHandler : IRequestHandler<StockSummaryRequest, StockSummaryResult>
    {
        private readonly IStockApi _stockApi;
        private readonly IMapper _mapper;

        public StockSummaryHandler(IStockApi stockApi, IMapper mapper)
        {
            _stockApi = stockApi;
            _mapper = mapper;
        }

        public async Task<StockSummaryResult> Handle(StockSummaryRequest request, CancellationToken cancellationToken)
        {
            var response = await (string.IsNullOrEmpty(request.Region)
                ? _stockApi.GetStockSummaryAsync(request.Symbol)
                : _stockApi.GetStockSummaryAsync(request.Symbol, request.Region));

            var mapped = _mapper.Map<StockSummaryResult>(response.Content);
            return mapped;
        }
    }
}