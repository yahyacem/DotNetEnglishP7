using DotNetEnglishP7.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DotNetEnglishP7.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public BaseController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        /// <summary>
        /// Add log for API call.
        /// </summary>
        /// <param name="message"></param>
        protected async Task AddLogInformation(string message)
        {
            // Add logs e.g:
            // 2019-07-25T21:48:02Z : 2 : GET /user/3 : User not authorized
            var userLogged = await _userManager.GetUserAsync(User);
            var userId = "UNKNOWN";
            if (userLogged != null)
            {
                userId = userLogged.Id.ToString();
            }
            Log.Information($"{userId} : {Request.Path.Value} : {message}");
        }
        protected async Task AddLogError(string message)
        {
            // Add logs e.g:
            // 2019-07-25T21:48:02Z : 2 : GET /user/3 : User not authorized
            var userLogged = await _userManager.GetUserAsync(User);
            var userId = "UNKNOWN";
            if (userLogged != null)
            {
                userId = userLogged.Id.ToString();
            }
            Log.Error($"{userId} : {Request.Path.Value} : {message}");
        }
    }
}