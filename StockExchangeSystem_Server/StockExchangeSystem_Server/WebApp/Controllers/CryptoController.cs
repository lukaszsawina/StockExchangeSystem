﻿using CurrencyExchangeLibrary.Data;
using CurrencyExchangeLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    }
}
