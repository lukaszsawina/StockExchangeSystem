using CurrencyExchangeLibrary.Interfaces;
using CurrencyExchangeLibrary.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace StockExchangeSystem_Server.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminRepository adminRepository, ILogger<AdminController> logger)
        {
            _adminRepository = adminRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AdminModel>))]
        public async Task<IActionResult> GetAdminsAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to receive all admins from database");
                var accounts = await _adminRepository.GetAdminsAsync();

                if (!ModelState.IsValid)
                    return BadRequest();

                _logger.LogInformation("All admins were send");
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while showing all admins");
                throw new Exception("Error");
            }
        }
        [HttpGet("id")]
        [ProducesResponseType(200, Type = typeof(AdminModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAdminAsync(int id)
        {
            try
            {
                if (!(await _adminRepository.IsAdmin(id)))
                {
                    _logger.LogInformation("{code} don't exist in database", id);
                    return NotFound();
                }

                _logger.LogInformation("Attempting to receive {code} data from database", id);
                var account = await _adminRepository.GetAdminAsync(id);

                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(account);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while showing admin {id}", id);
                throw new Exception("Error");
            }
        }
        [HttpGet("isAdmin/{id}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> IsAdminAsync(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to receive {code} data from database", id);
                var account = await _adminRepository.IsAdmin(id);

                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(account);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while showing admin {id}", id);
                throw new Exception("Error");
            }
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAdminAsync([FromBody] AdminModel admin)
        {
            try
            {
                if (admin == null)
                {
                    _logger.LogInformation("Invalid form data");
                    return BadRequest(ModelState);
                }

                if (await _adminRepository.IsAdmin(admin.ID))
                {
                    _logger.LogInformation("{code} already exist in database", admin.Email);

                    ModelState.AddModelError("", "Admin already exist");
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                admin.CreateTime = DateTime.Today;

                _logger.LogInformation("Attempt to add new Admin: {email}", admin.Email);
                if (!(await _adminRepository.CreateAdminAsync(admin)))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

                return Ok("Succesfully created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding admin {email}", admin.Email);
                throw new Exception("Error");
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAdminAsync(int id, [FromBody] AdminModel admin)
        {
            try
            {
                if (admin == null)
                {
                    _logger.LogInformation("Invalid form data");
                    return BadRequest(ModelState);
                }

                if (id != admin.ID)
                {
                    _logger.LogInformation("Invalid ID");
                    return BadRequest(ModelState);
                }

                if (!(await _adminRepository.IsAdmin(id)))
                {
                    _logger.LogInformation("{code} don't exist in database", admin.Email);
                    return NotFound();
                }

                _logger.LogInformation("Attempt to update Admin: {email}", admin.Email);

                if (!await _adminRepository.UpdateAdminAsync(admin))
                {
                    ModelState.AddModelError("", "Something went wrong updating");
                    return StatusCode(500, ModelState);
                }
                _logger.LogInformation("Admin updated");

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating account {email}", admin.Email);
                throw new Exception("Error");
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAdminAsync(int id)
        {
            try
            {
                if (!(await _adminRepository.IsAdmin(id)))
                {
                    _logger.LogInformation("{code} don't exist in database", id);
                    return NotFound();
                }

                _logger.LogInformation("Attempt to recive {id} data from database", id);
                var userToDelete = await _adminRepository.GetAdminAsync(id);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("Attempt to delete {id} data from database", id);
                if (!(await _adminRepository.DeleteAdminAsync(userToDelete)))
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
