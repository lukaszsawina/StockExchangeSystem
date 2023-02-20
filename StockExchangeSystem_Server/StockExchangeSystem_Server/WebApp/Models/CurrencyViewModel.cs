using CurrencyExchangeLibrary.Models.Currency;
using CurrencyExchangeLibrary.Models.OHLC;

namespace WebApp.Models
{
    public class CurrencyViewModel
    {
        public CurrencyModel Currency { get; set; }
        public List<OHLCCurrencyModel> OHLC { get; set; }
    }
}
