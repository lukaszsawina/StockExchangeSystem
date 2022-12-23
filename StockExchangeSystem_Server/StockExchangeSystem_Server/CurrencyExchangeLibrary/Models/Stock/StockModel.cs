using CurrencyExchangeLibrary.Models.OHLC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.Stock
{
    public class StockModel
    {
        [JsonIgnore]
        public int ID { get; set; }
        [JsonIgnore]
        public decimal CurrentValue { get; set; }
        [JsonProperty("Meta Data")]
        public StockDataModel MetaData { get; set; }
        public List<OHLCVStockModel> OHLCVData { get; set; } = new List<OHLCVStockModel>();
    }
}
