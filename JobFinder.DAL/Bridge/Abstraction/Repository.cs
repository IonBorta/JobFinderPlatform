using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.DAL.Bridge.Implementation;

namespace JobFinder.DAL.Bridge.Abstraction
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected IRepositoryImplementor<T> _implementor;

        protected Repository(IRepositoryImplementor<T> implementor)
        {
            _implementor = implementor;
        }

        public async Task<bool> AddAsync(T entity)
        {
            return await _implementor.Add(entity);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            var entities = await _implementor.GetAll();
            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _implementor.GetById(id);
            return entity;
        }

        public Task<bool> RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return await _implementor.Update(entity);
        }
    }
}
