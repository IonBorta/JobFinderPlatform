using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs.Company;
using JobFinder.Core.DTOs.User;
using JobFinder.DAL.AbstractFactory.Abstract.Factory;
using JobFinder.DAL.AbstractFactory.Abstract.Product;
using JobFinder.DAL.Entities;
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
        public async Task<Result> Add(CreateCompanyDTO companyDTO)
        { 
            var existingCompanyUser = await _userRepository.GetUserByEmailAsync(companyDTO.CreateUser.Email);
            if(existingCompanyUser != null)
            {
                return Result.Failure("This email is already used.");
            }
            var user = _mapper.Map<User>(companyDTO.CreateUser);

            var addedUser = await _userRepository.AddAsync(user);
            if(addedUser == true)
            {
                var userAdded = await _userRepository.GetUserByEmailAsync(companyDTO.CreateUser.Email);
                //companyDTO.UserId = userAdded.Id;
                companyDTO.CreateUser.Id = userAdded.Id;
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

        public Task<IList<GetCompanyDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<GetCompanyDTO>> GetById(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
            {
                return Result<GetCompanyDTO>.Failure($"Company with {id} id not found");
            }
            var dto = _mapper.Map<GetCompanyDTO>(company);
            var jobs = await _jobRepository.GetAllAsync();
            var jobsCount = jobs.Where(job => job.CompanyId == company.Id).Count();
            dto.JobsCount = jobsCount;
            var user = await _userRepository.GetByIdAsync(company.UserId);
            //dto.Name = user.Name;
            //dto.Email = user.Email;
            dto.UpdateUser = _mapper.Map<UpdateUserDTO>(user);
            return Result<GetCompanyDTO>.Success(dto);
        }

        public async Task<Result<GetCompanyDTO>> GetCompanyByUserId(int id, bool byUserId)
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
                return Result<GetCompanyDTO>.Failure($"Company with {id} id not found");
            }
            var dto = _mapper.Map<GetCompanyDTO>(company);
            var jobs = await _jobRepository.GetAllAsync();
            var jobsCount = jobs.Where(job => job.CompanyId == company.Id).Count();
            dto.JobsCount = jobsCount;
            var user = await _userRepository.GetByIdAsync(company.UserId);
            //dto.Name = user.Name;
            //dto.Email = user.Email;
            dto.UpdateUser = _mapper.Map<UpdateUserDTO>(user);
            return Result<GetCompanyDTO>.Success(dto);
        }

        public async Task<Result> Update(UpdateCompanyDTO companyDTO)
        {
            var existingCompany = await _companyRepository.GetByIdAsync(companyDTO.Id);
            if(existingCompany == null)
            {
                return Result.Failure($"Company not found");
            }
            var existingCompanyUser = await _userRepository.GetByIdAsync(companyDTO.UpdateUser.Id);
            if (existingCompanyUser == null)
            {
                return Result.Failure($"Company as User not found");
            }

            var user = _mapper.Map<User>(companyDTO.UpdateUser);
            var toUpdateUser = existingCompanyUser.Update(user);
            var updatedUser = false;
            if (toUpdateUser)
            {
                updatedUser = await _userRepository.UpdateAsync(existingCompanyUser);
            }
            
            var company = _mapper.Map<Company>(companyDTO);
            var toUpdateCompany = existingCompany.Update(company);
            var updatedCompany = false;
            if (toUpdateCompany)
            {
                updatedCompany = await _companyRepository.UpdateAsync(existingCompany);
            }
            if(toUpdateUser == false && toUpdateCompany == false) return Result.Failure($"No updates, You have not changed anything.");
            return updatedCompany || updatedUser ? Result.Success() : Result.Failure($"Failed to update {companyDTO.UpdateUser.Name} company");
        }
    }
}
