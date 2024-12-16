using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.BLL.Strategy.Interface;
using JobFinder.Core.Common;
using JobFinder.Core.Enums;
using JobFinder.DAL.Entities;

namespace JobFinder.BLL.Strategy.Concrete
{
    public class FilterByWorkingType : IJobFilterStrategy
    {
        /*        public IEnumerable<Job> Filter<T>(IEnumerable<Job> jobs, T param) where T : struct, Enum
                {
                    if (param is WorkingType workingType)
                    {
                        return jobs.Where(job => job.WorkingType == workingType);
                    }

                    throw new ArgumentException("Invalid parameter type");
                }*/
        public IList<Job> Filter(IEnumerable<Job> jobs, bool[] param)
        {
            int falseCount = 0;
            var filteredJobs = new List<Job>();

            for (int i = 0; i < param.Length; i++)
            {
                if (param[i] == true)
                {
                    var matchedJobs = jobs.Where(job => (int)job.WorkingType == i);
                    filteredJobs.AddRange(matchedJobs);
                }
                else falseCount++;
            }
            if (falseCount == param.Length)
            {
                return jobs.ToList();
            }

            // Ensure unique jobs if they matched multiple criteria
            return filteredJobs.Distinct().ToList();
        }

    }
}
