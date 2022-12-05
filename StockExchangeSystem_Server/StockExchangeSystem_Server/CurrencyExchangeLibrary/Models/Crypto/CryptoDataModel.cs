using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.Crypto
{
    public class CryptoDataModel
    {
        [JsonIgnore]
        public int ID { get; set; }
        [JsonProperty("1. Information")]
        public string Information { get; set; }
        [JsonProperty("2. Digital Currency Code")]
        public string DCCode { get; set; }
        [JsonProperty("3. Digital Currency Name")]
        public string DCName { get; set; }
        [JsonProperty("4. Market Code")]
        public string MarketCode { get; set; }
        [JsonProperty("5. Market Name")]
        public string MarketName { get; set; }
        [JsonProperty("6. Last Refreshed")]
        public DateTime LastRefreshed { get; set; }
        [JsonProperty("7. Time Zone")]
        public string TimeZone { get; set; }

    }
}
