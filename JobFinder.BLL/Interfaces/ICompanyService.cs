using JobFinder.Core.Common;
using JobFinder.Core.DTOs.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Interfaces
{
    public interface ICompanyService: IBaseService<CreateCompanyDTO,UpdateCompanyDTO,GetCompanyDTO>
    {
        Task<Result<GetCompanyDTO>> GetCompanyByUserId(int id,bool byUserId = false);
    }
}
