using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Interfaces
{
    public interface IApplicationService
    {
        Task<IList<ApplicationDTO>> GetApplications();
        Task<IList<ApplicationDTO>> GetApplicationsByCompany(int id);
        Task<Result<ApplicationDTO>> GetApplcationById(int id);
        Task<Result> AddApplication(ApplicationDTO applicationDTO);
        Task UpdateApplication(CompanyDTO applicationDTO);
        Task DeleteApplication(int id);
    }
}
