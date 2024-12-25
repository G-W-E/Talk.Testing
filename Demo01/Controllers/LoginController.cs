using Demo01.Data;
using Microsoft.AspNetCore.Mvc;

namespace Demo01.Controllers
{
    public class LoginController : Controller
    {
        private readonly DemoDbContext dbContext;

        public LoginController(DemoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: LoginController
        public ActionResult Index()
        {
            var users = dbContext.User.ToList();
            return View();
        }

    }
}
