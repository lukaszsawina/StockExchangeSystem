using CurrencyExchangeLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
