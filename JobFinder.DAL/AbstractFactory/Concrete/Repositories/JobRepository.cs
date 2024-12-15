using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.DAL.AbstractFactory.Abstract.Product;
using JobFinder.DAL.Bridge.Abstraction;
using JobFinder.DAL.Bridge.Implementation;
using JobFinder.DAL.Entities;

namespace JobFinder.DAL.AbstractFactory.Concrete.Repositories
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(IRepositoryImplementor<Job> implementor) : base(implementor)
        {
        }

        public async Task<Job> GetJobByNameAsync(string name)
        {
            var job = await _implementor.GetJobByName(name);
            return job;
        }
    }
}
