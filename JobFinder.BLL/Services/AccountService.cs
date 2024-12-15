using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
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
        public async Task<Result> Add(UserDTO userDTO)
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

        public async Task<UserDTO> GetByEmail(string email)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) return null;
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public Task<Result<UserDTO>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<UserDTO>> LoginUser(string username, string password)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(username);
            if (existingUser == null)
            {
                return Result<UserDTO>.Failure("This email is invalid.");
            }
            if(existingUser != null && existingUser.Password != password)
            {
                return Result<UserDTO>.Failure("Password is invalid.");
            }
            var userDTO = _mapper.Map<UserDTO>(existingUser);
            return Result<UserDTO>.Success(userDTO);
        }

        public Task<Result> Update(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
