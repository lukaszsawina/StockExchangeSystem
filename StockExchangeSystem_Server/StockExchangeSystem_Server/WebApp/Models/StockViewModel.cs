using CurrencyExchangeLibrary.Models.OHLC;
using CurrencyExchangeLibrary.Models.Stock;

namespace WebApp.Models
{
    public class StockViewModel
    {
        public StockModel Stock { get; set; }
        public List<OHLCVStockModel> OHLCV { get; set; }
    }
}
