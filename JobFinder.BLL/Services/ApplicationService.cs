using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.Interfaces;
using JobFinder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace JobFinder.BLL.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepository<ApplicationEntity> _applicationRepository;
        private readonly IRepository<Company> _companyRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Job> _jobRepository;
        private readonly IMapper _mapper;
        public ApplicationService(
            IRepository<ApplicationEntity> applicationRepository, 
            IMapper mapper,
            IRepository<Company> companyRepository,
            IRepository<User> userRepository,
            IRepository<Job> jobRepository
            )
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _jobRepository = jobRepository;
        }
        public async Task<Result> AddApplication(ApplicationDTO applicationDTO)
        {
            var applications = await _applicationRepository.GetAllAsync();
            if(applications.Count() > 0)
            {
                var applied = applications.FirstOrDefault(x => x.JobId == applicationDTO.JobId && x.UserId == applicationDTO.UserId);
                if(applied != null)
                {
                    return Result.Failure("You already applied for this");
                }
            }
            var application = _mapper.Map<ApplicationEntity>(applicationDTO);

            
            var added = await _applicationRepository.AddAsync(application);
            return added ? Result.Success() : Result.Failure($"Failed to add application for {application.Id} job");
        }

        public Task DeleteApplication(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ApplicationDTO>> GetApplcationById(int id)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            if (application == null)
            {
                return Result<ApplicationDTO>.Failure($"Application with {id} id not found");
            }
            var dto = _mapper.Map<ApplicationDTO>(application);
            return Result<ApplicationDTO>.Success(dto);
        }

        public async Task<IList<ApplicationDTO>> GetApplications()
        {
            var applications = await _applicationRepository.GetAllAsync();
            var dtos = applications.Select(x => _mapper.Map<ApplicationDTO>(x)).ToList();
            for (int i = 0; i < applications.Count(); i++)
            {
                var application = applications.ElementAt(i);
                var dto = dtos[i];
                var company = await _companyRepository.GetByIdAsync(application.CompanyId);
                var companyUser = await _userRepository.GetByIdAsync(company.UserId);
                dto.CompanyName = companyUser.Name;

                var userApplicant = await _userRepository.GetByIdAsync(application.UserId);
                dto.UserName = userApplicant.Name;
                dto.UserEmail = userApplicant.Email;

                var job = await _jobRepository.GetByIdAsync(application.JobId);
                dto.JobName = job.Title;
            }
            return dtos;
        }

        public async Task<IList<ApplicationDTO>> GetApplicationsByCompany(int id)
        {
            var applications = await _applicationRepository.GetAllAsync();

            var companyApp = applications.Where(app => app.CompanyId == id).ToList();
            var dtos = companyApp.Select(x => _mapper.Map<ApplicationDTO>(x)).ToList();
            for (int i = 0; i < dtos.Count(); i++)
            {
                var application = applications.ElementAt(i);
                var dto = dtos[i];
                var company = await _companyRepository.GetByIdAsync(application.CompanyId);
                var companyUser = await _userRepository.GetByIdAsync(company.UserId);
                dto.CompanyName = companyUser.Name;

                var userApplicant = await _userRepository.GetByIdAsync(application.UserId);
                dto.UserName = userApplicant.Name;
                dto.UserEmail = userApplicant.Email;

                var job = await _jobRepository.GetByIdAsync(application.JobId);
                dto.JobName = job.Title;
            }
            return dtos;
        }

        public Task UpdateApplication(CompanyDTO applicationDTO)
        {
            throw new NotImplementedException();
        }
    }
}
