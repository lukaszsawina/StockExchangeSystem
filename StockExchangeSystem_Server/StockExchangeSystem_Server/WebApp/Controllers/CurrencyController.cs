using CurrencyExchangeLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
