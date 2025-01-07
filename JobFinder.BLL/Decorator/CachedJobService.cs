using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.BLL.FactoryMethod;
using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Strategy.Interface;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs.Job;
using Microsoft.Extensions.Caching.Memory;

namespace JobFinder.BLL.Decorator
{
    public class CachedJobService : JobServiceDecorator
    {
        private readonly IMemoryCache _cache;
        private readonly IFilterStrategyFactory _filterFactory;
        private IJobFilterStrategy _jobFilterStrategy;

        public CachedJobService(IJobService jobService, IMemoryCache cache)
            : base(jobService)
        {
            _cache = cache;
            _filterFactory = new JobFilterStrategyFactory();
        }

        public override async Task<IList<GetJobDTO>> GetAll()
        {
            const string cacheKey = "GetAllJobs";
            if (!_cache.TryGetValue(cacheKey, out IList<GetJobDTO> cachedJobs))
            {
                cachedJobs = await _jobService.GetAll();
                _cache.Set(cacheKey, cachedJobs, TimeSpan.FromMinutes(10)); // Cache for 10 minutes
            }
            return cachedJobs;
        }
        public override async Task<IList<GetJobDTO>> GetJobsByCompany(int id)
        {
            var jobs = await GetAll();
            jobs = jobs.Where(job => job.CompanyId == id).ToList();
            return jobs;
        }

        public override async Task<Result<GetJobDTO>> GetById(int id)
        {
            string cacheKey = $"Job_{id}";
            if (!_cache.TryGetValue(cacheKey, out GetJobDTO cachedJob))
            {
                var result = await _jobService.GetById(id);
                if (result.IsSuccess)
                {
                    cachedJob = result.Data;
                    _cache.Set(cacheKey, cachedJob, TimeSpan.FromMinutes(10)); // Cache for 10 minutes
                }
                return result;
            }
            return Result<GetJobDTO>.Success(cachedJob);
        }
        public override async Task<IList<GetJobDTO>> FilterJobs(List<CriteriasToFilter> filterCriterias)
        {
            var sortedJobs = await GetAll();

            foreach (var filter in filterCriterias)
            {
                _jobFilterStrategy = _filterFactory.CreateFilteringStrategy(filter.Type);
                sortedJobs = _jobFilterStrategy.Filter(sortedJobs, filter.FilterParams);
            }

            return sortedJobs;
        }


    }

}
