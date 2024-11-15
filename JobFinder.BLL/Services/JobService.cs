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

        public async Task<JobDTO> GetJobById(int id)
        {
            var job  = await _jobRepository.GetByIdAsync(id);
            var jobDTO = new JobDTO()
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                Experience = job.Experience,
                //City = job.City,
                Studies = job.Studies,
                WorkingType = job.WorkingType,
                CompanyId = job.CompanyId,
                Posted = job.Posted,
            };
            return jobDTO;
        }

        public async Task<IList<JobDTO>> GetJobs()
        {
            var jobs = await _jobRepository.GetAllAsync();
            
            var jobsDTOs = jobs.Select(job => new JobDTO()
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                Experience = job.Experience,
                //City = job.City,
                Studies = job.Studies,
                WorkingType = job.WorkingType,
                CompanyId= job.CompanyId,
            }).ToList();

            return jobsDTOs;
        }

        public async Task UpdateJob(JobDTO jobDTO)
        {
            var job = new Job()
            {
                Id = jobDTO.Id,
                Title = jobDTO.Title,
                Description = jobDTO.Description,
                Requirements = jobDTO.Requirements,
                Benefits = jobDTO.Benefits,
                Salary = jobDTO.Salary,
                Experience = jobDTO.Experience,
                WorkingType = jobDTO.WorkingType,
                Studies = jobDTO.Studies,
            };
            await _jobRepository.UpdateAsync(job);
        }
    }
}
