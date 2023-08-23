using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MvcDemo.Interfaces;
using MvcDemo.Models;

namespace MvcDemo.Controllers
{
    public class StudentController : Controller
    {
        private IStudentService studentService;
        private readonly IMapper mapper;

        public StudentController(IStudentService studentService, IMapper mapper)
        {
            this.studentService = studentService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(StudentViewModel studentViewModel)
        {
            var student = mapper.Map<StudentViewModel, Student>(studentViewModel);
            studentService.Add(student);
            return View();
        }
    }
}
