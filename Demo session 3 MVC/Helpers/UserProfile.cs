using AutoMapper;
using Demo_session_3_MVC.ViewModels;
using Demoo.DAL.Models;

namespace Demo_session_3_MVC.Helpers
{
    public class UserProfile :Profile

    {
        public UserProfile() 
        {
            CreateMap<UserViewModel,ApplicationUser>().ReverseMap();
        }



    }
}
