using JobFinder.BLL.Interfaces;
using JobFinder.Core.DTOs;
using JobFinder.Core.Interfaces;
using JobFinder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Services
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> _jobRepository;

        public JobService(IRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task AddJob(JobDTO jobDTO)
        {
            var job = new Job()
            {
                Title = jobDTO.Title,
                Description = jobDTO.Description,
                Requirements = jobDTO.Requirements,
                Benefits = jobDTO.Benefits,
                Salary = jobDTO.Salary,
                Experience = jobDTO.Experience,
                //City = jobDTO.City,
                Studies = jobDTO.Studies,
                WorkingType = jobDTO.WorkingType,
                CompanyId = jobDTO.CompanyId,
            };

            await _jobRepository.AddAsync(job);
        }

        public Task DeleteJob(int id)
        {
            throw new NotImplementedException();
        }

        public Task<JobDTO> GetJobById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<JobDTO>> GetJobs()
        {
            var jobs = await _jobRepository.GetAllAsync();
            
            var jobsDTOs = jobs.Select(job => new JobDTO()
            {
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                Experience = job.Experience,
                //City = job.City,
                Studies = job.Studies,
                WorkingType = job.WorkingType
            }).ToList();

            return jobsDTOs;
        }

        public Task UpdateJob(JobDTO jobDTO)
        {
            throw new NotImplementedException();
        }
    }
}
