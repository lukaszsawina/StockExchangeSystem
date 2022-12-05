using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models.Crypto
{
    public class RealTimeCryptoModel
    {
        [JsonProperty("Realtime Currency Exchange Rate")]
        public ValueModel value { get; set; }
    }
}
