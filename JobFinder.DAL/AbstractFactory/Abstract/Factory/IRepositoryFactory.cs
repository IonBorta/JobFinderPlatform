﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.DAL.AbstractFactory.Abstract.Product;

namespace JobFinder.DAL.AbstractFactory.Abstract.Factory
{
    public interface IRepositoryFactory
    {
        IUserRepository CreateUserRepository();
        IJobRepository CreateJobRepository();
        ICompanyRepository CreateCompanyRepository();
        IApplicationRepository CreateApplicationRepository();
    }
}
