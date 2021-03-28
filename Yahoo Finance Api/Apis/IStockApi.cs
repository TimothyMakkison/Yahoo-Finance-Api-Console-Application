using Refit;
using System.Threading.Tasks;
using Yahoo_Finance_Api.Models.Responses;

namespace Yahoo_Finance_Api.Apis
{
    public interface IStockApi
    {
        [Get("/stock/v2/get-summary?symbol={symbol}&region={region}")]
        Task<ApiResponse<StockSummaryResponse>> GetStockSummaryAsync(string symbol, string region = "US");

        [Get("/stock/v3/get-historical-data?symbol={symbol}&region={region}")]
        Task<ApiResponse<StockHistoryResponse>> GetStockHistoryAsync(string symbol, string region = "US");
    }
}