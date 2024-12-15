using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Common;

namespace JobFinder.BLL.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<Result<T>> GetById(int id);
        Task<IList<T>> GetAll();
        Task<Result> Add(T dto);
        Task<Result> Update(T dto);
        Task<Result> Delete(int id);
    }
}
