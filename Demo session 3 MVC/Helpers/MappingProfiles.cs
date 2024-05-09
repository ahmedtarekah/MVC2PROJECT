using AutoMapper;
using AutoMapper;
using Demoo.DAL;
using Demo_session_3_MVC.ViewModels;


namespace Demo_session_3_MVC.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
