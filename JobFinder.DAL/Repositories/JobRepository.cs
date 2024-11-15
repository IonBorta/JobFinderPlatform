﻿using JobFinder.Core.Interfaces;
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
    public class JobRepository : IRepository<Job>
    {
        private readonly ApplicationDbContext _context;
        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Job entity)
        {
            entity.Posted = DateTime.Now;
            await _context.Jobs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            var jobs = await _context.Jobs.ToListAsync();
            return jobs;
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
