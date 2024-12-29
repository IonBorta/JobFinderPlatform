using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.Enums;
using JobFinder.DAL.AbstractFactory.Abstract.Factory;
using JobFinder.DAL.AbstractFactory.Abstract.Product;
using JobFinder.DAL.Entities;
using Microsoft.AspNetCore.Http;
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
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public ApplicationService(
            IRepositoryFactory repositoryFactory,
            IMapper mapper
            )
        {
            _applicationRepository = repositoryFactory.CreateApplicationRepository();
            _mapper = mapper;
            _companyRepository = repositoryFactory.CreateCompanyRepository();
            _userRepository = repositoryFactory.CreateUserRepository();
            _jobRepository = repositoryFactory.CreateJobRepository();
        }
        public async Task<Result> Add(ApplicationDTO applicationDTO)
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

        public Task<Result> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ApplicationDTO>> GetById(int id)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            if (application == null)
            {
                return Result<ApplicationDTO>.Failure($"Application with {id} id not found");
            }
            var dto = _mapper.Map<ApplicationDTO>(application);
            return Result<ApplicationDTO>.Success(dto);
        }

        public async Task<IList<ApplicationDTO>> GetAll()
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

            var companyApps = applications.Where(app => app.CompanyId == id).ToList();
            var dtos = companyApps.Select(x => _mapper.Map<ApplicationDTO>(x)).ToList();
            for (int i = 0; i < dtos.Count(); i++)
            {
                var application = companyApps.ElementAt(i);
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

        public async Task<Result> Update(ApplicationDTO applicationDTO)
        {
            var existingJobApplication = await _applicationRepository.GetByIdAsync(applicationDTO.Id);
            if(existingJobApplication == null)
            {
                return Result.Failure($"Job application with {applicationDTO.Id} not found");
            }

/*            if((existingJobApplication.State != Core.Enums.ApplicationJobStates.Pending && applicationDTO.State == Core.Enums.ApplicationJobStates.Withdrawn)
                || (existingJobApplication.State == Core.Enums.ApplicationJobStates.Seen && applicationDTO.State != Core.Enums.ApplicationJobStates.Accepted)
                || (existingJobApplication.State == Core.Enums.ApplicationJobStates.Seen && applicationDTO.State != Core.Enums.ApplicationJobStates.Rejected)
                || (existingJobApplication.State == Core.Enums.ApplicationJobStates.Withdrawn && applicationDTO.State != Core.Enums.ApplicationJobStates.Pending)
                )
            {
                return Result.Failure($"Failed to update job application from {existingJobApplication.State} to {applicationDTO.State}. Only Pending to Seen, Seen to Accepted or Rejected");
            }*/
            var currentJobApplication = _mapper.Map<ApplicationEntity>(applicationDTO);
            var toUpdate = existingJobApplication.Update(currentJobApplication);
            var updated = true;
            if (toUpdate == false)
            {
                return Result.Failure($"No updates, You have not changed anything.");
            }
            else{
                updated = await _applicationRepository.UpdateAsync(existingJobApplication);
            }
            return updated ? Result.Success() : Result.Failure($"Failed to update {existingJobApplication.Id} job application");
        }
        public async Task<Result<ApplicationDTO>> See(int jobApplicationId)
        {
            var result = await CheckExistingEntity(jobApplicationId);
            if (result.IsSuccess)
            {
                var existingEntity = result.Data;
                var updated = existingEntity.See();
                if (updated.IsSuccess)
                {
                    var dto = _mapper.Map<ApplicationDTO>(existingEntity);
                    return await _applicationRepository.UpdateAsync(existingEntity)
                        ? Result<ApplicationDTO>.Success(dto)
                        : Result<ApplicationDTO>.Failure($"Failed to update {existingEntity.Id} job application");
                }
                else return Result<ApplicationDTO>.Failure(updated.ErrorMessage);
            }
            return Result<ApplicationDTO>.Failure($"Not found {jobApplicationId} job application");
        }
        public async Task<Result> Withdraw(int jobApplicationId)
        {
            var result = await CheckExistingEntity(jobApplicationId);
            if (result.IsSuccess)
            {
                var existingEntity = result.Data;
                var updated = existingEntity.Withdraw();
                if (updated.IsSuccess)
                {
                    return await _applicationRepository.UpdateAsync(existingEntity)
                        ? Result.Success()
                        : Result.Failure($"Failed to update {existingEntity.Id} job application");
                }
                else return updated;
            }
            return Result.Failure($"Not found {jobApplicationId} job application");
        }
        public async Task<Result> Answer(int jobApplicationId,int state)
        {
            var result = await CheckExistingEntity(jobApplicationId);
            if (result.IsSuccess)
            {
                var existingEntity = result.Data;
                var updated = existingEntity.Answer((ApplicationJobStates)state);
                if (updated.IsSuccess)
                {
                    return await _applicationRepository.UpdateAsync(existingEntity)
                        ? Result.Success()
                        : Result.Failure($"Failed to update {existingEntity.Id} job application");
                }
                else return updated;
            }
            return Result.Failure($"Not found {jobApplicationId} job application");
        }
        public async Task<Result> Reaply(int jobApplicationId, IFormFile cvFile)
        {
            var result = await CheckExistingEntity(jobApplicationId);
            if (result.IsSuccess)
            {
                var existingEntity = result.Data;
                var updated = await existingEntity.Reaply(cvFile);
                if (updated.IsSuccess)
                {
                    return await _applicationRepository.UpdateAsync(existingEntity)
                        ? Result.Success()
                        : Result.Failure($"Failed to update {existingEntity.Id} job application");
                }
                else return updated;
            }
            return Result.Failure($"Not found {jobApplicationId} job application");
        }
        private async Task<Result<ApplicationEntity>> CheckExistingEntity(int id)
        {
            var existingJobApplication = await _applicationRepository.GetByIdAsync(id);
            if (existingJobApplication == null)
            {
                return Result<ApplicationEntity>.Failure($"Job application with {id} not found");
            }
            return Result<ApplicationEntity>.Success(existingJobApplication);
        }
    }
}
