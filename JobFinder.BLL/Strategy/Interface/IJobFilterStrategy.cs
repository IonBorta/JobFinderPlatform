using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs.Job;
using JobFinder.DAL.Entities;

namespace JobFinder.BLL.Strategy.Interface
{
    public interface IJobFilterStrategy
    {
        IList<GetJobDTO> Filter(IEnumerable<GetJobDTO> jobs, bool[] param);
    }
}
