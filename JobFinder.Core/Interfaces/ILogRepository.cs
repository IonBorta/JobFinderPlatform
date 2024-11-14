using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.Interfaces
{
    public interface ILogRepository<T> where T : class
    {
        Task<T> GetByEmailAsync(string email);
    }
}
