using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.DTOs.Job;
using JobFinder.Core.Enums;
using JobFinder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Interfaces
{
    public interface IJobService:IBaseService<CreateJobDTO,UpdateJobDTO,GetJobDTO>
    {
        Task<IList<GetJobDTO>> GetJobsByCompany(int id);
        Task<IList<GetJobDTO>> SortJobs(List<CriteriasToFilter> filterCriterias);
    }
}
