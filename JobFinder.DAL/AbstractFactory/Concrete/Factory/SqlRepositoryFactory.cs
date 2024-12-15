using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.DAL.AbstractFactory.Abstract.Factory;
using JobFinder.DAL.AbstractFactory.Abstract.Product;
using JobFinder.DAL.AbstractFactory.Concrete.Repositories;
using JobFinder.DAL.Bridge.Implementation;
using JobFinder.DAL.Context;
using JobFinder.DAL.Entities;

namespace JobFinder.DAL.AbstractFactory.Concrete.Factory
{
    public class SqlRepositoryFactory : IRepositoryFactory
    {
        private readonly ApplicationDbContext _context;

        public SqlRepositoryFactory(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUserRepository CreateUserRepository()
        {
            return new UserRepository(new SqlRepositoryImplementor<User>(_context));
        }

        public IJobRepository CreateJobRepository()
        {
            return new JobRepository(new SqlRepositoryImplementor<Job>(_context));
        }

        public ICompanyRepository CreateCompanyRepository()
        {
            return new CompanyRepository(new SqlRepositoryImplementor<Company>(_context));
        }

        public IApplicationRepository CreateApplicationRepository()
        {
            return new ApplicationRepository(new SqlRepositoryImplementor<ApplicationEntity>(_context));
        }
    }
}
