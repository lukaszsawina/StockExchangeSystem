using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace StockExchangeSystem_Server.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public UserController(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserModel>))]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            if (!(await _accountRepository.AccountExistAsync(id)))
                return NotFound();

            UserModel user = await _userRepository.GetUserAsync(id);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserAsync(string email)
        {
            if (!(await _accountRepository.AccountExistAsync(email)))
                return NotFound();

            var user = _userRepository.GetUserAsync(email);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserModel user)
        {
            if (user == null)
                return BadRequest(ModelState);

            if(await _accountRepository.AccountExistAsync(user.ID))
            {
                ModelState.AddModelError("", "User already exist");
                return BadRequest();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            user.CreateTime = DateTime.Today;

            if (!(await _userRepository.CreateUserAsync(user)))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserModel user)
        {
            if(user == null)
                return BadRequest(ModelState);

            if (id != user.ID)
                return BadRequest(ModelState);

            if (!(await _accountRepository.AccountExistAsync(id)))
                return NotFound();

            if (!await _userRepository.UpdateUserAsync(user))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            if (!(await _accountRepository.AccountExistAsync(id)))
                return NotFound();

            var userToDelete = await _userRepository.GetUserAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!(await _userRepository.DeleteUserAsync(userToDelete)))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
