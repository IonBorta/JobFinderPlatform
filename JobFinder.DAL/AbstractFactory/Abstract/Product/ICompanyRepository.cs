﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.DAL.Bridge.Abstraction;
using JobFinder.DAL.Entities;

namespace JobFinder.DAL.AbstractFactory.Abstract.Product
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> GetCompanyByUserIdAsync(int userId);
    }
}
