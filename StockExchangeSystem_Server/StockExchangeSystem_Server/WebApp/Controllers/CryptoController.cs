using CurrencyExchangeLibrary.Data;
using CurrencyExchangeLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CryptoController : Controller
    {
        private readonly ICryptoRepository _cryptoRepository;

        public CryptoController(ICryptoRepository cryptoRepository)
        {
            _cryptoRepository = cryptoRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var crypto = await _cryptoRepository.GetCryptosAsync();
            return View(crypto);
        }

        public async Task<IActionResult> CryptoPage(string symbol)
        {
            var crypto = await _cryptoRepository.GetCryptoAsync(symbol);
            var OHLCV = await _cryptoRepository.GetCryptoOHLCVAsync(symbol);

            var output = new CryptoViewModel()
            { 
                Crypto = crypto,
                OHLCV = OHLCV
            };


            return View(output);
        }

    }
}
