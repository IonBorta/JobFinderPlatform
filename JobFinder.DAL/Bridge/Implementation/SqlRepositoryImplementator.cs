﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.DAL.Context;
using JobFinder.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DAL.Bridge.Implementation
{
    public class SqlRepositoryImplementor<T> : IRepositoryImplementor<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public SqlRepositoryImplementor(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public Task<IList<T>> GetByCompanyId(int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            return entity;
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<Company> GetCompanyByUserId(int userId)
        {
            var entity = await _context.Companies.FirstOrDefaultAsync(company => company.UserId == userId);
            return entity;
        }

        public Task<bool> Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(T entity)
        {
            // Attach the entity to the context if it is not already tracked
            _context.Set<T>().Update(entity);

            // Save the changes to the database
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Job> GetJobByName(string name)
        {
            return await _context.Jobs.FirstOrDefaultAsync(job => job.Title == name);
        }
    }
}