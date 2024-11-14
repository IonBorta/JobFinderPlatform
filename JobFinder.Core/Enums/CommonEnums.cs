using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.Enums
{
    // Core/Enums/StudiesLevel.cs
    public enum StudiesLevel
    {
        NoStudies,
        Bachelor,
        Master,
        Doctorate
    }

    // Core/Enums/WorkingType.cs
    public enum WorkingType
    {
        FullTime,
        PartTime,
        Remote
    }

    public enum WorkExperience
    {
        [Display(Name = "No Experience")]
        NoExperience,

        [Display(Name = "1 Year")]
        OneYear,

        [Display(Name = "2 Years")]
        TwoYears,

        [Display(Name = "3 Years")]
        ThreeYears
    }
    public enum UserType
    {
        Employee,
        Employer
    }
    public enum CompanyDomains
    {
        DesignCreatitve,
        DesignDevelopment,
        SalesAndMarketing,
        Construction,
        InformationTehnology,
        RealEstate
    }
}
