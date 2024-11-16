using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Interfaces
{
    public interface IJobService
    {
        Task<IList<JobDTO>> GetJobs();
        Task<IList<JobDTO>> GetJobsByCompany(int id);
        Task<Result<JobDTO>> GetJobById(int id);
        Task<Result> AddJob(JobDTO jobDTO);
        Task<Result> UpdateJob(JobDTO jobDTO);
        Task DeleteJob(int id);
    }
}
