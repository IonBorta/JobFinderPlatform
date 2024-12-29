using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Interfaces
{
    public interface IApplicationService: IBaseService<ApplicationDTO, ApplicationDTO, ApplicationDTO>
    {
        Task<IList<ApplicationDTO>> GetApplicationsByCompany(int id);
        Task<Result> Answer(int jobApplicationId,int state);
        Task<Result<ApplicationDTO>> See(int jobApplicationId);
        Task<Result> Reaply(int jobApplicationId,IFormFile cvFile);

        Task<Result> Withdraw(int jobApplicationId);
    }
}
