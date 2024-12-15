using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.DAL.Bridge.Abstraction;
using JobFinder.DAL.Entities;

namespace JobFinder.DAL.AbstractFactory.Abstract.Product
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUserByEmailAsync(string email);
    }
}
