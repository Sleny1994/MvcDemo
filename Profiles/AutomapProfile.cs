using AutoMapper;
using MvcDemo.Models;

namespace MvcDemo.Profiles
{
    public class AutomapProfile: Profile
    {
        public AutomapProfile() {
            CreateMap<StudentViewModel, Student>();
        }
    }
}
