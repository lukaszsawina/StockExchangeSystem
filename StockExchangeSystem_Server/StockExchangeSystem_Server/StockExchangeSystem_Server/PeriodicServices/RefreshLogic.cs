using CurrencyExchangeLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeSystem_Server.PeriodicServices
{
    public class RefreshLogic : IRefreshLogic
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly ILogger<RefreshLogic> _logger;

        public RefreshLogic(ICryptoRepository cryptoRepository, ILogger<RefreshLogic> logger)
        {
            _cryptoRepository = cryptoRepository;
            _logger = logger;
        }

        public async Task Refresh()
        {
            foreach (var c in await _cryptoRepository.GetCryptoCodesAsync())
            {
                _logger.LogInformation("Refreshing crypto with code: {code}",c);
                await _cryptoRepository.UpdateCryptoCurrentAsync(c);
            }
        }
    }
}
