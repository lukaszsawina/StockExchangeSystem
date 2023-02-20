using CurrencyExchangeLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<IActionResult> Index()
        {
            var stock = await _stockRepository.GetStocksAsync();
            return View(stock);
        }
        public async Task<IActionResult> StockPage(string symbol)
        {
            var stock = await _stockRepository.GetStockAsync(symbol);
            var ohlcv = await _stockRepository.GetStockOHLCVAsync(symbol);
            var output = new StockViewModel()
            {
                Stock = stock,
                OHLCV = ohlcv
            };
            return View(output);
        }
    }
}
