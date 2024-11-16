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
        //private readonly IRepository<ApplicationDTO> _applicationRepository;
        public AccountService(IRepository<User> userRepository, ILogRepository<User> logRepository/*, IRepository<ApplicationDTO> applicationRepository*/)
        {
            _userRepository = userRepository;
           _logRepository = logRepository;
            //_applicationRepository = applicationRepository;
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

/*        public async Task<ApplicationDTO> GetApplicationById(int id)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            var applicationDTO = new ApplicationDTO()
            {
                Id = id,
                CompanyName = application.CompanyName,
                UserName = application.UserName,
                JobName = application.JobName,
                UserEmail = application.UserEmail,
                FilePath = application.FilePath,
                Submited = application.Submited,
            };
            return applicationDTO;
        }*/

/*        public Task<IList<ApplicationDTO>> GetApplicationDTOs()
        {
            throw new NotImplementedException();
        }*/

        public async Task<UserDTO> GetByEmail(string email)
        {
            User user = await _logRepository.GetByEmailAsync(email);
            if (user == null) return null;
            var userDTO = new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = user.UserType
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
