using CurrencyExchangeLibrary.Models.Currency;
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
        Task<List<string>> GetCurrenciesCodesAsync();
        Task<List<CurrencyOutModel>> GetCurrenciesAsync();
        Task<CurrencyModel> GetCurrencyAsync(string symbol);
        Task<bool> CurrencyExistAsync(string symbol);
        //Post
        Task<bool> CreateCurrencyAsync(string symbol);
        //Put
        Task<bool> UpdateCurrencyValueAsync(string symbol);

        Task<bool> SaveAsync();

    }
}
