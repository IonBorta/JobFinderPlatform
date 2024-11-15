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
    public class CompanyService : ICompanyService , ILogService<CompanyDTO>
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly ILogRepository<Company> _logRepository;
        public CompanyService(IRepository<Company> companyRepository, ILogRepository<Company> logRepository) 
        {
            _companyRepository = companyRepository;
            _logRepository = logRepository;
        }
        public async Task AddCompany(CompanyDTO companyDTO)
        {
            var company = new Company()
            {
                Name = companyDTO.Name,
                Email = companyDTO.Email,
                PhoneNumber = companyDTO.PhoneNumber,
                Domain = companyDTO.Domain,
                Password = companyDTO.Password,
            };
            await _companyRepository.AddAsync(company);
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

        public async Task<CompanyDTO> GetCompanyById(int id)
        {
            Company company = await _companyRepository.GetByIdAsync(id);
            var dto = new CompanyDTO()
            {
                Id = company.Id,
                Name = company.Name,
                Email = company.Email,
                PhoneNumber = company.PhoneNumber,
                Domain = company.Domain,
                Workers = company.Workers ?? 0,
                Description = company.Description,
                City = company.City
            };
            return dto;
        }

        public async Task UpdateCompany(CompanyDTO companyDTO)
        {
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
            await _companyRepository.UpdateAsync(company);
        }
    }
}
