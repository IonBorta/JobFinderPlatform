using JobFinder.Core.Common;
using JobFinder.Core.Enums;
using JobFinder.DAL.State.Concrete;
using JobFinder.Web.Models.Job.JobFilters;
using Mono.TextTemplating;

namespace JobFinder.Web.Models.Job
{
    public class JobPageViewModel
    {
        public IList<GetJobViewModel> Jobs { get; set; }
        public bool[] StudiesFilter { get; set; } = new bool[Enum.GetValues(typeof(StudiesLevel)).Length];
        public bool[] WorkingFilter { get; set; } = new bool[Enum.GetValues(typeof(WorkingType)).Length];

        private bool _toFilter = false;
        public bool ToFilter { 
            get 
            {
                 if (!_toFilter)
                {
                    if (StudiesFilter.Any(val => val != false))
                    {
                        _toFilter = true;
                        FilterCriterias.Add(new CriteriasToFilter(FilterCriteria.Studies, StudiesFilter));
                    }
                    if (WorkingFilter.Any(val => val != false))
                    {
                        _toFilter = true;
                        FilterCriterias.Add(new CriteriasToFilter(FilterCriteria.WorkingType, WorkingFilter));
                    }
                }
                return _toFilter;
            } 
        }
        public List<CriteriasToFilter> FilterCriterias { get; } = new List<CriteriasToFilter>();
    }
}
