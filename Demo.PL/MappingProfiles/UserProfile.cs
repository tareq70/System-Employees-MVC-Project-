using AutoMapper;
using DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();

        }

    }
}
