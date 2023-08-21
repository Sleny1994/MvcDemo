using Microsoft.AspNetCore.Mvc;

namespace MvcDemo.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];
            if (username == "admin" && password == "admin")
            {
                HttpContext.Session.SetString("username", username);
            }
            return Redirect("/Home");
        }
    }
}
