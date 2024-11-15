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
        Task<ApplicationDTO> GetApplcationById(int id);
        Task AddApplication(ApplicationDTO applicationDTO);
        Task UpdateApplication(CompanyDTO applicationDTO);
        Task DeleteApplication(int id);
    }
}
