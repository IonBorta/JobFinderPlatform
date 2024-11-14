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
        Task<JobDTO> GetJobById(int id);
        Task AddJob(JobDTO jobDTO);
        Task UpdateJob(JobDTO jobDTO);
        Task DeleteJob(int id);
    }
}
