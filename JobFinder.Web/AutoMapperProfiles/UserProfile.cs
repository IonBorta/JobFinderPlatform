using AutoMapper;
using JobFinder.Core.DTOs.User;
using JobFinder.DAL.Entities;
using JobFinder.Web.Models.Auth;
using JobFinder.Web.Models.User;

namespace JobFinder.Web.AutoMapperProfiles
{
    public class UserProfile: Profile
    {
        public UserProfile() 
        {
            // ViewModel to DTOs
            CreateMap<CreateUserViewModel, CreateUserDTO>();
            CreateMap<GetUserViewModel, UpdateUserDTO>();
            // DTO to Entity
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            // Entity to DTO
            CreateMap<User, GetUserDTO>();
            CreateMap<User, UpdateUserDTO>();
            // DTO to ViewModel
            CreateMap<GetUserDTO, GetUserViewModel>();
            CreateMap<UpdateUserDTO, GetUserViewModel>();
        }
    }
}
