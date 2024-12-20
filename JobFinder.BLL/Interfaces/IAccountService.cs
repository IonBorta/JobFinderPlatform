﻿using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<IList<UserDTO>> GetUsers();
        Task<UserDTO> GetUserById(int id);
        Task<Result> AddUser(UserDTO userDTO);
        Task<Result<UserDTO>> LoginUser(string username, string password);
        Task UpdateUser(UserDTO userDTO);
        Task DeleteUser(int id);
    }
}
