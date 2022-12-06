using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Data
{
    public class APIKeyLogic : IAPIKeyLogic
    {
        public static APIKeyModel ApiKey { get; set; }

        public APIKeyLogic()
        {
            if (ApiKey is null)
                InitKeys();
        }

        private void InitKeys()
        {
            ApiKey = new APIKeyModel("H8T0LC277CGD8MKO");
        }

        public async Task<string> GetKeyAsync()
        {

            while (ApiKey.LastUsed > DateTime.Now.AddSeconds(-20))
                await Task.Delay(200);

            ApiKey.LastUsed = DateTime.Now;
            return ApiKey.key;
        }

    }
}
