using Demo01.Data;
using Demo01.Dtos;
using Demo01.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo01.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthentication authentication;
        private readonly ILogger<LoginController> logger;

        public LoginController(IAuthentication authentication, ILogger<LoginController> logger)
        {
            this.authentication = authentication;
            this.logger = logger;
        }
        public ActionResult Index()
        {
            return View();
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
                    var validate = authentication.ValidateUser(loginDto.UserName, loginDto.Password);
                    if (validate.Result)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return View();
                }
                return View();
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex.StackTrace);
            }
            return View();
        }

    }
}
