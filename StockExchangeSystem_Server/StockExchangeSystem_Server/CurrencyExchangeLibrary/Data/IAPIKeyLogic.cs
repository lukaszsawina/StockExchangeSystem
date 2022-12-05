using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Data
{
    public interface IAPIKeyLogic
    {
        static List<APIKeyModel> ApiKeys { get; set; } = new List<APIKeyModel>();
        Task<string> GetKeyAsync();
    }
}
