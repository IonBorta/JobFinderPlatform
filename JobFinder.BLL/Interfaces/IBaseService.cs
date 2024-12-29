using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Common;

namespace JobFinder.BLL.Interfaces
{
    public interface IBaseService<TCreate, TUpdate, TGet>
    {
        Task<Result<TGet>> GetById(int id);
        Task<IList<TGet>> GetAll();
        Task<Result> Add(TCreate dto);
        Task<Result> Update(TUpdate dto);
        Task<Result> Delete(int id);
    }
}
