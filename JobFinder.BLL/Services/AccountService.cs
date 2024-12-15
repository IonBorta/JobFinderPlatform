using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.Interfaces;
using JobFinder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Services
{
    public class AccountService : IAccountService//, ILogService<UserDTO>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILogRepository<User> _logRepository;
        private readonly IMapper _mapper;
        public AccountService(IRepository<User> userRepository, ILogRepository<User> logRepository,IMapper mapper)
        {
            _userRepository = userRepository;
           _logRepository = logRepository;
            _mapper = mapper;
        }
        public async Task<Result> AddUser(UserDTO userDTO)
        {
            var existingUser = await _logRepository.GetByEmailAsync( userDTO.Email );
            if (existingUser != null)
            {
                return Result.Failure("This email is already used.");
            }
            var user = _mapper.Map<User>(userDTO);
            var success = await _userRepository.AddAsync(user);
            return success ? Result.Success() : Result.Failure("Failed to register user.");
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            User user = await _logRepository.GetByEmailAsync(email);
            if (user == null) return null;
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public Task<UserDTO> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserDTO>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<UserDTO>> LoginUser(string username, string password)
        {
            var existingUser = await _logRepository.GetByEmailAsync(username);
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

        public Task UpdateUser(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
