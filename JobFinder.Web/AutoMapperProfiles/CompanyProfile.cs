using AutoMapper;
using JobFinder.Core.DTOs;
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
            CreateMap<CreateCompanyViewModel, CompanyDTO>()
                .ForPath(dest => dest.UserDTO.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.UserDTO.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.UserDTO.Password, opt => opt.MapFrom(src => src.Password))
                .ForPath(dest => dest.UserDTO.UserType, opt => opt.MapFrom(src => src.UserType));
            CreateMap<EditCompanyViewModel, CompanyDTO>()
                .ForMember(dest => dest.WorkersCount, opt => opt.MapFrom(src => src.Workers))
                .ForMember(dest => dest.UserDTO, opt => opt.MapFrom(src => src.User));
            // DTO to Entity
            CreateMap<CompanyDTO, Company>();
            // Entity to DTO
            CreateMap<Company, CompanyDTO>();
            // DTO to ViewModel
            CreateMap<CompanyDTO, EditCompanyViewModel>()
                .ForMember(dest => dest.Workers, opt => opt.MapFrom(src => src.WorkersCount))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserDTO));
            CreateMap<CompanyDTO, GetCompanyViewModel>()
                .ForMember(dest => dest.Workers, opt => opt.MapFrom(src => src.WorkersCount))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserDTO));
        }
    }
}
