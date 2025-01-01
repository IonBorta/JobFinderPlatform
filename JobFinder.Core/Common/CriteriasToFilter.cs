using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Enums;

namespace JobFinder.Core.Common
{
    public class CriteriasToFilter
    {
        public CriteriasToFilter(FilterCriteria type, bool[] filterParams)
        {
            Type = type;
            FilterParams = filterParams;
        }
        public FilterCriteria Type { get; }
        public bool[] FilterParams { get; }
    }
}
