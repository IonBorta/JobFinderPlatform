using AutoMapper;
using JobFinder.BLL.FactoryMethod;
using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Strategy.Interface;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.DTOs.Job;
using JobFinder.Core.Enums;
using JobFinder.DAL.AbstractFactory.Abstract.Factory;
using JobFinder.DAL.AbstractFactory.Abstract.Product;
using JobFinder.DAL.Entities;
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
        private readonly IJobRepository _jobRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IFilterStrategyFactory _filterFactory;
        private IJobFilterStrategy _jobFilterStrategy;

        public JobService(
            IMapper mapper,
            IRepositoryFactory repositoryFactory
            )
        {
            _jobRepository = repositoryFactory.CreateJobRepository();
            _companyRepository = repositoryFactory.CreateCompanyRepository();
            _userRepository = repositoryFactory.CreateUserRepository();
            _mapper = mapper;
            _filterFactory = new JobFilterStrategyFactory();
        }
        public async Task<Result> Add(CreateJobDTO jobDTO)
        {
            var existingJob = await _jobRepository.GetJobByNameAsync(jobDTO.Title);
            if (existingJob != null)
            {
                return Result.Failure("This job already exists");
            }
            var job = _mapper.Map<Job>(jobDTO);

            var added = await _jobRepository.AddAsync(job);
            return added ? Result.Success() : Result.Failure($"Failed to add {jobDTO.Title} job");
        }

        public Task<Result> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<GetJobDTO>> GetById(int id)
        {
            var job  = await _jobRepository.GetByIdAsync(id);
            if (job == null)
            {
                return Result<GetJobDTO>.Failure($"Job with {id} id not found");
            }
            var company = await _companyRepository.GetByIdAsync(job.CompanyId);
            if (company == null)
            {
                return Result<GetJobDTO>.Failure($"Company with {job.CompanyId} id for job with {id} id not found");
            }
            var jobDTO = _mapper.Map<GetJobDTO>(job);
            jobDTO.City = company.City;

            var user = await _userRepository.GetByIdAsync(company.UserId);
            jobDTO.CompanyName = user.Name;
            return Result<GetJobDTO>.Success(jobDTO);
        }

        public async Task<IList<GetJobDTO>> GetAll()
        {
            var jobs = await _jobRepository.GetAllAsync();
            
            var jobsDTOs = jobs.Select(job => _mapper.Map<GetJobDTO>(job)).ToList();
            foreach (var job in jobsDTOs)
            {
                var company = await _companyRepository.GetByIdAsync(job.CompanyId);
                var user = await _userRepository.GetByIdAsync(company.UserId);
                job.CompanyName = user.Name;
                job.City = company.City;
            }
            return jobsDTOs;
        }

        public async Task<IList<GetJobDTO>> GetJobsByCompany(int id)
        {
            var jobs = await GetAll();
            jobs = jobs.Where(job => job.CompanyId ==  id).ToList();
            return jobs;
        }

        public async Task<Result> Update(UpdateJobDTO jobDTO)
        {
            var existingJob = await _jobRepository.GetByIdAsync(jobDTO.Id);
            if (existingJob == null)
            {
                return Result.Failure($"Job not found");
            }
            var job = _mapper.Map<Job>(jobDTO);
            var toUpdateJob = existingJob.Update(job);
            if (toUpdateJob == false)
            {
                return Result.Failure($"No updates, You have not changed anything.");
            }
            var updated = false;
            if (toUpdateJob) { 
                updated = await _jobRepository.UpdateAsync(job); 
            }
            return updated ? Result.Success() : Result.Failure($"Failed to update {jobDTO.Title} job");
        }

        public async Task<IList<GetJobDTO>> FilterJobs(List<CriteriasToFilter> filterCriterias)
        {
            var sortedJobs = await GetAll();

            foreach(var filter in filterCriterias)
            {
                _jobFilterStrategy = _filterFactory.CreateFilteringStrategy(filter.Type);
                sortedJobs = _jobFilterStrategy.Filter(sortedJobs, filter.FilterParams);
            }

            return sortedJobs;
        }
        public void SetStrategy(FilterCriteria type)
        {
            _jobFilterStrategy = _filterFactory.CreateFilteringStrategy(type);
        }
        public IList<GetJobDTO> FilterJobs(IList<GetJobDTO> jobs, bool[] filterParams)
        {
            return _jobFilterStrategy.Filter(jobs, filterParams);
        }
    }
}
