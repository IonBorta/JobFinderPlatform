using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Interfaces
{
    public interface ILogService<T> where T : class
    {
        Task<T> GetByEmail(string email);
    }
}
