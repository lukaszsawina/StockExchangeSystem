using CurrencyExchangeLibrary.Models.OHLC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.Currency
{
    public class CurrencyModel
    {
        [JsonIgnore]
        public int ID { get; set; }
        [JsonIgnore]
        public decimal CurrentValue { get; set; }
        [JsonProperty("Meta Data")]
        public CurrencyDataModel MetaData { get; set; }
        public List<OHLCCryptoModel> OHLCData { get; set; } = new List<OHLCCryptoModel>();
    }
}
