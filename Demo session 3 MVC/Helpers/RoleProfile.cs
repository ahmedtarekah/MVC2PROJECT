using AutoMapper;
using Demo_session_3_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo_session_3_MVC.Helpers
{
    public class RoleProfile :Profile
    {

        public RoleProfile() 
        {
        CreateMap<IdentityRole ,RoleViewModel>().ForMember(d=>d.RoleName ,O=>O.MapFrom(S=>S.Name)).ReverseMap();
        
        }

    }
}
