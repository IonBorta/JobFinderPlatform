using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.DTOs.User;
using JobFinder.DAL.AbstractFactory.Abstract.Factory;
using JobFinder.DAL.AbstractFactory.Abstract.Product;
using JobFinder.DAL.AbstractFactory.Concrete.Repositories;
using JobFinder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AccountService(
            IRepositoryFactory repositoryFactory,
            IMapper mapper)
        {
            _userRepository = repositoryFactory.CreateUserRepository();
            _mapper = mapper;
        }
        public async Task<Result> Add(CreateUserDTO userDTO)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync( userDTO.Email );
            if (existingUser != null)
            {
                return Result.Failure("This email is already used.");
            }
            var user = _mapper.Map<User>(userDTO);
            var success = await _userRepository.AddAsync(user);
            return success ? Result.Success() : Result.Failure("Failed to register user.");
        }

        public Task<Result> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserDTO> GetByEmail(string email)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) return null;
            var userDTO = _mapper.Map<GetUserDTO>(user);
            return userDTO;
        }

        public Task<Result<GetUserDTO>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<GetUserDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<GetUserDTO>> LoginUser(string username, string password)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(username);
            if (existingUser == null)
            {
                return Result<GetUserDTO>.Failure("This email is invalid.");
            }
            if(existingUser != null && existingUser.Password != password)
            {
                return Result<GetUserDTO>.Failure("Password is invalid.");
            }
            var userDTO = _mapper.Map<GetUserDTO>(existingUser);
            return Result<GetUserDTO>.Success(userDTO);
        }

        public Task<Result> Update(UpdateUserDTO updateUserDTO)
        {
            throw new NotImplementedException();
        }
    }
}
