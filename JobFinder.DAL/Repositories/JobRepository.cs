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
    public class JobRepository : IRepository<Job>,IJobRepository<Job>
    {
        private readonly ApplicationDbContext _context;
        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Job entity)
        {
            //entity.Posted = DateTime.Now;
            await _context.Jobs.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            var jobs = await _context.Jobs.OrderByDescending(job => job.Created).ToListAsync();
            return jobs;
        }

        public async Task<Job> GetByIdAsync(int id)
        {
            return await _context.Jobs.FindAsync(id);
        }

        public async Task<Job> GetByName(string name)
        {
            return await _context.Jobs.FirstOrDefaultAsync(job => job.Title == name);
        }

        public async Task<bool> UpdateAsync(Job entity)
        {
            var existingJob = await _context.Jobs.FindAsync(entity.Id);

            existingJob.Title = entity.Title;
            existingJob.Description = entity.Description;
            existingJob.Requirements = entity.Requirements;
            existingJob.Studies = entity.Studies;
            existingJob.Benefits = entity.Benefits;
            existingJob.Salary = entity.Salary;
            existingJob.WorkingType = entity.WorkingType;
            existingJob.Experience = entity.Experience;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
