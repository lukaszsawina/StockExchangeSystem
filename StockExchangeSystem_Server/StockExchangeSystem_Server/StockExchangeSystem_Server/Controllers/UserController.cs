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
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, IAccountRepository accountRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserModel>))]
        public async Task<IActionResult> GetUsersAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to receive all users from database");
                var users = await _userRepository.GetUsersAsync();

                if (!ModelState.IsValid)
                    return BadRequest();

                _logger.LogInformation("All users were send");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while showing all users");
                throw new Exception("Error");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            try
            {
                if (!(await _accountRepository.AccountExistAsync(id)))
                {
                    _logger.LogInformation("{code} don't exist in database", id);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} data from database", id);
                var user = await _userRepository.GetUserAsync(id);

                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while showing user {id}", id);
                throw new Exception("Error");
            }
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserAsync(string email)
        {
            try
            {
                if (!(await _accountRepository.AccountExistAsync(email)))
                {
                    _logger.LogInformation("{code} don't exist in database", email);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} data from database", email);
                var user = _userRepository.GetUserAsync(email);

                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while showing user {email}", email);
                throw new Exception("Error");
            }
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserModel user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogInformation("Invalid form data");
                    return BadRequest(ModelState);
                }

                if(await _accountRepository.AccountExistAsync(user.ID))
                {
                    _logger.LogInformation("{code} already exist in database", user.Email);

                    ModelState.AddModelError("", "User already exist");
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                user.CreateTime = DateTime.Today;

                _logger.LogInformation("Attempt to add new User: {email}",user.Email);
                if (!(await _userRepository.CreateUserAsync(user)))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

                return Ok("Succesfully created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding user {email}", user.Email);
                throw new Exception("Error");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserModel user)
        {
            try
            {
                if(user == null)
                {
                    _logger.LogInformation("Invalid form data");
                    return BadRequest(ModelState);
                }

                if (id != user.ID)
                {
                    _logger.LogInformation("Invalid ID");
                    return BadRequest(ModelState);
                }

                if (!(await _accountRepository.AccountExistAsync(id)))
                {
                    _logger.LogInformation("{code} don't exist in database", user.Email);
                    return NotFound();
                }

                _logger.LogInformation("Attempt to update User: {email}", user.Email);

                if (!await _userRepository.UpdateUserAsync(user))
                {
                    ModelState.AddModelError("", "Something went wrong updating");
                    return StatusCode(500, ModelState);
                }
                _logger.LogInformation("User updated");

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while logging account {email}", user.Email);
                throw new Exception("Error");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
            {
                if (!(await _accountRepository.AccountExistAsync(id)))
                {
                    _logger.LogInformation("{code} don't exist in database", id);
                    return NotFound();
                }

                _logger.LogInformation("Attempt to recive {id} data from database", id);
                var userToDelete = await _userRepository.GetUserAsync(id);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("Attempt to delete {id} data from database", id);
                if (!(await _userRepository.DeleteUserAsync(userToDelete)))
                {
                    _logger.LogInformation("Attempt to recive {id} data from database", id);

                    ModelState.AddModelError("", "Something went wrong deleting category");
                    return BadRequest(ModelState);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while logging account {email}", id);
                throw new Exception("Error");
            }
        }
    }
}
