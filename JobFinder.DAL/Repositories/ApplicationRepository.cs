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
    public class ApplicationRepository : IRepository<Application> , IGetByUserRepository<Application>
    {
        private readonly ApplicationDbContext _context;
        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Application entity)
        {
            //entity.Submitted = DateTime.Now;
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == entity.JobId);
            entity.CompanyId = job.CompanyId;

            await _context.Applications.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Application>> GetAllAsync()
        {
            var applications = await _context.Applications.ToListAsync();
/*            foreach (var application in applications)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == application.UserId);
                var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == application.JobId);
                var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == job.CompanyId);
                application.CompanyName = company.Name;
                application.JobName = job.Title;
                application.UserName = user.Name;
                application.UserEmail = user.Email;
            }*/
            return applications;
        }
        public async Task<IList<Application>> GetByUserIdAsync(int id)
        {
            var allApplications = await GetAllAsync();
            var userapplications = allApplications.Where(a => a.UserId == id).ToList();
            return userapplications;
        }

        public async Task<Application> GetByIdAsync(int id)
        {
            var application = await _context.Applications.FirstOrDefaultAsync(a => a.Id == id);
/*            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == application.UserId);
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == application.JobId);
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == job.CompanyId);
            application.CompanyName = company.Name;
            application.JobName = job.Title;
            application.UserName = user.Name;
            application.UserEmail = user.Email;*/
            return application;
        }

        public Task<bool> UpdateAsync(Application entity)
        {
            throw new NotImplementedException();
        }
    }
}
