using B2B_SiteListing.Service;
using B2B_SiteListing.Service.Contracts;
using B2B_SiteListing.Service.Exceptions;
using B2B_SiteListing.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2B_SiteListing.Controllers
{
    [Authorize]
    public class LogInDetailsController : Controller
    {
        private readonly ILogInDetailsService _logInDetailsService;
        private readonly UserService _userService;

        public LogInDetailsController(ILogInDetailsService logInDetailsService, UserService userService)
        {
            _logInDetailsService = logInDetailsService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userService.GetCurrentUserAsync();
            var isDataExist = await _logInDetailsService.GetLogInDetails(loggedInUser.Id);
            ViewBag.IsUpdate = isDataExist is not null ? true : false;
            return isDataExist is not null ? View(isDataExist) : View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LogInDetailsViewModel logInDetailsViewModel)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await this._logInDetailsService.AddLogInDetails(logInDetailsViewModel);
            }
            catch (AlreadyExistException ex)
            {
                return new BadRequestResult();
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
