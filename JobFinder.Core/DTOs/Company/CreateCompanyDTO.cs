using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.DTOs.User;
using JobFinder.Core.Enums;

namespace JobFinder.Core.DTOs.Company
{
    public class CreateCompanyDTO
    {
        public CreateUserDTO CreateUser { get; set; }
        public string PhoneNumber { get; set; }
        public CompanyDomains Domain { get; set; }
    }
}
