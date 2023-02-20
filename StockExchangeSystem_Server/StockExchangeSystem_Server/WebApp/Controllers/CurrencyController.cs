using CurrencyExchangeLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<IActionResult> Index()
        {
            var currency = await _currencyRepository.GetCurrenciesAsync();
            return View(currency);
        }
        public async Task<IActionResult> CurrencyPage(string symbol)
        {
            var currency = await _currencyRepository.GetCurrencyAsync(symbol);
            var ohlc = await _currencyRepository.GetCurrencyOHLCAsync(symbol);
            var output = new CurrencyViewModel()
            {
                Currency = currency,
                OHLC = ohlc
            };
            return View(output);
        }
    }
}
