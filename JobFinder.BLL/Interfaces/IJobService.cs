using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.Enums;
using JobFinder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Interfaces
{
    public interface IJobService:IBaseService<JobDTO>
    {
        Task<IList<JobDTO>> GetJobsByCompany(int id);
        //Task<IList<JobDTO>> SortJobs<T>(SortCriteria sortCriteria, T param) where T : struct, Enum;
        Task<IList<JobDTO>> SortJobs(SortCriteria sortCriteria, bool[] param);
    }
}
