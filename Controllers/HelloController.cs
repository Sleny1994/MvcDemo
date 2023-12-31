﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcDemo.Models;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace MvcDemo.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            var student = new Student()
            {
                Id = 1,
                Name = "Job",
                Age = 18,
                Sex = "男"
            };
            return View(student);
        }

        public string Welcome()
        {
            return "Hello World.";
        }

        public IActionResult ShowStudent(int id, string name, int age, string sex)
        {
            var student = new Student()
            { Id = id, Name = name, Age = age, Sex = sex };
            return Json(student);
        }

        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save()
        {
            var id = Request.Form["Id"];
            var name = Request.Form["Name"];
            var age = Request.Form["Age"];
            var sex = Request.Form["Sex"];
            var student = new Student()
            {
                Id = string.IsNullOrEmpty(id) ? 0 : int.Parse(id),
                Name = name,
                Age = string.IsNullOrEmpty(age) ? 0 : int.Parse(age),
                Sex = sex
            };
            return Json(student);
        }

        public IActionResult Test()
        {
            ViewData.Add("Name", "Sleny");
            ViewData.Add("Age", 18);
            return View();
        }

        public IActionResult Test2()
        {
            ViewBag.Name = "Sleny";
            ViewBag.Age = 25;
            return View();
        }

        public IActionResult Test3()
        {
            ViewData.Add("Name", "ViewData");
            TempData.Add("Name", "TempData");
            return View();
        }

        public IActionResult Test4()
        {
            return View();
        }
    }
}
