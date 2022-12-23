using Newtonsoft.Json;

namespace CurrencyExchangeLibrary.Models.OHLC
{
    public class OHLCVCryptoModel : OHLCCryptoModel
    {
        
        [JsonProperty("5. volume")]
        public decimal Volume { get; set; }
        [JsonProperty("6. market cap (USD)")]
        public decimal MarketCap { get; set; }
    }
}
