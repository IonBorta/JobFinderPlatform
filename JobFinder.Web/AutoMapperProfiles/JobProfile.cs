using AutoMapper;
using JobFinder.Core.DTOs;
using JobFinder.DAL.Entities;
using JobFinder.Web.Models.Job;

namespace JobFinder.Web.AutoMapperProfiles
{
    public class JobProfile: Profile
    {
        public JobProfile()
        {
            // ViewModel to DTO
            CreateMap<CreateJobViewModel, JobDTO>();
            CreateMap<EditJobViewModel, JobDTO>();
            // DTO to Entity
            CreateMap<JobDTO, Job>();
            // Entity to DTO
            CreateMap<Job, JobDTO>();
            // DTO to ViewModel
            CreateMap<JobDTO, GetJobViewModel>();
        }
    }
}
