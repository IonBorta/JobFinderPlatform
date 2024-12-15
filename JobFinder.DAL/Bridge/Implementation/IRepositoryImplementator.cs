using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.DAL.Entities;

namespace JobFinder.DAL.Bridge.Implementation
{
    public interface IRepositoryImplementor<T>
    {
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Remove(T entity);
        Task<T> GetById(int id);
        Task<IList<T>> GetAll();
        Task<User> GetUserByEmail(string email);
        Task<Company> GetCompanyByUserId(int userId);
        Task<Job> GetJobByName(string name);
        Task<IList<T>> GetByCompanyId(int companyId);

    }
}
