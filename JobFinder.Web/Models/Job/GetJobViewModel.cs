using JobFinder.Core.Enums;

namespace JobFinder.Web.Models.Job
{
    public class GetJobViewModel:EditJobViewModel
    {
        public string CompanyName { get; set; }
        public string City { get; set; }
/*        public WorkingType FilterByWorkingType { get; set; } = WorkingType.NotSelected;
        public WorkExperience FilterByWorkExperience { get; set; } = WorkExperience.NoExperience;
        public StudiesLevel FilterByStudiesLevel { get; set; } = StudiesLevel.NoStudies;
        public SortCriteria CriteriaToFilter { get; set; } = SortCriteria.None;*/
    }
}
