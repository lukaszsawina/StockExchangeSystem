using CurrencyExchangeLibrary.Models.Crypto;
using CurrencyExchangeLibrary.Models.OHLC;

namespace WebApp.Models
{
    public class CryptoViewModel
    {
        public CryptoModel Crypto { get; set; }
        public List<OHLCVCryptoModel> OHLCV { get; set; }
    }
}
