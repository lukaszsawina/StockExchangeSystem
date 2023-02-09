using CurrencyExchangeLibrary.Models.Currency;
using CurrencyExchangeLibrary.Models.OHLC;
using CurrencyExchangeLibrary.Models.OUTPUT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Interfaces
{
    public interface ICurrencyRepository
    {
        //Get
        Task<List<CurrencyOutModelDto>> GetCurrenciesAsync();
        Task<CurrencyModel> GetCurrencyAsync(string symbol);
        Task<bool> CurrencyExistAsync(string symbol);
        Task<List<OHLCCurrencyModel>> GetCurrencyOHLCAsync(string symbol);
        Task<List<OHLCCurrencyModel>> GetWeeklyCurrencyOHLCAsync(string symbol);
        Task<List<OHLCCurrencyModel>> GetMonthlyCurrencyOHLCAsync(string symbol);
        Task<OHLCCurrencyModel> GetLatestOHLCAsync(string symbol);
        Task<List<CurrencyOutModelDto>> GetBestCurrencyAsync();
        Task<List<string>> GetCurrenciesCodesAsync();
        //Post
        Task<bool> CreateCurrencyAsync(string symbol);
        Task<bool> CreateOHLCAsync(List<OHLCCurrencyModel> newOHLC);
        //Put
        Task<bool> UpdateCurrencyValueAsync(string symbol);
        Task<bool> UpdateCurrencyModelAsync(string symbol);

        Task<bool> SaveAsync();

    }
}
