using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models
{
    public class RealTimeModel
    {
        [JsonProperty("Realtime Currency Exchange Rate")]
        public ValueModel value { get; set; }
    }
}
