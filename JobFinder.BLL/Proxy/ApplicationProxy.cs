using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Services;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.Enums;
using JobFinder.DAL.AbstractFactory.Abstract.Factory;
using JobFinder.DAL.AbstractFactory.Abstract.Product;
using JobFinder.DAL.Entities;
using Microsoft.AspNetCore.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobFinder.BLL.Proxy
{
    public class ApplicationProxy : IApplicationService
    {
        private readonly IApplicationService _applicationService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public ApplicationProxy(
            IApplicationService applicationService, 
            IRepositoryFactory repositoryFactory, 
            ICompanyService companyService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _contextAccessor = httpContextAccessor;
            _applicationService = applicationService;
            _companyRepository = repositoryFactory.CreateCompanyRepository();
            _applicationRepository = repositoryFactory.CreateApplicationRepository();
        }
        private async Task<bool> CheckUserAccessAsync(int jobApplicationId)
        {
            if (!_contextAccessor.HttpContext.Session.TryGetValue("UserRole", out var userRoleBytes) 
                || !_contextAccessor.HttpContext.Session.TryGetValue("UserId", out var currentUserIdBytes)
                ) 
            { 
                return false; 
            }
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(userRoleBytes);
                Array.Reverse(currentUserIdBytes);
            }
            int userRole = BitConverter.ToInt32(userRoleBytes); 
            int currentUserId = BitConverter.ToInt32(currentUserIdBytes); 
            if (userRole != (int)UserType.Employee) 
            { 
                return false; 
            }
            var app = await _applicationRepository.GetByIdAsync(jobApplicationId);
            if (app != null)
            {
                if (currentUserId == app.UserId)
                {
                    return true;
                }
            }
            return true;
        }
        private async Task<bool> CheckCompanyAccessAsync(int jobApplicationId)
        {
            if (!_contextAccessor.HttpContext.Session.TryGetValue("UserRole", out var userRoleBytes)
                || !_contextAccessor.HttpContext.Session.TryGetValue("UserId", out var currentUserIdBytes)
                )
            {
                return false;
            }
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(userRoleBytes);
                Array.Reverse(currentUserIdBytes);
            }
            int userRole = BitConverter.ToInt32(userRoleBytes);
            int currentUserId = BitConverter.ToInt32(currentUserIdBytes);
            if(userRole != (int)UserType.Employer)
            {
                return false;
            }
            var company = await _companyRepository.GetCompanyByUserIdAsync(currentUserId);
            if (company != null)
            {
                var app = await _applicationRepository.GetByIdAsync(jobApplicationId);
                if (app != null)
                {
                    if (company.Id == app.CompanyId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public Task<Result> Add(ApplicationDTO dto) => _applicationService.Add(dto);

        public async Task<Result> Answer(int jobApplicationId, int state)
        {
            if (await CheckCompanyAccessAsync(jobApplicationId))
            {
                return await _applicationService.Answer(jobApplicationId, state);
            }
            return Result.NotAuthorized();
        }

        public Task<Result> Delete(int id) => _applicationService.Delete(id);

        public Task<IList<ApplicationDTO>> GetAll() => _applicationService.GetAll();

        public Task<IList<ApplicationDTO>> GetApplicationsByCompany(int id) => _applicationService.GetApplicationsByCompany(id);

        public async Task<Result<ApplicationDTO>> GetById(int id)
        {
            if(await CheckUserAccessAsync(id))
            {
                return await _applicationService.GetById(id);
            }
            return Result<ApplicationDTO>.NotAuthorized();
        }

        public async Task<Result> Reaply(int jobApplicationId, IFormFile cvFile)
        {
            if (await CheckUserAccessAsync(jobApplicationId))
            {
                return await _applicationService.Reaply(jobApplicationId,cvFile);
            }
            return Result.NotAuthorized();
        }

        public async Task<Result<ApplicationDTO>> See(int jobApplicationId)
        {
            if(await CheckCompanyAccessAsync(jobApplicationId))
            {
                return await _applicationService.See(jobApplicationId);
            }
            return Result<ApplicationDTO>.NotAuthorized();
        }

        public Task<Result> Update(ApplicationDTO dto) => _applicationService.Update(dto);

        public async Task<Result> Withdraw(int jobApplicationId)
        {
            if(await CheckUserAccessAsync(jobApplicationId))
            {
                return await _applicationService.Withdraw(jobApplicationId);
            }
            return Result.NotAuthorized();
        }
    }
}
