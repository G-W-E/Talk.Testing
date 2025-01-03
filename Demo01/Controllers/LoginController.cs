using Demo01.Data;
using Demo01.Dtos;
using Demo01.Models;
using Demo01.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo01.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService  authenticationService;
        private readonly ILogger<LoginController> logger;

        public LoginController(IAuthenticationService authenticationService, ILogger<LoginController> logger)
        {
            this.authenticationService = authenticationService;
            this.logger = logger;
        }
        public ActionResult Index()
        {
            return View("Index");
        }
        // GET: LoginController
        /// <summary>
        /// User: Admin
        /// Password ABC@1234abc!
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        /// 
        [HttpPost()]
        public ActionResult Index(LoginDto loginDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var validate = authenticationService.ValidateUser(loginDto.UserName, loginDto.Password);
                    if (validate.Result)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return View("Index");
                }
                return View("Index");
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex.StackTrace);
            }
            return View("Index");
        }

    }
}
