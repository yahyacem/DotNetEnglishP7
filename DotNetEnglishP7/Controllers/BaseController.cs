using DotNetEnglishP7.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetEnglishP7.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger logger;
        public BaseController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            this.logger = logger;
        }
        /// <summary>
        /// Add log for API call.
        /// </summary>
        /// <param name="message"></param>
        protected async void AddLog(string message)
        {
            // Add logs e.g:
            // 2019-07-25T21:48:02Z : 2 : GET /user/3 : User not authorized
            var userLogged = await _userManager.GetUserAsync(User);
            var userId = "UNKNOWN";
            if (userLogged != null)
            {
                userId = userLogged.Id.ToString();
            }
            logger.LogInformation($"{DateTime.UtcNow} : {userId} : {Request.Path.Value} : {message}");
        }
        /// <summary>
        /// Add log for API call.
        /// </summary>
        /// <param name="userLogged"></param>
        /// <param name="message"></param>
        protected void AddLog(AppUser userLogged, string message)
        {
            // Add logs e.g:
            // 2019-07-25T21:48:02Z : 2 : GET /user/3 : User not authorized
            var userId = "UNKNOWN";
            if (userLogged != null)
            {
                userId = userLogged.Id.ToString();
            }
            logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} : {userId} : {Request.Path.Value} : {message}");
        }
    }
}