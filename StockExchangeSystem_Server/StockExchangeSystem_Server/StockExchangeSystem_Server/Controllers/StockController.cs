using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models;
using CurrencyExchangeLibrary.Models.OUTPUT;
using CurrencyExchangeLibrary.Models.Stock;
using Microsoft.AspNetCore.Mvc;

namespace StockExchangeSystem_Server.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<StockController> _logger;

        public StockController(IStockRepository stockRepository, ILogger<StockController> logger )
        {
            _stockRepository = stockRepository;
            _logger = logger;
        }

        //Get
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StockOutModel>))]
        public async Task<IActionResult> GetStocksAsync()
        {

            try
            {
                _logger.LogInformation("Attempting to receive all crypto from database");
                var crypto = await _stockRepository.GetStocksAsync();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("All stocks were send");
                return Ok(crypto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while reveiving data from database");
                throw new Exception("Error");
            }

        }

        [HttpGet("{symbol}")]
        [ProducesResponseType(200, Type = typeof(StockModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStockAsync(string symbol)
        {
            try
            {
                if (!(await _stockRepository.StockExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} data from database", symbol);
                var crypto = await _stockRepository.GetStockAsync(symbol);

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
        [ProducesResponseType(200, Type = typeof(StockModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWeeklyStockAsync(string symbol)
        {

            try
            {
                if (!(await _stockRepository.StockExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive  {code} weekly data from database", symbol);
                var crypto = await _stockRepository.GetWeeklyStockAsync(symbol);

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
        [ProducesResponseType(200, Type = typeof(StockModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMonthlyStockAsync(string symbol)
        {
            try
            {
                if (!(await _stockRepository.StockExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} monthly data from database", symbol);
                var crypto = await _stockRepository.GetMonthlyStockAsync(symbol);

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

        //Post
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateStock([FromBody] SymbolModel symbol)
        {
            try
            {
                _logger.LogWarning("Attempting to add new stock {code}", symbol.Symbol);
                if (await _stockRepository.StockExistAsync(symbol.Symbol))
                {
                    _logger.LogCritical("Crypto already exist in database");
                    ModelState.AddModelError("", "Crypto already exist");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!(await _stockRepository.CreateStockAsync(symbol.Symbol)))
                {
                    _logger.LogError("Something went wrong while saving data in database");
                    ModelState.AddModelError("", "Something went wrong while adding stock");
                    return StatusCode(500, ModelState);
                }

                if (!(await _stockRepository.UpdateStockCurrentAsync(symbol.Symbol)))
                {
                    _logger.LogError("Something went wrong while saving data in database");
                    ModelState.AddModelError("", "Something went wrong while adding stock");
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
        public async Task<IActionResult> RefreshCurrentStockAsync(string symbol)
        {
            try
            {
                if (!(await _stockRepository.StockExistAsync(symbol)))
                {
                    _logger.LogError("{code} don't exist in database", symbol);
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogWarning("Refreshing {code} current value in database", symbol);
                await _stockRepository.UpdateStockCurrentAsync(symbol);

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
        public async Task<IActionResult> RefreshStockAsync(string symbol)
        {
            try
            {
                if (!(await _stockRepository.StockExistAsync(symbol)))
                {
                    _logger.LogInformation("{code} don't exist in database", symbol);
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("Refreshing {code} data in database", symbol);
                await _stockRepository.UpdateStockModelAsync(symbol);

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
        public async Task<IActionResult> StockExistAsync(string symbol)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var exist = await _stockRepository.StockExistAsync(symbol);
                
                return Ok(exist);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while refreshing data");
                throw new Exception("Error");
            }

        }
    }
}
