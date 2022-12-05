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

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountModel>))]
        public async Task<IActionResult> GetAccountsAsync()
        {
            var accounts = await _accountRepository.GetAccountsAsync();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(accounts);
        }
        [HttpGet("id")]
        [ProducesResponseType(200, Type = typeof(AccountModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAccountAsync(int id)
        {
            if (!(await _accountRepository.AccountExistAsync(id)))
                return NotFound();

            var account = await _accountRepository.GetAccountAsync(id);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(account);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAccountsAsync(string email)
        {
            if (!(await _accountRepository.AccountExistAsync(email)))
                return NotFound();

            var account = await _accountRepository.GetAccountAsync(email);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(account);
        }

        //TODO: Add AccountExist
    }
}
