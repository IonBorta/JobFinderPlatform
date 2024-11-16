using JobFinder.Core.Interfaces;
using JobFinder.DAL.Context;
using JobFinder.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DAL.Repositories
{
    public class AccountRepository : IRepository<User>, ILogRepository<User>
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(User entity)
        {
            entity.Created = DateTime.Now;
            await _context.Users.AddAsync(entity);
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            User user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            return user;
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
