using CurrencyExchangeLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeLibrary
{
    public class RefreshLogic : IRefreshLogic
    {
        private readonly ICryptoRepository _cryptoRepository;

        public RefreshLogic(ICryptoRepository cryptoRepository)
        {
            _cryptoRepository = cryptoRepository;
        }

        public async Task Refresh()
        {
            foreach(var c in await _cryptoRepository.GetCryptoCodesAsync())
            {
                Console.WriteLine($"Refresh! {c}");
                _cryptoRepository.UpdateCryptoCurrentAsync(c);
            }
        }
    }
}
