using JobFinder.BLL.Interfaces;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.Interfaces;
using JobFinder.DAL.Entities;
using JobFinder.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JobFinder.BLL.Services
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> _jobRepository;
        private readonly IRepository<Company> _companyRepository;

        public JobService(IRepository<Job> jobRepository, IRepository<Company> companyRepository)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
        }
        public async Task<Result> AddJob(JobDTO jobDTO)
        {
            var jobs = await _jobRepository.GetAllAsync();
            var existingJob = jobs.FirstOrDefault(j => j.CompanyId == jobDTO.CompanyId);
            if (existingJob != null)
            {
                return Result.Failure("This job already exists");
            }
            var job = new Job()
            {
                Title = jobDTO.Title,
                Description = jobDTO.Description,
                Requirements = jobDTO.Requirements,
                Benefits = jobDTO.Benefits,
                Salary = jobDTO.Salary,
                Experience = jobDTO.Experience,
                Studies = jobDTO.Studies,
                WorkingType = jobDTO.WorkingType,
                CompanyId = jobDTO.CompanyId,
            };

            var added = await _jobRepository.AddAsync(job);
            return added ? Result.Success() : Result.Failure($"Failed to add {jobDTO.Title} job");
        }

        public Task DeleteJob(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<JobDTO>> GetJobById(int id)
        {
            var job  = await _jobRepository.GetByIdAsync(id);
            if (job == null)
            {
                return Result<JobDTO>.Failure($"Job with {id} id not found");
            }
            var company = await _companyRepository.GetByIdAsync(job.CompanyId);
            if (company == null)
            {
                return Result<JobDTO>.Failure($"Company with {job.CompanyId} id for job with {id} id not found");
            }
            var jobDTO = new JobDTO()
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                Experience = job.Experience,
                City = company.City,
                Studies = job.Studies,
                WorkingType = job.WorkingType,
                CompanyId = job.CompanyId,
                Posted = job.Posted,
                CompanyName = company.Name
            };
            return Result<JobDTO>.Success(jobDTO);
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
                Studies = job.Studies,
                WorkingType = job.WorkingType,
                CompanyId= job.CompanyId,
            }).ToList();
            foreach (var job in jobsDTOs)
            {
                var company = await _companyRepository.GetByIdAsync(job.CompanyId);
                job.CompanyName = company.Name;
                job.City = company.City;
            }
            return jobsDTOs;
        }

        public async Task<IList<JobDTO>> GetJobsByCompany(int id)
        {
            var jobs = await GetJobs();
            jobs = jobs.Where(job => job.CompanyId ==  id).ToList();
            return jobs;
        }

        public async Task<Result> UpdateJob(JobDTO jobDTO)
        {
            var existingJob = await _jobRepository.GetByIdAsync(jobDTO.Id);
            if (existingJob == null)
            {
                return Result.Failure($"Job not found");
            }
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
            var updated = await _jobRepository.UpdateAsync(job);
            return updated ? Result.Success() : Result.Failure($"Failed to update {jobDTO.Title} job");
        }
    }
}
