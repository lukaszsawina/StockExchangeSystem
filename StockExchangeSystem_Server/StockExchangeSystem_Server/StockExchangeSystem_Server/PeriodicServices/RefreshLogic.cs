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

            var cryptosSymbol = await _cryptoRepository.GetCryptoCodesAsync();
            foreach (var c in cryptosSymbol.Select((symbol, index) => (symbol, index)))
            {
                _logger.LogInformation("Refreshing crypto current value with code: {code} | {current}/{whole}",c.symbol,c.index,cryptosSymbol.Count);
                await _cryptoRepository.UpdateCryptoCurrentAsync(c.symbol);
            }
        }

        public async Task StartUpAppRefresh()
        {
            var cryptosSymbol = await _cryptoRepository.GetCryptoCodesAsync();
            foreach (var c in cryptosSymbol.Select((symbol, index) => (symbol, index)))
            {
                _logger.LogInformation("Refreshing crypto data with code: {code} | {current}/{whole}", c.symbol, c.index, cryptosSymbol.Count);
                await _cryptoRepository.UpdateCryptoModelAsync(c.symbol);
            }
        }
    }
}
