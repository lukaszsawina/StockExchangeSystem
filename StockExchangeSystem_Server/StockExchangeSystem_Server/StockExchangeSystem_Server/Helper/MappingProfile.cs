using AutoMapper;
using CurrencyExchangeLibrary.Dto;
using CurrencyExchangeLibrary.Models.Crypto;
using CurrencyExchangeLibrary.Models.Currency;
using CurrencyExchangeLibrary.Models.Stock;

namespace StockExchangeSystem_Server.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CryptoModel, CryptoModelDto>();
            CreateMap<CurrencyModel, CurrencyModelDto>();
            CreateMap<StockModel, StockModelDto>();
        }
    }
}
