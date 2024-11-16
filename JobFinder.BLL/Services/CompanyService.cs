using JobFinder.BLL.Interfaces;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.Interfaces;
using JobFinder.DAL.Entities;
using JobFinder.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Services
{
    public class CompanyService : ICompanyService , ILogService<CompanyDTO>
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IGetByUserRepository<Company> _getByUserRepository;
        private readonly IRepository<Job> _jobRepository;
        private readonly ILogRepository<Company> _logRepository;
        public CompanyService(
            IRepository<Company> companyRepository, 
            ILogRepository<Company> logRepository,
            IRepository<Job> jobRepository,
            IGetByUserRepository<Company> getByUserRepository
            ) 
        {
            _companyRepository = companyRepository;
            _logRepository = logRepository;
            _jobRepository = jobRepository;
            _getByUserRepository = getByUserRepository;
        }
        public async Task<Result> AddCompany(CompanyDTO companyDTO)
        { 
            var existingCompanyUser = await _logRepository.GetByEmailAsync(companyDTO.Email);
            if(existingCompanyUser != null)
            {
                return Result.Failure("This email is already used.");
            }
            var company = new Company()
            {
                Name = companyDTO.Name,
                Email = companyDTO.Email,
                PhoneNumber = companyDTO.PhoneNumber,
                Domain = companyDTO.Domain,
                Password = companyDTO.Password,
            };
            var added = await _companyRepository.AddAsync(company);
            return added ? Result.Success() : Result.Failure("Failed to register company.");
        }

        public Task DeleteCompany(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CompanyDTO> GetByEmail(string email)
        {
            Company company = await _logRepository.GetByEmailAsync(email);
            if (company == null) return null;
            var companyDTO = new CompanyDTO() { Email = company.Email };
            return companyDTO;
        }

        public async Task<Result<CompanyDTO>> GetCompanyById(int id, bool byUserId)
        {
            Company company = null;
            if (byUserId)
            {
                var companies = await _getByUserRepository.GetByUserIdAsync(id);
                company = companies.FirstOrDefault();
            }
            else
            {
                company = await _companyRepository.GetByIdAsync(id);
            }
            if(company == null)
            {
                return Result<CompanyDTO>.Failure($"Company with {id} id not found");
            }
            var jobs = await _jobRepository.GetAllAsync();
            var jobsCount = jobs.Select(job => job.CompanyId == company.Id).ToList().Count();
            var dto = new CompanyDTO()
            {
                Id = company.Id,
                Name = company.Name,
                Email = company.Email,
                PhoneNumber = company.PhoneNumber,
                Domain = company.Domain,
                Workers = company.Workers ?? 0,
                Description = company.Description,
                City = company.City,
                JobsCount = jobsCount,
            };
            return Result<CompanyDTO>.Success(dto);
        }

        public async Task<Result> UpdateCompany(CompanyDTO companyDTO)
        {
            var existingCompany = await _companyRepository.GetByIdAsync(companyDTO.Id);
            if(existingCompany == null)
            {
                return Result.Failure($"Company not found");
            }
            var company = new Company()
            {
                Id = companyDTO.Id,
                Name = companyDTO.Name,
                Email = companyDTO.Email,
                Description = companyDTO.Description,
                City = companyDTO.City,
                Domain = companyDTO.Domain,
                Workers = companyDTO.Workers,
                PhoneNumber = companyDTO.PhoneNumber,
            };
            var updated =  await _companyRepository.UpdateAsync(company);
            return updated ? Result.Success() : Result.Failure($"Failed to update {companyDTO.Name} company");
        }
    }
}
