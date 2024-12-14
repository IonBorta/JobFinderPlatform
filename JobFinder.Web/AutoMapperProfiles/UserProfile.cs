using AutoMapper;
using JobFinder.Core.DTOs;
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
            CreateMap<RegisterViewModel, UserDTO>(); // Register inherits CreateUser
            // DTO to Entity
            CreateMap<UserDTO, User>();
            // Entity to DTO
            CreateMap<User, UserDTO>();
            // DTO to ViewModel
            CreateMap<UserDTO, GetUserViewModel>();
        }
    }
}
