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
    public class FilterByStudies : IJobFilterStrategy
    {
        public IList<Job> Filter(IEnumerable<Job> jobs, bool[] param)
        {
            for (int i = 0; i < param.Length; i++)
            {
                if (param[i] == true)
                {
                    var sorted = jobs.Where(job => (int)job.Studies == i).ToList();
                    return sorted;
                }
            }
            return jobs.ToList();
        }
    }
}
