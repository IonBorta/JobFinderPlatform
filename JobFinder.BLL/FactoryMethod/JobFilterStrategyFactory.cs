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
        public IJobFilterStrategy CreateFilteringStrategy(FilterCriteria filterCriteria)
        {
            return filterCriteria switch
            {
                FilterCriteria.Experience => new FilterByExperience(),
                FilterCriteria.Studies => new FilterByStudies(),
                FilterCriteria.WorkingType => new FilterByWorkingType(),
                _ => throw new ArgumentException("Invalid filtering criterion")
            };
        }
    }
}
