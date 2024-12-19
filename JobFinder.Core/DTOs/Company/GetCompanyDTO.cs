using JobFinder.Core.DTOs.User;
using JobFinder.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.DTOs.Company
{
    public class GetCompanyDTO: UpdateCompanyDTO
    {
        public int JobsCount { get; set; }
        public UserType UserType { get; } = UserType.Employer;
    }
}
