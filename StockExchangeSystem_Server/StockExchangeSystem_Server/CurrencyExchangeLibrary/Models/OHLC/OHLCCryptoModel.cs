using Microsoft.ML.Data;
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
        [JsonIgnore, LoadColumn(5)]
        public string Symbol { get; set; }
        [JsonIgnore, LoadColumn(6)]
        public DateTime Time { get; set; }
        [JsonProperty("1b. open (USD)"), LoadColumn(4)]
        public decimal OpenUSD { get; set; }
        [JsonProperty("2b. high (USD)"), LoadColumn(2)]
        public decimal HighUSD { get; set; }
        [JsonProperty("3b. low (USD)"), LoadColumn(1)]
        public decimal LowUSD { get; set; }
        [JsonProperty("4b. close (USD)"), LoadColumn(0)]
        public decimal CloseUSD { get; set; }
    }
}
