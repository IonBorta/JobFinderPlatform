using JobFinder.Core.Interfaces;
using JobFinder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DAL.Repositories
{
    public class JobRepository : IRepository<Job>
    {
        public Task AddAsync(Job entity)
        {
            entity.Posted = DateTime.Now;
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Job>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Job> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Job entity)
        {
            throw new NotImplementedException();
        }
    }
}
