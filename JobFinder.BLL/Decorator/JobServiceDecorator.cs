using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.BLL.Interfaces;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs.Job;

namespace JobFinder.BLL.Decorator
{
    public abstract class JobServiceDecorator : IJobService
    {
        protected readonly IJobService _jobService;

        protected JobServiceDecorator(IJobService jobService)
        {
            _jobService = jobService;
        }

        public Task<Result> Add(CreateJobDTO jobDTO) => _jobService.Add(jobDTO);
        public Task<Result> Delete(int id) => _jobService.Delete(id);
        public virtual Task<Result<GetJobDTO>> GetById(int id) => _jobService.GetById(id);
        public virtual Task<IList<GetJobDTO>> GetAll() => _jobService.GetAll();
        public virtual Task<IList<GetJobDTO>> GetJobsByCompany(int id) => _jobService.GetJobsByCompany(id);
        public virtual Task<IList<GetJobDTO>> SortJobs(List<CriteriasToFilter> filterCriterias) => _jobService.SortJobs(filterCriterias);
        public Task<Result> Update(UpdateJobDTO dto) => _jobService.Update(dto);
    }
}
