using AutoMapper;
using Yahoo_Finance_Api.Models.Responses;
using Yahoo_Finance_Api.Models.Results;

namespace Yahoo_Finance_Api.Mappers;

public class StockMapper : Profile
{
    public StockMapper()
    {
        CreateMap<StockSummaryResponse, StockSummaryResult>();
        CreateMap<Models.Responses.StockSummary.SummaryProfile, SummaryProfile>();

        CreateMap<Models.Responses.StockSummary.Price, Models.Results.StockSummary.Price>()
            .ForMember(dest => dest.PreMarketPrice, opt => opt.MapFrom(src => src.PreMarketPrice.Raw))
            .ForMember(dest => dest.PostMarketPrice, opt => opt.MapFrom(src => src.PostMarketPrice.Raw))
            .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => src.MarketCap.Raw));

        CreateMap<StockHistoryResponse, StockHistoryResult>()
            .ForMember(dest => dest.TimeZoneGmtOffset, opt => opt.MapFrom(src => src.TimeZone.GmtOffset));
        CreateMap<Models.Responses.StockHistory.Price, Models.Results.StockHistory.Price>();
    }
}
