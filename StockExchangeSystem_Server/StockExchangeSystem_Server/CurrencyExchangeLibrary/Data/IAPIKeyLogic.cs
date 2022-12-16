using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Data
{
    public interface IAPIKeyLogic
    {
        Task<string> GetKeyAsync();
    }
}
