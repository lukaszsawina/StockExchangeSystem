using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models.Crypto;
using CurrencyExchangeLibrary.Models.OHLC;
using CurrencyExchangeLibrary.Models.OUTPUT;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace StockExchangeSystem_Server.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [EnableCors("http://mywebclient.azurewebsites.net")]
    public class CryptoController : Controller
    {
        private readonly ICryptoRepository _cryptoRepository;

        public CryptoController(ICryptoRepository cryptoRepository)
        {
            _cryptoRepository = cryptoRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CryptoOutModel>))]
        public async Task<IActionResult> GetCryptosAsync()
        {
            var crypto = await _cryptoRepository.GetCryptosAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(crypto);
        }

        [HttpGet("{symbol}")]
        [ProducesResponseType(200, Type = typeof(CryptoModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCryptoAsync(string symbol)
        {
            if (!(await _cryptoRepository.CryptoExistAsync(symbol)))
                return NotFound();

            var crypto = await _cryptoRepository.GetCryptoAsync(symbol);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(crypto);
        }

        [HttpGet("WEEKLY/{symbol}")]
        [ProducesResponseType(200, Type = typeof(CryptoModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWeeklyCryptoAsync(string symbol)
        {
            if (!(await _cryptoRepository.CryptoExistAsync(symbol)))
                return NotFound();

            var crypto = await _cryptoRepository.GetWeeklyCryptoAsync(symbol);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(crypto);
        }

        [HttpGet("MONTHLY/{symbol}")]
        [ProducesResponseType(200, Type = typeof(CryptoModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMonthlyCryptoAsync(string symbol)
        {
            if (!(await _cryptoRepository.CryptoExistAsync(symbol)))
                return NotFound();

            var crypto = await _cryptoRepository.GetMonthlyCryptoAsync(symbol);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(crypto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCrypto([FromBody] string symbol)
        {
            if(await _cryptoRepository.CryptoExistAsync(symbol))
            {
                ModelState.AddModelError("", "Crypto already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!(await _cryptoRepository.CreateCryptoAsync(symbol)))
            {
                ModelState.AddModelError("", "Something went wrong while adding crypto");
                return StatusCode(500, ModelState);
            }
                
            return Ok("Succesfully created");
        }
        [HttpPut("refresh/current/{symbol}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RefreshCurrentCryptoAsync(string symbol)
        {
            if (!(await _cryptoRepository.CryptoExistAsync(symbol)))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _cryptoRepository.UpdateCryptoCurrentAsync(symbol);

            return Ok();
        }
        [HttpPut("refresh/{symbol}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RefreshCryptoAsync(string symbol)
        {
            if (!(await _cryptoRepository.CryptoExistAsync(symbol)))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _cryptoRepository.UpdateCryptoModelAsync(symbol);

            return Ok();
        }
    }
}
