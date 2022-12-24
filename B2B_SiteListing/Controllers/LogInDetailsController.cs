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
        public LogInDetailsController(ILogInDetailsService logInDetailsService)
        {
            _logInDetailsService = logInDetailsService;
        }
        public IActionResult Index()
        {
            return View();
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
