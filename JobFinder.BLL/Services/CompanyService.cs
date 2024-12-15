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
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IGetByUserRepository<Company> _getByUserRepository;
        private readonly IRepository<Job> _jobRepository;
        private readonly ILogRepository<User> _logRepository;
        private readonly IMapper _mapper;
        public CompanyService(
            IRepository<Company> companyRepository, 
            IRepository<User> userRepository,
            ILogRepository<User> logRepository,
            IRepository<Job> jobRepository,
            IGetByUserRepository<Company> getByUserRepository,
            IMapper mapper
            ) 
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _logRepository = logRepository;
            _jobRepository = jobRepository;
            _getByUserRepository = getByUserRepository;
            _mapper = mapper;
        }
        public async Task<Result> AddCompany(CompanyDTO companyDTO)
        { 
            var existingCompanyUser = await _logRepository.GetByEmailAsync(companyDTO.Email);
            if(existingCompanyUser != null)
            {
                return Result.Failure("This email is already used.");
            }
            var user = _mapper.Map<User>(companyDTO.UserDTO);
/*            var user = new User()
            {
                Name = companyDTO.Name,
                Email = companyDTO.Email,
                Password = companyDTO.Password,
            };*/
            var addedUser = await _userRepository.AddAsync(user);
            if(addedUser == true)
            {
                var userAdded = await _logRepository.GetByEmailAsync(companyDTO.Email);
                companyDTO.UserId = userAdded.Id;
            }
            else return Result.Failure("Failed to register company.");
            var company = _mapper.Map<Company>(companyDTO);
            var addedCompany = await _companyRepository.AddAsync(company);
            return addedCompany == addedUser ? Result.Success() : Result.Failure("Failed to register company.");
        }

        public Task DeleteCompany(int id)
        {
            throw new NotImplementedException();
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
            var dto = _mapper.Map<CompanyDTO>(company);
            var jobs = await _jobRepository.GetAllAsync();
            var jobsCount = jobs.Where(job => job.CompanyId == company.Id).Count();
            dto.JobsCount = jobsCount;
            var user = await _userRepository.GetByIdAsync(company.UserId);
            dto.Name = user.Name;
            dto.Email = user.Email;
            dto.UserDTO = _mapper.Map<UserDTO>(user);
            return Result<CompanyDTO>.Success(dto);
        }

        public async Task<Result> UpdateCompany(CompanyDTO companyDTO)
        {
            var existingCompany = await _companyRepository.GetByIdAsync(companyDTO.Id);
            if(existingCompany == null)
            {
                return Result.Failure($"Company not found");
            }
            var existingCompanyUser = await _userRepository.GetByIdAsync(companyDTO.UserId);
            if (existingCompanyUser == null)
            {
                return Result.Failure($"Company as User not found");
            }
            var user = _mapper.Map<User>(companyDTO.UserDTO);
            var updatedUser = await _userRepository.UpdateAsync(user);
            var company = _mapper.Map<Company>(companyDTO);
            var updatedCompany =  await _companyRepository.UpdateAsync(company);
            return updatedCompany || updatedUser ? Result.Success() : Result.Failure($"Failed to update {companyDTO.Name} company");
        }
    }
}
