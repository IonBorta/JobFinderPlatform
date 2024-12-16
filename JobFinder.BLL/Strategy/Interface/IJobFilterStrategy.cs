using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Common;
using JobFinder.DAL.Entities;

namespace JobFinder.BLL.Strategy.Interface
{
    public interface IJobFilterStrategy/*<T> where T : struct,IConvertible*/
    {
        //IEnumerable<Job> Filter(IEnumerable<Job> jobs, T param);
        //IEnumerable<Job> Filter<T>(IEnumerable<Job> jobs, T param) where T : struct, Enum;
        IList<Job> Filter(IEnumerable<Job> jobs, bool[] param);
    }
}
