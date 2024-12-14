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

namespace JobFinder.BLL.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepository<Application> _applicationRepository;
        public ApplicationService(IRepository<Application> applicationRepository)
        {
            _applicationRepository = applicationRepository;
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
            var application = new Application()
            {
                //UserName = applicationDTO.UserName,
                JobId = applicationDTO.JobId,
                UserId = applicationDTO.UserId,
                FileContent = applicationDTO.FileContent,
                FileName = applicationDTO.FileName,
                ContentType = applicationDTO.ContentType,
            };

            
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
            var dto = new ApplicationDTO()
            {
                //Id = application.Id,
                // CompanyName = application.CompanyName,
                // UserName = application.UserName,
                // JobName = application.JobName,
                JobId = application.JobId,
                // UserEmail = application.UserEmail,
                // Submited = application.Submitted,
                FileContent = application.FileContent,
                FileName = application.FileName,
                ContentType = application.ContentType,
            };
            return Result<ApplicationDTO>.Success(dto);
        }

        public async Task<IList<ApplicationDTO>> GetApplications()
        {
            var applications = await _applicationRepository.GetAllAsync();
            var dtos = applications.Select(x => new ApplicationDTO()
            {
                Id = x.Id,
                // CompanyName = x.CompanyName,
                // UserName = x.UserName,
                // JobName = x.JobName,
                JobId = x.JobId,
                // UserEmail = x.UserEmail,
                // Submited = x.Submitted,
                FileContent = x.FileContent,
                FileName = x.FileName,
                ContentType = x.ContentType,
            }).ToList();
            return dtos;
        }

        public async Task<IList<ApplicationDTO>> GetApplicationsByCompany(int id)
        {
            var applications = await _applicationRepository.GetAllAsync();

            var companyApp = applications.Where(app => app.CompanyId == id).ToList();
            var dtos = companyApp.Select(x => new ApplicationDTO()
            {
                Id = x.Id,
                // CompanyName = x.CompanyName,
                // UserName = x.UserName,
                // JobName = x.JobName,
                JobId = x.JobId,
                // UserEmail = x.UserEmail,
                // Submited = x.Submitted,
                FileContent = x.FileContent,
                FileName = x.FileName,
                ContentType = x.ContentType,

            }).ToList();
            return dtos;
        }

        public Task UpdateApplication(CompanyDTO applicationDTO)
        {
            throw new NotImplementedException();
        }
    }
}
