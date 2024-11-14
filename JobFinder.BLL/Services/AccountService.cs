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
    public class AccountService : IAccountService, ILogService<UserDTO>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILogRepository<User> _logRepository;
        public AccountService(IRepository<User> userRepository, ILogRepository<User> logRepository)
        {
            _userRepository = userRepository;
           _logRepository = logRepository;
        }
        public async Task AddUser(UserDTO userDTO)
        {
            var user = new User()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password,
            };
            await _userRepository.AddAsync(user);
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            User user = await _logRepository.GetByEmailAsync(email);
            var userDTO = new UserDTO()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
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

        public Task LoginUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
