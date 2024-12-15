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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IRepositoryImplementor<User> implementor) : base(implementor)
        {
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _implementor.GetUserByEmail(email);
            return user;
        }
    }
}
