using Microsoft.AspNetCore.Mvc;

namespace MvcDemo.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public string Test() 
        { 
            return "Hello World!";
        }
    }
}
