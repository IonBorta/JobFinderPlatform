using JobFinder.Core.Common;
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
        Task<Result<CompanyDTO>> GetCompanyById(int id,bool byUserId = false);
        Task<Result> AddCompany(CompanyDTO companyDTO);
        Task<Result> UpdateCompany(CompanyDTO companyDTO);
        Task DeleteCompany(int id);
    }
}
