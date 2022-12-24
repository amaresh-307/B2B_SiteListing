using B2B_SiteListing.Data.Entities;
using B2B_SiteListing.Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace B2B_SiteListing.Controllers
{
    [Route("auth/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            ApplicationUser user;
            if (model.UsernameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(model.UsernameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(model.UsernameOrEmail);
            }
            if (user == null)
            {
                ModelState.AddModelError("", "User does not exist.");
                return View(model);
            }
            var result = await
            _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Login failed.");
                return View(model);
            }
            if (ReturnUrl != null) return LocalRedirect(ReturnUrl);
            return RedirectToAction("LogInDetails", "LogInDetails");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) return View();
            ApplicationUser newUser = new ApplicationUser
            {
                Email = register.Email,
                UserName = register.Username
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(register);
            }

            if (!await _roleManager.RoleExistsAsync(Roles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            if (!await _roleManager.RoleExistsAsync(Roles.User))
                await _roleManager.CreateAsync(new IdentityRole(Roles.User));

            //add user to role - `User` & `Admin`
            await _userManager.AddToRoleAsync(newUser, Roles.Admin);
            //await _userManager.AddToRoleAsync(newUser, Roles.User);

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
