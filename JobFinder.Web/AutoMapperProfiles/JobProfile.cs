using AutoMapper;
using JobFinder.Core.DTOs;
using JobFinder.Core.DTOs.Job;
using JobFinder.DAL.Entities;
using JobFinder.Web.Models.Job;

namespace JobFinder.Web.AutoMapperProfiles
{
    public class JobProfile: Profile
    {
        public JobProfile()
        {
            // ViewModel to DTO
            CreateMap<CreateJobViewModel, CreateJobDTO>();
            CreateMap<EditJobViewModel, UpdateJobDTO>();
            // DTO to Entity
            CreateMap<UpdateJobDTO, Job>();
            CreateMap<CreateJobDTO, Job>();
            // Entity to DTO
            CreateMap<Job, GetJobDTO>();
            // DTO to ViewModel
            CreateMap<GetJobDTO, GetJobViewModel>();
            CreateMap<GetJobDTO, EditJobViewModel>();
        }
    }
}
