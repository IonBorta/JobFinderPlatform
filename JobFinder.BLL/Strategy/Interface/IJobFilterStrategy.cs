using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Common;
using JobFinder.DAL.Entities;

namespace JobFinder.BLL.Strategy.Interface
{
    public interface IJobFilterStrategy
    {
        IList<Job> Filter(IEnumerable<Job> jobs, bool[] param);
    }
}
