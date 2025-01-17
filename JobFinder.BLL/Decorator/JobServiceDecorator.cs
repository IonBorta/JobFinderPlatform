﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.BLL.Interfaces;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs.Job;
using JobFinder.Core.Enums;

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
        public virtual Task<IList<GetJobDTO>> FilterJobs(List<CriteriasToFilter> filterCriterias) => _jobService.FilterJobs(filterCriterias);
        public virtual Task<Result> Update(UpdateJobDTO dto) => _jobService.Update(dto);

        public IList<GetJobDTO> FilterJobs(IList<GetJobDTO> jobs, bool[] filterParams)
        {
            return _jobService.FilterJobs(jobs, filterParams);
        }

        public void SetStrategy(FilterCriteria type)
        {
            _jobService.SetStrategy(type);
        }
    }
}
