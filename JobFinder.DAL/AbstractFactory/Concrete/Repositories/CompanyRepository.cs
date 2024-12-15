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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(IRepositoryImplementor<Company> implementor) : base(implementor)
        {
        }

        public async Task<Company> GetCompanyByUserIdAsync(int userId)
        {
            var company = await _implementor.GetCompanyByUserId(userId);
            return company;
        }
    }
}
