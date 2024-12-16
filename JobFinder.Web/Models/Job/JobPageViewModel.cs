using JobFinder.Core.Enums;
using JobFinder.Web.Models.Job.JobFilters;

namespace JobFinder.Web.Models.Job
{
    public class JobPageViewModel
    {
        public IList<GetJobViewModel> Jobs { get; set; }
        public SortCriteria SortCriteria { get; set; } = SortCriteria.None;
        //public JobWorkTypeFilter WorkTypeFilter { get; set; } = new JobWorkTypeFilter();
        public bool[][] FilterParams { get; set; } = new bool[][] { 
            new bool[Enum.GetValues(typeof(WorkingType)).Length], 
            new bool[Enum.GetValues(typeof(WorkExperience)).Length], 
            new bool[Enum.GetValues(typeof(StudiesLevel)).Length] 
        };
        //public bool IsFullTime { get; set; }
    }
}
