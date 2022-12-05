using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.OHLC
{
    public class OHLCVModel : OHLCModel
    {
        [JsonProperty("5. volume")]
        public decimal Volume { get; set; }
        [JsonProperty("6. market cap (USD)")]
        public decimal MarketCap { get; set; }
    }
}
