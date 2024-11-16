using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JobFinder.Core.Interfaces
{
    public interface IGetByUserRepository<T> where T : class
    { 
        Task<IList<T>> GetByUserIdAsync(int id);
    }
}
