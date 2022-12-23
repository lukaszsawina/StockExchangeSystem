using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.OHLC
{
    public class OHLCCurrencyModel
    {
        [JsonIgnore]
        public string Symbol { get; set; }
        [JsonIgnore]
        public DateTime Time { get; set; }
        [JsonProperty("1. open")]
        public decimal Open { get; set; }
        [JsonProperty("2. high")]
        public decimal High { get; set; }
        [JsonProperty("3. low")]
        public decimal LowUSD { get; set; }
        [JsonProperty("4. close")]
        public decimal CloseUSD { get; set; }
    }
}
