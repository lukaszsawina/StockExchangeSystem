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
        Task<List<StockOutModel>> GetStocksAsync();
        Task<StockModel> GetStockAsync(string symbol);
        Task<bool> StockExistAsync(string symbol);
        Task<List<string>> GetStocksCodesAsync();
        Task<StockModel> GetWeeklyStockAsync(string symbol);
        Task<StockModel> GetMonthlyStockAsync(string symbol);
        Task<List<StockOutModel>> GetBestStocksAsync();
        Task<OHLCVStockModel> GetLatestOHLCVAsync(string symbol);
        //Post
        Task<bool> CreateStockAsync(string symbol);
        Task<bool> CreateOHLCAsync(List<OHLCVStockModel> newOHLC);
        //Put
        Task<bool> UpdateStockCurrentAsync(string symbol);
        Task<bool> UpdateStockModelAsync(string symbol);

        Task<bool> SaveAsync();

    }
}
