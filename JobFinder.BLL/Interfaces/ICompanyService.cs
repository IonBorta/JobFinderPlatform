using JobFinder.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Interfaces
{
    public interface ICompanyService
    {
        //Task<IList<CompanyDTO>> GetJobs();
        Task<CompanyDTO> GetCompanyById(int id);
        Task AddCompany(CompanyDTO companyDTO);
        Task UpdateCompany(CompanyDTO companyDTO);
        Task DeleteCompany(int id);
    }
}
