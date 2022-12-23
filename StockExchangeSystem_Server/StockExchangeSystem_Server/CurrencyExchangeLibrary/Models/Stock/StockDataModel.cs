using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.Stock
{
    public class StockDataModel
    {
        [JsonIgnore]
        public int ID { get; set; }
        [JsonProperty("1. Information")]
        public string Information { get; set; }
        [JsonProperty("2. Symbol")]
        public string Symbol { get; set; }
        [JsonProperty("3. Last Refreshed")]
        public DateTime LastRefreshed { get; set; }
        [JsonProperty("5. Time Zone")]
        public string TimeZone { get; set; }
    }
}
