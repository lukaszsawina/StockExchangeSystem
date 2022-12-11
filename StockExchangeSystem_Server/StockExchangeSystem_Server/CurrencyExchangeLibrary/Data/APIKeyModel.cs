using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Data
{
    public class APIKeyModel
    {
        public string key { get; set; }
        public DateTime LastUsed { get; set; }
        public APIKeyModel(string newKey)
        {
            key = newKey;
            LastUsed = DateTime.Now;
        }
        public APIKeyModel()
        {

        }
    }
}
