using MvcDemo.Interfaces;

namespace MvcDemo.Models
{
    public class DemoService : IDemoService
    {
        private readonly IHttpContextAccessor contextAccessor;

        public DemoService(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public void Save()
        {
            var name = this.contextAccessor.HttpContext?.Request.Query["name"];
            Console.WriteLine(name);
        }
    }
}
