using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Interfaces
{
    public interface IAccountService:IBaseService<UserDTO>
    {
        Task<Result<UserDTO>> LoginUser(string username, string password);
        Task<UserDTO> GetByEmail(string email);
    }
}
