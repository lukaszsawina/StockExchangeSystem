using AutoMapper;
using CurrencyExchangeLibrary.Dto;
using CurrencyExchangeLibrary.Models.Crypto;
using CurrencyExchangeLibrary.Models.Currency;
using CurrencyExchangeLibrary.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Helper
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
