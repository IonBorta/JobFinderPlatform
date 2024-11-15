using JobFinder.Core.Enums;
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
    public class CompanyRepository : IRepository<Company>, ILogRepository<Company>
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Company entity)
        {
            entity.Created = DateTime.Now;
            var companyUser = new User()
            {
                Name = entity.Name,
                Email = entity.Email,
                Password = entity.Password,
                Created = entity.Created,
                UserType = UserType.Employer
            };

            await _context.Users.AddAsync(companyUser);
            await _context.SaveChangesAsync();

            var userAdded = await _context.Users.FirstOrDefaultAsync(user => user.Email == entity.Email);

            entity.UserId = userAdded.Id;
            await _context.Companies.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Company>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Company> GetByEmailAsync(string email)
        {
            User user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            if (user == null) return null;
            return new Company() { Email = user.Email };
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(company => company.Id == id);
            return company;
        }

        public async Task UpdateAsync(Company entity)
        {
            var existingCompany = await _context.Companies.FindAsync(entity.Id);

            existingCompany.Name = entity.Name;
            existingCompany.Description = entity.Description;
            existingCompany.City = entity.City;
            existingCompany.Domain = entity.Domain;
            existingCompany.Workers = entity.Workers;
            existingCompany.Email = entity.Email;
            existingCompany.PhoneNumber = entity.PhoneNumber;

            await _context.SaveChangesAsync();

        }
    }
}
