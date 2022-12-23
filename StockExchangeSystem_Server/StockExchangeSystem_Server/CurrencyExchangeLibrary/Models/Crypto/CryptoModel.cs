using CurrencyExchangeLibrary.Models.OHLC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.Crypto
{
    public class CryptoModel
    {
        [JsonIgnore]
        public int ID { get; set; }
        [JsonIgnore]
        public decimal CurrentValue { get; set; }
        [JsonProperty("Meta Data")]
        public CryptoDataModel MetaData { get; set; }
        public List<OHLCVCryptoModel> OHLCVCryptoData { get; set; } = new List<OHLCVCryptoModel>();
    }
}
