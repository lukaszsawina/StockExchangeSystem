using CurrencyExchangeLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary.PeriodicServices
{
    public class RefreshLogic : IRefreshLogic
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<RefreshLogic> _logger;

        public RefreshLogic(ICryptoRepository cryptoRepository, ICurrencyRepository currencyRepository, IStockRepository stockRepository, ILogger<RefreshLogic> logger)
        {
            _cryptoRepository = cryptoRepository;
            _currencyRepository = currencyRepository;
            _stockRepository = stockRepository;
            _logger = logger;
        }

        public async Task Refresh()
        {
            try
            {
                await RefreshCrypto();
                await RefreshCurrency();
                await RefreshStock();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while data was refreshing");
                throw new Exception();
            }
        }

        private async Task RefreshCrypto()
        {
            var cryptosSymbol = await _cryptoRepository.GetCryptoCodesAsync();
            foreach (var c in cryptosSymbol.Select((symbol, index) => (symbol, index)))
            {
                _logger.LogInformation("Refreshing crypto current value with code: {code} | {current}/{whole}", c.symbol, c.index, cryptosSymbol.Count);
                await _cryptoRepository.UpdateCryptoCurrentAsync(c.symbol);
            }
        }
        private async Task RefreshCurrency()
        {
            var currenciesSymbols = await _currencyRepository.GetCurrenciesCodesAsync();
            foreach (var c in currenciesSymbols.Select((symbol, index) => (symbol, index)))
            {
                _logger.LogInformation("Refreshing currency current value with code: {code} | {current}/{whole}", c.symbol, c.index, currenciesSymbols.Count);
                await _currencyRepository.UpdateCurrencyValueAsync(c.symbol);
            }
        }
        private async Task RefreshStock()
        {
            var stockSymbols = await _stockRepository.GetStocksCodesAsync();
            foreach (var c in stockSymbols.Select((symbol, index) => (symbol, index)))
            {
                _logger.LogInformation("Refreshing stock current value with code: {code} | {current}/{whole}", c.symbol, c.index, stockSymbols.Count);
                await _stockRepository.UpdateStockCurrentAsync(c.symbol);
            }
        }
        public async Task StartUpAppRefresh()
        {

            var cryptosSymbol = await _cryptoRepository.GetCryptoCodesAsync();
            foreach (var c in cryptosSymbol.Select((symbol, index) => (symbol, index)))
            {
                _logger.LogInformation("Refreshing crypto data with code: {code} | {current}/{whole}", c.symbol, c.index + 1, cryptosSymbol.Count);
                await _cryptoRepository.UpdateCryptoModelAsync(c.symbol);
               
            }
            
        }
    }
}
