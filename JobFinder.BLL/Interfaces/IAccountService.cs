using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Interfaces
{
    public interface IAccountService:IBaseService<CreateUserDTO,UpdateUserDTO,GetUserDTO>
    {
        Task<Result<GetUserDTO>> LoginUser(string username, string password);
        Task<GetUserDTO> GetByEmail(string email);
    }
}
