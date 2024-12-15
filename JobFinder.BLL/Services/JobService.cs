using AutoMapper;
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
        private readonly IJobRepository<Job> _jobRepo;
        private readonly IRepository<Company> _companyRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public JobService(
            IRepository<Job> jobRepository, 
            IJobRepository<Job> jobRepo,
            IRepository<Company> companyRepository,
            IRepository<User> userRepository,
            IMapper mapper)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _jobRepo = jobRepo;
            _mapper = mapper;
        }
        public async Task<Result> AddJob(JobDTO jobDTO)
        {
            //var jobs = await _jobRepository.GetAllAsync();
            //var existingJob = jobs.FirstOrDefault(j => j.CompanyId == jobDTO.CompanyId && j.Title == jobDTO.Title);
            var existingJob = await _jobRepo.GetByName(jobDTO.Title);
            if (existingJob != null)
            {
                return Result.Failure("This job already exists");
            }
            var job = _mapper.Map<Job>(jobDTO);

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
            var jobDTO = _mapper.Map<JobDTO>(job);
            jobDTO.City = company.City;

            var user = await _userRepository.GetByIdAsync(company.UserId);
            jobDTO.CompanyName = user.Name;
            return Result<JobDTO>.Success(jobDTO);
        }

        public async Task<IList<JobDTO>> GetJobs()
        {
            var jobs = await _jobRepository.GetAllAsync();
            
            var jobsDTOs = jobs.Select(job => _mapper.Map<JobDTO>(job)).ToList();
            foreach (var job in jobsDTOs)
            {
                var company = await _companyRepository.GetByIdAsync(job.CompanyId);
                var user = await _userRepository.GetByIdAsync(company.UserId);
                job.CompanyName = user.Name;
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
            var job = _mapper.Map<Job>(jobDTO);
            var updated = await _jobRepository.UpdateAsync(job);
            return updated ? Result.Success() : Result.Failure($"Failed to update {jobDTO.Title} job");
        }
    }
}
