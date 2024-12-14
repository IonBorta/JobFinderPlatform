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
            CreateMap<CreateCompanyViewModel, CompanyDTO>();
            CreateMap<EditJobViewModel, CompanyDTO>();
            // DTO to Entity
            CreateMap<CompanyDTO, Company>();
            // Entity to DTO
            CreateMap<Company, CompanyDTO>();
            // DTO to ViewModel
            CreateMap<CompanyDTO, GetCompanyViewModel>();
        }
    }
}
