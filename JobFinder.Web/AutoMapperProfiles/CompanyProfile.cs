using AutoMapper;
using JobFinder.Core.DTOs.Company;
using JobFinder.DAL.Entities;
using JobFinder.Web.Models.Company;
using JobFinder.Web.Models.Job;

namespace JobFinder.Web.AutoMapperProfiles
{
    public class CompanyProfile:Profile
    {
        public CompanyProfile()
        {
            // ViewModel to DTO
            CreateMap<CreateCompanyViewModel, CreateCompanyDTO>()
                .ForPath(dest => dest.CreateUser.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.CreateUser.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.CreateUser.Password, opt => opt.MapFrom(src => src.Password))
                .ForPath(dest => dest.CreateUser.UserType, opt => opt.MapFrom(src => src.UserType));
            CreateMap<EditCompanyViewModel, UpdateCompanyDTO>()
                .ForMember(dest => dest.WorkersCount, opt => opt.MapFrom(src => src.Workers))
                .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.User));
            // DTO to Entity
            CreateMap<CreateCompanyDTO, Company>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.CreateUser.Id));
            CreateMap<UpdateCompanyDTO, Company>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UpdateUser.Id));
            // Entity to DTO
            CreateMap<Company, GetCompanyDTO>();
            // DTO to ViewModel
            CreateMap<GetCompanyDTO, EditCompanyViewModel>()
                .ForMember(dest => dest.Workers, opt => opt.MapFrom(src => src.WorkersCount))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UpdateUser));
            CreateMap<GetCompanyDTO, GetCompanyViewModel>()
                .ForMember(dest => dest.Workers, opt => opt.MapFrom(src => src.WorkersCount))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UpdateUser))
                .ForPath(dest => dest.User.UserType, opt => opt.MapFrom(src => src.UserType));
        }
    }
}

