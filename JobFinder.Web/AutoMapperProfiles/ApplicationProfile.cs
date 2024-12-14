using AutoMapper;
using JobFinder.Core.DTOs;
using JobFinder.DAL.Entities;
using JobFinder.Web.Models.Application;
using JobFinder.Web.Models.Job;

namespace JobFinder.Web.AutoMapperProfiles
{
    public class ApplicationProfile: Profile
    {
        public ApplicationProfile()
        {
            // ViewModel to DTO
            CreateMap<CreateApplicationViewModel, ApplicationDTO>();
            // DTO to Entity
            CreateMap<ApplicationDTO, Application>();
            // Entity to DTO
            CreateMap<Application, ApplicationDTO>();
            // DTO to ViewModel
            CreateMap<ApplicationDTO, GetApplicationViewModel>();
        }
    }
}
