using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.Interfaces
{
    public interface IJobRepository<T> where T : class
    {
        Task<T> GetByName(string name);
    }
}
