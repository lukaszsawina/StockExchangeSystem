using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Models
{

    /*
     * 
     *  "1. From_Currency Code": "BTC",
        "2. From_Currency Name": "Bitcoin",
        "3. To_Currency Code": "USD",
        "4. To_Currency Name": "United States Dollar",
        "5. Exchange Rate": "16720.96000000",
        "6. Last Refreshed": "2022-11-20 00:48:01",
        "7. Time Zone": "UTC",
        "8. Bid Price": "16720.83000000",
        "9. Ask Price": "16720.95000000"
    */
    public class ValueModel
    {
        [JsonProperty("5. Exchange Rate")]
        public decimal ExchangeRate { get; set; }
    }
}
