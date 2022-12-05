using CurrencyExchangeLibrary.Models.Crypto;
using CurrencyExchangeLibrary.Models.OHLC;
using CurrencyExchangeLibrary.Models.OUTPUT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Interfaces
{
    public interface ICryptoRepository
    {

        //Get
        Task<List<CryptoOutModel>> GetCryptosAsync();
        Task<CryptoModel> GetCryptoAsync(string symbol);
        Task<OHLCVModel> GetLatestOHLCVAsync(string symbol);
        Task<bool> CryptoExistAsync(string symbol);
        Task<CryptoModel> GetWeeklyCryptoAsync(string symbol);
        Task<CryptoModel> GetMonthlyCryptoAsync(string symbol);

        //Post
        Task<bool> CreateCryptoAsync(string symbol);
        Task<bool> CreateOHCLVAsync(OHLCVModel newOHCL);


        //Put
        Task<bool> UpdateCryptoCurrentAsync(string symbol);
        Task<bool> UpdateCryptoModelAsync(string symbol);

        Task<bool> SaveAsync();
    }
}
