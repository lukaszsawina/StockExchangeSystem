using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.Currency
{
    public class CurrencyDataModel
    {
        [JsonIgnore]
        public int ID { get; set; }
        [JsonProperty("1. Information")]
        public string Information { get; set; }
        [JsonProperty("2. From Symbol")]
        public string fromSymbol { get; set; }
        [JsonProperty("3. To Symbol")]
        public string toSymbol { get; set; }
        [JsonProperty("5. Last Refreshed")]
        public DateTime LastRefreshed { get; set; }
        [JsonProperty("6. Time Zone")]
        public string TimeZone { get; set; }
    }
}
