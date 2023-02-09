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
        Task<List<CryptoOutModelDto>> GetCryptosAsync();
        Task<CryptoModel> GetCryptoAsync(string symbol);
        Task<bool> CryptoExistAsync(string symbol);
        Task<List<OHLCVCryptoModel>> GetCryptoOHLCVAsync(string symbol);
        Task<List<OHLCVCryptoModel>> GetWeeklyCryptoOHLCVAsync(string symbol);
        Task<List<OHLCVCryptoModel>> GetMonthlyCryptoOHLCVAsync(string symbol);
        Task<OHLCVCryptoModel> GetLatestOHLCVAsync(string symbol);
        Task<List<CryptoOutModelDto>> GetBestCryptoAsync();
        Task<List<string>> GetCryptoCodesAsync();

        //Post
        Task<bool> CreateCryptoAsync(string symbol);
        Task<bool> CreateOHCLVAsync(List<OHLCVCryptoModel> updatedCrypto);

        //Put
        Task<bool> UpdateCryptoCurrentAsync(string symbol);
        Task<bool> UpdateCryptoModelAsync(string symbol);

        Task<bool> SaveAsync();
    }
}
