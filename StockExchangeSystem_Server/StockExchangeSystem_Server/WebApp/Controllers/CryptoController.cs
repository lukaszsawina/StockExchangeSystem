using CurrencyExchangeLibrary.Data;
using CurrencyExchangeLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class CryptoController : Controller
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly DataContext _context;

        public CryptoController(ICryptoRepository cryptoRepository, DataContext context)
        {
            _cryptoRepository = cryptoRepository;
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewData["CryptoTable"] = await _cryptoRepository.GetCryptosAsync();
            ViewData["Cryptos"] = await _context.Crypto.ToListAsync();
            return View();
        }

    }
}
