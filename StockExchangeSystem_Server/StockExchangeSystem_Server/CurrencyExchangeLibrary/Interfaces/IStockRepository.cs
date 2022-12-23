using CurrencyExchangeLibrary.Models.OHLC;
using CurrencyExchangeLibrary.Models.OUTPUT;
using CurrencyExchangeLibrary.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Interfaces
{
    public interface IStockRepository
    {
        //Get
        Task<List<string>> GetStocksCodesAsync();
        Task<List<StockOutModel>> GetStocksAsync();
        Task<OHLCVStockModel> GetLatestOHLCVAsync(string symbol);
        Task<StockModel> GetStockAsync(string symbol);
        Task<bool> StockExistAsync(string symbol);
        //Post
        Task<bool> CreateStockAsync(string symbol);
        //Put
        Task<bool> UpdateStockCurrentAsync(string symbol);

        Task<bool> SaveAsync();

    }
}
