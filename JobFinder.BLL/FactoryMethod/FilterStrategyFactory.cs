using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.BLL.Strategy.Interface;
using JobFinder.Core.Enums;

namespace JobFinder.BLL.FactoryMethod
{
    public interface IFilterStrategyFactory
    {
        IJobFilterStrategy CreateFilteringStrategy(FilterCriteria filterCriteria);
    }
}