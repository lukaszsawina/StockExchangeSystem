using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly SignInManager<AppUserModel> _signInManager;
        public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {

            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var output = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return View(output);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user)
        {
            if(!ModelState.IsValid) return RedirectToAction("Index", "Account");
            var originUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user.Id != originUser.Id) return RedirectToAction("Index", "Account");

            var editedUser = await _userManager.FindByIdAsync(originUser.Id);
            editedUser.FirstName = user.FirstName;
            editedUser.LastName = user.LastName;
            editedUser.Email = user.Email;
            editedUser.UserName = user.Email;
            editedUser.NormalizedUserName = user.Email.ToUpper();

            var result = await _userManager.UpdateAsync(editedUser);

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Account");
            }

            TempData["Error"] = "Something went wront, sorry!";
            return RedirectToAction("Index", "Account");

        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.Email);

            if(user != null)
            {

                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong password. Pleas try again";
                return View(loginViewModel);
            }
            TempData["Error"] = "Wrong data. Pleas try again";
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByNameAsync(registerViewModel.Email);

            if(user is not null)
            {
                TempData["Error"] = "User already exist!";
                return View(registerViewModel);
            }

            var newUser = new AppUserModel()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
