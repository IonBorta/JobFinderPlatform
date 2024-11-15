using JobFinder.BLL.Interfaces;
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
        public async Task AddApplication(ApplicationDTO applicationDTO)
        {
            var application = new Application()
            {
                //Id = applicationDTO.Id,
                CompanyName = applicationDTO.CompanyName,
                UserName = applicationDTO.UserName,
                JobId = applicationDTO.JobId,
                UserEmail = applicationDTO.UserEmail,
                //FilePath = applicationDTO.FilePath,
                FileContent = applicationDTO.FileContent,
                FileName = applicationDTO.FileName,
                ContentType = applicationDTO.ContentType,
            };

            await _applicationRepository.AddAsync(application);
        }

        public Task DeleteApplication(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationDTO> GetApplcationById(int id)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            var dto = new ApplicationDTO()
            {
                //Id = application.Id,
                CompanyName = application.CompanyName,
                UserName = application.UserName,
                JobName = application.JobName,
                JobId = application.JobId,
                UserEmail = application.UserEmail,
                //FilePath = application.FilePath,
                Submited = application.Submitted,
                FileContent = application.FileContent,
                FileName = application.FileName,
                ContentType = application.ContentType,
            };
            return dto;
        }

        public async Task<IList<ApplicationDTO>> GetApplications()
        {
            var applications = await _applicationRepository.GetAllAsync();
            var dtos = applications.Select(x => new ApplicationDTO()
            {
                Id = x.Id,
                CompanyName = x.CompanyName,
                UserName = x.UserName,
                JobName = x.JobName,
                JobId = x.JobId,
                UserEmail = x.UserEmail,
                //FilePath = x.FilePath,
                Submited = x.Submitted,
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
