using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.DAL.AbstractFactory.Abstract.Factory;
using JobFinder.DAL.AbstractFactory.Abstract.Product;
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
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public CompanyService(
            IRepositoryFactory repositoryFactory,
            IMapper mapper
            ) 
        {
            _companyRepository = repositoryFactory.CreateCompanyRepository();
            _userRepository = repositoryFactory.CreateUserRepository();
            _jobRepository = repositoryFactory.CreateJobRepository();
            _mapper = mapper;
        }
        public async Task<Result> Add(CompanyDTO companyDTO)
        { 
            var existingCompanyUser = await _userRepository.GetUserByEmailAsync(companyDTO.Email);
            if(existingCompanyUser != null)
            {
                return Result.Failure("This email is already used.");
            }
            var user = _mapper.Map<User>(companyDTO.UserDTO);

            var addedUser = await _userRepository.AddAsync(user);
            if(addedUser == true)
            {
                var userAdded = await _userRepository.GetUserByEmailAsync(companyDTO.Email);
                companyDTO.UserId = userAdded.Id;
            }
            else return Result.Failure("Failed to register company.");
            var company = _mapper.Map<Company>(companyDTO);
            var addedCompany = await _companyRepository.AddAsync(company);
            return addedCompany == addedUser ? Result.Success() : Result.Failure("Failed to register company.");
        }

        public Task<Result> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<CompanyDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<CompanyDTO>> GetById(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
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

        public async Task<Result<CompanyDTO>> GetCompanyByUserId(int id, bool byUserId)
        {
            Company company = await _companyRepository.GetCompanyByUserIdAsync(id);
            /*            if (byUserId)
                        {
                            company = await _companyRepository.GetCompanyByUserIdAsync(id);
                            //company = companies.FirstOrDefault();
                        }
                        else
                        {
                            company = await _companyRepository.GetByIdAsync(id);
                        }*/
            if (company == null)
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

        public async Task<Result> Update(CompanyDTO companyDTO)
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
