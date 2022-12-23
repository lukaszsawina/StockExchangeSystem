using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models
{
    public class ValueModel
    {
        [JsonProperty("5. Exchange Rate")]
        public decimal ExchangeRate { get; set; }
    }
}
