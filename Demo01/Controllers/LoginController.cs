using Demo01.Data;
using Demo01.Dtos;
using Demo01.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo01.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthentication authentication;

        public LoginController(IAuthentication authentication)
        {
            this.authentication = authentication;
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
            if (ModelState.IsValid)
            {
                var validate = authentication.ValidateUser(loginDto.UserName, loginDto.Password);
            }
            return View();
        }

    }
}
