using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models;
using CurrencyExchangeLibrary.Models.Currency;
using CurrencyExchangeLibrary.Models.OUTPUT;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PredictLibrary;

namespace StockExchangeSystem_Server.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [EnableCors("http://mywebclient.azurewebsites.net")]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ILogger<CurrencyModel> _logger;

        public CurrencyController(ICurrencyRepository currencyRepository, ILogger<CurrencyModel> logger)
        {
            _currencyRepository = currencyRepository;
            _logger = logger;
        }

        //Get
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CurrencyOutModel>))]
        public async Task<IActionResult> GetCurrenciesAsync()
        {

            try
            {
                _logger.LogInformation("Attempting to receive all currencies from database");
                var currency = await _currencyRepository.GetCurrenciesAsync();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("All currencies were send");
                return Ok(currency);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while reveiving data from database");
                throw new Exception("Error");
            }

        }

        [HttpGet("{symbol}")]
        [ProducesResponseType(200, Type = typeof(CurrencyModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCurrencyAsync(string symbol)
        {
            try
            {
                if (!(await _currencyRepository.CurrencyExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} data from database", symbol);
                var crypto = await _currencyRepository.GetCurrencyAsync(symbol);

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

        [HttpGet("WEEKLY/{symbol}")]
        [ProducesResponseType(200, Type = typeof(CurrencyModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWeeklyCurrencyAsync(string symbol)
        {

            try
            {
                if (!(await _currencyRepository.CurrencyExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} weekly data from database", symbol);
                var crypto = await _currencyRepository.GetWeeklyCurrencyAsync(symbol);

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

        [HttpGet("MONTHLY/{symbol}")]
        [ProducesResponseType(200, Type = typeof(CurrencyModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMonthlyCryptoAsync(string symbol)
        {
            try
            {
                if (!(await _currencyRepository.CurrencyExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} monthly data from database", symbol);
                var crypto = await _currencyRepository.GetMonthlyCurrencyAsync(symbol);

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

        [HttpGet("predict/{symbol}")]
        [ProducesResponseType(200, Type = typeof(CurrencyModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCurrencyPredictionAsync(string symbol)
        {
            try
            {
                if (!(await _currencyRepository.CurrencyExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} data from database", symbol);
                var currency = await _currencyRepository.GetCurrencyAsync(symbol);

                PredictCurrency predict = new PredictCurrency(_currencyRepository);

                var predicted = await predict.predict(symbol);
                currency.OHLCData.AddRange(predicted);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(currency);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while reveiving data from database");
                throw new Exception("Error");
            }

        }

        [HttpGet("proposition")]
        [ProducesResponseType(200, Type = typeof(List<CurrencyOutModel>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCurrencyPropositionAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to receive all crypto from database");
                var currency = await _currencyRepository.GetBestCurrencyAsync();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("All crypto was send");
                return Ok(currency);
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
        public async Task<IActionResult> CreateCurrencyAsync([FromBody] SymbolModel symbol)
        {
            try
            {

                _logger.LogInformation("Attempting to add new currency {code}", symbol.Symbol);
                if (await _currencyRepository.CurrencyExistAsync(symbol.Symbol))
                {
                    _logger.LogInformation("Currency already exist in database");
                    ModelState.AddModelError("", "Currency already exist");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!(await _currencyRepository.CreateCurrencyAsync(symbol.Symbol)))
                {
                    _logger.LogInformation("Something went wrong while saving data in database");
                    ModelState.AddModelError("", "Something went wrong while adding currency");
                    return StatusCode(500, ModelState);
                }
                if (!(await _currencyRepository.UpdateCurrencyValueAsync(symbol.Symbol)))
                {
                    _logger.LogInformation("Something went wrong while saving data in database");
                    ModelState.AddModelError("", "Something went wrong while adding currency");
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
        public async Task<IActionResult> RefreshCurrencyValueAsync(string symbol)
        {
            try
            {
                if (!(await _currencyRepository.CurrencyExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("Refreshing {code} current value in database", symbol);
                await _currencyRepository.UpdateCurrencyValueAsync(symbol);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while refreshing data");
                throw new Exception("Error");
            }

        }

        [HttpPut("refresh/{symbol}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RefreshCurrencyAsync(string symbol)
        {
            try
            {
                if (!(await _currencyRepository.CurrencyExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("Refreshing {code} data in database", symbol);
                await _currencyRepository.UpdateCurrencyModelAsync(symbol);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while refreshing data");
                throw new Exception("Error");
            }

        }


        [HttpGet("Exist/{symbol}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CurrencyExistAsync(string symbol)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var exist = await _currencyRepository.CurrencyExistAsync(symbol);

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
