using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yahoo_Finance_Api.Apis;
using Yahoo_Finance_Api.Errors;
using Yahoo_Finance_Api.Models.Requests;
using Yahoo_Finance_Api.Models.Results;

namespace Yahoo_Finance_Api.Handlers
{
    public class StockHistoryHandler : IRequestHandler<StockHistoryRequest, StockHistoryResult>
    {
        private readonly IStockApi _stockApi;
        private readonly IMapper _mapper;

        public StockHistoryHandler(IStockApi stockApi, IMapper mapper)
        {
            _stockApi = stockApi;
            _mapper = mapper;
        }

        public async Task<StockHistoryResult> Handle(StockHistoryRequest request, CancellationToken cancellationToken)
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
            Console.WriteLine(stringForm);
            return mapped;
        }
    }
}