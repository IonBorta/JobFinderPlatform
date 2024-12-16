using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.BLL.Strategy;
using JobFinder.BLL.Strategy.Concrete;
using JobFinder.BLL.Strategy.Interface;
using JobFinder.Core.Common;
using JobFinder.Core.Enums;

namespace JobFinder.BLL.FactoryMethod
{
    public class JobFilterStrategyFactory : IFilterStrategyFactory
    {
        public IJobFilterStrategy CreateFilteringStrategy(SortCriteria sortCriteria)
        {
            return sortCriteria switch
            {
                //SortCriteria.Salary => new FilterBySalary(),
                SortCriteria.Experience => new FilterByExperience(),
                SortCriteria.Studies => new FilterByStudies(),
                SortCriteria.WorkingType => new FilterByWorkingType(),
                _ => throw new ArgumentException("Invalid sorting criterion")
            };
        }
    }
}
