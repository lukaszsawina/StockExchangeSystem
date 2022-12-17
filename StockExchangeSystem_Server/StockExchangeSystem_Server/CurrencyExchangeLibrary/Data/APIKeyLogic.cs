using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.Data
{
    public class APIKeyLogic : IAPIKeyLogic
    {
        private readonly ILogger<APIKeyLogic> _logger;

        public static APIKeyModel ApiKey { get; set; }

        public APIKeyLogic(ILogger<APIKeyLogic> logger)
        {
            if (ApiKey is null)
                InitKeys();
            _logger = logger;
        }

        private void InitKeys()
        {
            ApiKey = new APIKeyModel("H8T0LC277CGD8MKO");
        }

        public async Task<string> GetKeyAsync()
        {
            _logger.LogInformation("Attempt to get API Key");
            while (ApiKey.LastUsed > DateTime.Now.AddSeconds(-20))
            {
                await Task.Delay(500);
            }


            ApiKey.LastUsed = DateTime.Now;
            _logger.LogInformation("Api key recived");
            return ApiKey.key;
        }

    }
}
