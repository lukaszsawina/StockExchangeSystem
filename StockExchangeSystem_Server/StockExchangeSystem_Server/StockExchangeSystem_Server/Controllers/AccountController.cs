using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace StockExchangeSystem_Server.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AccountModel>))]
        public async Task<IActionResult> GetAccountsAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to receive all account from database");
                var accounts = await _accountRepository.GetAccountsAsync();

                if (!ModelState.IsValid)
                    return BadRequest();

                _logger.LogInformation("All accounts were send");
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while showing all accounts");
                throw new Exception("Error");
            }
        }
        [HttpGet("id")]
        [ProducesResponseType(200, Type = typeof(AccountModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAccountAsync(int id)
        {
            try
            {
                if (!(await _accountRepository.AccountExistAsync(id)))
                {
                    _logger.LogInformation("{code} don't exist in database", id);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} data from database", id);
                var account = await _accountRepository.GetAccountAsync(id);

                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(account);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while showing account {id}", id);
                throw new Exception("Error");
            }
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAccountsAsync(string email)
        {
            try
            {
                if (!(await _accountRepository.AccountExistAsync(email)))
                {
                    _logger.LogInformation("{code} don't exist in database", email);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} data from database", email);
                var account = await _accountRepository.GetAccountAsync(email);

                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while showing account {email}", email);
                throw new Exception("Error");
            }
        }

        [HttpGet("loggin")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LogginAsync([FromBody] AccountModel account)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                if (!(await _accountRepository.LoginAsync(account.Email, account.Password)))
                {
                    _logger.LogInformation("{code} don't exist in database", account.Email);
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while logging account {email}", account.Email);
                throw new Exception("Error");
            }
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAccountAsync(int id, [FromBody] string email)
        {
            try
            {
                if (!(await _accountRepository.AccountExistAsync(id)))
                {
                    _logger.LogInformation("{code} don't exist in database", id);
                    return NotFound();
                }

                var account = await _accountRepository.GetAccountAsync(id);
                account.Email = email;

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("{code} email updated in database", id);
                await _accountRepository.UpdateAccountAsync(account);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while updating data");
                throw new Exception("Error");
            }

        }

    }
}
