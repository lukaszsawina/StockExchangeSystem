using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.Stock
{
    public class GlobalQuote
    {
        [JsonProperty("Global Quote")]
        public StockValueModel ValueData { get; set; }
    }
}
