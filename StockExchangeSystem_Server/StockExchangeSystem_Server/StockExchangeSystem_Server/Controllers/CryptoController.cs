using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models;
using CurrencyExchangeLibrary.Models.Crypto;
using CurrencyExchangeLibrary.Models.OHLC;
using CurrencyExchangeLibrary.Models.OUTPUT;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PredictLibrary;
using StockExchangeSystem_Server.PeriodicServices;

namespace StockExchangeSystem_Server.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [EnableCors("http://mywebclient.azurewebsites.net")]
    public class CryptoController : Controller
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly ILogger<CryptoController> _logger;
        private readonly IRefreshLogic _refreshLogic;

        public CryptoController(ICryptoRepository cryptoRepository, ILogger<CryptoController> logger, IRefreshLogic refreshLogic)
        {
            _cryptoRepository = cryptoRepository;
            _logger = logger;
            _refreshLogic = refreshLogic;
        }

        //Get
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CryptoOutModel>))]
        public async Task<IActionResult> GetCryptosAsync()
        {

            try
            {
                _logger.LogInformation("Attempting to receive all crypto from database");
                var crypto = await _cryptoRepository.GetCryptosAsync();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("All crypto was send");
                return Ok(crypto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while reveiving data from database");
                throw new Exception("Error");
            }
            
        }

        [HttpGet("{symbol}")]
        [ProducesResponseType(200, Type = typeof(CryptoModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCryptoAsync(string symbol)
        {
            try
            {
                if (!(await _cryptoRepository.CryptoExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} data from database",symbol);
                var crypto = await _cryptoRepository.GetCryptoAsync(symbol);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(crypto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while reveiving data from database");
                throw new Exception("Error");
            }
            
        }

        [HttpGet("WEEKLY/{symbol}")]
        [ProducesResponseType(200, Type = typeof(CryptoModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWeeklyCryptoAsync(string symbol)
        {

            try
            {
                if (!(await _cryptoRepository.CryptoExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive  {code} weekly data from database", symbol);
                var crypto = await _cryptoRepository.GetWeeklyCryptoAsync(symbol);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(crypto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while reveiving data from database");
                throw new Exception("Error");
            }
        }

        [HttpGet("MONTHLY/{symbol}")]
        [ProducesResponseType(200, Type = typeof(CryptoModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMonthlyCryptoAsync(string symbol)
        {
            try
            {
                if (!(await _cryptoRepository.CryptoExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} monthly data from database", symbol);
                var crypto = await _cryptoRepository.GetMonthlyCryptoAsync(symbol);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(crypto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while reveiving data from database");
                throw new Exception("Error");
            }
        }

        [HttpGet("predict/{symbol}")]
        [ProducesResponseType(200, Type = typeof(CryptoModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCryptoPredictionAsync(string symbol)
        {
            try
            {
                if (!(await _cryptoRepository.CryptoExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} data from database", symbol);
                var crypto = await _cryptoRepository.GetCryptoAsync(symbol);

                PredictCrypto predict = new PredictCrypto(_cryptoRepository);

                var predicted = await predict.predict(symbol);
                crypto.OHLCVCryptoData.AddRange(predicted);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(crypto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while reveiving data from database");
                throw new Exception("Error");
            }

        }

        [HttpGet("proposition")]
        [ProducesResponseType(200, Type = typeof(List<CryptoOutModel>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCryptoPropositionAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to receive all crypto from database");
                var crypto = await _cryptoRepository.GetBestCryptoAsync();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("All crypto was send");
                return Ok(crypto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while reveiving data from database");
                throw new Exception("Error");
            }

        }

        //Post
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCrypto([FromBody] SymbolModel symbol)
        {
            try
            {

                _logger.LogInformation("Attempting to add new crypto {code}", symbol.Symbol);
                if (await _cryptoRepository.CryptoExistAsync(symbol.Symbol))
                {
                    _logger.LogInformation("Crypto already exist in database");
                    ModelState.AddModelError("", "Crypto already exist");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!(await _cryptoRepository.CreateCryptoAsync(symbol.Symbol)))
                {
                    _logger.LogInformation("Something went wrong while saving data in database");
                    ModelState.AddModelError("", "Something went wrong while adding crypto");
                    return StatusCode(500, ModelState);
                }

                if (!(await _cryptoRepository.UpdateCryptoCurrentAsync(symbol.Symbol)))
                {
                    _logger.LogInformation("Something went wrong while saving data in database");
                    ModelState.AddModelError("", "Something went wrong while adding crypto");
                    return StatusCode(500, ModelState);
                }
                

                return Ok("Succesfully created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while saving data in database");
                throw new Exception("Error");
            }
            
        }

        //Put
        [HttpPut("refresh/current/{symbol}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RefreshCurrentCryptoAsync(string symbol)
        {
            try
            {
                if (!(await _cryptoRepository.CryptoExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("Refreshing {code} current value in database", symbol);
                await _cryptoRepository.UpdateCryptoCurrentAsync(symbol);

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while refreshing data");
                throw new Exception("Error");
            }
            
        }
        [HttpPut("refresh/{symbol}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RefreshCryptoAsync(string symbol)
        {
            try
            {
                if (!(await _cryptoRepository.CryptoExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("Refreshing {code} data in database", symbol);
                await _cryptoRepository.UpdateCryptoModelAsync(symbol);

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while refreshing data");
                throw new Exception("Error");
            }
            
        }

        [HttpGet("Exist/{symbol}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CryptoExistAsync(string symbol)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
             
                var exist = await _cryptoRepository.CryptoExistAsync(symbol);

                return Ok(exist);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while reveiving data from database");
                throw new Exception("Error");
            }

        }
    }
}
