using Microsoft.AspNetCore.Mvc;
using MvcDemo.Entities;
using MvcDemo.Models;

namespace MvcDemo.Controllers
{
    public class DemoController : Controller
    {
        private DemoDbContext demoDb;

        public DemoController(DemoDbContext demoDb)
        {
            this.demoDb = demoDb;
        }
        public IActionResult Index()
        {
            //1.获取数据库实体
            var entities = demoDb.Demo.Skip(0).Take(20).ToList();
            //2.将实体转换成业务模型
            var Demos = entities.Select(e => new Demo()
            {
                Id = e.Id,
                Name = e.Name,
                Genre = e.Genre,
                LeadingRole = e.LeadingRole,
                Price = e.Price,
                ReleaseDate = e.ReleaseDate,
            }).ToList();
            ViewData.Add("Demos", Demos);
            return View();
        }

        private readonly IDemoService demoService;

        public DemoController(IDemoService demoService) 
        {
            this.demoService = demoService;
        }

        public IActionResult Save()
        {
            demoService.Save();
            return Json("Succeeded！");
        }

    }
}
