using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.OHLC
{


    public class OHLCCryptoModel
    {
        [JsonIgnore]
        public string Symbol { get; set; }
        [JsonIgnore]
        public DateTime Time { get; set; }
        [JsonProperty("1b. open (USD)")]
        public decimal OpenUSD { get; set; }
        [JsonProperty("2b. high (USD)")]
        public decimal HighUSD { get; set; }
        [JsonProperty("3b. low (USD)")]
        public decimal LowUSD { get; set; }
        [JsonProperty("4b. close (USD)")]
        public decimal CloseUSD { get; set; }
    }
}
