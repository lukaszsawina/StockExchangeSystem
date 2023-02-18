using CurrencyExchangeLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICryptoRepository _cryptoRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IStockRepository _stockRepository;

        public HomeController(ILogger<HomeController> logger, ICryptoRepository cryptoRepository, ICurrencyRepository currencyRepository, IStockRepository stockRepository)
        {
            _logger = logger;
            _cryptoRepository = cryptoRepository;
            _currencyRepository = currencyRepository;
            _stockRepository = stockRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Crypto"] = await _cryptoRepository.GetBestCryptoAsync();
            ViewData["Currency"] = await _currencyRepository.GetBestCurrencyAsync();
            ViewData["Stock"] = await _stockRepository.GetBestStocksAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}