﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();      
        Task<T> GetByIdAsync(int id);            
        Task<bool> AddAsync(T entity);               
        Task<bool> UpdateAsync(T entity);               
        Task DeleteAsync(int id);                 
    }
}
